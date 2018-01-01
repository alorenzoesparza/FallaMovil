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
        public ActsViewModel Acts { get; set; }
        public ActAssistanceslViewModel ActAssistances { get; set; }
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
