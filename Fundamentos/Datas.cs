using System;
using System.Globalization;

namespace Fundamentos
{
    public static class Datas
    {
        public static void MenuData()
        {
            Console.Clear();
            Console.WriteLine("Menu Data".ToUpper());
            Console.WriteLine("--------------------");
            Console.WriteLine("1 - Formatacao");
            Console.WriteLine("2 - Culture");
            Console.WriteLine("3 - TimeZone");

            Console.WriteLine("0 - Voltar");

            Console.WriteLine("Selecione uma opcao:");
            short opcao = short.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    FortatandoData();
                    MenuData();
                    break;
                case 2:
                    Culture();
                    MenuData();
                    break;
                case 3:
                    TimeZone();
                    MenuData();
                    break;
                case 0:
                    MenuPrincipal.Menu();
                    break;
                default:
                    MenuData();
                    break;
            }

        }

        private static void FortatandoData()
        {
            DateTime data = DateTime.Now;

            Console.WriteLine(data); //18/12/2021 18:27:28
            Console.WriteLine(data.ToString("yyyy-MM-dd")); //2021-12-18
            Console.WriteLine(data.ToString("dd-MM-yyyy")); // 18-12-2021

            Console.WriteLine(data.ToString("t")); //18:27
            Console.WriteLine(data.ToString("T")); //18:27:28

            Console.WriteLine(data.ToString("d")); //18/12/2021
            Console.WriteLine(data.ToString("D")); //sábado, 18 de dezembro de 2021

            Console.WriteLine(data.ToString("f")); //sábado, 18 de dezembro de 2021 18:27
            Console.WriteLine(data.ToString("g")); //18/12/2021 18:27

            // Mais usados
            Console.WriteLine(data.ToString("r")); //Sat, 18 Dec 2021 18:27:28 GMT
            Console.WriteLine(data.ToString("s")); //2021-12-18T18:27:28
            Console.WriteLine(data.ToString("u")); //2021-12-18 18:27:28Z

        }

        private static void Culture()
        {
            var pt = new CultureInfo("pt-PT");
            var br = new CultureInfo("pt-BR");
            var en = new CultureInfo("en-US");
            var de = new CultureInfo("en-DE");

            Console.WriteLine(DateTime.Now.ToString("D", pt)); // 18 de dezembro de 2021
            Console.WriteLine(DateTime.Now.ToString("D", br)); // sábado, 18 de dezembro de 2021
            Console.WriteLine(DateTime.Now.ToString("D", en)); // Saturday, December 18, 2021
            Console.WriteLine(DateTime.Now.ToString("D", de)); // Saturday, 18 December 2021

        }

        private static void TimeZone()
        {
            var utcDate = DateTime.UtcNow; // Data Universal 

            Console.WriteLine(DateTime.Now); // 19/12/2021 08:09:03
            Console.WriteLine(utcDate); // 19/12/2021 11:09:03

            Console.WriteLine(utcDate.ToLocalTime()); //19/12/2021 08:09:03

            var timezoneAustralia = TimeZoneInfo.FindSystemTimeZoneById("Cen. Australia Standard Time");
            Console.WriteLine(timezoneAustralia);

            var horaAustralia = TimeZoneInfo.ConvertTimeFromUtc(utcDate, timezoneAustralia);
            Console.WriteLine(horaAustralia);

            var timezones = TimeZoneInfo.GetSystemTimeZones();
            foreach (var zones in timezones)
            {
                Console.WriteLine(zones);
                Console.WriteLine(zones.Id);
                Console.WriteLine(zones.DisplayName);
                Console.WriteLine(TimeZoneInfo.ConvertTimeFromUtc(utcDate, zones));
                Console.WriteLine("----------------------");

            }

            // E. South America Standard Time -> Brasilia
            // Central Brazilian Standard Time -> Cuiaba
            // SA Pacific Standard Time -> Rio Branco

            Console.ReadKey();

        }
    }

}