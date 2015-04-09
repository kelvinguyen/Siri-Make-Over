using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Capstone1.Model
{
    public class WorkerClass
    {
        public string getSourceCode(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string str = sr.ReadToEnd();
            sr.Close();
            return str;
        }

        public string getContentOnly(string url)
        {
            string reg = @"(<span.*?>.*?</span>)";
            string source = getSourceCode(url);
            MatchCollection m1 = Regex.Matches(source, reg, RegexOptions.Singleline);
            string result = "";
            foreach(Match group in m1)
            {
                string data = Regex.Replace(group.Value, @"<(.|\n)*?>", string.Empty);
                result += data+" \n";
            }
            return result;
        }

        public string getAllConnectingUrl(string url)
        {
            string regex = @"(<a.*?>.*?<b>.*?</b>.*?</a>)";
            string regexURL = @"herf= ";
            string source = getSourceCode(url);
            MatchCollection m1 = Regex.Matches(source, regex, RegexOptions.Singleline);
            string result = "";
            foreach (Match group in m1)
            {
               // string data = Regex.Replace(group.Value, @"<(.|\n)*?>", string.Empty);
                //result += data + " \n";
                result += group.ToString()+"\n";
            }
            return result;
           // return "";
        }

        //using htmlAgilityPack
        public string getAllConnectingUrl2(string url)
        {
            //get DOM elements of a url
            HtmlWeb htmlDoc = new HtmlWeb();
            var document = htmlDoc.Load(url);
            var aTags = document.DocumentNode.SelectNodes("//a");
            int count = 1;
            string result ="";
            if (aTags != null)
            {
                foreach (var tag in aTags)
                {
                    result += count + "." + tag.InnerHtml+"\n";

                    result += count + ". " + tag.InnerHtml + " - " + tag.Attributes["href"].Value + "\n";
                     //HtmlAttribute col = tag.Attributes["href"] ;
                     //result += col.Value;
                    
                    count++;
                }
                return result;
            }
            else
            {
                return "nothing";
            }
           
        }

        //take out the whole div
        public string analysisTheContentDiv(string url)
        {
            string reg = @"<div .*?>.*?</div>";
            
            string source = getSourceCode(url);
            MatchCollection m1 = Regex.Matches(source, reg, RegexOptions.Singleline);
            string result = "";
            foreach (Match group in m1)
            {
                //string data = Regex.Replace(group.Value, @"<(.|\n)*?>", string.Empty);
                //result += data + " \n";
                //result += group.Value + "\n\n\n\n";
                string link = collectLinkInDiv(group);
                if (link != null)
                {
                    result += link + "\n\n\n\n";
                }
                
            }
            return result;
        }

        public string collectLinkInDiv(Match group)
        {
            Regex linkReg = new Regex(@"href=""/.*?""");
            
            //Regex fixlink = new Regex(@"http://.*?http");
           
            Match matchLink = linkReg.Match(group.Value);
            string link = matchLink.Value.ToLower();
            string final = "";
            if (link.Contains("http://") || link.Contains("https://") || link.Contains("www."))
            {
                string linkReg2 = @"https?://.*?/(\D)+";
                MatchCollection secondMatch = Regex.Matches(link, linkReg2, RegexOptions.Singleline);
                int count = 0;
                foreach (Match finalLink in secondMatch)
                {
                    if (count == 1)
                    {
                        final += finalLink.Value.Substring(0,finalLink.Value.Length-1);
                    }
                    count++;
                }
                return final;
            }
            //link = link.Replace("href=\"/url?q=", "").Replace("\"", "");
            //Match fixlink1 = fixlink.Match(link);
            //string why = fixlink1.Value;
           // string shouldbe = secondMatch.Value;

           
           //return (link.Contains("http://") || link.Contains("https://")||link.Contains("www."))? link :null;
            return null;
            
        }

    }
}
