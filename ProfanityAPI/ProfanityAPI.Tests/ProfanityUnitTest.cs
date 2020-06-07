using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProfanityAPI.Controllers;

namespace ProfanityAPI.Tests
{
    [TestClass]
    public class ProfanityUnitTest
    {
        [TestMethod]
        public void APIPostTest_ExpectsError()
        {
            // Arrange
            var controller = new ProfanityController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.Post();
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            //Assert.IsNotNull(response);
        }
    }
}
