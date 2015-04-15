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
            
            foreach(string c in Cite)
            {
                // ignore http://www.
                if (Link.Contains(c))
                {
                    Link.Remove(c);
                }
                optLink.Add(c);
            }
            foreach (string l in Link)
            {
               
                optLink.Add(l);
            }
            return optLink;
        }
    }
}
