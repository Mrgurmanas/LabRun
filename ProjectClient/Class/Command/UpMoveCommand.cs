using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Command
{
    class UpMoveCommand : ICommand
    {
        public override void execute()
        {
            target.Move(0);
        }
        public UpMoveCommand(Player player) : base(player)
        {

        }
        public override void undo()
        {

        }
    }
}
