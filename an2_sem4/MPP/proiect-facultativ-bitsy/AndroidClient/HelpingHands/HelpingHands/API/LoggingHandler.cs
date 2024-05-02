using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Diagnostics;
using System.Net;
using Xamarin.Android.Net;
using Org.Apache.Http.Cookies;

namespace HelpingHands.API
{
    public interface IHttpClientHandlerService
    {
        HttpClientHandler GetInsecureHandler();
    }

    public class LoggingHandler : AndroidClientHandler, IHttpClientHandlerService
    {
        //new AndroidClientHandler() { UseCookies = true, AllowAutoRedirect = false, CookieContainer = Cookies}
        public LoggingHandler(HttpMessageHandler innerHandler)            
        {
            UseCookies = true;
            AllowAutoRedirect = false;
            CookieContainer = Cookies;
        }
        public CookieContainer Cookies { get; set; } = new CookieContainer();

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Request:");
            Debug.WriteLine(request.ToString());
            if (request.Content != null)
            {
                Debug.WriteLine(await request.Content.ReadAsStringAsync());
            }
            Debug.WriteLine("");

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Debug.WriteLine("Response:");
            Debug.WriteLine(response.ToString());
            if (response.Content != null)
            {
                Debug.WriteLine(await response.Content.ReadAsStringAsync());
            }
            Debug.WriteLine("");

            return response;
        }

        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    if (cert.Issuer.Equals("CN=localhost"))
                        return true;
                    return errors == System.Net.Security.SslPolicyErrors.None;
                }
            };
            return handler;
        }
    }
}