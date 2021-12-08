using System;
using System.Collections.Generic;
using System.Text;
using ProjectClient.Class.Builderis;
using System.Drawing;

namespace ProjectClient.Class.AbstractFactory
{
    class UltimateFactory : AbstractFactory
    {
        Rectangle rectangle;
        public override SpecialWall createSpecialWall()
        {
            SpecialWall wallUnit = new UltimateSpecialWall();
            Builder builder = new SpecialWallBuilder();
            return builder.startNewSpecialWall(wallUnit).addVisuals(Brushes.BlueViolet).addShape(rectangle).getBuildableSpecialWall();
        }

        public override Spikes createSpikes()
        {
            Spikes spikeUnit = new UltimateSpikes();
            Builder builder = new SpecialWallBuilder();
            return builder.startNewSpikes(spikeUnit).addVisuals(Brushes.BlueViolet).getBuildableSpikes();
        }
    }
}
