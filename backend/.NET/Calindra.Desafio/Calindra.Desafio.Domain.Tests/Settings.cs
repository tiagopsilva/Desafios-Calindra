using Microsoft.Extensions.Configuration;

namespace Calindra.Desafio.Domain.Tests
{
    public static class Settings
    {
         public static IConfiguration InitConfiguration()
         {
             var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables() 
                .Build();
            return config;
         }
    }
}
