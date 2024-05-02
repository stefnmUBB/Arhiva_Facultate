using AI.Lab1.Solvers;
using System;
using System.Collections.Generic;
using System.IO;

namespace AI.Lab1
{
    internal class Program
    { 
        public static void TestProblem1() => TestFileIO(1);
        public static void TestProblem2()
        {
            new Problem2().TestAssert(((1, 5), (4, 1)), 5);
            new Problem2().TestAssert(((0, 0), (4, 1)), Math.Sqrt(17));
        }

        public static void TestProblem3() => TestFileIO(3);

        public static void TestProblem4() => TestFileIO(4);

        public static void TestProblem5()
        {
            new Problem5().TestAssert(new List<int> { 1, 2, 3, 4, 2 }, 2);
            new Problem5Boost().TestAssert(new List<int> { 1, 2, 3, 4, 2 }, 2);

            new Problem5().TestAssert(new List<int> { 5, 1, 2, 2, 4, 3 }, 2);
            new Problem5Boost().TestAssert(new List<int> { 5, 1, 2, 2, 3, 4 }, 2);

            new Problem5().TestAssert(new List<int> { 5, 4, 1, 2, 3, 5 }, 5);
            new Problem5Boost().TestAssert(new List<int> { 5, 4, 1, 2, 3, 5 }, 5);
        }              

        public static void TestProblem6()
        {
            new Problem6().TestAssert(new List<int> { 2, 8, 7, 2, 2, 5, 2, 3, 1, 2, 2 }, 2);
            new Problem6().TestAssert(new List<int> { 1, 1, 1, 1, 1, 1, 3 }, 1);
            new Problem6().TestAssert(new List<int> { 1, 1, 1, 1, 1, 1, 1 }, 1);
            new Problem6().TestAssert(new List<int> { 1, 1, 1, 1, 3, 3, 3 }, 1);
            new Problem6().TestAssert(new List<int> { 1, 1, 1, 1, 2, 3, 4 }, 1);
        }

        public static void TestProblem7()
        {
            new Problem7().TestAssert((new List<int> { 7, 4, 6, 3, 9, 1 }, 2), 7);
            new Problem7().TestAssert((new List<int> { 1, 2, 3, 4 }, 2), 3);
            new Problem7().TestAssert((new List<int> { 5, 5, 9 }, 1), 9);
        }

        public static void TestProblem8() => TestFileIO(8);
        public static void TestProblem9() => TestFileIO(9);
        public static void TestProblem10() => TestFileIO(10);        
        public static void TestProblem11() => TestFileIO(11);        

        static void Main()
        {            
            TestProblems();
            Console.WriteLine("Done.");
            Console.ReadLine();
        }

        static void TestProblems()
        {            
            for (int i = 1; i <= 11; i++)
            {
                var method = typeof(Program).GetMethod($"TestProblem{i}");
                if (method == null) continue;
                Console.WriteLine($"Testing Problem {i}");
                try
                {
                    method.Invoke(null, null);
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
        }

        public static string InputFile(int problem, int fid) => $@"Inputs\P{problem}\{fid}.in";
        public static string OutputFile(int problem, int fid) => $@"Outputs\P{problem}\{fid}.out";

        private static void TestFileIO(int pId)
        {            
            Type problemType = Type.GetType($"AI.Lab1.Solvers.Problem{pId}");
            int tId = 0;

            while(File.Exists(InputFile(pId, tId)))
            {
                Console.Write($"Test {tId.ToString().PadLeft(2)} : ");
                try
                {
                    (Activator.CreateInstance(problemType) as FileIOProblem)
                        .TestAssert(InputFile(pId, tId), OutputFile(pId, tId));
                    Console.Write("Ok.");
                }
                catch(Exception e)
                {
                    Console.Write($"Fail : {e.Message}");                    
                }
                Console.WriteLine();
                tId++;
            }                                 
        }
    }
}
