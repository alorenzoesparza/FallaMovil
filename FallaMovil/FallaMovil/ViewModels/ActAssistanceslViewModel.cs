namespace FallaMovil.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using Services;
    using System;
    using FallaMovil.Models;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    public class ActAssistanceslViewModel : INotifyPropertyChanged
    {

        #region Eventos
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Servicios
        DialogService dialogService;
        ApiService apiservice;
        NavigationService navigationService;
        #endregion

        #region Atributos
        bool _isRunning;
        bool _isEnabled;
        Act _act;
        public ObservableCollection<ActAssistance> _actAssistance;
        public List<ActAssistance> actAssistances;
        #endregion

        #region Propiedades
        public int IdAct { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaActo { get; set; }
        public string HoraActo { get; set; }
        public string Precio { get; set; }
        public string PrecioInfantiles { get; set; }
        public string Nombre { get; set; }
        public bool Apuntado { get; set; }
        public string TextoBoton { get; set; }
        public string ColorBoton { get; set; }

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
        public Act Act
        {
            get
            {
                return _act;
            }
            set
            {
                if (_act != value)
                {
                    _act = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Act)));
                }
            }
        }

        public ObservableCollection<ActAssistance> ActAssitances
        {
            get
            {
                return _actAssistance;
            }
            set
            {
                if (_actAssistance != value)
                {
                    _actAssistance = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ActAssitances)));
                }
            }
        }

        #endregion

        #region Constructores
        public ActAssistanceslViewModel(Act act)
        {
            instance = this;

            dialogService = new DialogService();
            apiservice = new ApiService();
            navigationService = new NavigationService();

            Act = act;

            actAssistances = act.ActAssistances;

            Descripcion = act.Descripcion;
            FechaActo = act.FechaActo;
            HoraActo = act.HoraActo;
            Precio = act.Precio;
            PrecioInfantiles = act.PrecioInfantiles;

            ActAssitances = new ObservableCollection<ActAssistance>(act.ActAssistances);

            ModificarBoton(actAssistances);

            IsEnabled = true;
            IsRunning = false;
        }
        #endregion

        #region Sigleton
        static ActAssistanceslViewModel instance;

        public static ActAssistanceslViewModel GetInstance()
        {
            return instance;
        }
        #endregion

        #region Metodos
        void ModificarBoton(List<ActAssistance> actAssistances)
        {
            foreach (var listAsistencia in actAssistances)
            {
                if (listAsistencia.Apuntado)
                {
                    listAsistencia.TextoBoton = "Quitar";
                    listAsistencia.ColorBoton = "#A6212C";
                }
                else{
                    listAsistencia.TextoBoton = "Apuntar";
                    // Otro verde #3D5C3A
                    listAsistencia.ColorBoton = "#638C3C";
                }
            }
        }

        public async Task Apuntar(ActAssistance actAssistance)
        {
            IsRunning = true;
            IsEnabled = false;

            var connection = await apiservice.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.VerMensaje("Error", connection.Message);
                return;
            }
            var addAcAssistance = new ActAssistance
            {
                IdAct = actAssistance.IdAct,
                IdFallero = actAssistance.IdFallero,
            };

            var mainViewModel = MainViewModel.GetInstancia();

            var response = await apiservice.Post(
                string.Format("{0}", mainViewModel.BaseUrl),
                string.Format("{0}/api", mainViewModel.ApiUrl),
                "/ActAssistances",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                addAcAssistance);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.VerMensaje(
                    "Error",
                    response.Message);
                return;
            }

            var resultActAssistance = (ActAssistance)response.Result;

            actAssistance.IdAsistencia = resultActAssistance.IdAsistencia;
            actAssistance.Apuntado = true;

            ModificarBoton(actAssistances);
            ActAssitances = new ObservableCollection<ActAssistance>(actAssistances);

            IsRunning = false;
            IsEnabled = true;
        }

        public async Task Delete(ActAssistance actAssistance)
        {
            IsRunning = true;
            IsEnabled = false;

            var connection = await apiservice.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.VerMensaje("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstancia();

            var response = await apiservice.Delete(
                string.Format("{0}", mainViewModel.BaseUrl),
                string.Format("{0}/api", mainViewModel.ApiUrl),
                "/ActAssistances",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                actAssistance);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;

                await dialogService.VerMensaje(
                    "Error",
                    response.Message);
                return;
            }

            actAssistance.IdAsistencia = 0;
            actAssistance.Apuntado = false;

            ModificarBoton(actAssistances);
            ActAssitances = new ObservableCollection<ActAssistance>(actAssistances);

            IsRunning = false;
            IsEnabled = true;
        }

        public async void DelAsistencia()
        {
            await navigationService.Navegar("MasterView");
        }
        #endregion
    }
}
