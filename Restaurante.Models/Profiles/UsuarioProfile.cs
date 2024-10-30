using AutoMapper;
using System.Security.Claims;

namespace Restaurante.Models.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            #region Register User
            CreateMap<RegisterDTO, Usuario>()
            .ForMember(y => y.Email, x => x.MapFrom(xx => xx.Correo))
            .ForMember(y => y.Password, x => x.MapFrom(xx => xx.Password))
            .ForMember(y => y.Persona, x => x.MapFrom(xx => new Persona 
                { 
                    Nombre = xx.Nombre, 
                    Apellido = xx.Apellido,
                    Domicilio = !String.IsNullOrEmpty(xx.Calle) && xx.Numero.HasValue
                    ? new Domicilio 
                    {
                        Calle = xx.Calle,
                        Numero = xx.Numero.Value,
                        Localidad = !string.IsNullOrEmpty(xx.Localidad) ? xx.Localidad : null
                    } : null,
                    Telefono = xx.CodigoArea.HasValue && xx.NumeroTelefono.HasValue
                    ? new Telefono
                    {
                        CodigoArea = xx.CodigoArea.Value,
                        Numero = xx.NumeroTelefono.Value
                    } : null
                }
            ));
            #endregion

            #region Login User
            CreateMap<LoginDTO, Usuario>()
                .ForMember(y => y.Email, x => x.MapFrom(xx => xx.Correo))
                .ForMember(y => y.Password, x => x.MapFrom(xx => xx.Password));
            #endregion

            #region Info User
            CreateMap<Usuario, UserInfoDTO>()
                .ForMember(y => y.Correo, x => x.MapFrom(xx => xx.Email))
                .ForMember(y => y.TipoDeUsuario, x => x.MapFrom(xx => xx.Rol != null ? xx.Rol.Descripcion : "-"))
                .ForMember(y => y.NombreCompleto, x => x.MapFrom(xx => $"{xx.Persona.Nombre} {xx.Persona.Apellido}"))
                .ForMember(y => y.Domicilio, x => x.MapFrom(xx => xx.Persona != null && xx.Persona.Domicilio != null ? ($"{xx.Persona.Domicilio.Calle} {xx.Persona.Domicilio.Numero}") : string.Empty))
                .ForMember(y => y.Telefono, x => x.MapFrom(xx => xx.Persona != null && xx.Persona.Telefono != null ? $"({xx.Persona.Telefono.CodigoArea}) {xx.Persona.Telefono.Numero}" : string.Empty ));
            #endregion

            #region Claims to Info
            CreateMap<IEnumerable<Claim>, UserInfoDTO>()
                .ForMember(y => y.Correo, x => x.MapFrom(xx => xx.FirstOrDefault(yy => yy.Type == "Correo").Value))
                .ForMember(y => y.TipoDeUsuario, x => x.MapFrom(xx => xx.FirstOrDefault(yy => yy.Type == "Rol").Value))
                .ForMember(y => y.NombreCompleto, x => x.MapFrom(xx => xx.FirstOrDefault(yy => yy.Type == "Nombre").Value))
                .ForMember(y => y.Telefono, x => x.MapFrom(xx => xx.FirstOrDefault(yy => yy.Type == "Telefono").Value))
                .ForMember(y => y.Domicilio, x => x.MapFrom(xx => xx.FirstOrDefault(yy => yy.Type == "Domicilio").Value))
                .ForMember(y => y.CaducaEn, x => x.MapFrom(xx => xx.FirstOrDefault(yy => yy.Type == "CaducaEn").Value));

            #endregion
        }
    }
}
