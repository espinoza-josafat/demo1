using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RestPOS
{
    public class Notification : INotifyPropertyChanged
    {
        string message;

        public string Message
        {
            get { return message; }
            set
            {
                if (message == value)
                    return;

                message = value;

                OnPropertyChanged("Message");
            }
        }

        int id;

        public int Id
        {
            get { return id; }

            set
            {
                if (id == value)
                    return;

                id = value;

                OnPropertyChanged("Id");
            }
        }

        string imageUrl;

        public string ImageUrl
        {
            get { return imageUrl; }

            set
            {
                if (imageUrl == value)
                    return;

                imageUrl = value;

                OnPropertyChanged("ImageUrl");
            }
        }

        string title;

        public string Title
        {
            get { return title; }

            set
            {
                if (title == value)
                    return;

                title = value;

                OnPropertyChanged("Title");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Notifications : ObservableCollection<Notification> { }
}