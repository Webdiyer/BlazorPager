using BlazorHosted.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using X.PagedList;

namespace BlazorHosted.Server.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        public SampleDataController(IHostingEnvironment env)
        {
            Env = env;
        }
        private IHostingEnvironment Env;

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        [HttpGet("[action]")]
        public PagedListData<WeatherForecast> WeatherForecasts(int pageIndex, int pageSize)
        {
            var rng = new Random();
            var data = Enumerable.Range(1, 85).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToPagedList(pageIndex, pageSize);
            return new PagedListData<WeatherForecast> { Items = data, MetaData = data.GetMetaData() };
        }

        [HttpGet("[action]")]
        public PagedListData<Order> Orders(string companyName,int pageIndex, int pageSize)
        {
            var path = Path.Combine(Env.ContentRootPath, "orders.json");
            var ods = Newtonsoft.Json.JsonConvert.DeserializeObject<Order[]>(System.IO.File.ReadAllText(path));
            IPagedList<Order> orders;
            if (!string.IsNullOrWhiteSpace(companyName))
            {
                orders = ods.Where(o => o.CompanyName.Contains(companyName)).ToPagedList(pageIndex, pageSize);
            }
            else
            {
                orders = ods.ToPagedList(pageIndex, pageSize);
            }
            return new PagedListData<Order> { Items = orders, MetaData = orders.GetMetaData() };
        }
    }
}
