using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone1.KeywordEngine;
using System.Text.RegularExpressions;
using Capstone1.Metric;

namespace Capstone1.SummaryEngine
{
    public class PagesContainer
    {
        public List<PageObject> PageList { get; set; }

        public string BuildTheDocument()
        {
            string result = "";
            foreach (var page in PageList)
            {
                result += page.DisplaySummary();
            }

            return result;
        }

        public List<string> CollectAllPredictSentenceAllPages()
        {
            List<string> predictSentences = new List<string>();
            foreach (var page in PageList)
            {
                List<string> sentencesPerPage = page.FindSummaryContentForthisPage();
                foreach (string sentence in sentencesPerPage)
                {
                    predictSentences.Add(sentence);
                }
            }
            return predictSentences;
        }

        
        public List<string> FindBestPredictSentenceAllPages()
        {
            string document = BuildTheDocument();
            string[] allWords = KeywordResearcher.splitTheWordInString(document);
            string[] questionKeyword = KeywordResearcher.GetKeywordArray(PageList[0].Question);
            List<string> predictSentenceAllPage = CollectAllPredictSentenceAllPages();
            QuestionAnalysis questionAnalysis = new QuestionAnalysis(PageList[0].Question);
            int count = questionKeyword.Length;
            List<string> sentences = new List<string>();
            string key = "";
            if (questionAnalysis.isShortAnswer())
            {
                int num = PageList[0].Question.Split(' ').Count();
                if (num > 8)
                {
                    while (count > 0)
                    {
                        //sentences = KeywordResearcher.FindTheBestPredictSentence(count, questionKeyword, predictSentenceAllPage);
                        key = QuestionWordCombine(count, questionKeyword);
                        sentences = SentencesContainKeyCombine(key, predictSentenceAllPage);

                        count = (sentences.Count != 0) ? 0 : --count;
                    }
                }
                else
                {
                    List<string> allquestionPosibleCombine = QuestionWordConbime2(PageList[0].Question);
                    int predict = 0;
                    for (int i = 0; i < allquestionPosibleCombine.Count; i++)
                    {
                        sentences.AddRange(SentencesContainKeyCombine(allquestionPosibleCombine[i], predictSentenceAllPage));
                        if (predict == 0 || predict <= allquestionPosibleCombine[i].Split(' ').Count() - 1 || sentences.Count == 0)
                        {
                            predict = allquestionPosibleCombine[i].Split(' ').Count() - 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                while (count > 0)
                {
                    sentences = KeywordResearcher.FindTheBestPredictSentence(count, questionKeyword, predictSentenceAllPage);
                    count = (sentences.Count != 0) ? 0 : --count;
                }
            }
            return sentences;
            
        }

        public string DisplaySummarize()
        {
            string result = "------------------------WORD COUNT-------------------\n";
            string document = BuildTheDocument();
            string[] allWords = KeywordResearcher.splitTheWordInString(document);

            Dictionary<string,int> wordCount = KeywordResearcher.WordsCount(allWords);
            foreach (KeyValuePair<string, int> d in wordCount)
            {
                result += d.Key + " : " + d.Value + " \n";
            }
            result += "\n------------------QUESTION KEYWORD--------------------------------\n";
            string[] questionKeyword = KeywordResearcher.GetKeywordArray(PageList[0].Question);
            foreach (string question in questionKeyword)
            {
                result += "question keyword : "+ question + "\n";
            }
            result += "\n----------------------SENTENCES--------------------------------------\n";
            List<string> predictSentenceAllPage = CollectAllPredictSentenceAllPages();
            foreach (string sentence in predictSentenceAllPage)
            {
                result += sentence + "\n***************\n";
            }
            result += "\n--------------------------sentence analysis----------------\n";
            List<string> sentences = FindBestPredictSentenceAllPages();
            foreach (string sen in sentences)
            {
                result += sen + "\n********\n";
            }
            return result;
        }

        public string QuestionWordCombine(int numberOfWord, string[] keyword)
        {
            string result = "";
            if (numberOfWord <= keyword.Length)
            {
                for (int i = 0; i < numberOfWord; i++)
                {
                    result += keyword[i] + " ";
                }
            }

            return result;
            
        }
        public List<string> QuestionWordConbime2(string question)
        {
            CombinationAndPermutation cp = new CombinationAndPermutation();
            return cp.GetCombinationAndPermutationOfSentence(question);
        }
        public List<string> SentencesContainKeyCombine(string key, List<string> sentences)
        {
            List<string> list = new List<string>();
            foreach (string sentence in sentences)
            {
                if (Regex.IsMatch(sentence, key, RegexOptions.IgnoreCase))
                {
                    list.Add(sentence);
                }
            }
            return list;
        }
    }
}
