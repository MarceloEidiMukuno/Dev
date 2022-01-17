using System;

namespace EditorHtml
{
    public static class Menu
    {
        public static void Show()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;

            DrawScreen();
            DrawOption();

            HandleMenuOption(short.Parse(Console.ReadLine()));

        }

        public static void DrawScreen()
        {
            Console.Write("+");

            for (int i = 0; i <= 30; i++)
            {
                Console.Write("-");
            }

            Console.Write("+");

            Console.Write("\n");

            for (int i = 0; i <= 10; i++)
            {
                Console.Write("|");
                for (int j = 0; j <= 30; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("|");
                Console.Write("\n");
            }


            Console.Write("+");

            for (int i = 0; i <= 30; i++)
            {
                Console.Write("-");
            }

            Console.Write("+");

        }

        public static void DrawOption()
        {
            Console.SetCursorPosition(1, 0);
            Console.WriteLine("Selecione uma opção:");
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("1 - Abrir Arquivo");
            Console.SetCursorPosition(1, 2);
            Console.WriteLine("2 - Escrever Arquivo");
            Console.SetCursorPosition(1, 3);
            Console.WriteLine("0 - Sair");
            Console.SetCursorPosition(1, 4);
            Console.WriteLine("Opção:");



        }

        public static void HandleMenuOption(int option)
        {
            switch (option)
            {
                case 1: Editor2.Show(); break;
                case 2: Console.Write("Escrever"); break;
                case 0: { Console.Clear(); Environment.Exit(0); break; }
                default: Show(); break;
            }

        }
    }
}

