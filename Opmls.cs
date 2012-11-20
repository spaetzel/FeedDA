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
            List<Outline> outlines = new List<Outline>();

            foreach (var item in opmlDoc.Descendants("outline"))
            {
                outlines.AddRange(ParseOutline(item));
            }

          
            return outlines.ToList();
        }

        private static IEnumerable<Outline> ParseOutline(XElement item)
        {
            List<Outline> output = new List<Outline>();

            foreach (var subItem in item.Descendants("outline"))
            {
                output.AddRange(ParseOutline(subItem));
            }

            if (item.Attribute("xmlUrl") != null)
            {
                Outline newOutline = new Outline()
                           {
                               Title = GetAttributeValue(item.Attribute("title")),
                               Text = GetAttributeValue(item.Attribute("text")),
                               Type = GetAttributeValue(item.Attribute("type"))
                           };

                try
                {
                    newOutline.XmlUrl = new Uri(GetAttributeValue(item.Attribute("xmlUrl")));
                }
                catch (UriFormatException)
                {
                }

                try
                {
                    newOutline.HtmlUrl = new Uri(GetAttributeValue(item.Attribute("htmlUrl")));
                }
                catch (UriFormatException)
                {
                    newOutline.HtmlUrl = newOutline.XmlUrl;
                }

                if (  ( newOutline.XmlUrl != null && newOutline.XmlUrl.ToString().Length > 0 ) || (  newOutline.HtmlUrl != null && newOutline.HtmlUrl.ToString().Length > 0 ) )
                {
                    output.Add(newOutline);
                }
                else
                {
                    // Didn't get a good html or xml value, don't both adding
                }
            }

            return output;
    
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
