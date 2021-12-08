using System;
using System.Collections.Generic;
using System.Text;
using ProjectClient.Class.Builderis;
using System.Drawing;

namespace ProjectClient.Class.AbstractFactory
{
    class DefaultFactory : AbstractFactory
    {
        Rectangle rectangle;
        public override SpecialWall createSpecialWall()
        {
            SpecialWall wallUnit = new DefaultSpecialWall();
            Builder builder = new SpecialWallBuilder();
            return builder.startNewSpecialWall(wallUnit).addVisuals(Brushes.DeepSkyBlue).addShape(rectangle).getBuildableSpecialWall();
        }

        public override Spikes createSpikes()
        {
            Spikes spikeUnit = new UltimateSpikes();
            Builder builder = new SpecialWallBuilder();
            return builder.startNewSpikes(spikeUnit).addVisuals(Brushes.DeepSkyBlue).getBuildableSpikes();
        }
    }
}
