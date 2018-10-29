using HelloWorld.Data.Repos;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace HelloWord.Api.Controllers
{
    public class HelloWorldController : ApiController
    {
        private readonly IMessageRepo _messageRepo;

        public HelloWorldController(IMessageRepo messageRepo)
        {
            _messageRepo = messageRepo;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var messages = await _messageRepo.GetMessages();

                return Ok(messages);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }
    }
}
