using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOS
{
    public class TareaDto
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Estatus { get; set; }
        public int FkUsuario { get; set; }
    }
}
