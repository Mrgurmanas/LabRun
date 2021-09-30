using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.AbstractFactory
{
    abstract class AbstractFactory
    {
        public abstract SpecialWall createSpecialWall();
        public abstract Spikes createSpikes();
    }
}
