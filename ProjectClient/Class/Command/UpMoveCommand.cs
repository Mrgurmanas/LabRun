using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Command
{
    class UpMoveCommand : ICommand
    {
        public override void Execute()
        {
            
        }
        public UpMoveCommand(Player player) : base(player)
        {

        }
        public override void Undo()
        {
            target.Move("up");
        }
    }
}
