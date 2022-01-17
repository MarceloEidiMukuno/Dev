using System;
using System.Text;

namespace Fundamentos
{
    public static class Strings
    {

        public static void MenuString()
        {
            Console.Clear();
            Console.WriteLine("Menu String".ToUpper());
            Console.WriteLine("--------------------");
            Console.WriteLine("1 - Comparacao");
            Console.WriteLine("2 - Concatenar");
            Console.WriteLine("3 - Metodos Adicionais");

            Console.WriteLine("0 - Voltar");

            Console.WriteLine("Selecione uma opcao:");
            short opcao = short.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Comparacao();
                    MenuString();
                    break;
                case 2:
                    Concatenando();
                    MenuString();
                    break;
                case 3:
                    MetodosAdicionais();
                    MenuString();
                    break;
                case 0:
                    MenuPrincipal.Menu();
                    break;
                default:
                    MenuString();
                    break;
            }

        }
        private static void Comparacao()
        {
            var texto = "Comparando texto";

            Console.WriteLine(texto.CompareTo("Comparando texto")); // 1
            Console.WriteLine(texto.Equals("Comparando texto")); // 0

            Console.WriteLine(texto.Contains("Comparando")); // True
            Console.WriteLine(texto.Contains("comparando")); // False
            Console.WriteLine(texto.Contains("comparando", StringComparison.OrdinalIgnoreCase)); //Igonara o case sensitive

            Console.ReadKey();

        }

        private static void MetodosAdicionais()
        {
            var texto = "Este é um texto teste";

            Console.WriteLine(texto.ToLower()); // este é um texto teste  
            Console.WriteLine(texto.ToUpper()); // ESTE É UM TEXTO TESTE
            Console.WriteLine(texto.Insert(5, "Aqui")); // Este Aquié um texto teste
            Console.WriteLine(texto.Remove(5, 5)); // Este texto teste
            Console.WriteLine(texto.Length); // 21

            Console.WriteLine(texto.IndexOf("t")); // 2
            Console.WriteLine(texto.LastIndexOf("t")); // 19

            Console.ReadKey();

        }

        private static void Concatenando()
        {
            var texto1 = "Como fazer para concatenar strings";
            texto1 += "e de forma não otimizando é usando operador +";

            Console.WriteLine(texto1);

            StringBuilder texto = new StringBuilder();
            texto.Append("Como fazer para concatenar strings");
            texto.Append("e de forma otimizada é atraves do StringBuilder");

            Console.WriteLine(texto);

            Console.ReadKey();
        }
    }

}