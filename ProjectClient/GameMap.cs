using Microsoft.AspNetCore.SignalR.Client;
using ProjectClient.Class;
using ProjectClient.Class.AbstractFactory;
using ProjectClient.Class.Factory;
using ProjectClient.Class.Observer;
using ProjectClient.Class.Strategy;
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

        private Graphics g;
        private const int BLOCK_SIZE = 45;
        private List<string> groupPlayers;
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

        private const int EMPTY_ID = 5;
        private const int SPACE_ID = 0;
        private const int WALL_ID = 1;
        private const int PLAYER_ID = 2;
        private const int COIN_ID = 3;
        private const int ITEM_ID = 4;

        private const int SPECIAL_WALL_ID = 10;
        private const int SPIKES_ID = 20;
        private const int DESTROYER_ID = 40;

        public static int MAP_MAX_SIZE = 20;
        public static int MAP_MIN_SIZE = 0;
        private int[,] MapMatrix = new int[21, 21];
        private GraphicalElement[,] ObjectMatrix = new GraphicalElement[21, 21];
        private int[,] DefaultMap = new int[,] {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1 },
            { 1, 5, 5, 5, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 5, 5, 5, 1 },
            { 1, 10, 20, 40, 1, 0, 1, 0, 0, 0, 3, 0, 0, 0, 1, 0, 1, 10, 20, 40, 1 },
            { 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1 },
            { 1, 2, 0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 1, 0, 0, 0, 0, 0, 0, 2, 1 },
            { 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1 },
            { 1, 5, 5, 5, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 5, 5, 5, 1 },
            { 1, 5, 5, 5, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 5, 5, 5, 1 },
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
            this.groupPlayers = groupPlayers;

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

            CreateObjectMatrix();

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
                    player2.X = 1;
                    player2.Y = 10;
                    break;
            }

            GraphicalElement wall1 = creator.FactoryMethod(WALL_ID);
            GraphicalElement wall2 = creator.FactoryMethod(WALL_ID);
            GraphicalElement wall3 = creator.FactoryMethod(WALL_ID);
            GraphicalElement wall4 = creator.FactoryMethod(WALL_ID);

            //Observer
            //List<Item> items = new List<Item>();
            Subject subject = new Server();

            Item testCoin = new Coin();
            subject.Attach(testCoin);
            testCoin.UseItem();

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

            //Strategy
            Item itemCoin = new Item();

            //player
            itemCoin.SetAlgorithm(new PlayerAlgorithm());
            itemCoin.ItemActivated();

            //enemy 
            itemCoin.SetAlgorithm(new EnemyAlgorithm());
            itemCoin.ItemActivated();
        }

        private void CreateObjectMatrix()
        {
            //Factory
            Creator creator = new GraphicalElementCreator();

            player1 = creator.FactoryMethod(PLAYER_ID);
            player2 = creator.FactoryMethod(PLAYER_ID);

            switch (groupPlayers.IndexOf(connectionId))
            {
                case 0:
                    player1.X = 1;
                    player1.Y = 10;
                    player2.X = 19;
                    player2.Y = 10;

                    ObjectMatrix[1, 10] = player1;
                    ObjectMatrix[19, 10] = player2;
                    break;
                case 1:
                    player1.X = 19;
                    player1.Y = 10;
                    player2.X = 1;
                    player2.Y = 10;

                    ObjectMatrix[19, 10] = player1;
                    ObjectMatrix[1, 10] = player2;
                    break;
            }

            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    int blockId = MapMatrix[j, i];

                    switch (blockId)
                    {
                        case EMPTY_ID:
                            break;
                        case SPACE_ID:
                            break;
                        case PLAYER_ID:
                            break;
                        case WALL_ID:
                            GraphicalElement wall = creator.FactoryMethod(WALL_ID);
                            ObjectMatrix[i, j] = wall;
                            break;
                        case COIN_ID:
                            //GraphicalElement coin = creator.FactoryMethod(COIN_ID);
                            //ObjectMatrix[i, j] = coin;
                            break;
                        case SPECIAL_WALL_ID:
                            //GraphicalElement specwall = creator.FactoryMethod(COIN_ID);
                            //ObjectMatrix[i, j] = specwall;
                            break;
                        case DESTROYER_ID:
                            //GraphicalElement destroyer = creator.FactoryMethod(COIN_ID);
                            //ObjectMatrix[i, j] = destroyer;
                            break;
                        case SPIKES_ID:
                            //GraphicalElement spikes = creator.FactoryMethod(COIN_ID);
                            //ObjectMatrix[i, j] = spikes;
                            break;
                    }
                }
            }
        }

        private void SpawnItemToMap<T>(T item)
        {
            //spawn item
            bool found = false;
            int x, y;
            int blockId;
            Random rnd = new Random();

            while (!found)
            {
                x = rnd.Next(MAP_MIN_SIZE + 1, MAP_MAX_SIZE - 1);
                y = rnd.Next(MAP_MIN_SIZE + 1, MAP_MAX_SIZE - 1);
                blockId = MapMatrix[x, y];
                if (blockId == SPACE_ID)
                {
                    found = true;

                    ObjectMatrix[x, y] = item as Item;

                    if (item is DefaultSpecialWall)
                    {
                        MapMatrix[x, y] = SPECIAL_WALL_ID;
                    }

                    if (item is UpgradedSpecialWall)
                    {
                        MapMatrix[x, y] = SPECIAL_WALL_ID;
                    }

                    if (item is DefaultSpikes)
                    {
                        MapMatrix[x, y] = SPIKES_ID;
                    }

                    if (item is UpgradedSpikes)
                    {
                        MapMatrix[x, y] = SPIKES_ID;
                    }

                    if (item is Coin)
                    {
                        MapMatrix[x, y] = COIN_ID;
                    }

                    if (item is Destroyer)
                    {
                        MapMatrix[x, y] = DESTROYER_ID;
                    }
                }
            }
            Draw();
        }

        //dont need ?
        private void GameTimer(object sender, EventArgs e)
        {
            //Update();
            //UpdateMap();
            //Draw();
        }

        //update enemy movement
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
            //need to fix if's to not let player step on wall
            switch (pos)
            {
                case LEFT:
                    if (player1.X - 1 > MAP_MIN_SIZE && (MapMatrix[player1.X - 1, player1.Y] != WALL_ID && MapMatrix[player1.X - 1, player1.Y] != PLAYER_ID))
                    {
                        SetMap(player1.X, player1.Y, SPACE_ID);
                        player1.X = player1.X - 1;
                    }
                    break;
                case UP:
                    if (player1.Y - 1 > MAP_MIN_SIZE && (MapMatrix[player1.X, player1.Y - 1] != WALL_ID && MapMatrix[player1.X, player1.Y - 1] != PLAYER_ID))
                    {
                        SetMap(player1.X, player1.Y, SPACE_ID);
                        player1.Y = player1.Y - 1;
                    }
                    break;
                case RIGHT:
                    if (player1.X + 1 < MAP_MAX_SIZE && (MapMatrix[player1.X + 1, player1.Y] != WALL_ID && MapMatrix[player1.X + 1, player1.Y] != PLAYER_ID))
                    {
                        SetMap(player1.X, player1.Y, SPACE_ID);
                        player1.X = player1.X + 1;
                    }
                    break;
                case DOWN:
                    if (player1.Y + 1 < MAP_MAX_SIZE && (MapMatrix[player1.X, player1.Y + 1] != WALL_ID && MapMatrix[player1.X, player1.Y + 1] != PLAYER_ID))
                    {
                        SetMap(player1.X, player1.Y, SPACE_ID);
                        player1.Y = player1.Y + 1;
                    }
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

            Rectangle rectangle = new Rectangle();
            PointF[] triangle = new PointF[3];

            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    int blockId = MapMatrix[j, i];
                    GraphicalElement element = ObjectMatrix[i, j];

                    if (blockId != SPIKES_ID)
                    {
                        rectangle = new Rectangle(i * BLOCK_SIZE, j * BLOCK_SIZE, BLOCK_SIZE, BLOCK_SIZE);
                    }
                    else
                    {
                        PointF point1 = new PointF(i * BLOCK_SIZE, (j + 1) * BLOCK_SIZE);
                        PointF point2 = new PointF((i * BLOCK_SIZE) + (BLOCK_SIZE / 2), (j * BLOCK_SIZE));
                        PointF point3 = new PointF((i * BLOCK_SIZE) + BLOCK_SIZE, (j + 1) * BLOCK_SIZE);
                        triangle[0] = point1;
                        triangle[1] = point2;
                        triangle[2] = point3;
                    }

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
                            g.FillEllipse(Brushes.Yellow, rectangle);
                            break;
                        case SPECIAL_WALL_ID:
                            if (element is DefaultSpecialWall)
                            {
                                g.FillRectangle(Brushes.DeepSkyBlue, rectangle);
                            }
                            else if (element is UpgradedSpecialWall)
                            {
                                g.FillRectangle(Brushes.DarkBlue, rectangle);
                            }
                            else
                            {
                                g.FillRectangle(Brushes.AliceBlue, rectangle);
                            }
                            break;
                        case DESTROYER_ID:
                            //g.DrawRectangle(new Pen(Brushes.DarkMagenta, 3), rectangle);
                            g.FillRectangle(Brushes.DarkMagenta, rectangle);
                            break;
                        case SPIKES_ID:
                            if (element is DefaultSpecialWall)
                            {
                                g.FillPolygon(Brushes.DeepSkyBlue, triangle);
                            }
                            else if (element is UpgradedSpecialWall)
                            {
                                g.FillPolygon(Brushes.DarkBlue, triangle);
                            }
                            else
                            {
                                g.FillPolygon(Brushes.White, triangle);
                            }
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
