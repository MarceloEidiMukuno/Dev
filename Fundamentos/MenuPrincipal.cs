using System;

namespace Fundamentos
{
    public static class MenuPrincipal
    {
        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Menu Principal".ToUpper());
            Console.WriteLine("--------------------");

            Console.WriteLine("1 - Data");
            Console.WriteLine("2 - String");
            Console.WriteLine("3 - Moeda");
            Console.WriteLine("0 - Sair");

            Console.WriteLine("Selecione uma opção:");
            short opcao = short.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Datas.MenuData();
                    break;
                case 2:
                    Strings.MenuString();
                    break;
                case 3:
                    Moeda.MenuMoeada();
                    break;
                case 0:
                    System.Environment.Exit(0);
                    break;
                default:
                    Menu();
                    break;
            }
        }
    }
}