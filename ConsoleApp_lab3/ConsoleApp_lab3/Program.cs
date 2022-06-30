using System;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static int m, n, p, q, i, j;
        static int[,] a, b;

        static void loadParameters()
        {
            Console.WriteLine("\nPodaj rozmiar m macierzy A: ");
            m = int.Parse(Console.ReadLine());

            Console.WriteLine("\nPodaj rozmiar n macierzy A: ");
            n = int.Parse(Console.ReadLine());

            Console.WriteLine("\nPodaj rozmiar p macierzy B: ");
            p = int.Parse(Console.ReadLine());

            Console.WriteLine("\nPodaj rozmiar q macierzy B: ");
            q = int.Parse(Console.ReadLine());

            if (n != p)
            {
                Console.Write("Mnozenie macierzy niemozliwe. Podaj poprawne wymiary\n");
                loadParameters();
            }
            else
            {
                a = new int[m, n];
                b = new int[p, q];

                for (i = 0; i < m; i++)
                {
                    for (j = 0; j < n; j++)
                    {
                        a[i, j] = j + i*n;    // elementy macierzy = kolejne liczby naturalne + 0
                    }
                }

                for (i = 0; i < p; i++)
                {
                    for (j = 0; j < q; j++)
                    {
                        b[i, j] = j + i * n;    // elementy macierzy = kolejne liczby naturalne + 0
                    }
                }

            }
        }


        static void Main(string[] args)
        {
            //int m = 2, n = 3, p = 3, q = 3, i, j;
            //int[,] a = { { 1, 4, 2 }, { 2, 5, 1 } };
            //[,] b = { { 3, 4, 2 }, { 3, 5, 7 }, { 1, 2, 1 } };

            loadParameters();



/*
            Console.WriteLine("\nMatrix A:");
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < n; j++)
                {
                    Console.Write(a[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nMatrix B:");
            for (i = 0; i < p; i++)
            {
                for (j = 0; j < q; j++)
                {
                    Console.Write(b[i, j] + " ");
                }
                Console.WriteLine();
            }
*/

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
                /*
                for (int i = 0; i < q; i++)
                {
                    Console.WriteLine(c[i] + "\t");
                }
                */

            }
        }
    }
}
