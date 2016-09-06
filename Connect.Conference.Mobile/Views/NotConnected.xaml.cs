using System;
using Xamarin.Forms;

namespace Connect.Conference.Mobile.Views
{
    public partial class NotConnected : ContentPage
    {
        public NotConnected()
        {
            InitializeComponent();
            btnCancel.Clicked += Cancel;
        }

        void Cancel(object sender, EventArgs args)
        {

        }

        
    }
}
