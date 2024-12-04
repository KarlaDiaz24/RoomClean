using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOS
{
    public class FotoDto
    {
        public string? FotoUrl { get; set; }
        public int FkEvidencia { get; set; }
    }
}
