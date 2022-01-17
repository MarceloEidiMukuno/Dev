using System;
using System.Globalization;

namespace Fundamentos
{

    public static class Moeda
    {
        public static void MenuMoeada()
        {
            Console.Clear();
            Console.WriteLine("Menu Moead".ToUpper());
            Console.WriteLine("--------------------");
            Console.WriteLine("1 - Formato");
            Console.WriteLine("2 - Arredondando");

            Console.WriteLine("0 - Voltar");

            Console.WriteLine("Selecione uma opcao:");
            short opcao = short.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Formatando();
                    MenuMoeada();
                    break;
                case 2:
                    Arredondado();
                    MenuMoeada();
                    break;
                case 3:
                    break;
                case 0:
                    MenuPrincipal.Menu();
                    break;
                default:
                    MenuMoeada();
                    break;
            }

        }

        private static void Formatando()
        {
            decimal valor = 10536.25m;

            Console.WriteLine(valor.ToString("G", CultureInfo.CreateSpecificCulture("pt-BR"))); //Generico
            Console.WriteLine(valor.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"))); //Currecy//Moeda
            Console.WriteLine(valor.ToString("F", CultureInfo.CreateSpecificCulture("pt-BR"))); //Maior Precisão 
            Console.WriteLine(valor.ToString("N", CultureInfo.CreateSpecificCulture("pt-BR"))); //Numero
            Console.WriteLine(valor.ToString("P", CultureInfo.CreateSpecificCulture("pt-BR"))); //Porcentagem


            Console.WriteLine(valor.ToString("E04", CultureInfo.CreateSpecificCulture("pt-BR")));


        }

        private static void Arredondado()
        {
            decimal valor = 10536.25m;
            Console.WriteLine(Math.Round(valor)); // Arredonda
            Console.WriteLine(Math.Ceiling(valor)); // Arredonda pra cima
            Console.WriteLine(Math.Floor(valor)); // Arredonda pra baixo


        }
    }
}