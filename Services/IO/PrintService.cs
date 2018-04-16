using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Services.IO
{
    class PrintService : IService
    {
        public void Deinit()
        {
            // Nothing to do here
        }

        public ServiceMessage Send(ServiceMessage message)
        {
            if (!message.Type.Equals(typeof(String)))
            {
                throw new WrongMessageTypeException("PrintService: Wrong message type: "+message.Type);
            }

            Handle(message.Message);

            return null;
            
        }

        private void Handle(object messageObject)
        {
            Console.WriteLine(messageObject);
        }

        public void Init()
        {
            // Nothing to do here
        }
    }
}
