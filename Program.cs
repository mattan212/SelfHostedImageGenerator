using System;
using System.Threading;
using CefSharp;
using CefSharp.OffScreen;
using Microsoft.Owin.Hosting;

namespace SelfHostedImageGenerator
{
    class Program
    {
        private static readonly string BaseAddress = "http://localhost:9000/";
        private static readonly string WebsiteUrl = "https://localhost:44374/index.html?name={fileName}&endpoint={endpoint}";

        static void Main(string[] args)
        {
            //Initialized chromium engine.
            Cef.Initialize(new CefSettings());

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: BaseAddress))
            {
                Console.WriteLine($"Server listening at {BaseAddress}");

                var timer = new Timer(Process, null, 0, 10000);

                Console.ReadKey();
            }
        }

        static void Process(object obj)
        {
            var time = DateTime.Now.ToString("yyyyMMddHHmmss");
            var url = WebsiteUrl.Replace("{fileName}", $"snap_{time}.png")
                .Replace("{endpoint}", $"{BaseAddress}api/ImageBank/StoreImage");

            var browser = new ChromiumWebBrowser(url);
        }
    }
}

