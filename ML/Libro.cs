using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Libro
    {
        public int IdLibro { get; set; }
        public string Autor { get; set; }
        public string TituloLibro { get; set; }
        public DateTime AñoPublicacion { get; set; }
        public string Imagen { get; set; }
        public Editorial Editorial { get; set; }
    }
}
