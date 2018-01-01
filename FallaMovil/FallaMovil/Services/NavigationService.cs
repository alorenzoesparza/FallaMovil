namespace FallaMovil.Services
{
    using FallaMovil.Views;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    public class NavigationService
    {
        public async Task Navegar(string pageName)
        {
            switch (pageName)
            {
                case "ActView":
                    await Application.Current.MainPage.Navigation.PushAsync(new ActView());
                    break;
                case "ActAssistanceView":
                    await Application.Current.MainPage.Navigation.PushAsync(new ActAssistanceView());
                    break;
            }
        }

        public async Task Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}

