namespace FallaMovil.Models
{
    using FallaMovil.Services;
    using FallaMovil.ViewModels;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;

    public class ActAssistance
    {
        #region Servicios
        NavigationService navigationService;
        DialogService dialogService;
        ApiService apiService;
        #endregion

        #region Propiedades
        public int IdAsistencia { get; set; }
        public int IdFallero { get; set; }
        public string Nombre { get; set; }
        public bool Infantil { get; set; }
        public bool Apuntado { get; set; }
        public int IdAct { get; set; }
        public string TextoBoton { get; set; }
        public string ColorBoton { get; set; }

        #endregion
        #region Constructor
        public ActAssistance()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            apiService = new ApiService();

        }
        #endregion
        #region Comandos
        public ICommand SelectBotonCommand
        {
            get
            {
                return new RelayCommand(SelectBoton);
            }
        }

        private async void SelectBoton()
        {
            var color = ColorBoton;
            var textoBoton = TextoBoton;
            if (textoBoton == "Quitar")
            {
                await ActAssistanceslViewModel.GetInstance().Delete(this);
            }

            if (textoBoton == "Apuntar")
            {
                await ActAssistanceslViewModel.GetInstance().Apuntar(this);
            }
        }

        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return IdAsistencia;
        }
        #endregion
    }
}
