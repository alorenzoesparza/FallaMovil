namespace FallaMovil.Models
{
    using FallaMovil.ViewModels;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    public class Act
    {
        #region Propiedades
        public int IdAct { get; set; }
        public string Titular { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaActo { get; set; }
        public string HoraActo { get; set; }
        public string Precio { get; set; }
        public string PrecioInfantiles { get; set; }
        public bool ActoOficial { get; set; }
        public string Imagen { get; set; }
        public string ImagenFullPath
        {
            get
            {
                return string.Format(
                    "http://antoniole.com/falla/{0}", 
                    Imagen500.Substring(1));
            }
        }
        public string Imagen500 { get; set; }
        public bool PagInicio { get; set; }
        public bool YaEfectuado { get; set; }
        public List<ActAssistance> ActAssistances { get; set; }
        //public object ActSupporters { get; set; } 
        #endregion

        #region Comandos
        public ICommand SelectActCommand
        {
            get
            {
                return new RelayCommand(SelectAct);
            }
        }

        private async void SelectAct()
        {
            MainViewModel.GetInstancia().ActAssistances = new ActAssistanceslViewModel(this);

            //mainViewModel.Acts = new ActListViewModel();

            await navigationService.Navegar("ActAssistanceView");
        }
        #endregion

        #region Constructor
        public Act()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Servicios
        NavigationService navigationService;
        #endregion
    }
}
