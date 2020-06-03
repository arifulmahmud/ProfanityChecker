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
        public string Checker()
        {
            int iUploadedCount = 0;
            try
            {
                string responseText = "";
                string phyUploadPath = System.Web.Hosting.HostingEnvironment.MapPath("/Uploaded_Files/");
                HttpFileCollection fileCollection = HttpContext.Current.Request.Files;
                // check the file count
                for (int fCount = 0; fCount <= fileCollection.Count - 1; fCount++)
                {
                    HttpPostedFile postedFile = fileCollection[fCount];
                    //checking if the uploaded file's extenion is text format, only supported format
                    if (Path.GetExtension(postedFile.FileName) == ".txt")
                    {
                        if (postedFile.ContentLength > 0)
                        {
                            string fullFilePath = phyUploadPath + Path.GetFileName(postedFile.FileName);
                            postedFile.SaveAs(fullFilePath);
                            iUploadedCount += 1;
                            responseText = "Successfully uploaded " + iUploadedCount + " file";

                            // implement the profinity check logic here
                            string oFileContent = GetFileContent(fullFilePath);
                            System.Diagnostics.Debug.Write(oFileContent);

                            // initiate profinity checker class and check the file content
                            ProfanityFilter oProfinity = new ProfanityFilter();
                            if (oProfinity.CheckProfanity(oFileContent))
                            {
                                System.Diagnostics.Debug.WriteLine(postedFile.FileName + " File contains bad words");
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine(postedFile.FileName + " File is clean");
                            }
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

        private string GetFileContent(string fullFilePath)
        {
            string oFileTexts;
            var fileStream = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
            using (var strmReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                oFileTexts = strmReader.ReadToEnd();
            }
            return oFileTexts;
        }
    }
}
