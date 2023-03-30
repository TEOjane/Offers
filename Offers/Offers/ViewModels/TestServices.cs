using Offers.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using static System.Net.Mime.MediaTypeNames;

namespace Offers.ViewModels
{
    public class TestServices : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private TestModel _model;
        public TestModel Model
        {
            get
            {
                return _model;
            }
            set
            {
                SetProterty(ref _model, value);
                OnPropertyChanged(nameof(Model));
            }
        }

        public ICommand GetOffersCollection { get; set; }

        private string _selectedID;
        public string SelectedID
        {
            get
            {
                return _selectedID;
            }
            set
            {
                SetProterty(ref _selectedID, value);
                OnPropertyChanged(nameof(SelectedID));
            }
        }




        private ObservableCollection<TestModel> _offersList;
        public ObservableCollection<TestModel> OffersList
        {
            get
            {
                return _offersList;
            }
            set
            {
                SetProterty(ref _offersList, value);
                OnPropertyChanged(nameof(OffersList));
            }
        }

        public TestServices()
        {
            _offersList = new ObservableCollection<TestModel>();
            GetOffersCollection = new Command(async() => {
                Debug.WriteLine("Command started");
                await DoGetOffers();
            }) ;

        

        }

        private async Task DoGetOffers()
        {
        
            var offsTemp = await GetOffer();
            //OffersList = offsTemp;
            foreach (var offTemp in offsTemp)
                OffersList.Add(offTemp);

                

                Debug.WriteLine("OffersList loaded" + OffersList.Count);
            foreach (var offTemp in OffersList)
                Debug.WriteLine($"!!!!!ID = {offTemp.ID} Price = {offTemp.Price}!!!!!");
            return;
        }

        public async Task<ObservableCollection<TestModel>> GetOffer()
        {
            string url = "http://partner.market.yandex.ru/pages/help/YML.xml";

            HttpClient client = new HttpClient();
            var responseByte = await client.GetByteArrayAsync(url);


            Encoding win1251 = Encoding.GetEncoding(1251);
            Encoding unicode = Encoding.Unicode;
            byte[] unicodeBytes = Encoding.Convert(win1251, unicode, responseByte);

            // Convert the new byte[] into a char[] and then into a string.
            char[] unicodeChars = new char[unicode.GetCharCount(unicodeBytes, 0, unicodeBytes.Length)];
            unicode.GetChars(unicodeBytes, 0, unicodeBytes.Length, unicodeChars, 0);

            string unicodeString = new string(unicodeChars);
            string response = unicodeString;

            Debug.WriteLine($"Response {response}");

            var offsTemp = new ObservableCollection<TestModel>();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(response);

            XmlNodeList offers = xmlDocument.SelectNodes("//shop/offers/offer");
            XmlNodeList offersID = xmlDocument.SelectNodes("//shop/offers/offer/@id");
            XmlNodeList offersDescription = xmlDocument.SelectNodes("//shop/offers/offer/description");


           // Encoding ascii = Encoding.ASCII;
           // Encoding unicode = Encoding.Unicode;


            for (int i = 0; i < offersID.Count; i++)
                offsTemp.Add(new TestModel
                {
                    ID = offersID[i].Value,
                    Url = offers[i].ChildNodes[0].InnerText,
                    Price = offers[i].ChildNodes[1].InnerText,
                    CurrencyID = offers[i].ChildNodes[2].InnerText,
                    CategoryID = Convert.ToInt32(offers[i].ChildNodes[3].InnerText),
                    Picture = offers[i].ChildNodes[4].InnerText,
                    Description = offersDescription[i].InnerText,
                }); 

            return offsTemp;
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

        /*public string ToUnicode(string input)
        {
            // string unicodeString = "This string contains the unicode character Pi (\u03a0)";

            //var code = input.GetTypeCode;
            // Create two different encodings.
            Encoding ascii = Encoding.ASCII;
            Encoding unicode = Encoding.Unicode;

            // Convert the string into a byte array.
            byte[] asciiBytes = ascii.GetBytes(input);

            // Perform the conversion from one encoding to the other.
            byte[] unicodeBytes = Encoding.Convert(ascii, unicode, asciiBytes);

            // Convert the new byte[] into a char[] and then into a string.
            char[] unicodeChars = new char[unicode.GetCharCount(unicodeBytes, 0, unicodeBytes.Length)];
            unicode.GetChars(unicodeBytes, 0, unicodeBytes.Length, unicodeChars, 0);

            string unicodeString = new string(unicodeChars);

            // Display the strings created before and after the conversion.
            Debug.WriteLine("DESCRIPTION : {0}", unicodeString);
            Debug.WriteLine("DESCRIPTION BEFORE : {0}", input);
            //Console.WriteLine("Ascii converted string: {0}", asciiString);

            return unicodeString;
        }*/

    }
}



