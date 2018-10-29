using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWord.Api.Controllers;
using HelloWorld.Data.Repos;
using HelloWorld.Domain.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Results;

namespace HelloWorld.Test
{
    [TestClass]
    public class HelloWorldControllerTest
    {
        [TestMethod]
        public void GetAll()
        {
            var repoMoq = new Mock<IMessageRepo>();

            repoMoq.Setup(x => x.GetMessages())
                .Returns( Task.FromResult<IEnumerable<Message>>( new List<Message> { new Message { Text = "hello world" } }) );

            var controller = new HelloWorldController(repoMoq.Object);
            var response = (OkNegotiatedContentResult<IEnumerable<Message>>)(controller.GetAll().Result);
            var messages = response.Content;

            Assert.IsNotNull(response);
            Assert.IsNotNull(messages);
            Assert.IsTrue(messages.Count() > 0);
            Assert.IsTrue(messages.Any(w => w.Text == "hello world"));
            

        }
    }
}
