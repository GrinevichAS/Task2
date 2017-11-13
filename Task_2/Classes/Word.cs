using System;

namespace Task_2.Classes
{
    public class Word 
    {
        public string Value { set; get; }

        public int Length => this.Value.Length;
    }
}