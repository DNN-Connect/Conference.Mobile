using Akavache;
using Connect.Conference.Mobile.Common;
using Connect.Conference.Mobile.Data;
using Connect.Conference.Mobile.Security;
using Connect.Conference.Mobile.Views;
using System.Reactive.Linq;

using Xamarin.Forms;

namespace Connect.Conference.Mobile
{
    public class App : Application
    {
        public static Settings Settings { get; set; }
        public static AppData Data { get; set; }

        public App()
        {
            BlobCache.ApplicationName = "Connect.Conference.Mobile";
            try
            {
                Settings = BlobCache.UserAccount.GetObject<Settings>("Settings").Wait();
            }
            catch (System.Exception)
            {
            }
            Data = new AppData();
            MainPage = new NavigationPage(new Main());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static Jwt GetJwt(string host, string username)
        {
            try
            {
                return BlobCache.UserAccount.GetObject<Jwt>(string.Format("{1}@{0}", host, username)).Wait();
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        public static void SaveJwt(string host, string username, Jwt token)
        {
            BlobCache.UserAccount.InsertObject<Jwt>(string.Format("{1}@{0}", host, username), token).Wait();
        }
    }
}
