namespace FallaMovil.Models
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using System;
    using FallaMovil.Services;
    using FallaMovil.ViewModels;

    public class Menu
    {
        #region Servicios
        NavigationService navigationService;
        #endregion

        #region Atributos
        public string Icono { get; set; }

        public string Titulo { get; set; }

        public string NombrePagina { get; set; }

        #endregion

        #region Constructor
        public Menu()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Comandos
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }

        void Navigate()
        {
            switch (NombrePagina)
            {
                case "LoginView":
                    MainViewModel.GetInstancia().Login = new LoginViewModel();
                    navigationService.SetMainPage("LoginView");
                    break;
            }
        }
        #endregion
    }
}
