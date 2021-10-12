//Изменение
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;

namespace ex._9
{
    public class Node<T>
    {
        static int count;
        public Node(double data)
        {
            count ++;
            Data = data;
            number = count;
        }
        public double Data { get; set; }
        public int number { get; set; }
    }

    public class LinearList<T>
    {
        Node<double>[] nodes;

        public Node<double> this [int index]
        {
            get
            {
                return nodes[index];
            }
            set
            {
                nodes[index] = value;
            }
        }
        public int count;


        public LinearList(int N)
        {
            try
            {
                nodes = new Node<double>[N];
                count = N;
            }
            catch (System.OverflowException)
            {
                Console.WriteLine("Введена некорректная длина последовательности. Завершение работы программы.");
                Environment.Exit(1);
            }
        }

        

        public LinearList<double> Clear()
        {
            count = 0;

            return (new LinearList<double>(0));
        }

        public void Remove (ref LinearList<double> posl, int index)
        {

            for(int i=index; i<count; i++)
            {
                posl[i].number = posl[i].number - 1;
            }
            LinearList<double> copy = new LinearList<double>(count);
            copy = (LinearList<double>)posl.MemberwiseClone();
            posl = new LinearList<double>(count - 1);
            int COUNT = 0;
            for(int i=0; i<count; i++)
            {
                if (i != index-1)
                {
                    posl[COUNT] = copy[i];
                    COUNT++;
                }
            }
            count--;

        }

        public int Search(LinearList<double> posl,double znach)
        {
            for (int i = 0; i<count; i++)
            {
                if (posl[i].Data == znach)
                {
                    return posl[i].number;
                }
            }
            return -1;
        }
        public void Show(LinearList<double> posl)
        {
            for(int i=0; i<posl.count; i++)
            {
                Console.WriteLine(posl[i].Data + "            " + posl[i].number);
            }
            Console.ReadKey();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                #region posledovatelnist
                Console.WriteLine("Введите кол-во элементов списка");
                int N = int.Parse(Console.ReadLine());
                LinearList<double> linearList = new LinearList<double>(N);
                string[] s = File.ReadLines("numbers.txt").First().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                double[] numbers = new double[N];
                for (int i = 0; i < numbers.Length; i++)
                {
                    numbers[i] = double.Parse(s[i]);
                }

                int posCount = 0;
                foreach (double n in numbers)
                {
                    if (n > 0) posCount++;
                }
                int COUNT = 0;
                double[] positive = new double[posCount];
                for (int i = 0; i < N; i++)
                {
                    if (numbers[i] > 0)
                    {
                        positive[COUNT] = numbers[i];
                        COUNT++;
                    }
                }
                Array.Reverse(positive);


                int negCount = 0;
                foreach (double n in numbers)
                {
                    if (n < 0) negCount++;
                }
                double[] negative = new double[negCount];
                COUNT = 0;
                for (int i = 0; i < N; i++)
                {
                    if (numbers[i] < 0)
                    {
                        negative[COUNT] = numbers[i];
                        COUNT++;
                    }
                }

                int zeroCount = 0;
                foreach (double n in numbers)
                {
                    if (n == 0) zeroCount++;
                }


                double[] chisla = new double[N];
                for (int i = 0; i < posCount; i++)
                {
                    chisla[i] = positive[i];
                }
                for (int i = posCount; i < posCount + zeroCount; i++)
                {
                    chisla[i] = 0;
                }
                for (int i = posCount + zeroCount; i < N; i++)
                {
                    chisla[i] = negative[i - posCount - zeroCount];
                }
                #endregion
                LinearList<double> posl = new LinearList<double>(chisla.Length);
                for (int i = 1; i <= chisla.Length; i++)
                {
                    posl[i - 1] = new Node<double>(chisla[i - 1]);
                }
                Console.WriteLine(@"Линейный список:
Значение   Номер элемента");
                posl.Show(posl);

                Console.WriteLine("Введите номер элемента для удаления");
                int number = int.Parse(Console.ReadLine());
                posl.Remove(ref posl, number);
                Console.WriteLine(@"Линейный список:
Значение   Номер элемента");
                posl.Show(posl);
                Console.WriteLine("Введите значение элемента, который нужно найти");
                int znach = int.Parse(Console.ReadLine());
                double output = posl.Search(posl, znach);
                if (output != -1)
                {
                    Console.WriteLine($"Номер элемента со значением = {znach} - {output}");
                }
                else
                {
                    Console.WriteLine("В последовательности нет элемента с таким значением");
                }
                
            }
            catch (System.IndexOutOfRangeException)
            {
                Console.WriteLine("В файле нет столько элементов. Завершение работы программы");
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Введено не число. Завершение работы программы");
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("Файл не найден. Завершение работы программы");
            }
            catch (System.InvalidOperationException)
            {
                Console.WriteLine("Файл пуст. Завершение работы программы");
            }
            Console.ReadKey();
        }
    }
}
