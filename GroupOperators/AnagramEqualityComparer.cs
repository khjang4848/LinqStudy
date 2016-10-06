using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupOperators
{
    internal class AnagramEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return getCononicalString(x) == getCononicalString(y);
        }

        public int GetHashCode(string obj)
        {
            return getCononicalString(obj).GetHashCode();
        }


        private string getCononicalString(string word)
        {
            char[] wordChars = word.ToCharArray();
            Array.Sort<char>(wordChars);


            return new string(wordChars);
        }
    }
}
