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
            Console.WriteLine("padekoravom");
            
            playerToDecorate = player;
            //playerToDecorate.Move("starting");
        }
        public  void Move(string direction)
        {

            playerToDecorate.Move(direction);
            
        }
        public PictureBox GetDisplay()
        {
            return playerToDecorate.Display;
        }
        //public virtual PictureBox Display
        //{
        //    get
        //    {
        //        return _display;
        //    }
        //    set
        //    {
        //        _display = value;
        //    }
        //}



    }
}
