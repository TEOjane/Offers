using Newtonsoft.Json;
using Offers.Model;
using Offers.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using static Xamarin.Essentials.AppleSignInAuthenticator;
using Button = Xamarin.Forms.Button;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Offers
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new TestServices();
        }

        protected override void OnAppearing()
        {
            Debug.WriteLine("OnAppearing started");

            foreach (var off in (BindingContext as TestServices).OffersList)
            {
                Debug.WriteLine($"ID = {off.ID} Price = {off.Price}");
                Debug.WriteLine("Apering!!!!!");
            }

            ListViewOffers.ItemsSource = (BindingContext as TestServices).OffersList;

            foreach (TestModel off in (BindingContext as TestServices).OffersList)
            {
                Debug.WriteLine($"ID = {off.ID}");
                Debug.WriteLine("This IteamSourse!!!!!");
            }

            Debug.WriteLine("OnAppearing finished");

        }
        protected override void OnBindingContextChanged()
        {
            (BindingContext as TestServices).GetOffersCollection.Execute(null);

            foreach (var off in (BindingContext as TestServices).OffersList)
            {
                Debug.WriteLine($"ID = {off.ID} Price = {off.Price}");
                Debug.WriteLine("!!!!!");
            }

            Debug.WriteLine("Before BC changed");


            base.OnBindingContextChanged();


            Debug.WriteLine("After BC changed");

            //Thread.Sleep(1000);
            Debug.WriteLine("After sleep");

            foreach (var off in (BindingContext as TestServices).OffersList)
            {
                Debug.WriteLine($"ID = {off.ID} Price = {off.Price}");
                Debug.WriteLine("!!!!!");
            }
        }

        private async void OnItemSelected(Object sender, ItemTappedEventArgs e)
        {
            TestModel details = e.Item as TestModel;
            Debug.WriteLine($"Это описание {details.Description}");

            var OptionJson = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin,UnicodeRanges.LatinExtendedC,
                UnicodeRanges.Cyrillic, UnicodeRanges.CyrillicExtendedA, UnicodeRanges.CyrillicExtendedB, 
                UnicodeRanges.CyrillicExtendedC, UnicodeRanges.CyrillicSupplement),
                WriteIndented = true
            };
     
            string output = System.Text.Json.JsonSerializer.Serialize(details, OptionJson);



            await Navigation.PushAsync(new Page1(output));

        }


        //details.ID, details.Price, details.Url, details.CurrencyID, details.CategoryID, details.Picture, details.Description
        /*private async void buttonID_Clicked(object sender, EventArgs e)
        {
            Button button = (Button) sender;
            (BindingContext as TestServices).SelectedID = button.Text;
           
            await Navigation.PushAsync(new Page1());

        }*/

    } 
}



