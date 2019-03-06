using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace RazorComponentsDemo.App.Services
{
    public class WeatherForecastService
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<IPagedList<WeatherForecast>> GetForecastAsync(DateTime startDate,int pageIndex,int pageSize)
        {
            var rng = new Random();
            var data=Enumerable.Range(1, 58).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();

            return data.ToPagedListAsync(pageIndex, pageSize);
        }
    }
}
