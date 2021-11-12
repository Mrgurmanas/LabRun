using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjectClient.Class.Decorator
{
    class Hat : SkinDecorator
    {
        private PictureBox _display;
        private Image _image;

       
        private const int LEFT = -1;
        private const int UP = 0;
        private const int RIGHT = 1;
        private const int DOWN = 2;
        public Hat(Player player, int direction) : base(player)
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
            Console.WriteLine("fsdfsdfsdfdsf");
            base.PutOnSkin(_display);
        }
        //public  void PutOnSkin(int direction)
        //{
        //    if(direction == LEFT)
        //    {
        //        _display.Image = Properties.Resources.IMG_PLAYER_LEFT;
        //    }
        //    if (direction == RIGHT)
        //    {
        //        _display.Image = Properties.Resources.IMG_PLAYER_RIGHT;
        //    }
        //    if (direction == UP)
        //    {
        //        _display.Image = Properties.Resources.IMG_PLAYER_UP;
        //    }
        //    if (direction == DOWN)
        //    {
        //        _display.Image = Properties.Resources.IMG_PLAYER_DOWN;
        //    }
        //    base.PutOnSkin(_display);
        //}
    }
}
