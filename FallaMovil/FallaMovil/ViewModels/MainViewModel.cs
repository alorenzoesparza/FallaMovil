using FallaMovil.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FallaMovil.ViewModels
{
    public class MainViewModel
    {
        #region Propiedades
        public LoginViewModel Login { get; set; }
        public TokenResponse Token { get; set; }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            instancia = this;
            Login = new LoginViewModel();
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
    }
}
