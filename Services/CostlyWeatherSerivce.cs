using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using attr.Attributes;
using attr.Entities;

namespace attr.Services
{
    public interface IWeatherService
    {
        void GetNothing();
        IEnumerable<WeatherForecast> GetForecast();
    }

    public class CostlyWeatherService : IWeatherService
    {
        private ILogger<CostlyWeatherService> _logger;

        public CostlyWeatherService(Logger<CostlyWeatherService> logger)
        {
            _logger = logger;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public void GetNothing()
        {
            _logger.LogWarning("GetNothing is not annotated so it won't be logged by the interceptor");
        }

        [LogCall(logDecoration: " *** ")]
        public virtual IEnumerable<WeatherForecast> GetForecast()
        {
            _logger.LogWarning("GetForecast is annotated so it will be logged by the interceptor");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}