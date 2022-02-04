using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ServiceDataProcess
{
    public class ServiceMessages : INotifyPropertyChanged
    {
        private String _processMessage;
        public String ProcessMessage
        {
            set
            {
                _processMessage = value;
                NotifyPropertyChanged();
            }
            get
            {
                return _processMessage;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
