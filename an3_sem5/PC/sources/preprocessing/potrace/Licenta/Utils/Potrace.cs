using Licenta.Commons.Math;
using Licenta.Commons.Math.Arithmetics;
using Licenta.Commons.Utils;
using Licenta.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace Licenta.Utils
{
    using DoubleMatrix = Matrix<DoubleNumber>;

    public class PotraceOptions
    {
        public int ThurdSize = 30;
    }

    public static class Potrace
    {
        public static PotraceOutput Process(DoubleMatrix m, PotraceOptions options = null)
        {
            options = options ?? new PotraceOptions();

            var scale = 1;
            var allCorners = new List<Corner[]>();
            using (var bmp = new Bitmap(new ImageRGB(m).ToBitmap(), new Size(m.ColumnsCount * scale, m.RowsCount * scale)))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.Transparent);
                    foreach (var path in PathDecomposition(m, options.ThurdSize)) 
                    {
                        Console.WriteLine($"Found path of len = {path.Vertices.Count}");
                        var poly = FindPolygon(path, out var E);

                        int inversions = 0;
                        for (int i = 0; i < poly.Count - 1 && inversions <= 1; i++)
                            if (poly[i] > poly[i + 1])
                                inversions++;
                        if (inversions > 1) poly.Reverse();

                        //Console.WriteLine($"Found poly of len = {poly.Count}");
                        //Console.WriteLine(poly.JoinToString(" "));

                        var pts = poly.Select(_ => path.Vertices[_]).Select(_ => new Point(_.X * scale, _.Y * scale)).ToArray();
                        g.DrawPolygon(Pens.Green, pts);

                        List<(double A, double B, double C)> optimalSlopes = new List<(double, double, double)>();

                        for (int i = 0; i < poly.Count; i++)
                        {
                            optimalSlopes.Add(OptimalSlopes(path, poly[i], poly[(i + 1) % poly.Count], E));
                        }

                        var optimized_polygon = new List<(double X, double Y)>();

                        for (int i = 0; i < poly.Count; i++)
                        {
                            var l1 = optimalSlopes[i];
                            var l2 = optimalSlopes[(i + 1) % poly.Count];
                            optimized_polygon.Add(FindApproximatedVertex(path.Vertices[poly[i]], l1, l2));
                        }
                        
                        //Console.WriteLine(optimized_polygon.JoinToString(" "));

                        foreach (var _a in optimized_polygon)
                        {
                            g.FillEllipse(Brushes.Red, (float)_a.X * scale - 2, (float)_a.Y * scale - 2, 4, 4);
                        }

                        var corners = new List<Corner>();

                        for (int i = 0; i < optimized_polygon.Count; i++)
                        {
                            (double X, double Y) mid((double X, double Y) p, (double X, double Y) q) => ((p.X + q.X) / 2, (p.Y + q.Y) / 2);
                            var prv = optimized_polygon[i == 0 ? optimized_polygon.Count - 1 : i - 1];
                            var nxt = optimized_polygon[i == optimized_polygon.Count - 1 ? 0 : i + 1];
                            var crt = optimized_polygon[i];
                            corners.Add(new Corner(mid(prv, crt), crt, mid(nxt, crt)));
                        }

                        for (int i = 0; i < corners.Count; i++)
                        {
                            var γ = corners[i].GetCurveGamma();
                            var α = 4 * γ / 3;
                            if (α < 1)
                            {
                                corners[i].IsCurved = true;
                                corners[i].Alpha = γ;
                            }
                        }
                        corners.ForEach(Console.WriteLine);

                        foreach (var c in corners)
                        {
                            if (!c.IsCurved)
                            {
                                g.DrawLine(Pens.Blue, (float)(scale * c.B0.X), (float)(scale * c.B0.Y), (float)(scale * c.A.X), (float)(scale * c.A.Y));
                                g.DrawLine(Pens.Blue, (float)(scale * c.B1.X), (float)(scale * c.B1.Y), (float)(scale * c.A.X), (float)(scale * c.A.Y));
                            }
                            else
                            {
                                var b = c.GetBezier();
                                for (int i = 0; i < 4; i++)
                                    b[i] = new PointF(b[i].X * scale, b[i].Y * scale);
                                g.DrawBezier(Pens.Blue, b[0], b[1], b[2], b[3]);

                            }
                        }

                        allCorners.Add(corners.ToArray());
                    }
                }
                var resBitmap = new Bitmap(bmp.Width, bmp.Height);
                Graphics.FromImage(resBitmap).DrawImageUnscaled(bmp, 0, 0);

                //bmp.Save("haha_p.png");
                return new PotraceOutput(resBitmap, allCorners.ToArray());
            }
        }


        class E
        {
            long[] S_xx, S_xy, S_yy, S_x, S_y;                        
            private int verticesCount;
            public E(Path path)
            {                
                verticesCount = path.Vertices.Count;

                S_xx = new long[verticesCount];
                S_xy = new long[verticesCount];
                S_yy = new long[verticesCount];
                S_x = new long[verticesCount];
                S_y = new long[verticesCount];

                for(int i=0;i<verticesCount;i++)
                {
                    int x = path.Vertices[i].X;
                    int y = path.Vertices[i].Y;

                    S_xx[i] = 1L * x * x + (i == 0 ? 0 : S_xx[i - 1]);
                    S_xy[i] = 1L * x * y + (i == 0 ? 0 : S_xy[i - 1]);
                    S_yy[i] = 1L * y * y + (i == 0 ? 0 : S_yy[i - 1]);
                    S_x[i] = x + (i == 0 ? 0 : S_x[i - 1]);
                    S_y[i] = y + (i == 0 ? 0 : S_y[i - 1]);
                }
            }

            public int pdist(int i, int j) => i <= j ? j - i : j - i + verticesCount;

            double _getE(long[] S, int i, int j)
            {
                long s = i <= j ? (S[j] - (i == 0 ? 0 : S[i - 1])) : S[j] + S[S.Length - 1] - S[i - 1];
                return 1.0 * s / (pdist(i, j) + 1);
            }          

            public double XX(int i, int j) => _getE(S_xx, i, j);
            public double XY(int i, int j) => _getE(S_xy, i, j);
            public double YY(int i, int j) => _getE(S_yy, i, j);
            public double X(int i, int j) => _getE(S_x, i, j);
            public double Y(int i, int j) => _getE(S_y, i, j);
        }

        class ApprVertex
        {
            public double X, Y;
            public double Value;

            public ApprVertex(double x, double y, double value)
            {
                X = x;
                Y = y;
            }
        }

        static (double X, double Y) FindApproximatedVertex(Vertex v, (double A, double B, double C) L1, (double A, double B, double C) L2)
        {
            double dist2(double x, double y, (double A, double B, double C) L)
            {
                var d = L.A * x + L.B * y + L.C;
                return d * d / (L.A * L.A + L.B * L.B);
            }
            double f(double x, double y) => dist2(x, y, L1) + dist2(x, y, L2);

            double x1 = v.X - 0.5, y1 = v.Y - 0.5, x2 = v.X + 0.5, y2 = v.Y + 0.5;

            List<ApprVertex> pts = new List<ApprVertex>();
            int ptsCount = 20;
            var rand = new Random();

            for (int i = 0; i < ptsCount; i++)
            {
                var x = x1 + rand.NextDouble();
                var y = y1 + rand.NextDouble();
                pts.Add(new ApprVertex(x, y, f(x, y)));
            }

            for (int e = 0; e < 10; e++)
            {
                for (int i = 0; i < ptsCount; i++)
                {
                    var nx = (pts[i].X + rand.NextDouble() * 0.0001).Clamp(x1, x2);
                    var ny = (pts[i].Y + rand.NextDouble() * 0.0001).Clamp(y1, y2);
                    var val = f(nx, ny);
                    if (val < pts[i].Value)
                        pts[i] = new ApprVertex(nx, ny, val);
                }
            }

            var min = pts[0];
            for (int i = 1; i < ptsCount; i++)
            {
                if (pts[i].Value < min.Value) min = pts[i];
            }

            return (min.X, min.Y);
        }

        static (double A, double B, double C) OptimalSlopes(Path path, int i0, int i1, E e)
        {
            var px = e.X(i0, i1);
            var py = e.Y(i0, i1);

            var a = (decimal)(e.XX(i0, i1) - px * px);
            var b = (decimal)(e.XY(i0, i1) - px * py);
            var c = (decimal)(e.YY(i0, i1) - py * py);

            var del = ((a - c) * (a - c) + 4 * b * b);
            var l = (double)((a + c + (decimal)Math.Sqrt((double)del))) / 2;

            var nx = (double)b;
            var ny = l - (double)a;          

            var (ra, rb, rc) = (ny, -nx, -ny * px + nx * py);

            double es = 1e-7, ies = 1e7;

            if (ra < es && rb < es && rc < es)
            {
                ra *= ies;
                rb *= ies;
                rc *= ies;
            }            

            return (ra, rb, rc);
        }




        class Vertex
        {
            public int Y;
            public int X;
            public Vertex(int y, int x)
            {
                Y = y;
                X = x;
            }
            public override string ToString() => $"({Y},{X})";

        }

        class Path
        {
            public List<Vertex> Vertices = new List<Vertex>();
            public override string ToString() => Vertices.JoinToString(",");
            /*public override string ToString()
            {
                var x = Vertices.Select(_=>_.X).JoinToString(",");
                var y = Vertices.Select(_=>_.Y).JoinToString(",");
                return $"x={x}&y={y}";
            }*/

            public List<VerticalSegment> ToVerticalSegments()
            {
                var res = new List<VerticalSegment>();
                int x = -1;
                int y1 = 0, y2 = 0;
                for (int i = 0; i <= Vertices.Count; i++)
                {
                    var v = Vertices[i < Vertices.Count ? i : 0];

                    if (v.X != x)
                    {
                        if (y1 != y2)
                            res.Add(new VerticalSegment(x, y1, y2));
                        x = v.X;
                        y1 = y2 = v.Y;
                    }
                    else
                    {
                        y2 = v.Y;
                    }
                }
                if (y1 != y2)
                    res.Add(new VerticalSegment(x, y1, y2));
                return res;
            }
        }

        #region PathDecomposition
        class VerticalSegment
        {
            public int X;
            public int Y1;
            public int Y2;

            public VerticalSegment(int x, int y1, int y2)
            {
                X = x;
                Y1 = Math.Min(y1, y2);
                Y2 = Math.Max(y1, y2);
            }

            public override string ToString() => $"(X:{X},Y:{Y1}-{Y2})";
        }


        private static int XorPath(Matrix<byte> m, Path path)
        {
            int area = 0;
            var segments = path.ToVerticalSegments();

            var ymin = segments.Min(_ => _.Y1);
            var ymax = segments.Max(_ => _.Y2);

            var xmin = segments.Min(_ => _.X);
            var xmax = segments.Max(_ => _.X);
            for (int y = ymin; y < ymax; y++)
            {
                bool xoring = false;
                for (int x = xmin; x < xmax; x++)
                {
                    foreach (var s in segments)
                    {
                        if (x == s.X && y.IsBetween(s.Y1, s.Y2 - 1))
                        {
                            xoring = !xoring;
                            break;
                        }
                    }

                    if (xoring)
                    {
                        m[y, x] ^= 0xFF;
                        area++;
                    }
                }
            }
            return area;
        }

        private static IEnumerable<Path> PathDecomposition(DoubleMatrix matrix, int thurdSize)
        {
            List<Path> paths = new List<Path>();

            int border1 = 0;
            int border0 = 0;

            Matrices.ForEachItem(matrix, (x, i, j) =>
            {
                if (x.Value == 0) border0++; else border1++;
                return true;
            });
            var mat = Matrices.DoEachItem(matrix, i => (byte)((i.Value > 0 ? 0xFF : 0x00) ^ (border1 > border0 ? 0xFF : 0x00)));
            int by = 0, bx = 0;            
            while (FindVertex(mat, out var startVertex, ref by, ref bx))
            {
                var path = TracePath(mat, startVertex);
                int area = XorPath(mat, path);

                if (area >= thurdSize)
                    paths.Add(path);
                Console.WriteLine($"Found path of length {path.Vertices.Count} starting vertex {path.Vertices.First()}");                
                yield return path;              
            }
            yield break;
        }

        private static Path TracePath(Matrix<byte> m, Vertex startVertex)
        {
            var path = new Path();
            path.Vertices.Add(startVertex);
            for (var v = startVertex; (v = NextVertex(m, v, path)) != null;)
                path.Vertices.Add(v);
            return path;
        }

        private static (int dy, int dx)[] NextVertexDirections =
        {
            (0,0), (1,0), (0,-1), (0,-1), (0, 1), (1,0), (0,2), (0,-1),
            (-1, 0), (2,0), (-1, 0), (-1, 0), (0,1), (1, 0), (0,1), (0,0)
        };

        private static Vertex NextVertex(Matrix<byte> m, Vertex v, Path path)
        {
            var d = GetVertexDescriptor(m, v.Y, v.X);
            var (dy, dx) = NextVertexDirections[d];
            if (dx == 0 && dy == 0)
                throw new InvalidOperationException($"Invalid vertex position {v.Y},{v.X}");

            var candidates = new List<Vertex>();

            if (dx == 2)
            {
                candidates.Add(new Vertex(v.Y, v.X + 1));
                candidates.Add(new Vertex(v.Y, v.X - 1));
            }
            else if (dy == 2)
            {
                candidates.Add(new Vertex(v.Y - 1, v.X));
                candidates.Add(new Vertex(v.Y + 1, v.X));
            }
            else
            {
                candidates.Add(new Vertex(v.Y + dy, v.X + dx));
            }

            if (candidates.Count == 2)
            {
                if (path.Vertices.Any(w => w.X == candidates[0].X && w.Y == candidates[0].Y))
                    return candidates[1];
            }
            var first = path.Vertices[0];
            if (first.X == candidates[0].X && first.Y == candidates[0].Y)
                return null;

            return candidates[0];
        }

        private static bool FindVertex(Matrix<byte> m, out Vertex v, ref int by, ref int bx)
        {
            v = new Vertex(0, 0);

            for (int y = by; y < m.RowsCount; y++)
            {
                for (int x = y == by ? bx : 0; x < m.ColumnsCount; x++)
                {
                    if (IsVertex(m, y, x))
                    {
                        Console.WriteLine($"Found vertex {y} {x}");
                        by = y;
                        bx = x;
                        v = new Vertex(y, x);
                        return true;
                    }
                }
            }
            return false;
        }

        // encodes the 4 pixels around (y,x) into a 4-bit binary number
        // (1,0,0,1) = top-left and bottom-right pixels are "colored"
        private static int GetVertexDescriptor(Matrix<byte> m, int y, int x)
        {
            int r = 0;
            for (int dy = -1; dy <= 0; dy++)
                for (int dx = -1; dx <= 0; dx++)
                {
                    r *= 2;
                    int i = y + dy, j = x + dx;
                    if (i.IsBetween(0, m.RowsCount - 1) && j.IsBetween(0, m.ColumnsCount - 1) && m[i, j] > 0)
                        r++;
                }
            return r;
        }

        private static bool IsVertex(Matrix<byte> m, int y, int x)
        {
            var d = GetVertexDescriptor(m, y, x);
            return d != 0 && d != 15;
        }
        #endregion PathDecomposition

        #region Polygons

        static double Dist(Vertex v0, Vertex v1) => Math.Max(Math.Abs(v0.X - v1.X), Math.Abs(v0.Y - v1.Y));

        static double Slope(double y, double x)
        {
            var s = Math.Atan2(y, x);
            if (s < 0) s += 2 * Math.PI;
            return s;
        }

        static double Slope(double y0, double x0, double y1, double x1) => Slope(y1 - y0, x1 - x0);

        // builds a square of length 2 with the center in v1 and returns 
        // the angles limiting the beam starting from v0 that covers that square
        // (A,B) is the oriented angle from A to B radians
        static (double A, double B) GetSlopesRange(Vertex v0, Vertex v1)
        {
            if (Dist(v0, v1) <= 1) return (0, 2 * Math.PI);

            double[] angles =
            {
                Slope(v0.Y, v0.X, v1.Y - 1, v1.X - 1),
                Slope(v0.Y, v0.X, v1.Y - 1, v1.X + 1),
                Slope(v0.Y, v0.X, v1.Y + 1, v1.X - 1),
                Slope(v0.Y, v0.X, v1.Y + 1, v1.X + 1),
            };

            var min = angles.Min();
            var max = angles.Max();

            if (max - min <= Math.PI) return (min, max);

            var g = angles.Where(_ => _ >= Math.PI).Min();
            var l = angles.Where(_ => _ < Math.PI).Max();

            return (g, l);
        }

        static (double A, double B) IntersectSlopesRanges((double A, double B) p, (double A, double B) q)
        {
            if (p.A <= p.B)
            {
                if (q.A <= q.B)
                {
                    if (p.B <= q.A || q.B <= p.A) return (-1, -1);
                    return (Math.Max(p.A, q.A), Math.Min(p.B, q.B));
                }
                else
                {
                    var i1 = IntersectSlopesRanges(p, (q.A, 2 * Math.PI));
                    var i2 = IntersectSlopesRanges(p, (0, q.B));
                    if (i1 == (-1, -1)) return i2;
                    if (i2 == (-1, -1)) return i1;
                    return (i1.A, i2.B);
                }
            }
            else
            {
                if (q.A <= q.B) return IntersectSlopesRanges(q, p);
                else
                    return (Math.Max(p.A, q.A), Math.Min(p.B, q.B));
            }
        }

        static bool IncludesPath((int I, int J) p0, (int I, int J) p1)
        {
            if (p0.I <= p0.J)
            {
                if (p1.I <= p1.J) return p0.I <= p1.I && p1.J <= p0.J;
                else return false;
            }
            else
            {
                if (p1.I >= p1.J) return p0.J >= p1.J && p0.I <= p1.I;
                else return false;
            }
        }

        static List<(int I, int J)> FindStraightSubpaths(Path path)
        {
            var result = new List<(int, int)>();
            var directions = new HashSet<(int, int)>();

            (int I, int J) lastP = (-1, -1);

            for (int i = 0; i < path.Vertices.Count; i++)
            {
                var v0 = path.Vertices[i];
                (int I, int J) p = (i, i);
                directions.Clear();
                (double, double) angleView = (0, 2 * Math.PI);
                //Console.WriteLine($"i = {i}");
                var vPrev = v0;

                for (int j = (i + 1) % path.Vertices.Count; j != i; j = (j < path.Vertices.Count - 1) ? j + 1 : 0)
                {
                    //Debug.WriteLine($"{i} {j} :: {path.Vertices.Count}");
                    var v1 = path.Vertices[j];
                    directions.Add((v1.Y - vPrev.Y, v1.X - vPrev.X));
                    vPrev = v1;

                    //Console.WriteLine($"    Directions : {directions.Count}");

                    if (directions.Count > 3) break;
                    var newView = GetSlopesRange(v0, v1);
                    angleView = IntersectSlopesRanges(angleView, newView);

                    //Console.WriteLine($"    Updated view : {angleView}");

                    if (angleView == (-1, -1)) break;
                    p = (i, j);
                }
                if (!IncludesPath(lastP, p))
                    result.Add(p);
                lastP = p;
            }

            return result;
        }


        static List<int> FindPolygon(Path path, out E e)
        {
            int verticesCount = path.Vertices.Count;
            var possibleSegments = new Dictionary<int, HashSet<int>>();
            {
                void ps_add(int i, int j)
                {
                    var set = possibleSegments.TryGetValue(i, out var _set) ? _set : (possibleSegments[i] = new HashSet<int>());
                    set.Add(j);
                }

                var subpaths = FindStraightSubpaths(path);

                foreach (var (i, j) in subpaths)
                {
                    int ii = (i + 1) % path.Vertices.Count;

                    for (int p = ii; p != j; p = p < path.Vertices.Count - 1 ? p + 1 : 0)
                    {
                        for (int q = (p + 1) % path.Vertices.Count; q != j; q = q < path.Vertices.Count - 1 ? q + 1 : 0)
                        {
                            ps_add(p, q);
                        }
                    }
                }
            }


            HashSet<int> _g_get_neighbors(Dictionary<int, HashSet<int>> g, int i) => g.TryGetValue(i, out var result) ? result : new HashSet<int>();
            //bool _g_is_edge(Dictionary<int, HashSet<int>> g, int i, int j) => _g_get_neighbors(g, i).Contains(j);
            bool _g_add_edge(Dictionary<int, HashSet<int>> g, int i, int j) => (g.TryGetValue(i, out var result) ? result : g[i] = new HashSet<int>()).Add(j);

            HashSet<int> ps_get_neighbors(int i) => possibleSegments.TryGetValue(i, out var result) ? result : new HashSet<int>();
            bool ps_is_edge(int i, int j) => ps_get_neighbors(i).Contains(j);

            var _e = e = new E(path);


            Dictionary<(int, int), double> P = new Dictionary<(int, int), double>();

            double getPenalty(int i, int j)
            {
                if (P.TryGetValue((i, j), out var r)) return r;

                var xm = (path.Vertices[i].X + path.Vertices[j].X) / 2;
                var ym = (path.Vertices[i].Y + path.Vertices[j].Y) / 2;                

                decimal a = (decimal)(_e.XX(i, j) - 2 * xm * _e.X(i, j) + xm * xm);
                decimal b = (decimal)(_e.XY(i, j) - ym * _e.X(i, j) - xm * _e.Y(i, j) + xm * ym);
                decimal c = (decimal)(_e.YY(i, j) - 2 * ym * _e.Y(i, j) + ym * ym);

                var x = path.Vertices[j].X - path.Vertices[i].X;
                var y = path.Vertices[j].Y - path.Vertices[i].Y;
                           
                double sum = (double)(c * x * x - 2 * b * x * y + a * y * y);                

                if (sum < 0) 
                    throw new ArgumentException();

                return P[(i, j)] = Math.Sqrt(sum);
            }

            Dictionary<int, HashSet<int>> ReverseGraph(Dictionary<int, HashSet<int>> g)
            {
                var r = new Dictionary<int, HashSet<int>>();
                foreach (var u in g.Keys)
                    foreach (var v in g[u])
                        _g_add_edge(r, v, u);
                return r;
            }

            Dictionary<int, (int prevNode, int pathLen, double cost)> BFS(Dictionary<int, HashSet<int>> g, int startU, int maxLen)
            {
                Dictionary<int, (int prevNode, int pathLen, double cost)> result = new Dictionary<int, (int, int, double)>();

                Queue<int> Q = new Queue<int>();
                Q.Enqueue(startU);
                result[startU] = (-1, 0, 0);

                while (Q.Count > 0)
                {
                    var u = Q.Dequeue();                    
                    var (prevu, pathLenu, costu) = result[u];                    

                    foreach (var v in _g_get_neighbors(g, u))
                    {
                        var pathLenv = pathLenu + 1;                        
                        var costv = costu + getPenalty(u, v);

                        if (maxLen == 0 || pathLenv <= maxLen) 
                        {
                            if (result.TryGetValue(v, out var v0))
                            {
                                if (pathLenv < v0.pathLen || (pathLenv == v0.pathLen && costv < v0.cost))
                                    result[v] = (u, pathLenv, costv);
                            }
                            else
                            {
                                result[v] = (u, pathLenv, costv);
                                Q.Enqueue(v);
                            }                            
                        }
                    }
                }
                return result;

            }

            var R = ReverseGraph(possibleSegments);

            var rPathLen = path.Vertices.Count;
            var rCost = double.PositiveInfinity;
            int ru = 0;
            int rv = 0;
            Dictionary<int, (int prevNode, int, double)> rbfs = null;

            int rMaxLen = 0;
            HashSet<int> visited = new HashSet<int>();

            foreach (var u in possibleSegments.Keys)
            {
                if (visited.Contains(u)) continue;
                Debug.WriteLine($"N{u}");
                var bfs = BFS(R, u, rMaxLen);

                foreach (var v in bfs.Keys)
                {                    
                    if (!ps_is_edge(u, v)) continue;

                    var r = bfs[v];
                    var newCost = r.cost + getPenalty(u, v);
                    if (rPathLen == 0 || r.pathLen < rPathLen || (r.pathLen == rPathLen && newCost < rCost)) 
                    {                        
                        (rPathLen, rCost, ru, rv, rbfs) = (r.pathLen, newCost, u, v, bfs);                        
                    }                                       
                }

                if (rMaxLen == 0 || rPathLen < rMaxLen)                
                    rMaxLen = rPathLen;               
                
                int _nd = rv;
                visited.Add(_nd);
                while (_nd != ru)
                {                    
                    _nd = rbfs[_nd].prevNode;
                    visited.Add(_nd);
                }                
            }

            List<int> rPath = new List<int>();
            int nd = rv;
            rPath.Add(nd);

            while (nd != ru) 
            {
                nd = rbfs[nd].prevNode;
                rPath.Add(nd);
            }

            Console.WriteLine($"Final cost: {rCost}");
            Console.WriteLine($"Final path Len: {rPath.Count}");

            return rPath;
        }

        #endregion Polygons
    }

    public class Corner
    {
        public (double X, double Y) B0;
        public (double X, double Y) A;
        public (double X, double Y) B1;
        public bool IsCurved = false;
        public double Alpha = 0;

        public Corner((double X, double Y) b0, (double X, double Y) a, (double X, double Y) b1)
        {
            B0 = b0;
            A = a;
            B1 = b1;
        }

        public double GetCurveGamma()
        {
            var a = B0.Y - B1.Y;
            var b = B1.X - B0.X;
            var c = B0.X * B1.Y - B1.X * B0.Y;

            var cs = from p in new (double X, double Y)[] { (A.X - 0.5, A.Y - 0.5), (A.X - 0.5, A.Y + 0.5), (A.X + 0.5, A.Y - 0.5), (A.X + 0.5, A.Y + 0.5) }
                     select -(a * p.X + b * p.Y);
            var c0 = cs.Min();
            var c1 = cs.Max();

            var cr = c;

            if (!(c.IsBetween(c0, c1) || c.IsBetween(c1, c0)))
                cr = Math.Abs(c - c0) < Math.Abs(c - c1) ? c0 : c1;
            var ca = -(a * A.X + b * A.Y);

            return (c - cr) / (c - ca);
        }

        public PointF[] GetBezier()
        {
            return new[]
            {
                    new PointF((float)B0.X, (float)B0.Y),
                    new PointF((float)(B0.X*(1-Alpha)+A.X*Alpha), (float)(B0.Y*(1-Alpha)+A.Y*Alpha)),
                    new PointF((float)(B1.X*(1-Alpha)+A.X*Alpha), (float)(B1.Y*(1-Alpha)+A.Y*Alpha)),
                    new PointF((float)B1.X, (float)B1.Y),
                };
        }

        private static string ToStr((double X, double Y) p) => $"({p.X:0.00},{p.Y:0.00})";

        public override string ToString()
            => $"{{{(IsCurved ? "Bezier" : "Sharp")}, B0={ToStr(B0)}, A={ToStr(A)}, B1={ToStr(B1)}{(IsCurved ? $", Alpha={Alpha}" : "")}}}";


    }

    public class PotraceOutput
    {
        public Bitmap Bitmap { get; }
        public Corner[][] Curves { get; }
        public PotraceOutput(Bitmap bitmap, Corner[][] curves)
        {
            Bitmap = bitmap;
            Curves = curves;
        }
    }
}
