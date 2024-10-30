using Microsoft.EntityFrameworkCore;
using Restaurante.Models;
using System.Collections.Generic;

namespace Restaurante.Migrations.Extensions
{
    public static class RestauranteContextDemoExtensions
    {

        /// <summary>
        /// Password = 123456aA$
        /// </summary>
        private static string Password = "$2a$11$Md2.ACBRv9JENhYZo/OsdOPem7J1VyKhjCQK4/xjthVy7Hb/kxd0i";
        public static ModelBuilder GenerateDemoRoles(this ModelBuilder mB)
        {
            mB.Entity<Rol>().HasData(
                new Rol[]
                {
                    new Rol { Id = 1, Descripcion = "Cliente" },
                    new Rol { Id = 2, Descripcion = "Delivery" },
                    new Rol { Id = 3, Descripcion = "Recepcionista" },
                    new Rol { Id = 4, Descripcion = "Cocinero" },
                    new Rol { Id = 5, Descripcion = "Administrador" }
                }
            );
            return mB;
        }

        public static ModelBuilder GenerateDemoDomicilios(this ModelBuilder mB)
        {
            var list = new List<Domicilio>()
            {
                new Domicilio
                {
                    Id = 1,
                    Calle = "Espejo",
                    Numero = 450,
                    Localidad = "Capital"
                },
                new Domicilio
                {
                    Id = 2,
                    Calle = "Patricias Mendocinas",
                    Numero = 890,
                    Localidad = "Capital"
                },
                new Domicilio
                {
                    Id = 3,
                    Calle = "España",
                    Numero = 305,
                    Localidad = "Capital"
                },
                new Domicilio
                {
                    Id = 4,
                    Calle = "Godoy Cruz",
                    Numero = 210,
                    Localidad = "Capital"
                },
                new Domicilio
                {
                    Id = 5,
                    Calle = "Avenida San Martín",
                    Numero = 1200,
                    Localidad = "Capital"
                }
            };
            mB.Entity<Domicilio>().HasData(list);
            return mB;
        }

        public static ModelBuilder GenerateDemoTelefonos(this ModelBuilder mB)
        {
            var list = new List<Telefono>()
            {
                new Telefono
                {
                    Id = 1,
                    CodigoArea = 261,
                    Numero = 1235670
                },
                new Telefono
                {
                    Id = 2,
                    CodigoArea = 261,
                    Numero = 9875420
                },
                new Telefono
                {
                    Id = 3,
                    CodigoArea = 261,
                    Numero = 6548320
                },
                new Telefono
                {
                    Id = 4,
                    CodigoArea = 261,
                    Numero = 9852010
                },
                new Telefono
                {
                    Id = 5,
                    CodigoArea = 261,
                    Numero = 7116429
                }
            };
            mB.Entity<Telefono>().HasData(list);
            return mB;
        }

        public static ModelBuilder GenerateDemoPersonas(this ModelBuilder mB)
        {
            var list = new List<Persona>()
            {
                new Persona
                    {
                        Id = 1,
                        Nombre = "Lucía",
                        Apellido = "Fernández",
                        DomicilioId = 1,
                        TelefonoId = 1
                    },
                new Persona
                    {
                        Id = 2,
                        Nombre = "Mateo",
                        Apellido = "González",
                        DomicilioId = 2,
                        TelefonoId = 2
                    },
                new Persona
                    {
                        Id = 3,
                        Nombre = "Sofía",
                        Apellido = "López",
                        DomicilioId = 3,
                        TelefonoId = 3
                    },
                new Persona
                     {
                         Id=4,
                         Nombre = "Tomás",
                         Apellido = "Rodríguez",
                         DomicilioId = 4,
                         TelefonoId = 4
                     },
                new Persona
                    {
                        Id=5,
                        Nombre = "Ariel",
                        Apellido = "Gallardo",
                        DomicilioId = 5,
                        TelefonoId = 5
                    }
            };
            mB.Entity<Persona>().HasData(list);
            return mB;
        }

        public static ModelBuilder GenerateDemoUsers(this ModelBuilder mB)
        {
            mB.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Email = "cliente@restaurante.com",
                    Password = Password,
                    PersonaId = 1,
                    RolId = 1
                }
            );

            mB.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 2,
                    Email = "delivery@restaurante.com",
                    Password = Password,
                    PersonaId = 2,
                    RolId = 2
                }
            );

            mB.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 3,
                    Email = "recepcionista@restaurante.com",
                    Password = Password,
                    PersonaId = 3,
                    RolId = 3
                }
            );

            mB.Entity<Usuario>().HasData(
                 new Usuario
                 {
                     Id = 4,
                     Email = "cocinero@restaurante.com",
                     Password = Password,
                     PersonaId = 4,
                     RolId = 4
                 }
             );

            mB.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 5,
                    Email = "administrador@restaurante.com",
                    Password = Password,
                    PersonaId = 5,
                    RolId = 5
                }
            );
            return mB;
        }


    }
}
