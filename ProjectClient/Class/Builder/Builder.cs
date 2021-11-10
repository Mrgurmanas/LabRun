using System;
using System.Collections.Generic;
using System.Text;
using ProjectClient.Class.AbstractFactory;

namespace ProjectClient.Class.Builder
{
    abstract class Builder
    {
        public Spikes spikes;
        public SpecialWall specialWall;

        public Builder startNewSpikes(Spikes newSpikes)
        {
            this.spikes = newSpikes;
            return this;
        }

        public Builder startNewSpecialWall(SpecialWall newSpecialWall)
        {
            this.specialWall = newSpecialWall;
            return this;
        }

        public Spikes getBuildableSpikes()
        {
            return this.spikes;
        }
        public SpecialWall getBuildableSpecialWall()
        {
            return this.specialWall;
        }

    }
}
