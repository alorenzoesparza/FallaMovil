namespace FallaMovil.API.Models
{
    using Domain;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ComponentResponse
    {
        public int IdFallero { get; set; }

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

        //Relaciones
        [JsonIgnore]
        public virtual List<ActAssistance> ActAssistances { get; set; }
    }
}