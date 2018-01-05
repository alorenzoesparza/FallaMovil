namespace FallaMovil.ViewModels
{
    using FallaMovil.Services;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Eventos
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Servicios
        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Atributos
        string _email;
        string _password;
        bool _isToggled;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Propiedades
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }

            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }

            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }

        public bool IsToggled
        {
            get
            {
                return _isToggled;
            }

            set
            {
                if (_isToggled != value)
                {
                    _isToggled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsToggled)));
                }
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Password)));
                }
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                if (_email != value)
                {
                    _email = value;
                    PropertyChanged?.Invoke(
                        this, 
                        new PropertyChangedEventArgs(nameof(Email)));
                }
            }
        }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            Email = "carlos@gmail.com";
            Password = "carlos@gmail.com";

            IsEnabled = true;
            IsToggled = true;
        }
        #endregion

        #region Comandos
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.VerMensaje(
                    "Error",
                    "El campo email no puede estar en blanco");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.VerMensaje(
                    "Error",
                    "El campo password no puede estar en blanco");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.VerMensaje("Error", connection.Message);
                return;
            }
            
            var mainViewModel = MainViewModel.GetInstancia();

            var response = await apiService.GetToken(
                string.Format("{0}{1}/Token", mainViewModel.BaseUrl,mainViewModel.ApiUrl),
                Email,
                Password);

            if (response == null)
            {
                IsRunning = false;
                IsEnabled = true;

                await dialogService.VerMensaje(
                    "Error",
                    "El servicio no está listo. Reintente más tarde.");
                Password = null;
                return;
            }

            if (string.IsNullOrEmpty(response.AccessToken))
            {
                IsRunning = false;
                IsEnabled = true;

                await dialogService.VerMensaje(
                    "Error",
                    response.ErrorDescription);
                Password = null;
                return;
            }

            //var mainViewModel = MainViewModel.GetInstancia();
            mainViewModel.Acts = new ActsViewModel();
            mainViewModel.Token = response;

            navigationService.SetMainPage("MasterView");
            Email = null;
            Password = null;

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }
}
