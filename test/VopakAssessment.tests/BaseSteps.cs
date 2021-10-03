using System;
using TechTalk.SpecFlow;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using FluentAssertions;

namespace VopakAssessmentApp.tests
{
    public abstract class BaseSteps
    {
        protected readonly HttpClient client;
        protected readonly ScenarioContext scenarioContext;
        protected const string RESULT = "RESULT";
        protected const string QUERY_PARAMETERS = "QUERYPARAMETERS";
        protected string baseUrl = string.Empty;

        private protected object Result => scenarioContext.Get<object>(RESULT);
        private protected HttpResponseMessage Response => scenarioContext.Get<HttpResponseMessage>();

        public BaseSteps(ScenarioContext scenarioContext, TestSettings settings)
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(settings.ServiceHost)
            };

            this.scenarioContext = scenarioContext;
        }

        public async Task PerformTheRequest(string url, string method, object queryParams = null)
        {
            var finalUrl = $"{baseUrl}{url}";

            if (queryParams != null)
            {
                finalUrl = AppendQueryParameters(finalUrl, queryParams);
            }

            var request = new HttpRequestMessage(new HttpMethod(method), finalUrl);
            request.Headers.Add("Accept", "application/json");

            scenarioContext.Set(await client.SendAsync(request));
        }

        [Then(@"the response code is (.*)")]
        public void ThenTheResponseCodeIs(int code)
        {
            Response.StatusCode.Should().Be((HttpStatusCode)code);
        }

        protected async Task GetTheResults<T>()
        {
            string content = await Response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(content);
            SetResultToScenarioContext(result);
        }

        protected void SetResultToScenarioContext(object result)
        {
            scenarioContext.Set(result, RESULT);
        }

        private static string AppendQueryParameters(string finalUrl, object obj)
        {
            var dict = new Dictionary<string, string>();

            var type = obj.GetType();

            foreach (var propInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var value = propInfo.GetValue(obj) is Enum ? (int)propInfo.GetValue(obj) : propInfo.GetValue(obj);
                dict.Add(propInfo.Name, value.ToString());
            }

            return QueryHelpers.AddQueryString(finalUrl, dict);
        }
    }
}
