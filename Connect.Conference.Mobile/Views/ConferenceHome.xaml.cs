using System.Collections.Generic;
using Xamarin.Forms;

namespace Connect.Conference.Mobile.Views
{
    public partial class ConferenceHome : ContentPage
    {
        private class Button
        {
            public string Title { get; set; }
            public string Detail { get; set; }
            public string Image { get; set; }
        }

        public ConferenceHome()
        {
            InitializeComponent();
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            var btns = new List<Button>();
            btns.Add(new Button
            {
                Title = "Schedule",
                Detail = "Show the schedule",
                Image = "calendar.png"
            });
            btns.Add(new Button
            {
                Title = "My Sessions",
                Detail = "Show sessions I attended",
                Image = "graduation-cap.png"
            });
            lstButtons.ItemsSource = btns;
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;

        void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var btn = ((ListView)sender).SelectedItem as Button;
            if (btn == null)
                return;
        }
    }
}
