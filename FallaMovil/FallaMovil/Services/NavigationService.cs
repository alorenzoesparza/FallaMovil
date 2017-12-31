namespace FallaMovil.Services
{
    using System.Threading.Tasks;
    using Xamarin.Forms;
    public class NavigationService
    {
        public async Task Navegar(string pageName)
        {
            switch (pageName)
            {
                case "ActListView":
                    //await Application.Current.MainPage.Navigation.PushAsync(new ActListView());
                    break;
                case "ActDetailView":
                    //await Application.Current.MainPage.Navigation.PushAsync(new ActDetailView());
                    break;
            }
        }

        public async Task Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}

