using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOS
{
    public class InventarioDto
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int Cantidad { get; set; }
    }

}
