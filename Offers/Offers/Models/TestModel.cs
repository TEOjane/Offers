using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Offers.Model
{
    public class TestModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _id;
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                SetProterty(ref _id, value);
                OnPropertyChanged(nameof(ID));
            }
        }


        private string _price;
        public string Price 
        {
            get
            {
                return _price;
            }
            set
            {
                SetProterty(ref _price, value);
                OnPropertyChanged(nameof(Price));
            }
        }


        private string _url;
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                SetProterty(ref _url, value);
                OnPropertyChanged(nameof(Url));
            }
        }


        private string _currencyID;
        public string CurrencyID
        {
            get
            {
                return _currencyID;
            }
            set
            {
                SetProterty(ref _currencyID, value);
                OnPropertyChanged(nameof(CurrencyID));
            }
        }


        private int _categoryID;
        public int CategoryID
        {
            get
            {
                return _categoryID;
            }
            set
            {
                SetProterty(ref _categoryID, value);
                OnPropertyChanged(nameof(CategoryID));
            }
        }


        private string _picture;
        public string Picture
        {
            get
            {
                return _picture;
            }
            set
            {
                SetProterty(ref _picture, value);
                OnPropertyChanged(nameof(Picture));
            }
        }


       /* private bool _delivery;
        public bool Delivery
        {
            get
            {
                return _delivery;
            }
            set
            {
                SetProterty(ref _delivery, value);
                OnPropertyChanged(nameof(Delivery));
            }
        }


        private int _localDeliveryCost;
        public int LocalDeliveryCost
        {
            get
            {
                return _localDeliveryCost;
            }
            set
            {
                SetProterty(ref _localDeliveryCost, value);
                OnPropertyChanged(nameof(LocalDeliveryCost));
            }
        }*/


        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                SetProterty(ref _description, value);
                OnPropertyChanged(nameof(Description));
            }
        }



        protected bool SetProterty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value)) { return false; };

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


