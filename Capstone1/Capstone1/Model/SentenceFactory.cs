using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Capstone1.Model
{
    /**
     * This class to take in data and return the list of sentences
     */
    public class SentenceFactory
    {
        
        public static List<string> AddContent(string data)
        {
            List<string> sentences = new List<string>();
            string CompleteSentences = "";
            string word = @".*?\s";
            MatchCollection m1 = Regex.Matches(data, word, RegexOptions.Singleline);
            foreach (Match w in m1)
            {
                string strWord = w.Value;
                int length = strWord.Length;
                if (length > 2 && (strWord[length - 2] == '.' || strWord[length - 2] == '?' || strWord[length - 2] == '!'))
                {
                    CompleteSentences += strWord;
                    sentences.Add(CompleteSentences);
                    CompleteSentences = "";
                }
                else
                {
                    CompleteSentences += strWord;
                }    
            }
           // sentences.Add("\n$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$\n");
            return sentences;
        }
    }
}
