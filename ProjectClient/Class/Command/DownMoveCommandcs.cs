using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Command
{
    class DownMoveCommandcs : ICommand
    {
        public DownMoveCommandcs(Player player) :base(player)
        {
            
        }
        public override void Execute()
        {
            
        }
        public override void Undo()
        {
            target.Move("down");
        }
    }
}
