using ProjectClient.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjectClient
{
    public partial class GameMap : Form
    {
        private const int GROUP_SIZE = 2;
        private const int PLAYER_1_ID = 1;
        private const int PLAYER_2_ID = 2;

        public GameMap(List<string> groupPlayers)
        {
            InitializeComponent();

            if (groupPlayers.Count == GROUP_SIZE)
            {
                txtPlayerId.Text = "Player: " + groupPlayers[0];
                txtPlayerId.Text = "Player: " + groupPlayers[1];
            }
            else
            {
                txtPlayerId.Text = "Error: incorrect group size";
                txtPlayerId.Text = "Error: incorrect group size";
            }

            //Singleton Inner class approach
            GameRound gameRound = GameRound.getInstance();
        }

        private void GameMap_Load(object sender, EventArgs e)
        {

        }
    }
}
