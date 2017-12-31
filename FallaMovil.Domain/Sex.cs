namespace FallaMovil.Domain
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [NotMapped]
    public class Sex
    {
        public int SexId { get; set; }

        public string Sexo { get; set; }

        // Cada sexo puede tener varios falleros
        [JsonIgnore]
        public virtual ICollection<Component> Components { get; set; }
    }
}