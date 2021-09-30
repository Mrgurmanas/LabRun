using ProjectClient.Class;
using ProjectClient.Class.AbstractFactory;
using ProjectClient.Class.Factory;
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

        private const string PLAYER = "P";
        private const string COIN = "C";
        private const string ITEM = "I";
        private const string WALL = "W";

        private int MapLength = 1000;
        private int MapWidth = 1000;
        private string MapLayout;
        private int Level = 1;

        private const int SPACE_ID = 0;
        private const int WALL_ID = 1;
        private const int PLAYER_ID = 2;
        private const int COIN_ID = 3;

        private int[,] MapMatrix = new int[,] {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1 },
            { 1, 2, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 2, 1 },
            { 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }};

        public GameMap(List<string> groupPlayers, string connectionId)
        {
            InitializeComponent();

            if (groupPlayers.Count == GROUP_SIZE)
            {
                switch (groupPlayers.IndexOf(connectionId)){
                    case 0:
                        txtPlayerId.Text = "Player: " + groupPlayers[0];
                        txtPlayer2Id.Text = "Player: " + groupPlayers[1];
                        break;
                    case 1:
                        txtPlayerId.Text = "Player: " + groupPlayers[1];
                        txtPlayer2Id.Text = "Player: " + groupPlayers[0];
                        break;
                }
            }
            else
            {
                txtPlayerId.Text = "Error: incorrect group size";
                txtPlayer2Id.Text = "Error: incorrect group size";
            }

            //Singleton Inner class approach
            GameRound gameRound = GameRound.getInstance();

            //Factory
            Creator creator = new GraphicalElementCreator();
            GraphicalElement player1 = creator.FactoryMethod(PLAYER);
            GraphicalElement player2 = creator.FactoryMethod(PLAYER);

            GraphicalElement coin = creator.FactoryMethod(COIN);

            //GraphicalElement item1 = creator.FactoryMethod(ITEM);
            //GraphicalElement item2 = creator.FactoryMethod(ITEM);

            GraphicalElement wall1 = creator.FactoryMethod(WALL);
            GraphicalElement wall2 = creator.FactoryMethod(WALL);
            GraphicalElement wall3 = creator.FactoryMethod(WALL);
            GraphicalElement wall4 = creator.FactoryMethod(WALL);

            //abstract factory
            //AbstractFactory defaultItemFactory = new DefaultFactory();
            //AbstractFactory upgradedItemFactory = new UpgradedFactory();
            //var specialItem = defaultItemFactory.createSpecialWall();
            //var specialItem2 = upgradedItemFactory.createSpikes();

            //randomizing special item level
            AbstractFactory itemFactory = null;

            Random rnd = new Random();
            switch(rnd.Next(1, 2)){
                case 1:
                    itemFactory = new DefaultFactory();
                    break;
                case 2:
                    itemFactory = new UpgradedFactory();
                    break;
            }

            //randomizing special item

            rnd = new Random();
            switch (rnd.Next(1, 2))
            {
                case 1:
                    SpawnItemToMap(itemFactory.createSpecialWall());
                    break;
                case 2:
                    SpawnItemToMap(itemFactory.createSpikes());
                    break;
            }
        }

        private void SpawnItemToMap<T>(T item)
        {
            //spawn item
        }

        private void Update()
        {

        }

        private void Draw()
        {

        }



        private void GameMap_Load(object sender, EventArgs e)
        {

        }
    }
}
