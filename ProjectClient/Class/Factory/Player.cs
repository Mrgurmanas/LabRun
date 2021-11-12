using System;
using System.Collections.Generic;
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

        public void PutOnSkin(PictureBox picture)
        {
            _picture = picture;
        }
        public virtual PictureBox Display
        {
            get
            {
                return _picture;
            }
            
        }

    }
}
