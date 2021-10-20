using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.AbstractFactory
{
    class UltimateFactory : AbstractFactory
    {
        public override SpecialWall createSpecialWall()
        {
            return new UltimateSpecialWall();
        }

        public override Spikes createSpikes()
        {
            return new UltimateSpikes();
        }
    }
}
