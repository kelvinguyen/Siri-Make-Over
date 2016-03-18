using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Capstone1.Metric
{
    public class ContentComparationMetric
    {
        

        public string IsContentMatching(List<string> result, string answer)
        {
            List<string> answerCombination = new CombinationAndPermutation().GetCombinationAndPermutationOfSentence(answer);
            answerCombination.Sort((x, y) => y.Length.CompareTo(x.Length));
            
            string r = PointSystemCalculation(result, answerCombination, CalculatePoint(answerCombination[0]));
            //foreach (var x in answerCombination)
            //{
            //    r += x + "\n";
            
            
            //}
            return r;
        }

        public bool SentencesContainKeyCombine(string key, string sentences)
        {
            return (Regex.IsMatch(sentences, key, RegexOptions.IgnoreCase));
        }

        public int CalculatePoint(string key)
        {
            return key.Split(' ').Length;
        }
        public string PointSystemCalculation(List<string> summary , List<string> keys, int maxPoint)
        {
            string result = "";
            int totalPoint = PerfectPoint(maxPoint);
            Dictionary<int, int> pointSystem = new Dictionary<int, int>();
            for (int i = 0; i < summary.Count; i++ )
            {
                int point = 0;
                result +="sentence "+ i+": \n";
                foreach (string key in keys)
                {
                    if (SentencesContainKeyCombine(key, summary[i]))
                    {
                        //calculate the point of that key
                        int currentPoint = CalculatePoint(key);
                        result +="\t"+ key + " = " + currentPoint+" ;\n ";
                        point += (currentPoint==maxPoint)?currentPoint*100: currentPoint;
                    }
                }
                result += "\tThe total = " + point + "/" + totalPoint;
                string determine = (double)point / totalPoint * 100.0 + "%";
                result += "\tAccuratcy : " + determine+" \n";
                pointSystem.Add(i, point);
            }
            //pointSystem = pointSystem.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            ////KeyValuePair<int,int> test = pointSystem.Take(1);
            
            //foreach (var kvp in pointSystem)
            //{
            //    result += kvp.Key + " : " + kvp.Value + "\n";
            //}
            return result;
        }

        public int PerfectPoint(int maxPoint)
        {
            int total = maxPoint;
            for (int i = 2; i <= maxPoint; i++)
            {
                total += i;
            }
            return total;
        }
    }
}
