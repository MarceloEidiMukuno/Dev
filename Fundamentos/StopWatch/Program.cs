using System;
using System.Threading;

namespace Stopwatch
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("S - Segundo");
            Console.WriteLine("M - Minuto");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("Deseja contar em?");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "S":
                case "s":
                    Segundo();
                    break;

                case "M":
                case "m":
                    Minuto();
                    break;

                case "0":
                    System.Environment.Exit(0);
                    break;
                default:
                    Menu();
                    break;

            }
        }

        static void Segundo()
        {
            Console.Clear();
            Console.WriteLine("Até quanto?");

            string tempo = Console.ReadLine();

            int contador = 0;
            while (contador != int.Parse(tempo))
            {
                Console.Clear();
                contador++;
                Console.WriteLine(contador);
                Thread.Sleep(1000);

            }
            Console.Clear();
            Console.WriteLine("Stopwatch finalizado!");

        }

        static void Minuto()
        {
            Console.Clear();
            Console.WriteLine("Até quanto?");

            string tempo = Console.ReadLine();

            Console.Clear();
            int contador = 0;
            while (contador != int.Parse(tempo))
            {

                contador++;
                Console.WriteLine(contador);
                Thread.Sleep(5000);

            }
            Console.Clear();
            Console.WriteLine("Stopwatch finalizado!");

        }


    }
}
