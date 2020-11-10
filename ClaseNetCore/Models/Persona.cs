using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaseNetCore.Models
{
    public class Persona
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Estado { get; set; }
        public int CodigoGenero { get; set; }
        public int CodigoDocumento { get; set; }

        public virtual Documento CodigoDocumentoNavigation { get; set; }
        public virtual Genero CodigoGeneroNavigation { get; set; }
    }
}
