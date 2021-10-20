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
        Connection connection;
        private string connectionId;
        private string groupName;

        private Graphics g;
        private const int BLOCK_SIZE = 45;
        private int currentRoundPoint = -1;
        private List<string> groupPlayers;
        GraphicalElement player1;
        GraphicalElement player2;
        private bool MainPlayer = false;
        GameRound gameRound;
        Subject subject;

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
            { 1, 10, 20, 40, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 10, 20, 40, 1 },
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

        public GameMap(List<string> groupPlayers, string groupName, string connectionId, Connection connection)
        {
            InitializeComponent();
            this.connection = connection;
            this.connectionId = connectionId;
            this.groupName = groupName;
            this.groupPlayers = groupPlayers;

            g = canvas.CreateGraphics();
            MapMatrix = DefaultMap;
            CreateObjectMatrix();

            if (groupPlayers.Count == GROUP_SIZE)
            {
                switch (groupPlayers.IndexOf(connectionId))
                {
                    case 0:
                        txtPlayerId.Text = "Player: " + groupPlayers[0];
                        txtPlayer2Id.Text = "Player: " + groupPlayers[1];
                        MainPlayer = true;
                        break;
                    case 1:
                        txtPlayerId.Text = "Player: " + groupPlayers[1];
                        txtPlayer2Id.Text = "Player: " + groupPlayers[0];
                        MainPlayer = false;
                        break;
                }
            }
            else
            {
                txtPlayerId.Text = "Error: incorrect group size";
                txtPlayer2Id.Text = "Error: incorrect group size";
            }

            //Singleton Inner class approach
            gameRound = GameRound.getInstance();

            //Observer
            subject = new Server();

            //test
            /*Item testCoin = new Coin();
            subject.Attach(testCoin);
            testCoin.UseItem();*/

            UpdateGame();

            //Strategy
            Item itemCoin = new Item();

            //player
            itemCoin.SetAlgorithm(new PlayerAlgorithm());
            itemCoin.ItemActivated();

            //enemy 
            itemCoin.SetAlgorithm(new EnemyAlgorithm());
            itemCoin.ItemActivated();

            //Draw();
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
                            creator.FactoryMethod(WALL_ID);
                            ObjectMatrix[i, j] = wall;
                            break;
                        case COIN_ID:
                            GraphicalElement coin = creator.FactoryMethod(COIN_ID);
                            ObjectMatrix[i, j] = coin;
                            break;
                        case SPECIAL_WALL_ID:
                            GraphicalElement specwall = creator.FactoryMethod(SPECIAL_WALL_ID);
                            ObjectMatrix[i, j] = specwall;
                            break;
                        case DESTROYER_ID:
                            GraphicalElement destroyer = creator.FactoryMethod(DESTROYER_ID);
                            ObjectMatrix[i, j] = destroyer;
                            break;
                        case SPIKES_ID:
                            GraphicalElement spikes = creator.FactoryMethod(SPIKES_ID);
                            ObjectMatrix[i, j] = spikes;
                            break;
                    }
                }
            }
        }

        private Item RandomSpawnItemToMap<T>(T item)
        {
            //spawn item
            bool found = false;
            int x, y;
            int blockId;
            Random rnd = new Random();
            Item ourItem = item as Item;

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
                        ourItem.X = x;
                        ourItem.Y = y;

                        return ourItem;
                    }

                    if (item is UpgradedSpecialWall)
                    {
                        MapMatrix[x, y] = SPECIAL_WALL_ID;
                        ourItem.X = x;
                        ourItem.Y = y;

                        return ourItem;
                    }

                    if (item is UltimateSpecialWall)
                    {
                        MapMatrix[x, y] = SPECIAL_WALL_ID;
                        ourItem.X = x;
                        ourItem.Y = y;

                        return ourItem;
                    }

                    if (item is DefaultSpikes)
                    {
                        MapMatrix[x, y] = SPIKES_ID;
                        ourItem.X = x;
                        ourItem.Y = y;

                        return ourItem;
                    }

                    if (item is UpgradedSpikes)
                    {
                        MapMatrix[x, y] = SPIKES_ID;
                        ourItem.X = x;
                        ourItem.Y = y;

                        return ourItem;
                    }

                    if (item is UltimateSpikes)
                    {
                        MapMatrix[x, y] = SPIKES_ID;
                        ourItem.X = x;
                        ourItem.Y = y;

                        return ourItem;
                    }

                    if (item is Coin)
                    {
                        MapMatrix[x, y] = COIN_ID;
                        ourItem.X = x;
                        ourItem.Y = y;

                        return ourItem;
                    }

                    if (item is Destroyer)
                    {
                        MapMatrix[x, y] = DESTROYER_ID;
                        ourItem.X = x;
                        ourItem.Y = y;

                        return ourItem;
                    }
                }
            }
            //Draw();
            return null;
        }

        //dont need 
        private void GameTimer(object sender, EventArgs e)
        {
        }

        public void SpawnSpecialItemByServer(int X, int Y, int itemID, int level)
        {
            //Abstract Factory
            AbstractFactory itemFactory = null;

            switch (level)
            {
                case 1:
                    itemFactory = new DefaultFactory();
                    break;
                case 2:
                    itemFactory = new UpgradedFactory();
                    break;
                case 3:
                    itemFactory = new UltimateFactory();
                    break;
            }

            switch (itemID)
            {
                case 1:
                    ObjectMatrix[X, Y] = itemFactory.createSpecialWall();
                    MapMatrix[X, Y] = SPECIAL_WALL_ID;
                    break;
                case 2:
                    ObjectMatrix[X, Y] = itemFactory.createSpikes();
                    MapMatrix[X, Y] = SPIKES_ID;
                    break;
            }
        }

        public void SpawnCoinByServer(int X, int Y)
        {
            Item coin = new Coin();
            MapMatrix[X, Y] = COIN_ID;
            ObjectMatrix[X, Y] = coin;
            Draw();
        }

        //update enemy movement
        public void UpdatePlayerByServer(int X, int Y, string connectionId)
        {
            if (this.connectionId != connectionId)
            {
                SetMap(player2.X, player2.Y, SPACE_ID);
                player2.X = X;
                player2.Y = Y;
                UpdatePlayersPosMap();
                Draw();
            }
        }

        private void RandomizeSpecialItem()
        {
            //Abstract Factory
            //randomizing special item level
            AbstractFactory itemFactory = null;

            Random rnd = new Random();
            switch (rnd.Next(1, 3))
            {
                case 1:
                    itemFactory = new DefaultFactory();
                    break;
                case 2:
                    itemFactory = new UpgradedFactory();
                    break;
                case 3:
                    itemFactory = new UltimateFactory();
                    break;
            }

            //randomizing special item
            rnd = new Random();
            switch (rnd.Next(1, 2))
            {
                case 1:
                    RandomSpawnItemToMap(itemFactory.createSpecialWall());
                    break;
                case 2:
                    RandomSpawnItemToMap(itemFactory.createSpikes());
                    break;
            }
        }

        //game spawn logic
        private void UpdateGame()
        {
            if (MainPlayer)
            {
                int updatedRoundPoint = gameRound.GetCurrentCoin();
                if (currentRoundPoint != updatedRoundPoint)
                {
                    currentRoundPoint = updatedRoundPoint;
                    Coin coin = null;
                    AbstractFactory specialItem;

                    switch (currentRoundPoint)
                    {
                        case -1:
                            //end of game 
                            break;
                        case 5:
                            //spawn coin and item

                            break;
                        case 10:
                            //spawn coin and item

                            break;
                        case 15:
                            //spawn coin and item

                            break;
                        default:
                            //spawn coin
                            coin = new Coin();
                            coin = RandomSpawnItemToMap(coin) as Coin;
                            connection.SpawnCoin(coin.X, coin.Y, groupName);
                            break;

                    }
                }
            }
            Draw();
        }

        private void CheckForItem(Player player, int x, int y)
        {
            int blockId = MapMatrix[x, y];
            if (blockId != SPACE_ID)
            {
                GraphicalElement element = ObjectMatrix[x, y];
                switch (blockId)
                {
                    case SPACE_ID:
                        break;
                    case PLAYER_ID:
                        break;
                    case WALL_ID:
                        break;
                    case COIN_ID:
                        gameRound.AddPoints((element as Coin).Value, PLAYER_ID);
                        break;
                    case SPECIAL_WALL_ID:
                        if (element is DefaultSpecialWall)
                        {
                        }
                        else if (element is UpgradedSpecialWall)
                        {
                        }
                        else if (element is UltimateSpecialWall)
                        {
                        }
                        else
                        {
                        }
                        break;
                    case DESTROYER_ID:
                        break;
                    case SPIKES_ID:
                        if (element is DefaultSpikes)
                        {
                        }
                        else if (element is UpgradedSpikes)
                        {
                        }
                        else if (element is UltimateSpikes)
                        {
                        }
                        else
                        {
                        }
                        break;
                }
            }
            UpdateGame();
        }

        private void UpdatePlayerPosition(int pos)
        {
            //need to fix if's to not let player step on wall
            switch (pos)
            {
                case LEFT:
                    //  if (player1.X - 1 > MAP_MIN_SIZE && (MapMatrix[player1.X - 1, player1.Y] != WALL_ID && MapMatrix[player1.X - 1, player1.Y] != PLAYER_ID))
                    //   {
                    CheckForItem(player1 as Player, player1.X - 1, player1.Y);
                    SetMap(player1.X, player1.Y, SPACE_ID);
                    player1.X = player1.X - 1;
                    //  }
                    break;
                case UP:
                    //  if (player1.Y - 1 > MAP_MIN_SIZE && (MapMatrix[player1.X, player1.Y - 1] != WALL_ID && MapMatrix[player1.X, player1.Y - 1] != PLAYER_ID))
                    //  {
                    CheckForItem(player1 as Player, player1.X, player1.Y - 1);
                    SetMap(player1.X, player1.Y, SPACE_ID);
                    player1.Y = player1.Y - 1;
                    //  }
                    break;
                case RIGHT:
                    //  if (player1.X + 1 < MAP_MAX_SIZE && (MapMatrix[player1.X + 1, player1.Y] != WALL_ID && MapMatrix[player1.X + 1, player1.Y] != PLAYER_ID))
                    //  {
                    CheckForItem(player1 as Player, player1.X + 1, player1.Y);
                    SetMap(player1.X, player1.Y, SPACE_ID);
                    player1.X = player1.X + 1;
                    // }
                    break;
                case DOWN:
                    // if (player1.Y + 1 < MAP_MAX_SIZE && (MapMatrix[player1.X, player1.Y + 1] != WALL_ID && MapMatrix[player1.X, player1.Y + 1] != PLAYER_ID))
                    // {
                    CheckForItem(player1 as Player, player1.X, player1.Y + 1);
                    SetMap(player1.X, player1.Y, SPACE_ID);
                    player1.Y = player1.Y + 1;
                    // }
                    break;
            }
            connection.GameMapUpdatePlayerPos(player1.X.ToString(), player1.Y.ToString(), connectionId, groupName);

            UpdatePlayersPosMap();
            Draw();
        }

        private void SetMap(int X, int Y, int id)
        {
            MapMatrix[Y, X] = id;
        }

        private void UpdatePlayersPosMap()
        {
            //update map from GraphicElements objects
            MapMatrix[player1.Y, player1.X] = PLAYER_ID;
            MapMatrix[player2.Y, player2.X] = PLAYER_ID;

            ObjectMatrix[player1.Y, player1.X] = player1;
            ObjectMatrix[player2.Y, player2.X] = player1;
        }

        private void Draw()
        {
            //UpdateGame();
            canvas.Refresh();

            // Create string to draw.
            String coinsLeft = (GameRound.MAX_COIN_COUNT - gameRound.GetCurrentCoin()) + " coins";

            // Create font and brush.
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.White);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            //drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;

            // Draw string to screen.
            g.DrawString(coinsLeft, drawFont, drawBrush, ((MAP_MAX_SIZE / 2)-1) * BLOCK_SIZE, MAP_MAX_SIZE / 2 * BLOCK_SIZE, drawFormat);

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
                            else if (element is UltimateSpecialWall)
                            {
                                g.FillRectangle(Brushes.DarkMagenta, rectangle);
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
                            if (element is DefaultSpikes)
                            {
                                g.FillPolygon(Brushes.DeepSkyBlue, triangle);
                            }
                            else if (element is UpgradedSpikes)
                            {
                                g.FillPolygon(Brushes.DarkBlue, triangle);
                            }
                            else if (element is UltimateSpikes)
                            {
                                g.FillPolygon(Brushes.DarkMagenta, triangle);
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
