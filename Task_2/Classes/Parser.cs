using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Task_2.Classes
{
    public class Parser
    {
        private readonly Separator separatorContainer = new Separator();

        public Text Parse(TextReader reader)
        {
            string currentString = reader.ReadLine();
            int bufferlength = 10000;
            Text textResult = new Text();
            StringBuilder buffer = new StringBuilder(bufferlength);
            var orderedSentenceSeparators = new Separator().SentenceSeparators().OrderByDescending(x => x.Length);
            buffer.Clear();

            while (currentString != null)
            {
                bool read = true;
                while (read)
                {
                    int firstSentenceSeparatorOccurence = -1;
                    string firstSentenceSeparator = null;
                    List<int> sentenceSeparatorsIndexis = new List<int>();
                    List<string> sentenceSeparatorsList = new List<string>();
                    foreach (var separator in orderedSentenceSeparators.Where(separator => currentString.IndexOf(separator) != -1))
                    {
                        sentenceSeparatorsIndexis.Add(currentString.IndexOf(separator));
                        sentenceSeparatorsList.Add(separator);
                    }

                    if (sentenceSeparatorsIndexis.Count > 0)
                    {
                        firstSentenceSeparatorOccurence = sentenceSeparatorsIndexis.Min();
                        firstSentenceSeparator = sentenceSeparatorsList[sentenceSeparatorsIndexis.FindIndex(x => x == firstSentenceSeparatorOccurence)];
                    }

                    if (firstSentenceSeparator != null)
                    {
                        buffer.Append(currentString.Substring(0, firstSentenceSeparatorOccurence + firstSentenceSeparator.Length));
                        textResult.Sentences.Add(this.ParseSentence(buffer.ToString()));
                        buffer.Clear();
                        currentString = currentString.Substring(firstSentenceSeparatorOccurence + firstSentenceSeparator.Length, currentString.Length - (firstSentenceSeparatorOccurence + firstSentenceSeparator.Length));
                    }
                    else
                    {
                        buffer.Append(" ");
                        buffer.Append(currentString);
                        read = false;
                    }
                }
                currentString = reader.ReadLine();
            }
            return textResult;
        }

        protected Sentence ParseSentence(string source)
        {
            string reduceMultiSpace = @"[ ]{2,}";
            source = Regex.Replace(source.Replace("\t", " "), reduceMultiSpace, " ");

            Sentence sentenceResult = new Sentence
            {
                WordList = ParseWords(source),
                PunctuationList = ParseSeparators(source)
            };
            return sentenceResult;
        }

        protected ICollection<Word> ParseWords(string source)
        {
            List<string> splitList = source.Split(separatorContainer.allSeparators, StringSplitOptions.RemoveEmptyEntries).ToList();
            return splitList.Select(item => new Word {Value = item}).ToList();
        }

        protected ICollection<Punctuation> ParseSeparators(string source)
        {
            ICollection<Punctuation> tempPunctuations = new List<Punctuation>();

            foreach (var separator in separatorContainer.WordSeparators())
            {
                var indexes = new List<int>();
                for (int index = 0; ; index += separator.Length)
                {
                    index = source.IndexOf(separator, index);
                    if (index == -1)
                    {
                        break;
                    }
                    indexes.Add(index);
                }
                if (indexes.Count <= 0) continue;
                Punctuation resultPunctuation = new Punctuation
                {
                    Value = new Dictionary<string, int[]> { { separator, indexes.ToArray() } }
                };
                tempPunctuations.Add(resultPunctuation);
            }

            foreach (var separator in separatorContainer.SentenceSeparators())
            {
                int index = source.IndexOf(separator, source.Length - separator.Length);
                if (index == -1) continue;
                int[] indexSentenceSeparator = new[] { index };
                Punctuation resultPunctuation = new Punctuation
                {
                    Value = new Dictionary<string, int[]> {{separator, indexSentenceSeparator}}
                };
                tempPunctuations.Add(resultPunctuation);
                break;
            }

            return tempPunctuations;
        }
    }
}
