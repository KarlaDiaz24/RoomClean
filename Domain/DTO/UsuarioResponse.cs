using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Número { get; set; }
        public string? Correo { get; set; }
        public string? Contraseña { get; set; }
        public string? Foto { get; set; }
        public int FKRol { get; set; }

    }
}
