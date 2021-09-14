using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Net;

namespace cert_pin
{
    public partial class App : Application
    {

        public App(WebProxy proxy)
        {
            InitializeComponent();

            MainPage = new MainPage(proxy);

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
