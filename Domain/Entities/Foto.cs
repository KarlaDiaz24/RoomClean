using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Foto
    {
        public int Id { get; set; }
        public string? FotoUrl { get; set; }
        public int FkEvidencia { get; set; }
        public Evidencia? Evidencia { get; set; }
    }
}
