using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Task_2.Classes;

namespace Task_2
{
    class Program
    {
        public static void OutputByWordsCount(Text text)
        {
            Dictionary<Sentence, int> sentenceDictionary = text.Sentences.ToDictionary(sentence => sentence, sentence => sentence.WordList.Count);
            foreach (var sentence in sentenceDictionary.OrderBy(x => x.Value))
            {
                List<Word> output = sentence.Key.WordList.ToList();
                foreach (var word in output)
                {
                    Console.Write(word.Value + " ");
                }
                Console.WriteLine();
            }
        }

        public static void OutputWordsFromQuestions(Text text, int length)
        {
            List<Sentence> questionSentences = (from sentence in text.Sentences from item in sentence.PunctuationList where item.Value.Keys.Contains("?") || item.Value.Keys.Contains("?!") select sentence).ToList();
            List<String> wordsToPrint = new List<String>();

            foreach (var sentence in questionSentences)
            {
                wordsToPrint.AddRange(from item in sentence.WordList where item.Length == length select item.Value);
                wordsToPrint = wordsToPrint.Distinct().ToList();
               
            }
            Console.WriteLine();
            foreach (var word in wordsToPrint)
            {
                Console.Write(word + " ");
            }
            
        }

        public static void DeleteWordsFromText(Text text, int length, char beginChar)
        {
            List<Word> wordsToDelete = (from sentence in text.Sentences from word in sentence.WordList where word.Length == length && word.Value[0] == beginChar select word).ToList();
            foreach (var sentence in text.Sentences)
            {
                foreach (var word in wordsToDelete)
                {
                    if (sentence.WordList.Contains(word))
                    {
                        sentence.WordList.Remove(word);
                    }
                }

                foreach (var word in sentence.WordList)
                {
                    Console.Write(word.Value + " ");
                }
                Console.WriteLine();
            }
           
        }

        public static void ChangeWordsToString(Text text, int sentenceNumber, int length, string substring)
        {
            Sentence sentenceToChange = text.Sentences[sentenceNumber-1];
            foreach (var word in sentenceToChange.WordList.Where(word => word.Length == length))
            {
                word.Value = substring;
            }

            foreach (var word in sentenceToChange.WordList)
            {
                Console.Write(word.Value + " ");
            }
        }

        static void Main(string[] args)
        {
            StreamReader file = new StreamReader(@"C:\Users\Alexandr\Desktop\ex.txt", Encoding.Default);
            Parser parserTest = new Parser();
            Text textTest = parserTest.Parse(file);

            OutputByWordsCount(textTest);
            Console.WriteLine("-------------");

            OutputWordsFromQuestions(textTest, 7);
            Console.WriteLine("");
            Console.WriteLine("-------------");

            DeleteWordsFromText(textTest, 8, 'D');
            Console.WriteLine("");
            Console.WriteLine("-------------");

            ChangeWordsToString(textTest, 2, 8, "New string");
            Console.ReadKey();
        }
    }
}
