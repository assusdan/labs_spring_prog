using Labs.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Labs.Model.UI.MenuModel;

namespace Labs.Services.IO
{


    class MenuService : IService
    {
        private MenuModel MenuModel = null;
        private IService printService = null;
        private MenuCommand command = null;

        public void Deinit()
        {
        }

        public ServiceMessage Send(ServiceMessage message)
        {
            if (!message.Type.Equals(typeof(PrintRequest)) && !message.Type.Equals(typeof(String)))
            {
                throw new WrongMessageTypeException("MenuService: Wrong message type: " + message.Type);
            }

            if (message.Type.Equals(typeof(String)))
                handle((String)message.Message);

            if (message.Type.Equals(typeof(PrintRequest)))
                printService.Send(new ServiceMessage(typeof(String), message.Message));

            return null;
            
        }

        private void handle(String metadata)
        {
            if (Program.CurrentState == Program.State.Running)
            {
                if ("quit".Equals(metadata)) 
                {
                    Console.WriteLine("Quitting...");
                    Program.CurrentState = Program.State.Deiniting;
                    ServicesManager.Instance.GetService(ServiceTypes.Reader).Deinit();
                    return;
                }

                if ((command = MenuModel.getValidCommand(metadata)) != null)
                {
                    Program.CurrentState = Program.State.Processing;
                    if (!command.NeedArgs())
                    {
                        command.Run();
                        printService.Send(new ServiceMessage(typeof(String), command.FinalMessage));
                        Program.CurrentState = Program.State.Running;
                    }
                    else
                    {
                        printService.Send(new ServiceMessage(typeof(String), command.PrintArgInfo()));
                    }
                }
                else
                {
                    SendGreetings();
                }

            }
            else
            {
                if (command.NeedArgs())
                {

                    command.AddArg(metadata);
                    if (!command.NeedArgs())
                    {
                        command.Run();
                        printService.Send(new ServiceMessage(typeof(String), command.FinalMessage));
                        Program.CurrentState = Program.State.Running;
                    }
                    else
                    {
                        printService.Send(new ServiceMessage(typeof(String), command.PrintArgInfo()));
                    }
                }
            }
            
        }

        public void Init()
        {
            MenuModel = new MenuModel();
            printService = ServicesManager.Instance.GetService(ServiceTypes.Printer);
            SendGreetings();
        }


        private void SendGreetings()
        {
            printService.Send(new ServiceMessage(typeof(String), MenuModel.Greetings));
        }
    }
}
