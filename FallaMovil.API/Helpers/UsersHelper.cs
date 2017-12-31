namespace FallaMovil.API.Helpers
{
    using Models;
    using Domain;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class UsersHelper
    {
        private static ApplicationDbContext userContext = new ApplicationDbContext();
        private static DataContext db = new DataContext();

        public static string GetRoles(string email)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

            var userASP = userManager.FindByName(email);

            if (userASP == null)
            {
                return string.Empty;
            }

            // var Roles = userManager.GetRoles(userASP.Id).ToString();

            foreach (var item in userManager.GetRoles(userASP.Id))
            {
                return item;
            }

            return string.Empty;
        }
        public void Dispose()
        {
            userContext.Dispose();
            db.Dispose();
        }
    }
}