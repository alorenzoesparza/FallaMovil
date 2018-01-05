namespace FallaMovil.Services
{
    using FallaMovil.Views;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    public class NavigationService
    {
        public void SetMainPage(string pageName)
        {
            switch (pageName)
            {
                case "LoginView":
                    Application.Current.MainPage = new NavigationPage(new LoginView());
                    break;
                case "MasterView":
                    Application.Current.MainPage = new MasterView();
                    break;
            }
        }

        public async Task Navegar(string pageName)
        {
            App.Master.IsPresented = false;

            switch (pageName)
            {
                case "ActView":
                    await App.Navigator.PushAsync(new ActView());
                    break;
                case "ActAssistanceView":
                    await App.Navigator.PushAsync(new ActAssistanceView());
                    break;
            }
        }

        //public async Task NavigateOnLogin(string pageName)
        //{
        //    switch (pageName)
        //    {
        //        case "NewCustomerView":
        //            await Application.Current.MainPage.Navigation.PushAsync(
        //                new NewCustomerView());
        //            break;
        //    }
        //}

        public async Task BackOnMaster()
        {
            await App.Navigator.PopAsync();
        }

        public async Task BackOnLogin()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}

