using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Command
{
    class RightMoveCommandcs : ICommand
    {
        public override void Execute()
        {
            
        }
        public RightMoveCommandcs(Player player) : base(player)
        {

        }
        public override void Undo()
        {
            target.Move("right");
        }
    }
}
