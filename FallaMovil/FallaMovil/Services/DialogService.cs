namespace FallaMovil.Services
{
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class DialogService
    {
        public async Task VerMensaje(string titulo, string mensaje)
        {
            await Application.Current.MainPage.DisplayAlert(
                titulo,
                mensaje,
                "Aceptar");
        }
        public async Task<bool> ConfirmacionMensaje(string title, string message)
        {
            return await Application.Current.MainPage.DisplayAlert(
                title,
                message,
                "Si",
                "No");
        }
    }
}
