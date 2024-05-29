using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Tarea
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Estatus { get; set; }
        public int FkAdmin { get; set; }
        public int FkCamarista { get; set; }
        public Usuario? Admin { get; set; }
        public Usuario? Camarista { get; set; }
    }
}
