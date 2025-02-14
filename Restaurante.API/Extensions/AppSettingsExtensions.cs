using Restaurante.Infraestructure;
using Restaurante.Services;

namespace Restaurante.API
{
    public static class AppSettingsExtensions
    {
        public static void AddAppSettingsCustomCFG(this IConfiguration cfg) 
        {
            AppSettings.ClientURL = cfg.GetValue<string>("ClientUrl");
            AppSettings.JWTSecretKey = cfg.GetValue<string>("JWT:SecretKey");
            AppSettings.JWTHourExpirationTime = cfg.GetValue<int>("JWT:HourExpirationTime");
            AppSettings.Take = cfg.GetValue<int>("Take");
        }
    }
}
