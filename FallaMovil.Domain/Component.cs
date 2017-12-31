namespace FallaMovil.Domain
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class Component
    {
        [Key]
        [Display(Name = "Componente Comisión")]
        public int IdFallero { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(256, ErrorMessage = "El campo{0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(256, ErrorMessage = "El campo{0} debe tener como máximo {1} caracteres")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(256, ErrorMessage = "El campo{0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        //[Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(20, ErrorMessage = "El campo{0} debe tener como máximo {1} caracteres")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [MaxLength(20, ErrorMessage = "El campo{0} debe tener como máximo {1} caracteres")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Móvil")]
        public string Movil { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Sexo")]
        public int SexId { get; set; }

        [Display(Name = "¿Es infantil?")]
        public bool Infantil { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Foto { get; set; }

        //[NotMapped]
        //[Display(Name = "Foto")]
        //public HttpPostedFileBase FotoFile { get; set; }

        public bool Baja { get; set; }

        public int IdDependent { get; set; }

        public bool TieneDependientes { get; set; }

        public bool EsDependiente { get; set; }
        //Relaciones
        //Varias componentes en un sexo
        [JsonIgnore]
        public virtual Sex Sex { get; set; }
    }
}
