using System.Collections;
using System.Collections.Generic;

namespace Task_2.Classes
{
    public class Sentence
    {
        public ICollection<Word> WordList { set; get; }
        public ICollection<Punctuation> PunctuationList { set; get; }
    }
}