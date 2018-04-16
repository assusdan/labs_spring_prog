using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Labs.Services.IO
{
    class MainService : IService
    {

        private Thread reader = null;
        private Thread main = null;


        private String answer = null;
        private string cache;

        public void Deinit()
        {
            new Thread(() =>
            {
                while (reader.IsAlive && main.IsAlive)
                {
                    Thread.Sleep(5);

                }
                reader = null;
                main = null;
            });
        }

        public ServiceMessage Send(ServiceMessage message)
        {


            return new ServiceMessage(typeof(String), cache);

        }

        private void Run()
        {
            answer = null;
            cache.Trim('\n');

            lock (cache)
            {
                if (cache.Contains("\n"))
                {
                    String[] inputs = cache.Split('\n');
                    answer = inputs[0];
                    cache = String.Join("\n", inputs.Skip(1).ToArray());
                    return;
                }
                else
                {
                    if (cache != "")
                    {
                        answer = cache;
                        cache = "";
                        return;
                    }
                }
            }
            while (cache == "")
            {
                Thread.Sleep(5);
            }
            lock (cache)
            {
                if (cache.Contains("\n"))
                {
                    String[] inputs = cache.Split('\n');
                    answer = inputs[0];
                    cache = String.Join("\n", inputs.Skip(1).ToArray());
                    return;
                }
                else
                {
                    if (cache != "")
                    {
                        answer = cache;
                        cache = "";
                        return;
                    }
                }
            }


        }

        public void Init()
        {
            cache = "";

            main = new Thread(() =>
            {
                while (!Program.CurrentState.Equals(Program.State.Deiniting))
                {

                    Run();
                    ServicesManager.Instance.GetService(ServiceTypes.Menu).Send(new ServiceMessage(typeof(String), answer));
                }
            }
            );

            main.Start();

        }




        internal void StartWaiting()
        {
            reader = new Thread(() =>
            {

                while (!Program.CurrentState.Equals(Program.State.Deiniting))
                {

                    String readed = Console.ReadLine() + "\n";

                    lock (cache)
                    {
                        cache = cache + readed;
                        cache.Trim('\n');
                    }

                }
            }
            );
            reader.Start();

        }





    }
}
