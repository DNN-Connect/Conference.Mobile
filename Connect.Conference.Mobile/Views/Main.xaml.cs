using Connect.Conference.Mobile.Common;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace Connect.Conference.Mobile.Views
{
    public partial class Main : ContentPage
    {
        public Main()
        {
            InitializeComponent();
            Conferences = new ObservableCollection<Models.Conference>(App.Data.Conferences);
            listView.ItemsSource = Conferences;
            btnAdd.Clicked += AddConference;
            listView.ItemSelected += ShowConference;
        }

        public ObservableCollection<Models.Conference> Conferences { get; set; }

        async void AddConference(object sender, EventArgs args)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var scanPage = new ZXingScannerPage
                {
                    DefaultOverlayShowFlashButton = false,
                    DefaultOverlayBottomText = "Scan for conference"
                };
                scanPage.OnScanResult += (result) =>
                {
                    scanPage.IsScanning = false;

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PopAsync();
                        try
                        {
                            var site = JsonConvert.DeserializeObject<AppLink>(result.Text);
                            var li = new Login(site, this);
                            await Navigation.PushAsync(li);
                        }
                        catch (Exception)
                        {
                            await DisplayAlert("Error", "Could not find a valid conference link", "OK");
                        }
                    });
                };

                //await Navigation.PushAsync(scanPage);

                // Mocking code
                var mockSite = JsonConvert.DeserializeObject<AppLink>("{\"Host\":\"www.dnnconnect.dev\",\"Scheme\":\"http\",\"TabId\":234,\"ModuleId\":736,\"Username\":\"peter@donker.name\",\"ConferenceId\":2}");
                var mockLi = new Login(mockSite, this);
                await Navigation.PushAsync(mockLi);

            }
            else
            {
                await DisplayAlert("Connection", "You are not connected to the Internet", "Cancel");
            }
        }

        public void RefreshConferences()
        {
            Conferences = new ObservableCollection<Models.Conference>(App.Data.Conferences);
            listView.ItemsSource = Conferences;
        }

        public async void ShowConference(object sender, SelectedItemChangedEventArgs e)
        {
            var itm = (Models.Conference)e.SelectedItem;
            var cpage = new NavigationPage(new ConferenceHome());
            cpage.BindingContext = new ViewModels.ConferenceViewModel(itm);
            await Navigation.PushAsync(cpage);
        }

    }
}
