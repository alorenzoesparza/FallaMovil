using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallaMovil.Models
{
    public class Component
    {
        public int IdFallero { get; set; }

        public int TipoComponente { get; set; }

        public string Nombre { get; set; }

        public string Email { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Movil { get; set; }

        public int SexId { get; set; }

        public bool Infantil { get; set; }

        public string Foto { get; set; }

        public int IdDependent { get; set; }

        public bool TieneDependientes { get; set; }

        public bool EsDependiente { get; set; }

        public string Password { get; set; }
    }
}
