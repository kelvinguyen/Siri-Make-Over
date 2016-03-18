using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone1.SummaryEngine
{
    /**
     * this class will generate page object that will contain all 
     * Question
     * Document
     * list of sentence
     */
    public class FilterFactory
    {
        public string Question { get; set; }
        public List<string> Documents { get; set; }
        public List<List<string>> SentencesPagesList { get; set; }
        public List<string> LinkList { get; set; }

        List<PageObject> pageList = new List<PageObject>();
        public List<PageObject> CreatePageObject()
        {
            //List<PageObject> pages = new List<PageObject>();
            //for (int i = 0; i < Documents.Count; i++)
            //{
            //    PageObject page = new PageObject();
            //    page.Question = Question;
            //    page.Document = Documents[i];
            //    page.Sentences = SentencesPagesList[i];
            //    page.Link = LinkList[i];
            //    pages.Add(page);
            //}

            return pageList;
        }

        public PageObject CreatePage(string link, string question, string document, List<string> sentences)
        {
            PageObject page = new PageObject();
            page.Question = question;
            page.Document =document;
            page.Sentences = sentences;
            page.Link =link;
            pageList.Add(page);
            return page;

        }
    }
}
