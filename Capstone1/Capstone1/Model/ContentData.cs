using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;
using System.IO;
using System.Text.RegularExpressions;

namespace Capstone1.Model
{
    public class ContentData
    {
        List<string> linkList;
        string question;
        public ContentData(List<string> linkList ,string question)
        {
            this.linkList = linkList;
            this.question = question;
        }
        public string CollectDataAllLink()
        {
            string result = "";
            foreach (string s in linkList)
            {
               // Console.WriteLine("the link : {0}",s);
                result += CollectDataPerLink(s);
            }
            return result;
        }
        public string CollectDataPerLink(string url)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Timeout = 200000;
            string str = "";
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());
                str = sr.ReadToEnd();
                sr.Close();
            }
            catch (WebException e)
            {
                return "";
            }
            
            HtmlDocument domDocument = new HtmlDocument();
            domDocument.LoadHtml(str);
            //HtmlWeb htmlWeb = new HtmlWeb();
            
            //HtmlDocument domDocument = htmlWeb.Load(url);
            HtmlNode bodyNode = domDocument.DocumentNode.SelectSingleNode("//body");
           
            if (bodyNode != null && bodyNode.ChildNodes != null)
            {
                IEnumerable<HtmlNode> childNode = bodyNode.ChildNodes;
                List<string> list = new List<string>();
                foreach (HtmlNode node in childNode)
                {
                    if (node != null && IsContainQuestionAnswer(node))
                    {
                        string testing = "";
                        testing = Regex.Replace(node.InnerText, @"[\s]", " ");
                        list.Add(testing);
                        result += testing + "\n--------------------------------------------------------";
                    }

                }
            }
            
            
            //IEnumerable<HtmlNode> data = domDocument.DocumentNode.Descendants("body");
           
           // result += bodyNode.InnerText;
            return result;
        }

        public bool IsContainQuestionAnswer(HtmlNode node)
        {
            string content = node.InnerText;
            string[] ques = question.Split(' ');
            foreach (string s in ques)
            {
                if (!content.Contains(s))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
