using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.AbstractFactory
{
    class DefaultFactory : AbstractFactory
    {
        public override SpecialWall createSpecialWall()
        {
            return new DefaultSpecialWall();
        }

        public override Spikes createSpikes()
        {
            return new DefaultSpikes();
        }
    }
}
