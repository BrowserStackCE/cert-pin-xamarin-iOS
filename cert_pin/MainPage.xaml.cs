using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace cert_pin
{
    public partial class MainPage : ContentPage
    {
        HttpClient client;
        public MainPage(WebProxy proxy)
        {
            InitializeComponent();

            client = new HttpClient(GetInsecureHandler(proxy));

            var req = client.GetAsync("https://self-signed.badssl.com");

            req.Wait();
            HttpResponseMessage resp = req.Result;

            var body = resp.Content.ReadAsStringAsync();

            body.Wait();
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = body.Result.ToString();

            webView.Source = htmlSource;
        }

        public HttpClientHandler GetInsecureHandler(WebProxy proxy)
        {
            HttpClientHandler handler = new HttpClientHandler();
            if(proxy != null)
            {
                handler.Proxy = proxy;
            }
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                Console.WriteLine(message.ToString());
                Console.WriteLine(chain);
                Console.WriteLine(cert);
                return cert.Thumbprint == "99C1DAF07C8D69A8A065492DCAAE43C43FF13497";
            };
            return handler;
        }
    }
}
