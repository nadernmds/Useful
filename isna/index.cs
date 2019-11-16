using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Isna.Grabber;
using Microsoft.AspNetCore.Mvc;

namespace Isna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<object> getNews(int? id)

        {
            return await Task.Run(() =>
            {
                //http://www.rtl-theme.com/theme-demo/121641/
                //  var url = "https://www.isna.ir/archive?pi=1&tp=30";              
                var url = "https://www.isna.ir/archive?pi=1";

                var web = new HtmlWeb();
                var doc = web.Load(url);

                var lis = doc.DocumentNode
                .SelectNodes("//div[@class='items']").Descendants("ul").FirstOrDefault()
                .Descendants("li").Select(c => new News
                {
                    ID = GetID(c.Descendants("div").FirstOrDefault().Descendants("h3").FirstOrDefault().Descendants("a").FirstOrDefault().Attributes.FirstOrDefault(w => w.Name == "href").Value),
                    image = c.Descendants("figure").Count() > 0 ? c.Descendants("figure").FirstOrDefault().Descendants("a").FirstOrDefault().Descendants("img").FirstOrDefault().Attributes.FirstOrDefault(w => w.Name == "src").Value : "",
                    link = "https://www.isna.ir" + c.Descendants("div").FirstOrDefault().Descendants("h3").FirstOrDefault().Descendants("a").FirstOrDefault().Attributes.FirstOrDefault(w => w.Name == "href").Value
                    ,
                    title = c.Descendants("div").FirstOrDefault().Descendants("h3").FirstOrDefault().Descendants("a").FirstOrDefault().InnerText
                    ,
                    summary = c.Descendants("div").FirstOrDefault().Descendants("p").FirstOrDefault().InnerText
                    ,
                    type = News.Type.isna,
                    full=id==1?loadFullNews("https://www.isna.ir" + c.Descendants("div").FirstOrDefault().Descendants("h3").FirstOrDefault().Descendants("a").FirstOrDefault().Attributes.FirstOrDefault(w => w.Name == "href").Value):""
                });
                return lis;
            }
            );
        }
        private string loadFullNews(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var lis = doc.DocumentNode
            .SelectNodes("//div[@class='item-text']").Descendants().Select(c => c.InnerText);
            string res = "";
            foreach (var item in lis)
            {
                res += item;
            }
            return res;

        }
        private string GetID(string value)
        {
            var w = value.Split('/')[2];
            return w;
        }
    }
}
