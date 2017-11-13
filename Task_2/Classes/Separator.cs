using System.Collections.Generic;
using System.Linq;

namespace Task_2.Classes
{
    public class Separator
    {
        public readonly string[] sentenceSeparators = new string[] { "?!", "?", "...", "!", "." };
        public readonly string[] wordSeparators = new string[] { " - ", " ", ":", "," };
        public readonly string[] allSeparators = new string[] { "?!", " - ", "...", " ", ":", ",", "?", "!", "." };
        

        public IEnumerable<string> SentenceSeparators()
        {
            return sentenceSeparators.AsEnumerable();
        }

        public IEnumerable<string> WordSeparators()
        {
            return wordSeparators.AsEnumerable();
        }

        public IEnumerable<string> AllSeparators()
        {
            return allSeparators.AsEnumerable();
        }
    }
}