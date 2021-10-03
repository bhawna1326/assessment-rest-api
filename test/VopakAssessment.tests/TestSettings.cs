using Microsoft.Extensions.Configuration;
using System;

namespace VopakAssessmentApp.tests
{
    public class TestSettings
    {
        public string ServiceHost { get; set; }

        public TestSettings()
        {
            var environmentName = Environment.GetEnvironmentVariable("ENVIRONMENT");

            if (string.IsNullOrEmpty(environmentName))
            {
                environmentName = Environment.GetEnvironmentVariable("RELEASE_ENVIRONMENT");
            }

            if (string.IsNullOrEmpty(environmentName))
            {
                // Default to "development"
                environmentName = "development";
            }

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{environmentName}.json", true, true) // Environment settings file is optional
                   .AddEnvironmentVariables();

            var configuration = builder.Build();

            configuration.Bind(this);
        }
    }
}
