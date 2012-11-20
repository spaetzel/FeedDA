using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Spaetzel.FeedDA
{
    public class FeedItem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string ItunesSummary { get; set; }
        public string Guid { get; set; }
        public DateTime PubDate { get; set; }
        public int Duration { get; set; }
        public Enclosure Enclosure { get; set; }
        public string Link { get; set; }
       

        public XElement GetXElement()
        {
            XNamespace itunesNamespace = "http://www.itunes.com/dtds/podcast-1.0.dtd";

            var output = new XElement( "item",
                new XElement("title", Title),
                new XElement("link", Link),
                
                new XElement(itunesNamespace + "summary", ItunesSummary),
                new XElement("description", Description),
               
                new XElement("pubDate", PubDate)
                
                );

            if (Enclosure != null)
            {
                output.Add(Enclosure.GetXElement());
            }

            if ( Author != null && Author != String.Empty)
            {
                output.Add(new XElement(itunesNamespace + "author", Author));
            }

            if (Duration > 0)
            {
                output.Add(new XElement(itunesNamespace + "duration", Duration));
            }

            return output;
        
        }
    }


    public class Enclosure
    {
        public Uri Url { get; set; }
        public int Length { get; set; }
        public string Type { get; set; }

        public XElement GetXElement()
        {
            return new XElement("enclosure",
                new XAttribute("url", Url == null ? "" : Url.ToString() ),
                new XAttribute("length", Length),
                new XAttribute("type", Type)
                );
        }

    }

 
}
