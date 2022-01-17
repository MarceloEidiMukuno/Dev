using System.Text;
using System.Text.Json.Serialization;
using Blog;
using Blog.Data;
using Blog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

ConfigureMVC(builder);

ConfigureAuthentication(builder);

ConfigureServices(builder);

var app = builder.Build();

LoadConfiguration(app);

app.UseAuthentication(); // Adicionando ou informando que utilizaremos Autenticação na APP
app.UseAuthorization(); // Adicionando ou informando que utilizaremos Autorização na APP
app.MapControllers(); // Mapeando as classes Controlers
app.UseResponseCompression();// Adicionando ou informando que utilizaremos compressão no retorno da API
app.UseStaticFiles(); // Adicionando ou informando que utilizaremos arquivos estaticos

app.Run();

void LoadConfiguration(WebApplication app)
{
    Configuration.JwtKey = app.Configuration.GetValue<string>("JwtKey");
    app.Configuration.GetSection("Smtp").Bind(Configuration.Smtp);

}

void ConfigureAuthentication(WebApplicationBuilder builder)
{
    var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
    builder.Services.AddAuthentication(x =>
        {
            //Schema de Autenticação
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            // Tipo de autenticação -> JwtBearer
            x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true, // Valida a chave de assinatura?
                IssuerSigningKey = new SymmetricSecurityKey(key), // Como validar a chave de assinatura
                ValidateAudience = false,
                ValidateIssuer = false
            };
        });
}

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers(); // Adicionando ou informando que utilizaremos o serviço de Controllers(MVC)
    builder.Services.AddDbContext<BlogDataContext>(); // Adicionando ou informando que utilizaremos o serviço de conexão com o BD
    builder.Services.AddTransient<TokenService>();
    // builder.Services.AddTransient(); // Sempre cria um novo
    // builder.Services.AddScoped(); // Cria por transação
    // builder.Services.AddSingleton(); // 1 por aplicação

    builder.Services.AddTransient<EmailService>();
}

void ConfigureMVC(WebApplicationBuilder builder)
{
    builder.Services.AddMemoryCache(); // Habilita o serviço da cache

    builder.Services.AddResponseCompression(options => // Habilita o serviço de compressão no retorno da API
    {
        options.Providers.Add<GzipCompressionProvider>();
    });

    builder.Services.Configure<GzipCompressionProviderOptions>(options => // Configura a compressão
    {
        options.Level = System.IO.Compression.CompressionLevel.Optimal;
    });

    builder
    .Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Ignora Referencia circular
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault; // Não renderiza o campo caso seja nulo
    });
}