using System;
using System.Threading;


namespace ConsoleApp1
{
    class Program
    {
        // zmienne globalne
        static int aRows, aColumns, bRows, bColumns;
        static int[,] aMatrix, bMatrix;
        static int threadsAmount;
        const int maxThreadsAmount = 8; // ilość wątków


        // metoda globalna do ustalenia przez użytkownika
        // wymiarów macierzy do przemnożenia
        static void LoadParameters()
        {
            Console.WriteLine("\nPodaj rozmiar m macierzy A: ");
            aRows = int.Parse(Console.ReadLine());

            Console.WriteLine("\nPodaj rozmiar n macierzy A: ");
            aColumns = int.Parse(Console.ReadLine());

            Console.WriteLine("\nPodaj rozmiar p macierzy B: ");
            bRows = int.Parse(Console.ReadLine());

            Console.WriteLine("\nPodaj rozmiar q macierzy B: ");
            bColumns = int.Parse(Console.ReadLine());

            if (aColumns != bRows)
            {
                Console.Write("Mnozenie macierzy niemozliwe. Podaj poprawne wymiary\n");
                LoadParameters();
            }
            else
            {
                // jesli wierszy macierzy wynikowej jest mniej niż 8 (maxThreadsAmount)
                // to ilość wątków = ilość wierszy 
                if (aRows < maxThreadsAmount)
                {
                    threadsAmount = aRows;
                }
                else
                {
                    threadsAmount = maxThreadsAmount;
                }

                aMatrix = new int[aRows, aColumns];
                bMatrix = new int[bRows, bColumns];

                for (int i = 0; i < aRows; i++)
                {
                    for (int j = 0; j < aColumns; j++)
                    {
                        aMatrix[i, j] = j + i*aColumns;    // elementy macierzy = kolejne liczby naturalne + 0
                    }
                }

                for (int i = 0; i < bRows; i++)
                {
                    for (int j = 0; j < bColumns; j++)
                    {
                        bMatrix[i, j] = j + i * bColumns;    // elementy macierzy = kolejne liczby naturalne + 0
                    }
                }
            }
        }




        public class ThreadClass
        {
            private int[,] Matrix = new int[aRows, bColumns];

            public void Multiplicate()
            {
                int threadNumber = int.Parse(Thread.CurrentThread.Name);

                for (int i = threadNumber; i < aRows; i += threadsAmount) // tu możliwy błąd
                {
                    for (int j = 0; j < bColumns; j++)
                    {
                        Matrix[i, j] = 0;
                        for (int k = 0; k < aColumns; k++)
                        {
                            Matrix[i, j] += aMatrix[i, k] * bMatrix[k, j];
                        }
                    }
                }
            }

            public void ViewMatrix()
            {
                for (int i = 0; i < aRows; i++)
                {
                    for (int j = 0; j < bColumns; j++)
                    {
                        Console.Write(Matrix[i, j] + "      ");
                    }
                    Console.WriteLine("");
                }
            }
        }




        static void Main(string[] args)
        {
            LoadParameters();
            const int maxSizeToView = 12; 

            if (aRows <= maxSizeToView && aColumns <= maxSizeToView)
            {
                Console.WriteLine("\nMacierz A:");
                for (int i = 0; i < aRows; i++)
                {
                    for (int j = 0; j < aColumns; j++)
                    {
                        Console.Write(aMatrix[i, j] + "     ");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("\nNie wyświetlono macierzy A ze względu na zbyt duże wymiary");
            }

            if (bRows <= maxSizeToView && bColumns <= maxSizeToView)
            {
                Console.WriteLine("\nMacierz B:");
                for (int i = 0; i < bRows; i++)
                {
                    for (int j = 0; j < bColumns; j++)
                    {
                        Console.Write(bMatrix[i, j] + "     ");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("\nNie wyświetlono macierzy B ze względu na zbyt duże wymiary");
            }

            ThreadClass threadObject = new ThreadClass();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Thread[] threads = new Thread[threadsAmount];

            for (int i = 0; i < threadsAmount; i++)
            {
                threads[i] = new Thread(threadObject.Multiplicate);
                threads[i].Name = String.Format("{0}", i);
            }

            for (int i = 0; i < threadsAmount; i++)
            {
                threads[i].Start();
            }

            for (int i = 0; i < threadsAmount; i++)
                threads[i].Join();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            if (aRows <= maxSizeToView && bColumns <= maxSizeToView)
            {
                Console.WriteLine("\nMacierz wynikowa:");
                threadObject.ViewMatrix();
            }
            else
            {
                Console.WriteLine("\nNie wyświetlono macierzy wynikowej ze względu na zbyt duże wymiary");
            }

            Console.WriteLine("\nCzas obliczeń wersji wielowątkowej: " + elapsedMs + " ms");
        }
    }
}