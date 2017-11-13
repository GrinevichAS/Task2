using System.Collections.Generic;

namespace Task_2.Classes
{
    public class Text
    {
        public IList<Sentence> Sentences { get; set; }

        public Text()
        {
            Sentences = new List<Sentence>();
        }
    }
}