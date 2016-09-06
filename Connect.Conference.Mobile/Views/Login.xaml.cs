using Connect.Conference.Mobile.Common;
using Connect.Conference.Mobile.Controllers;
using Connect.Conference.Mobile.Security;
using System;
using Xamarin.Forms;

namespace Connect.Conference.Mobile.Views
{
    public partial class Login : ContentPage
    {
        public AppLink Site { get; set; }
        private Main MainPage { get; set; }

        public Login(AppLink site, Main mainPage)
        {
            InitializeComponent();
            Site = site;
            MainPage = mainPage;
            txtUsername.Text = site.Username;
            btnCancel.Clicked += btnCancelClick;
            btnLogin.Clicked += btnLoginClick;
            lblSite.Text = site.Host;
        }

        async void btnLoginClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(this.txtPassword.Text))
            {
                var token = await Authorization.GetTokenAsync(Site.Host, Site.Username, this.txtPassword.Text);
                if (token != null)
                {
                    App.SaveJwt(Site.Host, Site.Username, token);
                    var conference = await ConferenceController.LoadConference(Site, token);
                    MainPage.RefreshConferences();
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Login", "Your login failed. Please try again.", "OK");
                }
            }
        }

        async void btnCancelClick(object sender, EventArgs args)
        {
            await Navigation.PopAsync();
        }
    }
}
