using Labs.Services;
using Labs.Services.IO;
using Labs.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs
{
    class Program
    {

        public enum State 
        {
            Initing,
            Running,
            Processing,
            Waiting,
            Deiniting,

        }

        public static State CurrentState = State.Initing;

        static void Main(string[] args)
        {

            ServicesManager.Instance.RegisterService(ServiceTypes.Printer, new PrintService());
            ServicesManager.Instance.RegisterService(ServiceTypes.Model, new ModelService());
            
            // Main service depends on Menu service!
            ServicesManager.Instance.RegisterService(ServiceTypes.Menu, new MenuService());

            MainService mainService = new MainService();
            mainService.StartWaiting();
            ServicesManager.Instance.RegisterService(ServiceTypes.Reader, mainService);

            CurrentState = State.Running;

        }
    }
}
