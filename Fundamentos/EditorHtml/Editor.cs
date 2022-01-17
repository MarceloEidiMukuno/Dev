using System;
using System.Text;

namespace EditorHtml
{

    public static class Editor
    {
        public static void Show()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("Modo Editor");
            Console.WriteLine("-----------");

            Start();

        }

        public static void Start()
        {
            var texto = new StringBuilder();

            do
            {
                texto.Append(Console.ReadLine());
                texto.Append(Environment.NewLine);

            } while (ConsoleKey.Escape != Console.ReadKey().Key);

        }
    }
}