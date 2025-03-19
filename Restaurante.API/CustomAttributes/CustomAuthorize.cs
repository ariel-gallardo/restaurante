using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Restaurante.Const;
using Restaurante.Models;
using System;
using System.Security.Claims;

namespace Restaurante.API
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;
        private static string allRoles =
            $"{Roles.Administrador},{Roles.Cliente},{Roles.Delivery},{Roles.Recepcionista},{Roles.Cocinero}";

        public CustomAuthorizeAttribute(string roles = "")
        {
            if (string.IsNullOrEmpty(roles)) _roles = allRoles.Split(",");
            else _roles = roles.Split(",");
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new ObjectResult(new ResultResponse
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "UNAUTHORIZED"
                })
                { StatusCode = StatusCodes.Status401Unauthorized};
                return;
            }

            bool hasRole = false;
            var rol = user.Claims.FirstOrDefault(x => x.Type == "Rol");

            if(rol != null && !string.IsNullOrEmpty(rol.Value) && rol.Value != "Ninguno")
            foreach (var role in _roles)
            {
                    if (_roles.Contains(rol.Value) || Roles.Administrador == rol.Value)
                    {
                        hasRole = true;
                        break;
                    }
                }

            if (!hasRole)
            {
                context.Result = new ObjectResult(new ResultResponse
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = $@"ROLES_REQUIRED ""{string.Join(",", _roles)}"""
                })
                { StatusCode = StatusCodes.Status403Forbidden};
            }
        }
    }

}
