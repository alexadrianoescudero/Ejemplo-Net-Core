using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaseNetCore.ViewModel
{
    public class ViewModelPersonaGenero
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Estado { get; set; }
        public string GeneroPersona { get; set; }
        public int CodigoGenero { get; set; }
        public string DocumentoPersona { get; set; }
    }
}
