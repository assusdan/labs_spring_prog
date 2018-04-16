using System;

namespace Labs.Services
{
    public class ServiceMessage
    {

        public Type Type;

        public Object Message;

        public ServiceMessage(Type type, object message)
        {
            Type = type;
            Message = message;
        }
    }
}