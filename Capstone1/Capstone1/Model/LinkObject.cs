using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone1.Model
{
    public class LinkObject
    {
        public List<string> Link { get; set; }
        public List<string> Cite { get; set; }

        public List<string> OptimizeLink()
        {
            List<string> optLink = new List<string>();
            string http = "http://";
            string https = "https://";
            for (int i = 0; i < Cite.Count;i++ )
            {
                if (!Cite[i].Contains(http) && !Cite[i].Contains(https))
                {
                    Cite[i] = http + Cite[i];
                }

                if (Cite[i].Contains(".") && !Cite[i].Contains("...") && !Cite[i].Contains("/_/"))
                {
                    optLink.Add(Cite[i]);
                }
                //if (Link.Contains(http))
                //{
                //    Link.Remove(c);
                //}
               
            }
            for (int i = 0; i < Link.Count;i++ )
            {
                if (!Link[i].Contains(http) && !Link[i].Contains(https))
                {
                    Link[i] = http + Link[i];
                }
                if (Link[i].Contains(".") && !Link[i].Contains("...") && !Link[i].Contains("/_/"))
                {
                    optLink.Add(Link[i]);
                }
               
            }
            return optLink;
        }
    }
}
