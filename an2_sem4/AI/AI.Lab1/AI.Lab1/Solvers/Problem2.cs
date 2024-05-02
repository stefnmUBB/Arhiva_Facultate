using System;

namespace AI.Lab1.Solvers
{
    internal class Problem2 : Problem<((int X, int Y) Point1, (int X, int Y) Point2), double>
    {

        /*Să se determine distanța Euclideană între două locații identificate prin perechi de numere. 
         * De ex. distanța între (1,5) și (4,1) este 5.0
         * 
         * IO : Parametri
         * Date de intrare : Doua seturi de coordonate in plan (x1,y1) (x2,y2)
         * Date de iesire : Distanta dintre cele doua puncte 
         * 
         * Complexitate: Θ(1), O(1) memorie
         */
        public override void Solve()
        {
            int dx = Input.Point1.X - Input.Point2.X;
            int dy = Input.Point1.Y - Input.Point2.Y;
            Output = Math.Sqrt(dx * dx + dy * dy);
        }      
    }
}
