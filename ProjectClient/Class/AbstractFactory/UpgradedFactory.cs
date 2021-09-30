using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.AbstractFactory
{
    class UpgradedFactory : AbstractFactory
    {
        public override SpecialWall createSpecialWall()
        {
            return new UpgradedSpecialWall();
        }

        public override Spikes createSpikes()
        {
            return new UpgradedSpikes();
        }
    }
}
