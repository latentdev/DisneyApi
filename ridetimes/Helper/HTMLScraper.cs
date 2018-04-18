using HtmlAgilityPack;
using ridetimes.Models;
using ridetimes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ridetimes.Helper
{
    public static class HTMLScraper
    {
        public static RideViewModel ScrapeWikipedia(Entry entry)
        {
            var viewModel = Ride.GetRideViewModel(entry);
            var html = @"https://en.wikipedia.org/wiki/"+entry.name;

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);
            var rideName = htmlDoc.DocumentNode.SelectSingleNode(@"//link[contains(@rel,'canonical')]").Attributes.First(x=>x.Name.Equals("href")).Value.Split('/').Last().Replace('_',' ');
            var title = htmlDoc.DocumentNode.SelectSingleNode(@"//h1[contains(@id,'firstHeading')]");
            if(title!=null && title.InnerText.Equals(rideName))
            {  
                viewModel.History = GetHistory(htmlDoc);
            }
            return viewModel;

        }

        public static List<string> GetHistory(HtmlDocument htmlDoc)
        {
            try
            {
                var h2 = htmlDoc.DocumentNode.SelectNodes("//h2");
                //bool found = false;
                var history = h2.SkipWhile(x => !x.FirstChild.Id.Equals("History"));
                int start = history.First().Line;
                int end = history.Skip(1).First().Line;
                var h3 = htmlDoc.DocumentNode.SelectNodes("//h3").Where(x => x.Line >= start && x.Line <= end);
                if (h3.Count() > 0)
                {
                    var section = h3.SkipWhile(x => !x.FirstChild.Id.Equals("Disneyland"));
                    start = section.First().Line;
                    end = section.Skip(1).First().Line;
                }
                var p = htmlDoc.DocumentNode.SelectNodes("//p").Where(x => x.Line > start && x.Line < end);
                List<string> historyStrings = new List<string>();
                foreach (var paragraph in p)
                {
                    historyStrings.Add(paragraph.InnerText);
                }
                return historyStrings;
            }
            catch (Exception) {
                return new List<string> { "Data not available" };
            }

            
        }
    }
}