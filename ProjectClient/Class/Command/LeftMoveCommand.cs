using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Command
{
    class LeftMoveCommand : ICommand
    {
        public override void Execute()
        {
            target.Move("left");
        }
        public LeftMoveCommand(Player player) : base(player)
        {

        }
        public override void Undo()
        {
            //target.Move();
            
        }
    }
}
