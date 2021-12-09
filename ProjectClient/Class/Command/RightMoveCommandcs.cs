using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Command
{
    class RightMoveCommandcs : ICommand
    {
        public override void execute()
        {
            target.Move(1);
        }
        public RightMoveCommandcs(Player player) : base(player)
        {

        }
        public override void undo()
        {

        }
    }
}
