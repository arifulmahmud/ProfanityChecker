using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace ProfanityAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProfanityController : ApiController
    {
        /// <summary>This method handles the post request to the API controller,
        /// Utilizes ProfanityFilter class to filter bad words from the provided file,
        /// Only text file is supported at this moment.
        /// Returns response as HttpResponseMessage, after checking the provided file.
        /// </summary>
        public HttpResponseMessage Post()
        {
            int iUploadedCount = 0;
            try
            {
                string responseText = "";
                HttpFileCollection fileCollection = HttpContext.Current.Request.Files;
                // check if the request contained any file (File Count)
                if (fileCollection.Count > 0)
                {
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
                                //System.Diagnostics.Debug.Write(oFileContent);

                                // initiate profinity checker class and check the file content

                                ProfanityFilter oProfinity = new ProfanityFilter();
                                oProfinity.CheckProfanity(oFileContent, out bool hasBadWords, out Int32 badWordCount);
                                if (hasBadWords)
                                {
                                    responseText = postedFile.FileName + ", file contains " + badWordCount + " bad word or phrase !!";
                                }
                                else
                                {
                                    responseText = postedFile.FileName + ", file is clean";
                                }
                                iUploadedCount += 1;
                            }
                            else
                            {
                                responseText = "The uploded file has no content, empty!";
                            }
                        }
                        else
                        {
                            responseText = "Only text file is supported now";
                        }
                    }
                }
                else
                {
                    responseText = "No File is posted. Please add file in request body";
                }
                //retunrn the response with formated string
                var response = Request.CreateResponse(HttpStatusCode.OK, responseText);
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        /// <summary>This method handles the file posted to API controller,
        /// Extracts the raw content of the file, ie. text file's content.
        /// Utilizes .NET framework StreamReader() for extracting the content.
        /// Returns oTextContent, string.
        /// </summary>
        /// <param name="postedFile">HttpPostedFile as parameter</param>
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
