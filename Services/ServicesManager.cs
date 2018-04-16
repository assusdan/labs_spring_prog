using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Services
{
    class ServicesManager
    {
        public static ServicesManager Instance { get; private set; } = new ServicesManager();
        private ServicesManager() { }

        private IDictionary<ServiceTypes, IService> services = new Dictionary<ServiceTypes, IService>();

        public void RegisterService(ServiceTypes type, IService service)
        {
            services.Add(type, service);
            service.Init();
        }

        public void UnregisterService(ServiceTypes type)
        {
            services.Remove(type);
        }

        public IService GetService(ServiceTypes type)
        {
            IService service;

            if (services.TryGetValue(type, out service))
                return service;

            throw new ServiceNotFoundException("No such service: "+type.ToString());
        }

    }
};
