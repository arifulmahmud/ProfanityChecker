using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfanityAPI
{
    public class ProfanityFilter
    {
        public void CheckProfanity(string checkStr, out bool hasBadWords, out Int32 badWordCount)
        {
            hasBadWords = false;
            badWordCount = 0;
            List<string> badWordList = new List<string>();

            string[] inStrArray = checkStr.Split(new char[] { ' ' });
            string[] words = this.ProfanityArray();
            // LOOP THROUGH WORDS IN MESSAGE
            for (int x = 0; x < inStrArray.Length; x++)
            {
                // LOOP THROUGH PROFANITY WORDS
                for (int i = 0; i < words.Length; i++)
                {
                    // IF WORD IS PROFANITY, SET FLAG AND BREAK OUT OF LOOP
                    //if (inStrArray[x].toString().toLowerCase().equals(words[i]))
                    if (inStrArray[x].ToLower() == words[i].ToLower())
                    {
                        if (!badWordList.Contains(words[i]))
                        {
                            badWordList.Add(words[i]);
                            badWordCount += 1;
                        }
                        //break;
                        // need to continue the process till the last word is checked
                    }
                }
                // IF FLAG IS SET, BREAK OUT OF OUTER LOOP
                // if (hasBadWords == true) break;
            }
            if (badWordCount > 0)
                hasBadWords = true;
        }
        private string[] ProfanityArray()
        {
            string[] words = { "Fuck", "Ass", "Asshole", "Bastard", "Bitch",
                             "Cock", "Dick","Cunt", "Piss", "Shit" };
            return words;
        }
    }
}