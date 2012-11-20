using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Spaetzel.FeedDA
{
    public class Outline
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public Uri XmlUrl { get; set; }
        public Uri HtmlUrl { get; set; }
        public List<Outline> Descendants { get; set; }

        public XElement GetXElement()
        {
            XElement output = new XElement("outline",
                                new XAttribute("text", this.Text),
                                new XAttribute("title", Title),
                                new XAttribute("type", Type),
                                new XAttribute("xmlUrl", XmlUrl),
                                new XAttribute("htmlUrl", HtmlUrl)
                                );

            return output;
        }
    }
}
