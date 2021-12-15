using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ProjectClient.Class.Command
{
    
    class PlayerController
    {

        private List<ICommand> list = new List<ICommand>();
        public List<ICommand> GetList()
        {
            return list;
        }
        public void SetList(List<ICommand> list)
        {
            this.list = list;
        }
        public void Run(ICommand cmd)
        {
            list.Add(cmd);
            cmd.Execute();
        }
        public void Undo()
        {
            int index = list.Count;
            ICommand cmd = list[index - 1];
            list.RemoveAt(index-1);


            
            cmd.Undo();
        }
        
    }

    
}
