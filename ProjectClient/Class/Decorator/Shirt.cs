//using ProjectClient.Class.Factory;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Windows.Forms;
//using System.Drawing;
//namespace ProjectClient.Class.Decorator
//{

//    class Shirt : SkinDecorator
//    {
//        private PictureBox _display;

//        public Shirt(Player player, PictureBox display) : base(player)
//        {
//            _display = display;
//            playerToDecorate = player;

//        }
//        public  override PictureBox Display
//        {
//            get
//            {
//                if (_display == null)
//                {
//                    _display = new PictureBox()
//                    {
//                        Location = new Point(playerToDecorate.X, playerToDecorate.Y),
//                        Size = new Size(playerToDecorate.Length, playerToDecorate.Width),
//                        BackColor =  System.Drawing.Color.Black
//                    };
//                }

//                return _display;
//            }
//            set
//            {
//                base.Display = value;
//            }
//        }
//        public void PutOnSkin()
//        {
//            base.PutOnSkin();
//        }
//    }
//}
