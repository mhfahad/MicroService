using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enventbus.Messages.Events
{
    public class BaseEvent
    {
        
        public int Id { get; private set; }
        public DateTime CreatedDate { get; private set; }
    }
}
