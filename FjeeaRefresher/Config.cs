﻿using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using System.ComponentModel;

namespace FjeeaRefresher
{
    [DataContract]
    public class ConfigField : INotifyPropertyChanged
    {
        private string username = "";
        private string password = "";
        private int interval = 60;
        
        [DataMember]
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (username == value) return;
                username = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Username"));
            }
        }

        [DataMember]
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password == value) return;
                password = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Password"));
            }
        }


        [DataMember]
        public int Interval
        {
            get
            {
                return interval;
            }
            set
            {
                if (interval == value) return;
                interval = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Interval"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }

    public class Config
    {
        public static ConfigField Data = new ConfigField();
        public static bool Load()
        {
            using (FileStream Fs = File.Open($"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}/Config.json", FileMode.Open))
            {
                Data = (ConfigField)new DataContractJsonSerializer(typeof(ConfigField)).ReadObject(Fs);
            }
            return true;
        }

        public static bool Save()
        {
            using (FileStream Fs = File.Open($"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}/Config.json", FileMode.Create, FileAccess.Write))
            {
                new DataContractJsonSerializer(typeof(ConfigField)).WriteObject(Fs, Data);
            }
            return true;
        }
    }
}