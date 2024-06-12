using System;
using System.Linq;
using System.Security.Claims;
using RoomClean.Context;

namespace Domain.Entities
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public static dynamic Validartoken(ClaimsIdentity identity, ApplicationDBContext context)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Verifica tu token",
                        result = ""
                    };
                }

                var idClaim = identity.Claims.FirstOrDefault(x => x.Type == "id");

                if (idClaim == null)
                {
                    return new
                    {
                        success = false,
                        message = "Token inválido, no contiene un id",
                        result = ""
                    };
                }

                var id = idClaim.Value;
                Usuario usuario = context.Usuarios.FirstOrDefault(x => x.Id.ToString() == id);
                if (usuario == null)
                {
                    return new
                    {
                        success = false,
                        message = "Usuario no encontrado",
                        result = ""
                    };
                }

                return new
                {
                    success = true,
                    message = "Exito",
                    result = usuario
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Error de captura: " + ex.Message,
                    result = ""
                };
            }
        }
    }
}
