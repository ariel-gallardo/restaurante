using Restaurante.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurante.API
{
    public class LoginRequestExample : IMultipleExamplesProvider<LoginDTO>
    {
        public IEnumerable<SwaggerExample<LoginDTO>> GetExamples()
        {
            yield return SwaggerExample.Create("Cliente", new LoginDTO { Correo = "cliente@restaurante.com", Password = "123456aA$" });
            yield return SwaggerExample.Create("Delivery", new LoginDTO { Correo = "delivery@restaurante.com", Password = "123456aA$" });
            yield return SwaggerExample.Create("Recepcionista", new LoginDTO {Correo = "recepcionista@restaurante.com", Password = "123456aA$" });
            yield return SwaggerExample.Create("Cocinero", new LoginDTO { Correo = "cocinero@restaurante.com", Password = "123456aA$" });
            yield return SwaggerExample.Create("Administrador", new LoginDTO { Correo = "administrador@restaurante.com", Password = "123456aA$" });
            yield return SwaggerExample.Create("Cliente Luis", new LoginDTO { Correo = "luis.suarez@restaurante.com", Password = "123456aA$" });
        }
    }
}
