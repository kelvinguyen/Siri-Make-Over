using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone1.Metric
{
    public class CombinationAndPermutation
    {
        public List<string> perList ;

        public List<string> GetCombinationAndPermutationOfSentence(string sentence)
        {
            perList = new List<string>();
            AllCombinationOfAllPermutation(sentence);
            return perList;
        }

        public void Swap(ref string a, ref string b)
        {
            if (a == b) return;
            string temp = "";
            temp = a;
            a = b;
            b = temp;
        }

        public void GetPer(string[] list)
        {
            int x = list.Length - 1;
            GetPer(list, 0, x);
        }

        private void GetPer(string[] list, int k, int m)
        {

            if (k == m)
            {
                string result = "";
                foreach (string word in list)
                {
                    result += word + " ";
                }
                //Console.WriteLine(result);
                perList.Add(result);
            }
            else
                for (int i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    GetPer(list, k + 1, m);
                    Swap(ref list[k], ref list[i]);
                }
        }
        public static List<string> GetCombination(List<string> list)
        {
            List<string> combination = new List<string>();
            double count = Math.Pow(2, list.Count);
            for (int i = 1; i <= count - 1; i++)
            {
                string str = Convert.ToString(i, 2).PadLeft(list.Count, '0');
                string result = "";
                for (int j = 0; j < str.Length; j++)
                {

                    if (str[j] == '1' && j != str.Length - 1)
                    {

                        //Console.Write(list[j]);
                        result += list[j] + " ";
                    }
                    else if (str[j] == '1' && j == str.Length - 1)
                    {
                        result += list[j];
                    }
                }
                combination.Add(result);
                combination.Sort((x,y) => y.Length.CompareTo(x.Length));
                //Console.WriteLine();
            }
            return combination;
        }
        public void AllCombinationOfAllPermutation(string correctAnswer)
        {
            List<string> answer = correctAnswer.Split(' ').ToList<string>();
            List<string> combination = GetCombination(answer);
            foreach (string stringCombine in combination)
            {
                GetPer(stringCombine.TrimEnd().Split(' '));
            }
        }
    }
}
