using System.ComponentModel;

namespace Connect.Conference.Mobile.ViewModels
{
    public class ConferenceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ConferenceViewModel(Models.Conference conference)
        {
            Conference = conference;
        }

        public Models.Conference Conference { get; set; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
