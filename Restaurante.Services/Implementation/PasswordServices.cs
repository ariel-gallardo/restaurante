using Microsoft.IdentityModel.Tokens;
using Restaurante.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurante.Services
{
    public class PasswordServices : IPasswordServices
    {
        public string Encrypt(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

        public (string,string) GenerateToken(Usuario usuario)
        {
            var symmetricKey = Encoding.UTF8.GetBytes(AppSettings.JWTSecretKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var expirationTime = DateTime.UtcNow.AddHours(AppSettings.JWTHourExpirationTime).ToString("dd-MM-yyyy HH:mm:ss");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Correo", usuario.Email),
                new Claim("Nombre", $"{usuario.Persona.Nombre} {usuario.Persona.Apellido}"),
                new Claim("CaducaEn", $"{expirationTime}"),
                new Claim("Rol", usuario.Rol.Descripcion),
                new Claim("Domicilio",$"{usuario.Persona.Domicilio.Calle}, {usuario.Persona.Domicilio.Numero} - {usuario.Persona.Domicilio.Localidad}"),
                new Claim("Telefono",$"({usuario.Persona.Telefono.CodigoArea}) {usuario.Persona.Telefono.Numero}")
            }),
                Expires = DateTime.UtcNow.AddHours(AppSettings.JWTHourExpirationTime), 
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(symmetricKey),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(token), expirationTime);
        }

        public bool Ok(string password, string hashPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashPassword);
    }
}
