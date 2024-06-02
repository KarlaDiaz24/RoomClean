using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOS
{
    public class EvidenciaDto
    {
        public string? Comentarios { get; set; }
        public int FKTarea { get; set; }
    }
}
