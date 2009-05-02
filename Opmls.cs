using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace Spaetzel.FeedDA
{
    public static class Opmls
    {
        public static List<Outline> ParseOpml(string url)
        {
            XDocument opmlDoc = XDocument.Load(url);

            return ParseOpml(opmlDoc);
 

        }

        public static List<Outline> ParseOpml(Stream stream)
        {

            StreamReader reader = new StreamReader(stream);

            XDocument doc = XDocument.Load(reader);

            return ParseOpml(doc);

        }

        public static List<Outline> ParseOpml(XDocument opmlDoc)
        {
            var outlines = from item in opmlDoc.Descendants("outline")
                           select new Outline()
                           {
                               Title = GetAttributeValue(item.Attribute("title")),
                               Text = GetAttributeValue(item.Attribute("text")),
                               Type = GetAttributeValue(item.Attribute("type")),
                               XmlUrl = GetAttributeValue(item.Attribute("xmlUrl")),
                               HtmlUrl = GetAttributeValue(item.Attribute("htmlUrl"))

                           };

            return outlines.ToList();
        }

        public static string GetAttributeValue(XAttribute attribute)
        {
            if (attribute == null)
            {
                return "";
            }
            else
            {
                return attribute.Value;
            }
        }
    }
}
