using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PrettyWeather.Model
{
    public class DataItem : INotifyPropertyChanged
    {
        string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        string theValue;
        public string Value
        {
            get => theValue;
            set
            {
                theValue = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
