using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using CoreFoundation;
using Foundation;
using UIKit;

namespace cert_pin.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Console.WriteLine(NSProcessInfo.ProcessInfo.Environment.ToString());
            if (NSProcessInfo.ProcessInfo.Environment["LAUNCH_ENVIRONMENT"] != null && NSProcessInfo.ProcessInfo.Environment["LAUNCH_ENVIRONMENT"].ToString() == "BROWSERSTACK")
            {
                Console.WriteLine(NSProcessInfo.ProcessInfo.Environment.ToString());
                LoadApplication(new App(GetProxySettings()));
            } else
            {
                LoadApplication(new App(null));
            }
            return base.FinishedLaunching(app, options);
        }

        public WebProxy GetProxySettings()
        {
            var settings = CFNetwork.GetSystemProxySettings();
            Console.WriteLine("Setting" + settings.Dictionary.ToString());
            var proxyPACURL = settings.ProxyAutoConfigURLString;
            String fetchedResponse = GetResponseData(proxyPACURL);
            String proxyHostPortMatch = Regex.Match(fetchedResponse, "(\\d+.\\d+.\\d+.\\d+:\\d+)", RegexOptions.IgnoreCase).ToString();
            String[] proxyHostPortArray = proxyHostPortMatch.Split(':');
            var proxyHost = proxyHostPortArray[0];
            var proxyPort = int.Parse(proxyHostPortArray[1]);
            Console.WriteLine("proxyPort  " + proxyPort);
            Console.WriteLine("proxyHost  " + proxyHost);
            return !string.IsNullOrEmpty(proxyHost) && proxyPort != 0
                ? new WebProxy($"{proxyHost}:{proxyPort}")
                : null;
        }
        public String GetResponseData(string apiUri)
        {
            var httpClient = new HttpClient();
            var httpResponseMessage = httpClient.GetAsync(apiUri).Result;
            var responseContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
            Console.WriteLine("HTTP Response message: " + responseContent);
            return responseContent;
        }
    }

}
