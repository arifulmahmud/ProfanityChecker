using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfanityAPI
{
    public class ProfanityFilter
    {
        public bool CheckProfanity(string checkStr)
        {
            bool hasBadWords = false;
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
                        hasBadWords = true;
                        break;
                    }
                }
                // IF FLAG IS SET, BREAK OUT OF OUTER LOOP
                if (hasBadWords == true) break;
            }

            return hasBadWords;
        }
        private string[] ProfanityArray()
        {
            string[] words = { "Fuck", "Ass", "Asshole", "Bastard", "Bitch",
                             "Cock", "Dick","Cunt", "Piss", "Shit" };
            return words;
        }
    }
}