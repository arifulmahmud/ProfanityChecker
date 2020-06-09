using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace ProfanityAPI
{
    /// <summary>Class <c>ProfanityFilter</c> is the base calss for checking bad words </summary>
    public class ProfanityFilter
    {
        /// <summary>This method checks the raw content of the file for bad words,
        /// </summary>
        /// <param name="checkStr">string, the whole string for checking</param>
        /// <param name="hasBadWords">bool, returns True or False </param>
        /// <param name="badWordCount">Int32, returns the count of bad words </param>
        public void CheckProfanity(string checkStr, out bool hasBadWords, out Int32 badWordCount)
        {
            hasBadWords = false;
            badWordCount = 0;
            List<string> badWordList = new List<string>();

            string[] inStrArray = checkStr.Split(new char[] { ' ' });
            List<string> profanityWords = this.getProfanityArray();
            // Loop through words
            for (int x = 0; x < inStrArray.Length; x++)
            {
                // Loop through profanity words from the list array
                for (int i = 0; i < profanityWords.Count; i++)
                {
                    // if the word is profanity, add to badWordList and increase the counter
                    if (inStrArray[x].ToLower() == profanityWords[i].ToLower())
                    {
                        if (!badWordList.Contains(profanityWords[i]))
                        {
                            badWordList.Add(profanityWords[i]);
                            badWordCount += 1;
                        }
                        // need to continue the process till the last word is checked
                    }
                }
            }
            if (badWordCount > 0)
                hasBadWords = true;
        }

        /// <summary>This method gets the bad words from a json file in project's Assets directory, ProfanityWords.json,
        /// JavaScriptSerializer the content of the json file into List of badWords, List<string>.
        /// Utilizes .NET frameworks JavaScriptSerializer() for extracting the content.
        /// Returns badWords, List<string>.
        /// </summary>
        private List<string> getProfanityArray()
        {
            string jsonFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Assets/ProfanityWords.json");
            string Json = System.IO.File.ReadAllText(jsonFilePath);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<string> badWords = ser.Deserialize<List<string>>(Json);
            return badWords;
        }
    }
}