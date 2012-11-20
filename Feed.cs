using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Spaetzel.UtilityLibrary;

namespace Spaetzel.FeedDA
{
    public class Feed
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Language { get; set; }
        public string Copyright { get; set; }
        public string ItunesSubtitle { get; set; }
        public string ItunesSummary { get; set; }
        public string Description { get; set; }
        public Image Image { get; set; }
        public DateTime LastBuildDate { get; set; }
        public DateTime PubDate { get; set; }
        public string Generator { get; set; }
        public List<FeedItem> Items { get; set; }


        public XElement GetXElement()
        {
            XNamespace itunesNamespace = "http://www.itunes.com/dtds/podcast-1.0.dtd";

            //XElement itunesElement = ;

            return new XElement("rss", 
                new XAttribute(XNamespace.Xmlns + "itunes", "http://www.itunes.com/dtds/podcast-1.0.dtd"),
                new XAttribute("version", "2.0"),
                new XElement("channel",
                new XElement("title", Title),
                new XElement("link", Link),
                new XElement("language", Language),
                new XElement("copyright", Copyright),
                new XElement("generator", Generator ),
                new XElement("lastBuildDate", Utilities.FormatDateTimeRFC1132(LastBuildDate) ),
                new XElement("pubDate", Utilities.FormatDateTimeRFC1132(PubDate) ),
                new XElement(itunesNamespace + "subtitle", ItunesSubtitle),
                new XElement(itunesNamespace + "summary", ItunesSummary),
                new XElement("description", Description),
                Image.GetXElement(),
                from i in Items
                select i.GetXElement()

                )
                );




        }
    }

    public class Image
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }

        public XElement GetXElement()
        {
            return new XElement("image",
                new XElement("url", Url),
                new XElement("title", Title),
                new XElement("link", Link)
                );
        }

    }
}
