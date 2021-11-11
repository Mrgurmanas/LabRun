﻿using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ProjectClient.Class.Decorator
{
    class GangstaSkin : SkinDecorator
    {
        private PictureBox _display;

        public GangstaSkin(Player player, PictureBox display) : base(player)
        {
            _display = display;
            playerToDecorate = player;

        }
        public void PutOnSkin()
        {
            base.PutOnSkin();
        }
    }
}