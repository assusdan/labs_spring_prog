using Labs.Model.UI;
using Labs.Services.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Labs.Model.UI.MenuModel;

namespace Labs.Services.Model
{
    class ModelService : IService
    {
        public IService printService { private get; set; }

        CatalogModel catalog;
        public void Deinit()
        {
            catalog = null;
        }

        public void Init()
        {
            catalog = new CatalogModel();
            printService = ServicesManager.Instance.GetService(ServiceTypes.Printer);

            // DEBUG
            catalog.Add("Twisted Sister", "I Wanna Rock");
            catalog.Add("Eduard Khil", "I Am So Happy to Finally Be Back Home");
        }

        public ServiceMessage Send(ServiceMessage message)
        {

            if (!message.Type.Equals(typeof(String)) && !message.Type.Equals(typeof(PrintRequest)) && !message.Type.Equals(typeof(MenuCommand)))
            {
                throw new WrongMessageTypeException("ServiceMessage: Wrong message type: " + message.Type);
            }

            if (message.Type.Equals(typeof(MenuCommand)))
            {
                String query = (String)(((MenuCommand)message.Message).Name);
                switch (query)
                {
                    case "list":
                        catalog.SearchAll();
                        break;
                    case "search author":
                        catalog.SearchByAuthorName((String)((MenuCommand)message.Message).Arg);
                        break;
                    case "search name":
                        catalog.SearchByTrackName((String)((MenuCommand)message.Message).Arg);
                        break;
                    case "add":
                        catalog.Add(((MenuCommand)message.Message).Arg, ((MenuCommand)message.Message).Arg);
                        break;
                    case "del":
                        catalog.Remove(((MenuCommand)message.Message).Arg, ((MenuCommand)message.Message).Arg);
                        break;

                }
            }


            if (message.Type.Equals(typeof(PrintRequest)))
            {
                printService.Send(new ServiceMessage(typeof(String), message.Message));
            }

            return null;
        }
    }
}
