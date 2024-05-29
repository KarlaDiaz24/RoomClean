using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Evidencia
    {
        public int Id { get; set; }
        public string? Comentarios { get; set; }
        public int FkTarea { get; set; }
        public Tarea? Tarea { get; set; }
    }
}
