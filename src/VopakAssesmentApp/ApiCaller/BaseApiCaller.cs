using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace VopakAssesmentApp.ApiCaller
{
    public class BaseApiCaller
    {
        private readonly HttpClient _client;

        public BaseApiCaller(HttpClient client)
        {
            _client = client;
        }

        public static HttpClient GetClient(string baseAddress, HttpMessageHandler httpMsgHandler = null)
        {
            var client = httpMsgHandler == null ? new HttpClient() : new HttpClient(httpMsgHandler);

            client.BaseAddress = new Uri(baseAddress);
            client.Timeout = TimeSpan.FromSeconds(3);

            return client;
        }

        protected async Task<HttpResponseMessage> ExecuteAsync(Func<HttpClient, Task<HttpResponseMessage>> callHttpMethod, bool processResponseErrors = true)
        {
            if (callHttpMethod == null)
            {
                throw new ArgumentNullException(nameof(callHttpMethod));
            }

            var response = await callHttpMethod(_client);

            if (processResponseErrors)
            {
                ProcessResponseErrors(response);
            }

            return response;
        }

        private void ProcessResponseErrors(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
        }
    }
}