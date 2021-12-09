using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Command
{
     abstract class ICommand
    {

        public abstract void execute();

        protected Player target;
        public abstract void undo();
        
        public ICommand(Player player):base()
        {
            this.target = player;

        }
        public ICommand()
        {

        }
    }
}
