namespace FallaMovil.API.Models
{

    public class ActAssistanceResponse
    {
        public int IdAsistencia { get; set; }

        public int IdAct { get; set; }

        public int IdFallero { get; set; }

        public string Nombre { get; set; }

        public bool Apuntado { get; set; }

        public bool Infantil { get; set; }
    }
}