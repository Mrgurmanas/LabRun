using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ProjectClient.Class.Command
{
    
    class PlayerController
    {

        private List<ICommand> list = new List<ICommand>();
        public List<ICommand> getList()
        {
            return list;
        }
        public void setList(List<ICommand> list)
        {
            this.list = list;
        }
        public void run(ICommand cmd)
        {
            list.Add(cmd);
            cmd.execute();
        }
        public void undo()
        {
            int index = list.Count;
            ICommand cmd = list[index - 1];
            list.RemoveAt(index-1);


            
            cmd.undo();
        }
        
    }

    
}
