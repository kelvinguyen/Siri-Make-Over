using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Capstone1.KeywordEngine
{
    public static class KeywordResearcher
    {

        public static string FindKeyWord(string sentence)
        {
            string regex = @"\bis\b|\ba\b|\bthe\b|\bdoes\b|\bthat\b|\bthis\b|\ban\b|\band\b|\bor\b|\bwith\b|\bif\b|\bin\b|\bout\b|\bbetween\b|\bwas\b|\bwere\b|\bto\b|\bsuch\b|\baren't\b|\bisn't\b|\bweren't\b|\bwasn't\b|\bon\b\|\bwould\b|\bshould\b|\bwe\b|\byou\b|\bthem\b|\bus\b|\bour\b|\btheir\b|\btheirs\b|\bmaybe\b|\bcan\b|\bcould\b|\bin\b|\bon\b|\babove\b|\bat\b|\bfor\b|\bonly\b|\bare\b|\bas\b|\bwill\b|\bbout\b|\babout\b|\bwithout\b|\bneed\b|\bthese\b|\bthose\b|\bthen\b|\bso\b|\bhow\b|\bwhere\b|\bwhy\b|\bwhen\b|\bwhat\b|\bwho\b|\bwhose\b|\bwhom\b|\bour\b|\byour\b|\byours\b|\bhis\b|\bhe\b|\bshe\b|\bher\b|[!?.,;"":]";
            string keyword = Regex.Replace(sentence.ToLower(), regex, "");
            //string[] keywords = keyword.Split(' ');
            return keyword;
        }

        public static string[] GetKeywordArray(string sentence)
        { 
            string keyword = FindKeyWord(sentence);

           // string[] word = keyword.Split(' ');
            List<string> list = keyword.Split(' ').ToList<string>();
            list.RemoveAll(p => string.IsNullOrEmpty(p));
            return list.ToArray();
        }

        public static string[] splitTheWordInString(string document)
        {
            string doc = document.ToLower();
            doc = KeywordResearcher.FindKeyWord(doc);
            doc = Regex.Replace(doc, @"[^a-zA-z]", " ");
            return doc.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static Dictionary<string, int> WordsCount(string[] allWords)
        {
            Dictionary<string, int> wordCountDic = new Dictionary<string, int>();
            //string[] allWords = SplitWords();
            foreach (string word in allWords)
            {
                if (!wordCountDic.ContainsKey(word))
                {
                    wordCountDic.Add(word, 1);
                }
                else
                {
                    wordCountDic[word] = wordCountDic[word] + 1;

                }
            }
            wordCountDic = wordCountDic.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return wordCountDic;
        }

        public static string[] BestPredictWord(int topNumberOfPredictWords, string[] allWords)
        {
            int topWord = topNumberOfPredictWords;
            string[] keys = new string[topWord];
            Dictionary<string, int> wordCount = WordsCount(allWords);
            int count = 0;
            foreach (KeyValuePair<string, int> word in wordCount.Take(topWord))
            {
                keys[count] = word.Key;
                count++;
            }
            //IEnumerable<KeyValuePair<string,int>> test = wordCount.Take(5);

            return keys;
        }

        public static List<string> FindTheBestPredictSentence(int NumberOfWords, string[] allWords, List<string> sentences)
        {
            int topWord = NumberOfWords;
            List<string> priority1 = new List<string>();

            string[] keys = BestPredictWord(topWord,allWords);
            foreach (string s in sentences)
            {
                if (IsContainAllQuestionKeyword(s, keys))
                {
                   // priority1.Add(s + "\nTOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOP: " + topWord + "The Words : " + DisplayTopWord(keys));
                    priority1.Add(s);
                }
            }

            return priority1;
        }

        public static bool IsContainAllQuestionKeyword(string sentence, string[] keys)
        {

            return keys.All(x =>x!= null && sentence.ToLower().Contains(x));
        }

        public static bool IsContainAtLeastOneKeyword(string sentence, string[] keys)
        {
            return keys.Any(w => sentence.ToLower().Contains(w));
        }

       
        public static string DisplayTopWord(string[] keys)
        {
            string result = "";
            foreach (string s in keys)
            {
                result += s + " , ";
            }
            return result;
        }

    }
}
