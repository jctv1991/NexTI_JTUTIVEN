using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPCore.Entidades
{
    public class Evento
    {
        public int IdEvento { get; set; } = -1;
        public DateTime FechaEvento { get; set; } = DateTime.MinValue;
        public string LugarEvento { get; set; } = string.Empty;
        public string DescripcionEvento { get; set; } = string.Empty;
        public decimal Precio { get; set; } = 0;
        public bool EsEliminado { get; set; } = false;
        public DateTime FechaCreacion { get; set; } = DateTime.MinValue;
    }
}
