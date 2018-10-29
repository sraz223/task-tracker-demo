using HelloWorld.Data.Connection;
using HelloWorld.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloWorld.Data.Repos
{
    public interface IMessageRepo
    {
        Task<IEnumerable<Message>> GetMessages();
    }

    public class MessageStaticRepo : IMessageRepo
    {
        public async Task<IEnumerable<Message>> GetMessages()
        {
            return new List<Message> { new Message { Text = "Hello World" } };
        }
    }

    public class MessageDbRepo : IMessageRepo
    {
        private readonly IDbContext _context;

        public MessageDbRepo(IDbContext context) { _context = context; }

        public async Task<IEnumerable<Message>> GetMessages()
        {
            var messages = await _context.GetManyAsync<Message>("HelloWorld_qryAllMessages");
            return messages;            
        }
    }
}
