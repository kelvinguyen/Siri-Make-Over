using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone1.KeywordEngine;
using System.Text.RegularExpressions;

namespace Capstone1.SummaryEngine
{
    public class PageObject
    {
        public string Question { get; set; }
        public string Document { get; set; }
        public List<string> Sentences { get; set; }
        public string Link { get; set; }

        public List<string> Priority1()
        {
            QuestionAnalysis questionFactory = new QuestionAnalysis(Question);
            List<string> priority1 = new List<string>();

            string[] questionKeyword = questionFactory.FindKeywordInQuestion();
            foreach (string s in Sentences)
            {
                if (KeywordResearcher.IsContainAllQuestionKeyword(s, questionKeyword))
                {
                    priority1.Add(s);
                }
            }
            

            return priority1;
        }

        public List<string> Priority2()
        {
            List<string> priority2 = new List<string>();
            QuestionAnalysis questionFactory = new QuestionAnalysis(Question);
            string[] questionKeyword = questionFactory.FindKeywordInQuestion();

            foreach(string s in Sentences)
            {
                if (KeywordResearcher.IsContainAtLeastOneKeyword(s, questionKeyword))
                { 
                    priority2.Add(s);
                }
            }

            return priority2;
        }

        public List<string> FindSummaryContentForthisPage()
        {
            int count = 20;
            List<string> sentences = new List<string>();
            while (count > 0)
            {
                sentences = FindTheBestPredictSentence(count);
                count = (sentences.Count != 0) ? 0 : --count;
            }
     
            return sentences;
        }

        //public bool IsContainAllQuestionKeyword( string sentence, string[] keys)
        //{

        //    return keys.All(x => sentence.ToLower().Contains(x));
        //}

        //public bool IsContainAtLeastOneKeyword(string sentence, string[] keys)
        //{
        //    return keys.Any(w => sentence.ToLower().Contains(w));
        //}

        public string[] SplitWords()
        {
            //string doc = Document.ToLower();
            //doc = KeywordResearcher.FindKeyWord(doc);
            //doc = Regex.Replace(doc, @"[^a-zA-z]", " ");
            //return doc.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
            return KeywordResearcher.splitTheWordInString(Document);
        }

        public Dictionary<string, int> WordsCount()
        {
            //Dictionary<string, int> wordCountDic = new Dictionary<string, int>();
            //string[] allWords = SplitWords();
            //foreach (string word in allWords)
            //{
            //    if (!wordCountDic.ContainsKey(word))
            //    {
            //        wordCountDic.Add(word, 1);
            //    }
            //    else
            //    {
            //        wordCountDic[word] = wordCountDic[word] +1; 
                    
            //    }
            //}
            //wordCountDic = wordCountDic.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            //return wordCountDic;
            string[] allWords = SplitWords();
            return KeywordResearcher.WordsCount(allWords);
        }
        public string[] BestPredictWord(int topNumberOfPredictWords)
        {
            //int topWord = topNumberOfPredictWords;
            //string[] keys = new string[topWord];
            //Dictionary<string, int> wordCount = WordsCount();
            //int count = 0;
            //foreach (KeyValuePair<string, int> word in wordCount.Take(topWord))
            //{
            //    keys[count] = word.Key;
            //    count++;
            //}
            ////IEnumerable<KeyValuePair<string,int>> test = wordCount.Take(5);
            
            //return keys;

            return KeywordResearcher.BestPredictWord(topNumberOfPredictWords, SplitWords());
        }

        public List<string> FindTheBestPredictSentence(int NumberOfWords)
        {
            //int topWord = NumberOfWords;
            //List<string> priority1 = new List<string>();

            //string[] keys = BestPredictWord(topWord);
            //foreach (string s in Sentences)
            //{
            //    if (IsContainAllQuestionKeyword(s, keys))
            //    {
            //        priority1.Add(s + "\nTOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOP: "+ topWord +"The Words : " + DisplayTopWord(keys));
            //    }
            //}

            //return priority1;
            return KeywordResearcher.FindTheBestPredictSentence(NumberOfWords, SplitWords(), Sentences);
        }

        public string DisplaySummary()
        {
            List<string> list = FindSummaryContentForthisPage();
            string result = "";
            foreach (string s in list)
            {
                //result += s + "\n-----------------------------------------------------------------------------\n";
                result += s + "\n";
            }
            return result;
        }

        //public string DisplayTopWord(string[] keys)
        //{
        //    string result = "";
        //    foreach(string s in keys)
        //    {
        //        result += s + " , ";
        //    }
        //    return result;
        //}
    }
}
