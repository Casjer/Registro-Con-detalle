using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tarea3.Models
{
    public class Moras
    {
        [Key]
        public int MoraId { get; set; } 
        public DateTime Fecha { get; set; }
        public float Total { get; set; }

        [ForeignKey("MoraId")]
        public virtual List<MorasDetalle> MorasDetalle { get; set; } = new List<MorasDetalle>();

        public Moras()
        {
            this.Fecha = DateTime.Now;
        }
    }
}
