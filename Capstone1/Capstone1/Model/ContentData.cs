using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;
using System.IO;
using System.Text.RegularExpressions;
using Capstone1.SummaryEngine;
using Capstone1.KeywordEngine;

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
        public List<string> CollectDataAllLink()
        {
            string result = "";
       
            List<string> document = new List<string>();
            List<List<string>> sentencesPageList = new List<List<string>>();
            FilterFactory filterFactory = new FilterFactory();
            PagesContainer pagesContainer = new PagesContainer();
            foreach (string s in linkList)
            {
                List<string> sentences = new List<string>();
                string data = CollectDataPerLink(s);
                document.Add(data);
                sentences = SentenceFactory.AddContent(data);
                sentencesPageList.Add(sentences);
               // result += CollectDataPerLink(s);
                filterFactory.CreatePage(s , question, data , sentences);

            }
            //FilterFactory filterFactory = CreateFilterFactoryObject(question,document,sentencesPageList,linkList);
            pagesContainer.PageList = filterFactory.CreatePageObject();
            //result += pagesContainer.DisplaySummarize();
            //foreach (string sentence in pagesContainer.FindBestPredictSentenceAllPages())
            //{
            //    result += sentence + "\n---------------\n";
            //}
/*
            int count = 1;
            foreach (var c in pagesContainer.PageList)
            {
                //List<string> tempSen = c.Sentences;
                //foreach (var y in tempSen)
                //{
                //    result += y ;
                //}
                result += c.Document;
               
                //result += "\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\n";
                //Dictionary<string, int> wordCount = c.WordsCount();
                //foreach(KeyValuePair<string,int> d in wordCount)
                //{
                //    result += d.Key + " : " + d.Value+" \n";
                //}


                result += "\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@        LINK " + count + "               @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\n";
                result += c.DisplaySummary();
                count++;
                result += "\n------------------------------------------------------------------------------- ================ -------------------------------------------------------\n";
            }
 */ 
     
    /*
            QuestionAnalysis ques = new QuestionAnalysis(question);
            string[] temp = ques.FindKeywordInQuestion();
            foreach (string c in temp)
            {
                result += c + "\n";
            }
     */
            return pagesContainer.FindBestPredictSentenceAllPages();
        }
        public FilterFactory CreateFilterFactoryObject( string question, List<string> document , List<List<string>> sentencesPageList,List<string>linkList)
        {
            FilterFactory filterFactory = new FilterFactory();
            filterFactory.Question = question;
            filterFactory.Documents = document;
            filterFactory.SentencesPagesList = sentencesPageList;
            filterFactory.LinkList = linkList;

            return filterFactory;
        }
        public string CollectDataPerLink(string url)
        {
            string result = "";


            HtmlNodeCollection collectNodes = CollectTextFromAllTagInALink(url);
           
            //if (bodyNode != null && bodyNode.ChildNodes != null)
            //{
            //    IEnumerable<HtmlNode> childNode = bodyNode.ChildNodes;
            //    List<string> list = new List<string>();
            //    foreach (HtmlNode node in childNode)
            //    {
            //        if (node != null )
            //        {
                       
            //            string testing = "";
            //            testing = Regex.Replace(node.InnerText, @"[\s]", " ");
            //            list.Add(testing);
            //            result += testing + "\n--------------------------------------------------------";
            //        }

            //    }
            //}
            List<string> list = new List<string>();
            if (collectNodes != null)
            {
                foreach (HtmlNode node in collectNodes)
                {
                    if (node != null)
                    {
                        string testing = node.InnerText;
                       // testing = Regex.Replace(testing, @"[\n\r]+", "\n");
                        if (!isStringEmpty(testing))
                        {
                            // list.Add(node.InnerText);
                            result += node.InnerText;
                        }
                        //else
                        //{
                        //    if(!isStringJunkInfo(testing))
                        //    testing= ReformatString(node.InnerText);
                        //    result += testing;
                        //}
                        
                        
                        //testing = Regex.Replace(testing, @"[\t]+", "\t");    
                    }
                }
            }

            //result = Regex.Replace(result, @"[\n]+", "\n");
            //IEnumerable<HtmlNode> data = domDocument.DocumentNode.Descendants("body");
           
           // result += bodyNode.InnerText;
            result = ReformatString(result);
            result = HtmlCharacterDecode(result);
            return result ;
        }
        public string HtmlCharacterDecode(String htmlString)
        {
            string character = @"&.+?;";
            MatchCollection m1 = Regex.Matches(htmlString, character, RegexOptions.Singleline);
            foreach (Match c in m1)
            {
                string ch = WebUtility.HtmlDecode(c.Value);
                htmlString = htmlString.Replace(c.Value , ch);
            }
            //String temp = Regex.Replace(htmlString, @"&.+?", "");
            return htmlString;
        }
        public string ReformatString(string str)
        {
            string final = "";
            final = Regex.Replace(str, @"[\n\r]+", "\n\r");
            final = Regex.Replace(final, @"[\n]+", "\n");
            final = Regex.Replace(final, @"[\r]+", "\r");
            //final = Regex.Replace(final, @"[\s]+", " ");
            return final;
        }
        public bool isStringEmpty(string str)
        {
            string final = "";
            final = Regex.Replace(str, @"[\n\r \r \n \s]+", "");
            return String.IsNullOrWhiteSpace(final);
        }
        public HtmlNodeCollection CollectTextFromAllTagInALink(string url)
        {
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
                return null;
            }

            HtmlDocument domDocument = new HtmlDocument();
            domDocument.LoadHtml(str);
            //HtmlWeb htmlWeb = new HtmlWeb();

            //HtmlDocument domDocument = htmlWeb.Load(url);
            HtmlNode bodyNode = domDocument.DocumentNode.SelectSingleNode("//body");
            HtmlNodeCollection collectNodes = null;
            if (bodyNode != null)
            {
                //collectNodes = bodyNode.SelectNodes("//*[not(self::script or self::link)]/text()");
                collectNodes = bodyNode.SelectNodes("//*[not(self::script or self::meta or self::link or self::footer or self::header or self::a or self::html or self::img or self::style)]/text()");
            }

            return collectNodes;
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
