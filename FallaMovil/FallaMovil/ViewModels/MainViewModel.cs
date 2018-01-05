using FallaMovil.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FallaMovil.ViewModels
{
    public class MainViewModel
    {
        #region Propiedades
        public ObservableCollection<Menu> MiMenu { get; set; }
        public LoginViewModel Login { get; set; }
        public ActsViewModel Acts { get; set; }
        public ActAssistanceslViewModel ActAssistances { get; set; }
        //public NewComponentViewModel NewComponent { get; set; }
        public TokenResponse Token { get; set; }
        public Act Act { get; set; }
        public ActAssistance ActAssistance { get; set; }
        public string BaseUrl { get; set; }
        public string ApiUrl { get; set; }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            instancia = this;

            BaseUrl = "http://antoniole.com/";
            ApiUrl = "/FallaMovilApi";
            Login = new LoginViewModel();

            LoadMenu();
        }

        #endregion

        #region Singleton
        static MainViewModel instancia;

        public static MainViewModel GetInstancia()
        {
            if (instancia == null)
            {
                return new MainViewModel();
            }

            return instancia;
        }
        #endregion

        #region Metodos

        private void LoadMenu()
        {
            MiMenu = new ObservableCollection<Menu>
            {
                new Menu
                {
                    Icono = "ic_ajustes",
                    NombrePagina = "MisAjustesView",
                    Titulo = "Mis Ajustes",
                },
                new Menu
                {
                    Icono = "ic_mapas",
                    NombrePagina = "UbicacionView",
                    Titulo = "Ubicaciones",
                },
                new Menu
                {
                    Icono = "ic_salir",
                    NombrePagina = "LoginView",
                    Titulo = "Cerrar Sesión",
                }
            };
        }
        #endregion
    }
}
