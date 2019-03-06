using BlazorDemo.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace BlazorDemo.Server.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public PagedListData<WeatherForecast> WeatherForecasts(int pageIndex, int pageSize)
        {
            var rng = new Random();
            var data= Enumerable.Range(1, 85).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToPagedList(pageIndex,pageSize);
            return new PagedListData<WeatherForecast>{ Items = data,MetaData=data.GetMetaData()};
        }
    }
}
