using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Command
{
    class UnitController
    {
		private List<ICommand> List = new List<ICommand>();

		public List<ICommand> GetList()
		{
			return List;
		}

		public void run(ICommand cmd)
		{ //addCommand
			List.Add(cmd);
			
			cmd.Execute();
		}

		public void Undo()
		{ //removeCommand
			int index = List.Count;
			ICommand cmd = List[index - 1];
			List.RemoveAt(index - 1);
			
			cmd.Undo();
		}
	}
}
