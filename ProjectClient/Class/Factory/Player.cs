using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjectClient.Class.Factory
{
     class Player : GraphicalElement
    {
        public int Color { get; set; }
        public Inventory inventory = new Inventory();
        public bool Freezed = false;
        public string ConnectionId { get; set; } = "";
        private PictureBox _picture;
        private PictureBox _display;
        private Image _image;
        private const int LEFT = -1;
        private const int UP = 0;
        private const int RIGHT = 1;
        private const int DOWN = 2;
        public void PutOnSkin(PictureBox picture)
        {
            _picture = picture;
        }
        public virtual PictureBox Picture
        {
            get
            {
                return _picture;
            }
            
        }
        public virtual PictureBox Display
        {
            get
            {
                return _display;
            }

        }
        //public void Move(string direction)
        //{
        //    if(direction == "left")
        //    {
        //        X = X - 1;
        //    }

        //    if(direction == "right")
        //    {
        //        X += 1;
        //    }
        //    if(direction == "up")
        //    {
        //        Y = Y - 1;
        //    }
        //    if (direction == "down")
        //    {
        //        Y = Y + 1;
        //    }

        //}
        public void Move(string direction)
        {
            if (direction == "left")
            {
                _image = Properties.Resources.IMG_PLAYER_LEFT;
                X = X - 1;
            }
            if (direction == "right")
            {
                _image = Properties.Resources.IMG_PLAYER_RIGHT;
                X += 1;
            }
            if (direction == "up")
            {
                _image = Properties.Resources.IMG_PLAYER_UP;
                Y = Y - 1;
            }
            if (direction == "down")
            {
                _image = Properties.Resources.IMG_PLAYER_DOWN;
                Y = Y + 1;
            }
            if(direction == "starting")
            {
                _image = Properties.Resources.IMG_PLAYER_RIGHT;
            }
            _display = new PictureBox()
            {
                Location = new Point(X, Y),
                Size = new Size(Length, Width),
                Image = _image,
                BackColor = System.Drawing.Color.Red
            };
        }

    }
}
