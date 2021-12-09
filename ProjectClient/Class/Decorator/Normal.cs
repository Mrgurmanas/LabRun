using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjectClient.Class.Decorator
{
    class Normal : SkinDecorator
    {
        private PictureBox _display;
        private Image _image;

       
        private const int LEFT = -1;
        private const int UP = 0;
        private const int RIGHT = 1;
        private const int DOWN = 2;
        public Normal(Player player, int direction) : base(player)
        {
            
            playerToDecorate = player;
            
            if (direction == LEFT)
            {
                _image = Properties.Resources.IMG_PLAYER_RIGHT;
            }
            if (direction == RIGHT)
            {
                _image = Properties.Resources.IMG_PLAYER_LEFT;
            }
            if (direction == UP)
            {
                _image = Properties.Resources.IMG_PLAYER_UP;
            }
            if (direction == DOWN)
            {
                _image = Properties.Resources.IMG_PLAYER_DOWN;
            }

            _display = new PictureBox()
            {
                Location = new Point(playerToDecorate.X, playerToDecorate.Y),
                Size = new Size(playerToDecorate.Length, playerToDecorate.Width),
                Image = _image,
                BackColor = System.Drawing.Color.Red
            };
            
            base.PutOnSkin(_display);
        }
        
    }
}
