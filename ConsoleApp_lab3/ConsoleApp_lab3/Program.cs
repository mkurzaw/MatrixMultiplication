using System;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int m = 2, n = 3, p = 3, q = 3, i, j;
            int[,] a = { { 1, 4, 2 }, { 2, 5, 1 } };
            int[,] b = { { 3, 4, 2 }, { 3, 5, 7 }, { 1, 2, 1 } };
            Console.WriteLine("Matrix a:");
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < n; j++)
                {
                    Console.Write(a[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Matrix b:");
            for (i = 0; i < p; i++)
            {
                for (j = 0; j < q; j++)
                {
                    Console.Write(b[i, j] + " ");
                }
                Console.WriteLine();
            }
            if (n != p)
            {
                Console.Write("Mnozenie macierzy niemozliwe");

            }
            else
            {
                Test test = new Test();
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Thread[] threads = new Thread[m];

                for (i = 0; i < m; i++)
                {
                    var temp = i;
                    threads[i] = new Thread(() => test.multiplicate(a, b, q, m, n, i));
                }
                for (i = 0; i < m; i++)
                {
                    threads[i].Start();
                }
                for (i = 0; i < m; i++)
                    threads[i].Join();
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine(elapsedMs + " ms");

            }

        }
        public class Test
        {



            public void multiplicate(int[,] A, int[,] B, int q, int m, int n, int j)
            {
                Thread.Sleep(100);
                Console.WriteLine();
                int[] c = new int[q];

                for (int i = 0; i < q; i++)
                {
                    c[i] = 0;
                    for (int k = 0; k < n; k++)
                    {
                        c[i] += A[j, k] * B[k, i];
                    }


                }
                for (int i = 0; i < q; i++)
                {
                    Console.WriteLine(c[i] + "\t");
                }

            }
        }
    }
}