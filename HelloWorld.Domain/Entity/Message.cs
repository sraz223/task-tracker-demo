using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Domain.Entity
{
    public class Message
    {
        public string Text { get; set; }

        public override bool Equals(object obj)
        {
            return ((Message)(obj)).Text == Text;
        }
    }
}
