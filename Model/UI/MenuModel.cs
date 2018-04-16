using Labs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Model.UI
{

    class MenuModel
    {

        private Dictionary<String, MenuCommand> commands = new Dictionary<string, MenuCommand>();
        public MenuModel()
        {
            init();
            Greetings = "    'list' to display all items\n" +
                "    'search author' to search by author's name\n" +
                "    'search name' to search by track's name\n" +
                "    'add' to add new item\n" +
                "    'del' to remove item\n" +
                "    'quit' to quit\n";
        }

        private void init()
        {
            commands.Add("list", new MenuCommand(0, "list", null, ""));

            commands.Add("search author", new MenuCommand(1, "search author", new String[] { "Enter author name" }, "\n"));

            commands.Add("search name", new MenuCommand(1, "search name", new String[] { "Enter track name" }, "\n"));

            commands.Add("add", new MenuCommand(2, "add", new String[] { "Enter track name", "Enter author name" }, "Added successfully!\n"));

            commands.Add("del", new MenuCommand(2, "del", new String[] { "Enter track name", "Enter author name" }, "Deleted if found.\n"));

            commands.Add("quit", new MenuCommand(0, "quit", null, "\n"));

        }

        public object Greetings { get; internal set; }

        internal class MenuCommand
        {
            private int Args;

            private Stack<String> argument = new Stack<String>();

            private String[] Info; 
            public String Name { get; private set; }
            public String FinalMessage { get; private set; }
            public String Arg {
                get
                {
                    return argument.Pop();
                }
                private set { }
            }

            public MenuCommand(int args, String name, String[] info, String finalMessage)
            {
                Args = args;
                Name = name;
                Info = info;
                FinalMessage = finalMessage;
            }

            internal bool NeedArgs()
            {
                return argument.Count < Args;
            }

            internal void Run()
            {
                ServicesManager.Instance.GetService(ServiceTypes.Model).Send(new ServiceMessage(typeof(MenuCommand), this));
                argument.Clear();
            }

            internal String PrintArgInfo()
            {
                return Info[argument.Count];
            }

            internal void AddArg(String arg)
            {
                argument.Push(arg);
            }
        }

        internal MenuCommand getValidCommand(string name)
        {
            MenuCommand target = null;
            commands.TryGetValue(name, out target);
            return target;
        }




    }
}
