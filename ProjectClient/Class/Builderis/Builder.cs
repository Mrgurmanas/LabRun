using System;
using System.Collections.Generic;
using System.Text;
using ProjectClient.Class.AbstractFactory;
using System.Drawing;

namespace ProjectClient.Class.Builderis
{
    abstract class Builder
    {
        public Spikes spikes;
        public SpecialWall specialWall;

        public abstract Builder addVisuals(Brush color);
        public abstract Builder addShape(Rectangle rectangle);

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
