using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;

namespace Capstone1.Model
{
    public class ContentData
    {
        List<string> linkList;
        public ContentData()
        {
            //this.linkList = linkList;
        }

        public string CollectDataPerLink(string url)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument domDocument = htmlWeb.Load(url);
            HtmlNode bodyNode = domDocument.DocumentNode.SelectSingleNode("//body");
            IEnumerable<HtmlNode> childNode = bodyNode.ChildNodes;
            //IEnumerable<HtmlNode> data = domDocument.DocumentNode.Descendants("body");
            string result = "";
            List<string> list = new List<string>();
            foreach (HtmlNode node in childNode)
            {
                if (node.InnerText != null || node.InnerText != "")
                {
                    list.Add(node.InnerText);
                    result += node.InnerText + "\n--------------------------------------------------------";
                }
               
            }
           // result += bodyNode.InnerText;
            return result;
        }
    }
}
