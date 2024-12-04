using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Evidencia
    {
        [Key]
        public int Id { get; set; }
        public string? Comentarios { get; set; }
        public string CompletedDescriptions { get; set; }
        [ForeignKey("Tarea")]
        public int FKTarea { get; set; }
        public Tarea? Tarea { get; set; }
    }
}
