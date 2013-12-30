using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAnalytics.UTIL
{
    public enum WordOrigin { Removed, Old, New, Replaced }

    /// <summary>
    /// Utility Layer class for XML parsing
    /// </summary>
    public class Parser
    {
        public List<ParsedWord> Comparer(String originalText, String textToCompare)
        {
            originalText = RemoveSpecialChars(originalText);
            textToCompare = RemoveSpecialChars(textToCompare);

            List<ParsedWord> final = new List<ParsedWord>();

            List<String> original = new List<String>(originalText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            List<String> modified = new List<String>(textToCompare.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            Int32 startIndex = 0, tmpIndex = 0;
            string currentWord = string.Empty;

            for (Int32 i = 0; i < original.Count; i++)
            {
                if (modified.Count <= 0) { break; }

                tmpIndex = modified.IndexOf(original[i], startIndex);

                // This means the word already exists in the modified
                if (tmpIndex == startIndex)
                {
                    final.Add(new ParsedWord(original[i], WordOrigin.Old));
                    startIndex++;
                    if (startIndex == modified.Count) { break; }
                }
                else
                {
                    // This means that we found 2 of the same words next to each other
                    // So lets assume this is where the old text picks up again
                    if (tmpIndex != -1)
                    {
                        // Insert all the words in the second string up to the place
                        // where we found the similar word that was in the original string.
                        for (Int32 x = startIndex; x < tmpIndex; x++)
                        {
                            final.Add(new ParsedWord(modified[x], WordOrigin.New));
                        }
                        // This is the first old word we found
                        final.Add(new ParsedWord(modified[tmpIndex], WordOrigin.Old));

                        // Update the new position in the second string
                        startIndex = tmpIndex + 1;
                    }
                    else
                    {
                        final.Add(new ParsedWord(original[i], WordOrigin.Removed));
                        final.Add(new ParsedWord(modified[startIndex], WordOrigin.Replaced));
                        startIndex++;
                    }
                    if (startIndex == modified.Count) { break; }
                }
            }

            if (modified.Count > startIndex)
            {
                for (Int32 c = startIndex; c < modified.Count; c++)
                {
                    final.Add(new ParsedWord(modified[c], WordOrigin.New));
                }
            }
            if (original.Count > startIndex)
            {
                for (Int32 c = startIndex; c < original.Count; c++)
                {
                    final.Add(new ParsedWord(original[c], WordOrigin.Removed));
                }
            }
            return final;
        }

        private string RemoveSpecialChars(string data)
        {
            //data = data.Replace("'", " ").Replace(",", " ").Replace("!", " ").Replace("@", " ").Replace("#", " ").Replace("$", " ").Replace("%", " ").Replace("^", " ");
            //data = data.Replace("&", " ").Replace("*", " ").Replace("(", " ").Replace(")", " ").Replace(";", " ").Replace(":", " ").Replace(".", " ").Replace(",", " ");
            data = data.Replace(".", " ");
            return data;
        }
    }

    public class ParsedWord
    {
        public ParsedWord(String word, WordOrigin origin)
        {
            Word = word;
            Origin = origin;
        }

        public String Word;

        public WordOrigin Origin;
    }
}
