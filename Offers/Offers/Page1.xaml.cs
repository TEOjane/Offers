using Offers.Model;
using Offers.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Offers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1(string JsonOffer)
        {
            InitializeComponent();

            OfferToJson.Text =JsonOffer;

            //BindingContext = BindingContext as TestServices;

           //Debug.WriteLine($"Selected Page ID = {SelectedID}, Page Price = {SelectedPrice}");
        }

    }
}

//SelectedID, string SelectedPrice, string SelectedURL,string CurrencyID, int CategoryID, string LinkPicture, string Description