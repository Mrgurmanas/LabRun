using ProjectClient.Class.Factory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ProjectClient.Class.Decorator
{
     abstract class SkinDecorator : Player
    {
        protected Player playerToDecorate;

        PictureBox _display;
        public SkinDecorator(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("ghostToDecorate");
            }

            playerToDecorate = player;
        }
        public  void PutOnSkin(PictureBox picture)
        {
            Console.WriteLine("bebebeebe");
            _display = picture;
            playerToDecorate.PutOnSkin(picture);
        }
        public virtual PictureBox Display
        {
            get
            {
                return _display;
            }
            set
            {
                _display = value;
            }
        }



    }
}
