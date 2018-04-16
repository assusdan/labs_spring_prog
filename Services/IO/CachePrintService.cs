using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Services.IO
{
    class PrintToCacheStringService : IService
    {

        private String cache = null;
        public void Deinit()
        {
            cache = null;
        }

        public ServiceMessage Send(ServiceMessage message)
        {
            if (!message.Type.Equals(typeof(String)) && !message.Type.Equals(typeof(PrintRequest)))
            {
                throw new WrongMessageTypeException("CachePrintService: Wrong message type: "+message.Type);
            }

            if (message.Type.Equals(typeof(String)))
            {
                Handle(message.Message);
                return null;
            }
            else
            {
                return new ServiceMessage(typeof(String), cache);
            }

        }

        private void Handle(object messageObject)
        {
            cache += messageObject + "\n";
        }

        public void Init()
        {
            cache = "";
        }
    }
}
