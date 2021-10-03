using TechTalk.SpecFlow;
using System.Threading.Tasks;
using VopakAssessmentApp.tests;
using FluentAssertions;
using VopakAssesmentApp.Models;

namespace VopakAssessment.tests.Steps
{
    [Binding]
    [Scope(Feature = "GetTemperature")]
    public sealed class GetTemperatureSteps: BaseSteps
    {
        public GetTemperatureSteps(ScenarioContext scenarioContext, TestSettings settings): base(scenarioContext, settings)
        {
            baseUrl = "temperature/";
        }

        [Given("the city name as (.*)")]
        public void GivenTheCityName(string cityName)
        {
            if(cityName == "empty")
            {
                cityName = string.Empty;
            }

            scenarioContext.Set(new
            {
                city = cityName
            }, QUERY_PARAMETERS);
        }

        [When("I make a (.*) request to the (.*) endpoint")]
        public async Task WhenICallTheGetTemperatureEndpoint(string method, string endpointName)
        {
            scenarioContext.TryGetValue(QUERY_PARAMETERS, out object queryParams);

            await PerformTheRequest(endpointName, method, queryParams);
        }

        [Then("the result should not be null")]
        public void TheResultIsNotEmpty()
        {
            Result.Should().NotBeNull();

            var temperature = Result as Temperature;

            temperature.TemperatureInCelcius.Should().NotBe(null);
            temperature.TemperatureInFahrenheit.Should().NotBe(null);
        }

        [Then(@"I get the response")]
        public async Task ThenIGetTheResponseAsync()
        {
            await GetTheResults<Temperature>();
        }
    }
}
