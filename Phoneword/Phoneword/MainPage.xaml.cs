using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoneword
{
    public partial class MainPage : ContentPage
    {
        private string _translatedNumber;
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnTranslate(object sender, EventArgs e)
        {
            _translatedNumber = PhonewordTranslator.ToNumber(phoneNumberText.Text);
            if (!string.IsNullOrWhiteSpace(_translatedNumber))
            {
                callButton.IsEnabled = true;
                callButton.Text = "Call " + _translatedNumber;
            }
            else
            {
                callButton.IsEnabled = false;
                callButton.Text = "Call";
            }
        }

        private async void OnCall(object sender, EventArgs e)
        {
            if (await this.DisplayAlert(
                "Dial a Number",
                "Would you like to call " + _translatedNumber + "?",
                "Yes",
                "No"))
            {
                var dialer = DependencyService.Get<IDialer>();
                dialer?.Dial(_translatedNumber);
            }
        }
    }
}
