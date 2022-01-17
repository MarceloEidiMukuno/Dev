namespace Blog
{

    public static class Configuration
    {
        // Toke - JWT -  Json Web Token
        public static string JwtKey { get; set; } = "y*agrqkp`LJ!5Wbpxs$Wjw,#2Y633*Zb";
        public static SmtpConfigurarion Smtp = new SmtpConfigurarion();

        public class SmtpConfigurarion
        {
            public string Host { get; set; }
            public int Port { get; set; } = 25;

            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}