using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace ProfanityAPI.Controllers
{
    public class ProfanityController : ApiController
    {
        public string ProfanityChecker()
        {
            int iUploadedCount = 0;
            try
            {
                string responseText = "";
                string phyUploadPath = System.Web.Hosting.HostingEnvironment.MapPath("/TempFiles/");
                HttpFileCollection fileCollection = HttpContext.Current.Request.Files;
                // check the file count
                for (int fCount = 0; fCount <= fileCollection.Count - 1; fCount++)
                {
                    HttpPostedFile postedFile = fileCollection[fCount];

                    //checking if the uploaded file's extension, only text format is supported for now !!!
                    if (Path.GetExtension(postedFile.FileName) == ".txt")
                    {
                        if (postedFile.ContentLength > 0)
                        {
                            // get the file text content via GetPostedFileContent method 

                            string oFileContent = GetPostedFileContent(postedFile);
                            System.Diagnostics.Debug.Write(oFileContent);

                            // initiate profinity checker class and check the file content
                            bool hasBadWords;
                            Int32 badWordCount;
                            ProfanityFilter oProfinity = new ProfanityFilter();
                            oProfinity.CheckProfanity(oFileContent, out hasBadWords, out badWordCount);
                            if (hasBadWords)
                            {
                                System.Diagnostics.Debug.WriteLine(postedFile.FileName + " File contains bad words, Word Count: " + badWordCount);
                                responseText = postedFile.FileName + " File contains "+ badWordCount + " bad words !!!!!";
                            }
                            else
                            {
                                responseText = postedFile.FileName + " File is clean";
                                System.Diagnostics.Debug.WriteLine(postedFile.FileName + " File is clean");
                            }
                            iUploadedCount += 1;
                        }
                        else
                        {
                            responseText = "Can't upload file with no content";
                        }
                    }
                    else
                    {
                        responseText = "Only text file is supported now";
                    }
                }
                //retunrn the response with formated string
                return responseText;
            }
            catch (Exception ex)
            {
                return "Error, Description : " + ex.ToString();
            }
        }

        private string GetPostedFileContent(HttpPostedFile postedFile)
        {
            string oTextContent;
            // read the file content using posted file input stream
            using (var strmReader = new StreamReader(postedFile.InputStream, Encoding.UTF8))
            {
                oTextContent = strmReader.ReadToEnd();
            }

            // retunrs the whole text contenet of the file.
            return oTextContent;
        }
    }
}
