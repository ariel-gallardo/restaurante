using Restaurante.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Restaurante.API
{
    public class RegisterRequestExample : IMultipleExamplesProvider<RegisterDTO>
    {
        public IEnumerable<SwaggerExample<RegisterDTO>> GetExamples()
        {
            yield return SwaggerExample.Create("Cliente", 
                new RegisterDTO 
                { 
                    Nombre = "Luis",
                    Apellido = "Suarez",
                    Calle = "Espejo",
                    Numero = 3044,
                    CodigoArea = 261,
                    Correo = "luis.suarez@restaurante.com",
                    NumeroTelefono = 2456870,
                    Password = "123456aA$",
                    RePassword = "123456aA$",
                    Localidad = "Capital"
                }
            );
        }
    }
}
