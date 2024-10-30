using Restaurante.Services;

namespace Restaurante.API
{
    public static class AppSettingsExtensions
    {
        public static void AddAppSettingsCustomCFG(this IConfiguration cfg) 
        {
            AppSettings.JWTSecretKey = cfg.GetValue<string>("JWT:SecretKey");
            AppSettings.JWTHourExpirationTime = cfg.GetValue<int>("JWT:HourExpirationTime");
        }
    }
}
