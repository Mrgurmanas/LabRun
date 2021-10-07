using Microsoft.AspNetCore.SignalR.Client;
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
        HubConnection connection;
        private string connectionId;
        private string groupName;

        //private bool goUp, goDown, goLeft, goRight, isGameOver;
        private Graphics g;
        private const int BLOCK_SIZE = 45;
        GraphicalElement player1;
        GraphicalElement player2;

        private const int LEFT = -1;
        private const int UP = 0;
        private const int RIGHT = 1;
        private const int DOWN = 2;

        private const int GROUP_SIZE = 2;
        private const int PLAYER_1_ID = 1;
        private const int PLAYER_2_ID = 2;

        private int MapLength = 1000;
        private int MapWidth = 1000;
        private string MapLayout;
        private int Level = 1;

        private const int SPACE_ID = 0;
        private const int WALL_ID = 1;
        private const int PLAYER_ID = 2;
        private const int COIN_ID = 3;
        private const int ITEM_ID = 4;

        public static int MAP_MAX_SIZE = 20;
        public static int MAP_MIN_SIZE = 0;
        private int[,] MapMatrix = new int[21, 21];
        private int[,] DefaultMap = new int[,] {
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

        public GameMap(List<string> groupPlayers, string groupName, string connectionId, HubConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            this.connectionId = connectionId;
            this.groupName = groupName;

            /*this.connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/gamehub")
                .Build();

            connection.On("UpdatePlayers", (int X, int Y, string connectionId, string groupName) =>
            {
                connection.InvokeCoreAsync("ConnectionTest", args: new[] { "conenction UpdatePlayers" });
                UpdatePlayerByServer(X, Y, connectionId);
            });

            connection.StartAsync();
            connection.InvokeCoreAsync("ConnectionTest", args: new[] { "testing connection" });
            */

            g = canvas.CreateGraphics();
            MapMatrix = DefaultMap;
            Draw();

            if (groupPlayers.Count == GROUP_SIZE)
            {
                switch (groupPlayers.IndexOf(connectionId))
                {
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

            Item coin = new Coin();

            //Factory
            Creator creator = new GraphicalElementCreator();

            player1 = creator.FactoryMethod(PLAYER_ID);
            player2 = creator.FactoryMethod(PLAYER_ID);

            //initial players positions
            switch (groupPlayers.IndexOf(connectionId))
            {
                case 0:
                    player1.X = 1;
                    player1.Y = 10;
                    player2.X = 19;
                    player2.Y = 10;
                    break;
                case 1:
                    player1.X = 19;
                    player1.Y = 10;
                    player2.X = 19;
                    player2.Y = 10;
                    break;
            }

            GraphicalElement wall1 = creator.FactoryMethod(WALL_ID);
            GraphicalElement wall2 = creator.FactoryMethod(WALL_ID);
            GraphicalElement wall3 = creator.FactoryMethod(WALL_ID);
            GraphicalElement wall4 = creator.FactoryMethod(WALL_ID);

            //Abstract Factory
            //randomizing special item level
            AbstractFactory itemFactory = null;

            Random rnd = new Random();
            switch (rnd.Next(1, 2))
            {
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

        private void GameTimer(object sender, EventArgs e)
        {
            //Update();
            //UpdateMap();
            //Draw();
        }

        public void UpdatePlayerByServer(int X, int Y, string connectionId)
        {
            if (this.connectionId != connectionId)
            {
                player2.X = X;
                player2.Y = Y;
                UpdateMap();
                Draw();
            }
        }
        
        private void UpdatePlayerPosition(int pos)
        {
            switch (pos)
            {
                case LEFT:
                    //if (player1.X - 1 > MAP_MIN_SIZE && (MapMatrix[player1.X - 1, player1.Y] != WALL_ID && MapMatrix[player1.X - 1, player1.Y] != PLAYER_ID))
                    // {
                    SetMap(player1.X, player1.Y, SPACE_ID);
                    player1.X = player1.X - 1;
                    // }
                    break;
                case UP:
                    //if (player1.Y - 1 > MAP_MIN_SIZE && (MapMatrix[player1.X, player1.Y - 1] != WALL_ID && MapMatrix[player1.X, player1.Y - 1] != PLAYER_ID)) 
                    //{
                    SetMap(player1.X, player1.Y, SPACE_ID);
                    player1.Y = player1.Y - 1;
                    // }
                    break;
                case RIGHT:
                    // if (player1.X + 1 < MAP_MAX_SIZE && (MapMatrix[player1.X + 1, player1.Y] != WALL_ID && MapMatrix[player1.X + 1, player1.Y] != PLAYER_ID))
                    // {
                    SetMap(player1.X, player1.Y, SPACE_ID);
                    player1.X = player1.X + 1;
                    //  }
                    break;
                case DOWN:
                    //  if (player1.Y + 1 < MAP_MAX_SIZE && (MapMatrix[player1.X, player1.Y + 1] != WALL_ID && MapMatrix[player1.X, player1.Y + 1] != PLAYER_ID))
                    //  {
                    SetMap(player1.X, player1.Y, SPACE_ID);
                    player1.Y = player1.Y + 1;
                    //  }
                    break;
            }
            connection.InvokeCoreAsync("UpdatePlayerPos", args: new[] { player1.X.ToString(), player1.Y.ToString(), connectionId, groupName });

            UpdateMap();
            Draw();
        }

        private void SetMap(int X, int Y, int id)
        {
            MapMatrix[Y, X] = id;
        }

        private void UpdateMap()
        {
            //update map from GraphicElements objects
            MapMatrix[player1.Y, player1.X] = PLAYER_ID;
            MapMatrix[player2.Y, player2.X] = PLAYER_ID;
        }

        private void Draw()
        {
            canvas.Refresh();

            Rectangle rectangle;
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    int blockId = MapMatrix[j, i];
                    rectangle = new Rectangle(i * BLOCK_SIZE, j * BLOCK_SIZE, BLOCK_SIZE, BLOCK_SIZE);

                    switch (blockId)
                    {
                        case SPACE_ID:
                            //g.FillRectangle(Brushes.GreenYellow, rectangle);
                            break;
                        case PLAYER_ID:
                            g.FillRectangle(Brushes.GreenYellow, rectangle);
                            break;
                        case WALL_ID:
                            g.FillRectangle(Brushes.Blue, rectangle);
                            break;
                        case COIN_ID:
                            g.FillRectangle(Brushes.Yellow, rectangle);
                            break;
                    }
                }
            }
        }

        private void KeyIsPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('w'))
            {
                UpdatePlayerPosition(UP);
            }
            if (e.KeyChar.Equals('s'))
            {
                UpdatePlayerPosition(DOWN);
            }
            if (e.KeyChar.Equals('a'))
            {
                UpdatePlayerPosition(LEFT);
            }
            if (e.KeyChar.Equals('d'))
            {
                UpdatePlayerPosition(RIGHT);
            }
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
        }

        private void GameMap_Load(object sender, EventArgs e)
        {
        }
    }
}
