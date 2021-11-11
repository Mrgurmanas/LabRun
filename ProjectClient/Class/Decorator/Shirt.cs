using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ProjectClient.Class.Decorator
{

    class PoliceSkin : SkinDecorator
    {
        private PictureBox _display;

        public PoliceSkin(Player player, PictureBox display) : base(player)
        {
            _display = display;
            playerToDecorate = player;

        }
        public  PictureBox Display
        {
            get
            {
                if (_display == null)
                {
                    _display = new PictureBox()
                    {
                        Location = Location,
                        Size = OccupiedSpace.Size,
                        BackColor = Color.Red
                    };
                }

                return _display;
            }
            set
            {
                base.Display = value;
            }
        }
        public void PutOnSkin()
        {
            base.PutOnSkin();
        }
    }
}
