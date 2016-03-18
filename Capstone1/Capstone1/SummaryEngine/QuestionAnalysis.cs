using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone1.KeywordEngine;

namespace Capstone1.SummaryEngine
{
    public class QuestionAnalysis
    {
        string question;
        public QuestionAnalysis(string question)
        {
            this.question = question;
        }

        public string[] FindKeywordInQuestion()
        {
            return KeywordResearcher.GetKeywordArray(question);
        }

        public string GetMeFirstWordInQuestion()
        {

            return (question.Contains(' '))? question.Substring(0, question.IndexOf(' ')):question;
        }

        public bool isShortAnswer()
        {
            string[] shortAnswer = new[] { "who", "where", "when", "what", "does", "do", "is", "are", "whose", "why" ,"did","would","may","should","could","can","were","was","have","has"};
            foreach (string s in shortAnswer)
            {
                if (GetMeFirstWordInQuestion().Equals(s, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
