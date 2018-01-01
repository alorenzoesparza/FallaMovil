namespace FallaMovil.ViewModels
{
    using FallaMovil.Models;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;

    public class ActsViewModel : INotifyPropertyChanged
    {
        #region Atributos
        public ObservableCollection<Act> _acts;
        bool _isRefreshing;
        #endregion

        #region Eventos
        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion

        #region Servicios
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Propiedades
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRefreshing)));
                }
            }
        }
        public ObservableCollection<Act> Acts
        {
            get
            {
                return _acts;
            }
            set
            {
                if (_acts != value)
                {
                    _acts = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Acts)));
                }
            }
        }
        #endregion

        #region Constructores
        public ActsViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();

            LoadActos();
        }
        #endregion

        #region Comandos
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadActos);
            }
        }
        #endregion

        #region Metodos
        async void LoadActos()
        {
            IsRefreshing = true;

            var conexion = await apiService.CheckConnection();
            if (!conexion.IsSuccess)
            {
                await dialogService.VerMensaje(
                    "Error",
                    conexion.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstancia(); // Acceder al Singleton

            var response = await apiService.GetList<Act>(
                string.Format("{0}", mainViewModel.BaseUrl),
                string.Format("{0}/api", mainViewModel.ApiUrl),
                "/Acts",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.VerMensaje(
                    "Error",
                    conexion.Message);
                return;
            }

            var acts = (List<Act>)response.Result;
            Acts = new ObservableCollection<Act>(
                acts.OrderByDescending(a => a.FechaActo));

            IsRefreshing = false;
        }
        #endregion
    }
}
