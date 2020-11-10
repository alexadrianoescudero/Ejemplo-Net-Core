using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaseNetCore.Models
{
    public class Documento
    {
        public Documento()
        {
            Persona = new HashSet<Persona>();
        }

        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }

        public virtual ICollection<Persona> Persona { get; set; }
    }
}
