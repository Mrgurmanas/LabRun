using ProjectClient.Class;
using ProjectClient.Class.AbstractFactory;
using ProjectClient.Class.ChainOfResponsibility;
using ProjectClient.Class.Factory;
using ProjectClient.Class.Observer;
using ProjectClient.Class.State;
using ProjectClient.Class.Strategy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectClient
{
    public partial class GameMap : Form
    {
        public Connection connection;
        private string connectionId;
        public string groupName;

        public Graphics g;
        private const int BLOCK_SIZE = 45;
        private int currentRoundPoint = -1;
        private List<string> groupPlayers;
        GraphicalElement player1;
        GraphicalElement player2;
        public bool MainPlayer = false;
        GameLevel gameLevel;
        GameRounds gameRounds;
        Subject subject;

        private const int LEFT = -1;
        private const int UP = 0;
        private const int RIGHT = 1;
        private const int DOWN = 2;

        private const int GROUP_SIZE = 2;
        public const int PLAYER_1_ID = 1;
        public const int PLAYER_2_ID = 2;

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
            { 1, 101, 102, 103, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 201, 202, 203, 1 },
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
            MapMatrix = Transpose(DefaultMap);
            CreateObjectMatrix();

            if (groupPlayers.Count == GROUP_SIZE)
            {
                switch (groupPlayers.IndexOf(connectionId))
                {
                    case 0:
                        txtPlayerId.Text = "MainPlayer: " + groupPlayers[0];
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
            gameLevel = GameLevel.getInstance();
            gameLevel.SetGameMap(this);
            gameRounds = new GameRounds();

            //Observer
            subject = new Server();

            UpdateGame();
        }


        public int[,] Transpose(int[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            int[,] result = new int[h, w];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }

            return result;
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

                    (player1 as Player).ConnectionId = groupPlayers[0];
                    (player2 as Player).ConnectionId = groupPlayers[1];

                    ObjectMatrix[1, 10] = player1;
                    ObjectMatrix[19, 10] = player2;
                    break;
                case 1:
                    player1.X = 19;
                    player1.Y = 10;
                    player2.X = 1;
                    player2.Y = 10;

                    (player1 as Player).ConnectionId = groupPlayers[1];
                    (player2 as Player).ConnectionId = groupPlayers[0];

                    ObjectMatrix[19, 10] = player1;
                    ObjectMatrix[1, 10] = player2;
                    break;
            }

            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    int blockId = MapMatrix[i, j];

                    GraphicalElement block = null;

                    switch (blockId)
                    {
                        case EMPTY_ID:
                            break;
                        case SPACE_ID:
                            break;
                        case PLAYER_ID:
                            break;
                        case WALL_ID:
                            block = creator.FactoryMethod(WALL_ID);
                            break;
                        case COIN_ID:
                            block = creator.FactoryMethod(COIN_ID);
                            break;
                        case SPECIAL_WALL_ID:
                            block = creator.FactoryMethod(SPECIAL_WALL_ID);
                            break;
                        case DESTROYER_ID:
                            block = creator.FactoryMethod(DESTROYER_ID);
                            break;
                        case SPIKES_ID:
                            block = creator.FactoryMethod(SPIKES_ID);
                            break;
                        default:
                            block = null;
                            break;
                    }
                    if (block != null)
                    {
                        block.X = i;
                        block.Y = j;
                        ObjectMatrix[i, j] = block;
                    }
                }
            }
        }

        private int[] FindEmptySpace()
        {
            bool found = false;
            int blockId;
            int x, y;
            Random rnd = new Random();
            int[] coords = new int[2];

            while (!found)
            {
                x = rnd.Next(MAP_MIN_SIZE + 1, MAP_MAX_SIZE - 1);
                y = rnd.Next(MAP_MIN_SIZE + 1, MAP_MAX_SIZE - 1);
                blockId = MapMatrix[x, y];
                if (blockId == SPACE_ID)
                {
                    coords[0] = x;
                    coords[1] = y;
                    return coords;
                }
            }
            return null;
        }

        public void RandomSpawnSpecialItem()
        {
            Item item = RandomizeSpecialItem();
            RandomSpawnItem(item);
        }

        public void RandomSpawnItem<T>(T item)
        {
            //spawn item
            Item ourItem = item as Item;
            int x, y;
            int[] coords = FindEmptySpace();

            if (coords != null)
            {
                x = coords[0];
                y = coords[1];


                if (item is DefaultSpecialWall)
                {
                    ourItem.X = x;
                    ourItem.Y = y;

                    SpawnSpecialItemToServer(ourItem);
                }

                if (item is UpgradedSpecialWall)
                {
                    ourItem.X = x;
                    ourItem.Y = y;

                    SpawnSpecialItemToServer(ourItem);
                }

                if (item is UltimateSpecialWall)
                {
                    ourItem.X = x;
                    ourItem.Y = y;

                    SpawnSpecialItemToServer(ourItem);
                }

                if (item is DefaultSpikes)
                {
                    ourItem.X = x;
                    ourItem.Y = y;

                    SpawnSpecialItemToServer(ourItem);
                }

                if (item is UpgradedSpikes)
                {
                    ourItem.X = x;
                    ourItem.Y = y;

                    SpawnSpecialItemToServer(ourItem);
                }

                if (item is UltimateSpikes)
                {
                    ourItem.X = x;
                    ourItem.Y = y;

                    SpawnSpecialItemToServer(ourItem);
                }

                if (item is Coin)
                {
                    ourItem.X = x;
                    ourItem.Y = y;

                    SpawnSpecialItemToServer(ourItem);
                }

                if (item is Destroyer)
                {
                    ourItem.X = x;
                    ourItem.Y = y;

                    SpawnSpecialItemToServer(ourItem);
                }
            }
        }

        private void SpawnSpecialItemToServer(Item ourItem)
        {
            if (ourItem is Coin)
            {
                connection.SpawnCoin(ourItem.X, ourItem.Y, groupName);
            }
            else
            {
                connection.SpawnSpecialItem(ourItem.X, ourItem.Y, ourItem.GetSpecialItemId(), ourItem.PlayerConnection, groupName);
            }
        }

        //dont need 
        private void GameTimer(object sender, EventArgs e)
        {
        }

        public void AddPlayerItemByServer(int itemId, string connection)
        {
            if (this.connectionId == connection)
            {
                (player1 as Player).inventory.AddItem(itemId);
            }
            else
            {
                (player2 as Player).inventory.AddItem(itemId);
            }
            Draw();
        }

        public void RemovePlayerItemByServer(string connection)
        {
            if (this.connectionId == connection)
            {
                (player1 as Player).inventory.RemoveItem();
            }
            else
            {
                (player2 as Player).inventory.RemoveItem();
            }
            Draw();
        }

        public void SpawnSpecialItemByServer(int X, int Y, int itemId, string playerConnection)
        {
            //Abstract Factory
            AbstractFactory itemFactory = null;
            Item item = null;

            switch (itemId)
            {
                case Item.DEFAULT_SPECIAL_WALL_ID:
                    itemFactory = new DefaultFactory();
                    item = itemFactory.createSpecialWall();
                    item.X = X;
                    item.Y = Y;
                    item.PlayerConnection = playerConnection;
                    ObjectMatrix[X, Y] = item;
                    MapMatrix[X, Y] = SPECIAL_WALL_ID;
                    break;
                case Item.UPGRADED_SPECIAL_WALL_ID:
                    itemFactory = new UpgradedFactory();
                    item = itemFactory.createSpecialWall();
                    item.X = X;
                    item.Y = Y;
                    item.PlayerConnection = playerConnection;
                    ObjectMatrix[X, Y] = item;
                    MapMatrix[X, Y] = SPECIAL_WALL_ID;
                    break;
                case Item.ULTIMATE_SPECIAL_WALL_ID:
                    itemFactory = new UltimateFactory();
                    item = itemFactory.createSpecialWall();
                    item.X = X;
                    item.Y = Y;
                    item.PlayerConnection = playerConnection;
                    ObjectMatrix[X, Y] = item;
                    MapMatrix[X, Y] = SPECIAL_WALL_ID;
                    break;
                case Item.DEFAULT_SPIKES_ID:
                    itemFactory = new DefaultFactory();
                    item = itemFactory.createSpikes();
                    item.X = X;
                    item.Y = Y;
                    item.PlayerConnection = playerConnection;
                    ObjectMatrix[X, Y] = item;
                    MapMatrix[X, Y] = SPIKES_ID;
                    break;
                case Item.UPGRADED_SPIKES_ID:
                    itemFactory = new UpgradedFactory();
                    item = itemFactory.createSpikes();
                    item.X = X;
                    item.Y = Y;
                    item.PlayerConnection = playerConnection;
                    ObjectMatrix[X, Y] = item;
                    MapMatrix[X, Y] = SPIKES_ID;
                    break;
                case Item.ULTIMATE_SPIKES_ID:
                    itemFactory = new UltimateFactory();
                    item = itemFactory.createSpikes();
                    item.X = X;
                    item.Y = Y;
                    item.PlayerConnection = playerConnection;
                    ObjectMatrix[X, Y] = item;
                    MapMatrix[X, Y] = SPIKES_ID;
                    break;
                case Item.COIN_ID:
                    item = new Coin();
                    item.X = X;
                    item.Y = Y;
                    item.PlayerConnection = playerConnection;
                    ObjectMatrix[X, Y] = item;
                    MapMatrix[X, Y] = Item.COIN_ID;
                    break;
                case Item.DESTROYER_ID:
                    item = new Destroyer();
                    item.X = X;
                    item.Y = Y;
                    item.PlayerConnection = playerConnection;
                    ObjectMatrix[X, Y] = item;
                    MapMatrix[X, Y] = Item.DESTROYER_ID;
                    break;
                case Item.STATE_DESTROYED:
                    ObjectMatrix[X, Y] = null;
                    MapMatrix[X, Y] = SPACE_ID;
                    break;
            }
            Draw();
        }

        public void SpawnCoinByServer(int X, int Y)
        {
            Item coin = new Coin();
            MapMatrix[X, Y] = COIN_ID;
            ObjectMatrix[X, Y] = coin;
            Draw();
        }

        public void PlaceItemByServer(int X, int Y, int itemId, string connectionId)
        {
            AbstractFactory itemFactory = null;
            Item item = null;

            if (this.connectionId != connectionId)
            {
                (player2 as Player).inventory.RemoveItem();
            }
            else
            {
                (player1 as Player).inventory.RemoveItem();
            }

            switch (itemId)
            {
                case Item.DEFAULT_SPECIAL_WALL_ID:
                    itemFactory = new DefaultFactory();
                    item = itemFactory.createSpecialWall();
                    item.X = X;
                    item.Y = Y;
                    item.PlayerConnection = connectionId;
                    subject.Attach(item);
                    ObjectMatrix[X, Y] = item;
                    MapMatrix[X, Y] = SPECIAL_WALL_ID;
                    break;
                case Item.UPGRADED_SPECIAL_WALL_ID:
                    itemFactory = new UpgradedFactory();
                    item = itemFactory.createSpecialWall();
                    item.X = X;
                    item.Y = Y;
                    item.PlayerConnection = connectionId;
                    subject.Attach(item);
                    ObjectMatrix[X, Y] = item;
                    MapMatrix[X, Y] = SPECIAL_WALL_ID;
                    break;
                case Item.ULTIMATE_SPECIAL_WALL_ID:
                    itemFactory = new UltimateFactory();
                    item = itemFactory.createSpecialWall();
                    item.X = X;
                    item.Y = Y;
                    item.PlayerConnection = connectionId;
                    subject.Attach(item);
                    ObjectMatrix[X, Y] = item;
                    MapMatrix[X, Y] = SPECIAL_WALL_ID;
                    break;
                case Item.DEFAULT_SPIKES_ID:
                    itemFactory = new DefaultFactory();
                    item = itemFactory.createSpikes();
                    item.X = X;
                    item.Y = Y;
                    item.PlayerConnection = connectionId;
                    subject.Attach(item);
                    ObjectMatrix[X, Y] = item;
                    MapMatrix[X, Y] = SPIKES_ID;
                    break;
                case Item.UPGRADED_SPIKES_ID:
                    itemFactory = new UpgradedFactory();
                    item = itemFactory.createSpikes();
                    item.X = X;
                    item.Y = Y;
                    item.PlayerConnection = connectionId;
                    subject.Attach(item);
                    ObjectMatrix[X, Y] = item;
                    MapMatrix[X, Y] = SPIKES_ID;
                    break;
                case Item.ULTIMATE_SPIKES_ID:
                    itemFactory = new UltimateFactory();
                    item = itemFactory.createSpikes();
                    item.X = X;
                    item.Y = Y;
                    item.PlayerConnection = connectionId;
                    subject.Attach(item);
                    ObjectMatrix[X, Y] = item;
                    MapMatrix[X, Y] = SPIKES_ID;
                    break;
                case Item.COIN_ID:
                    item = new Coin();
                    item.X = X;
                    item.Y = Y;
                    item.PlayerConnection = connectionId;
                    subject.Attach(item);
                    ObjectMatrix[X, Y] = item;
                    MapMatrix[X, Y] = Item.COIN_ID;
                    break;
                case Item.DESTROYER_ID:
                    item = new Destroyer();
                    item.X = X;
                    item.Y = Y;
                    item.PlayerConnection = connectionId;
                    subject.Attach(item);
                    ObjectMatrix[X, Y] = item;
                    MapMatrix[X, Y] = Item.DESTROYER_ID;
                    break;
            }
            Draw();
        }

        public void AddPlayerPointsByServer(int points, string connectionId)
        {
            int playerId = groupPlayers.IndexOf(connectionId) + 1;
            if (points > 0)
            {
                gameLevel.AddPoints(points, playerId);
            }
            else if (points < 0)
            {
                gameLevel.RemovePoints(points, playerId);
            }
            UpdateGame();
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

        private Item RandomizeSpecialItem()
        {
            //Abstract Factory
            //randomizing special item level
            AbstractFactory itemFactory = null;

            Random rnd = new Random();
            switch (rnd.Next(1, 4))
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
            switch (rnd.Next(1, 4))
            {
                case 1:
                    return itemFactory.createSpecialWall();
                case 2:
                    return itemFactory.createSpikes();
                case 3:
                    return new Destroyer();
            }
            return null;
        }

        /*public void AddToMap(GraphicalElement ge, int x, int y)
        {
            connection.SpawnSpecialItem(x, y, (ge as Item).GetSpecialItemId(), groupName);
            //Draw();
        }*/

        //game spawn logic
        private void UpdateGame()
        {
            if (MainPlayer)
            {
                int updatedRoundPoint = gameLevel.GetCurrentCoin();
                if (currentRoundPoint != updatedRoundPoint)
                {
                    currentRoundPoint = updatedRoundPoint;

                    gameRounds.SpawnItems();
                }
            }
            Draw();
        }

        private void SteppedOnItem(Player player, Item item)
        {
            //Strategy
            //player
            if (item.PlayerConnection == connectionId)
            {
                if (item is SpecialWall)
                {
                    item.SetAlgorithm(new PlayerSpecialWallAlgorithm());
                }
                else if (item is Spikes)
                {
                    item.SetAlgorithm(new PlayerSpikesAlgorithm());
                }
            }
            else
            {
                //enemy 
                if (item is SpecialWall)
                {
                    item.SetAlgorithm(new EnemySpecialWallAlgorithm());
                }
                else if (item is Spikes)
                {
                    item.SetAlgorithm(new EnemySpikesAlgorithm());
                }
            }
            item.ItemActivated(player);
        }

        public void AddPlayerPoints(int value, string connectionId)
        {
            connection.AddPlayerPoints(value, connectionId, groupName);
        }

        private void CheckForItem(Player player, int x, int y)
        {
            int blockId = MapMatrix[x, y];
            if (blockId != SPACE_ID)
            {
                GraphicalElement element = ObjectMatrix[x, y];
                if ((element as Item).PlayerConnection == "")
                {
                    switch (blockId)
                    {
                        case SPACE_ID:
                            break;
                        case PLAYER_ID:
                            break;
                        case WALL_ID:
                            break;
                        case Item.COIN_ID:
                            AddPlayerPoints((element as Coin).Value, connectionId);
                            ObjectMatrix[x, y] = null;
                            break;
                        case SPECIAL_WALL_ID:
                            if (element is DefaultSpecialWall)
                            {
                                if (player.inventory.CanAddItem())
                                {
                                    connection.AddPlayerItem(Item.DEFAULT_SPECIAL_WALL_ID, connectionId, groupName);
                                    subject.Deattach(element as Item);
                                    ObjectMatrix[x, y] = null;
                                }
                            }
                            else if (element is UpgradedSpecialWall)
                            {
                                if (player.inventory.CanAddItem())
                                {
                                    connection.AddPlayerItem(Item.UPGRADED_SPECIAL_WALL_ID, connectionId, groupName);
                                    subject.Deattach(element as Item);
                                    ObjectMatrix[x, y] = null;
                                }
                            }
                            else if (element is UltimateSpecialWall)
                            {
                                if (player.inventory.CanAddItem())
                                {
                                    connection.AddPlayerItem(Item.ULTIMATE_SPECIAL_WALL_ID, connectionId, groupName);
                                    subject.Deattach(element as Item);
                                    ObjectMatrix[x, y] = null;
                                }
                            }
                            else
                            {
                            }
                            break;
                        case Item.DESTROYER_ID:
                            if (player.inventory.CanAddItem())
                            {
                                connection.AddPlayerItem(Item.DESTROYER_ID, connectionId, groupName);
                                subject.Deattach(element as Item);
                                ObjectMatrix[x, y] = null;
                            }
                            break;
                        case SPIKES_ID:
                            if (element is DefaultSpikes)
                            {
                                if (player.inventory.CanAddItem())
                                {
                                    connection.AddPlayerItem(Item.DEFAULT_SPIKES_ID, connectionId, groupName);
                                    subject.Deattach(element as Item);
                                    ObjectMatrix[x, y] = null;
                                }
                            }
                            else if (element is UpgradedSpikes)
                            {
                                if (player.inventory.CanAddItem())
                                {
                                    connection.AddPlayerItem(Item.UPGRADED_SPIKES_ID, connectionId, groupName);
                                    subject.Deattach(element as Item);
                                    ObjectMatrix[x, y] = null;
                                }
                            }
                            else if (element is UltimateSpikes)
                            {
                                if (player.inventory.CanAddItem())
                                {
                                    connection.AddPlayerItem(Item.ULTIMATE_SPIKES_ID, connectionId, groupName);
                                    subject.Deattach(element as Item);
                                    ObjectMatrix[x, y] = null;
                                }
                            }
                            else
                            {
                            }
                            break;
                    }
                }
                else
                {
                    SteppedOnItem(player, element as Item);
                }
            }
        }

        private void UpdatePlayerPosition(int pos)
        {
            Player player = player1 as Player;
            switch (pos)
            {
                case LEFT:
                    if (player1.X - 1 > MAP_MIN_SIZE && (MapMatrix[player1.X - 1, player1.Y] != WALL_ID && MapMatrix[player1.X - 1, player1.Y] != PLAYER_ID))
                    {
                        CheckForItem(player, player1.X - 1, player1.Y);
                        if (!player.Freezed)
                        {
                            SetMap(player1.X, player1.Y, SPACE_ID);
                            player1.X = player1.X - 1;
                        }
                        else
                        {
                            player.Freezed = false;
                        }
                    }
                    break;
                case UP:
                    if (player1.Y - 1 > MAP_MIN_SIZE && (MapMatrix[player1.X, player1.Y - 1] != WALL_ID && MapMatrix[player1.X, player1.Y - 1] != PLAYER_ID))
                    {
                        CheckForItem(player1 as Player, player1.X, player1.Y - 1);
                        if (!player.Freezed)
                        {
                            SetMap(player1.X, player1.Y, SPACE_ID);
                            player1.Y = player1.Y - 1;
                        }
                        else
                        {
                            player.Freezed = false;
                        }
                    }
                    break;
                case RIGHT:
                    if (player1.X + 1 < MAP_MAX_SIZE && (MapMatrix[player1.X + 1, player1.Y] != WALL_ID && MapMatrix[player1.X + 1, player1.Y] != PLAYER_ID))
                    {
                        CheckForItem(player1 as Player, player1.X + 1, player1.Y);
                        if (!player.Freezed)
                        {
                            SetMap(player1.X, player1.Y, SPACE_ID);
                            player1.X = player1.X + 1;
                        }
                        else
                        {
                            player.Freezed = false;
                        }
                    }
                    break;
                case DOWN:
                    if (player1.Y + 1 < MAP_MAX_SIZE && (MapMatrix[player1.X, player1.Y + 1] != WALL_ID && MapMatrix[player1.X, player1.Y + 1] != PLAYER_ID))
                    {
                        CheckForItem(player1 as Player, player1.X, player1.Y + 1);
                        if (!player.Freezed)
                        {
                            SetMap(player1.X, player1.Y, SPACE_ID);
                            player1.Y = player1.Y + 1;
                        }
                        else
                        {
                            player.Freezed = false;
                        }
                    }
                    break;
            }
            connection.GameMapUpdatePlayerPos(player1.X.ToString(), player1.Y.ToString(), connectionId, groupName);

            UpdatePlayersPosMap();
            Draw();
        }

        private void UsePlayerItem()
        {
            int itemId = (player1 as Player).inventory.GetItem(0);
            if (itemId != -1 && itemId != DESTROYER_ID)
            {
                if (MapMatrix[player1.X, player1.Y + 1] == SPACE_ID)
                {
                    connection.PlaceItem(player1.X, player1.Y + 1, itemId, connectionId, groupName);
                    return;
                }
                if (MapMatrix[player1.X + 1, player1.Y + 1] == SPACE_ID)
                {
                    connection.PlaceItem(player1.X + 1, player1.Y + 1, itemId, connectionId, groupName);
                    return;
                }

                if (MapMatrix[player1.X + 1, player1.Y] == SPACE_ID)
                {
                    connection.PlaceItem(player1.X + 1, player1.Y, itemId, connectionId, groupName);
                    return;
                }

                if (MapMatrix[player1.X + 1, player1.Y - 1] == SPACE_ID)
                {
                    connection.PlaceItem(player1.X + 1, player1.Y - 1, itemId, connectionId, groupName);
                    return;
                }

                if (MapMatrix[player1.X, player1.Y - 1] == SPACE_ID)
                {
                    connection.PlaceItem(player1.X, player1.Y - 1, itemId, connectionId, groupName);
                    return;
                }

                if (MapMatrix[player1.X - 1, player1.Y - 1] == SPACE_ID)
                {
                    connection.PlaceItem(player1.X - 1, player1.Y - 1, itemId, connectionId, groupName);
                    return;
                }

                if (MapMatrix[player1.X - 1, player1.Y] == SPACE_ID)
                {
                    connection.PlaceItem(player1.X - 1, player1.Y, itemId, connectionId, groupName);
                    return;
                }

                if (MapMatrix[player1.X - 1, player1.Y + 1] == SPACE_ID)
                {
                    connection.PlaceItem(player1.X - 1, player1.Y + 1, itemId, connectionId, groupName);
                    return;
                }
            }
            else if (itemId == DESTROYER_ID)
            {
                connection.RemovePlayerItem(connectionId, groupName);

                //Observer
                Item destroyer = new Destroyer();
                subject.Attach(destroyer);
                destroyer.UseItem();
                UpdateMap();
            }
        }

        private void SetMap(int X, int Y, int id)
        {
            MapMatrix[X, Y] = id;
        }

        private Color mainTextColor;

        public void SetTextColorByServer(string color)
        {
            mainTextColor = Color.FromName(color);
            if(mainTextColor == null)
            {
                mainTextColor = Color.White;
            }
        }

        public void SetTextColor(Color color)
        {
            string colorName = color.Name;
            connection.SetTextColor(colorName, groupName);
        }

        private void UpdatePlayersPosMap()
        {
            //update map from GraphicElements objects
            MapMatrix[player1.X, player1.Y] = PLAYER_ID;
            MapMatrix[player2.X, player2.Y] = PLAYER_ID;

            ObjectMatrix[player1.X, player1.Y] = player1;
            ObjectMatrix[player2.X, player2.Y] = player1;
        }

        private void UpdateMap()
        {
            GraphicalElement element;
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    element = ObjectMatrix[i, j];
                    if (element is Item)
                    {
                        if ((element as Item).State == Item.STATE_DESTROYED)
                        {
                            connection.SpawnSpecialItem(i, j, Item.STATE_DESTROYED, "", groupName);
                        }
                    }
                }
            }
        }

        public int GetPlayerItem(int playerId, int inventoryId)
        {
            int itemId = -1;
            if (playerId == PLAYER_1_ID)
            {
                itemId = (player1 as Player).inventory.GetItem(inventoryId);
            }
            if (playerId == PLAYER_2_ID)
            {
                itemId = (player2 as Player).inventory.GetItem(inventoryId);
            }
            return itemId;
        }

        private void Draw()
        {
            //UpdateGame();
            canvas.Refresh();

            // Create string to draw.
            string coinsLeft = (GameLevel.MAX_COIN_COUNT - gameLevel.GetCurrentCoin()) + " coins";
            string player1Score = gameLevel.GetPlayerPoints(PLAYER_1_ID) + " points";
            string player2Score = gameLevel.GetPlayerPoints(PLAYER_2_ID) + " points";

            // Create font and brush.
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(mainTextColor);
            //SolidBrush drawBrush = new SolidBrush(Color.White);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            //drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;

            // Draw string to screen.
            g.DrawString(coinsLeft, drawFont, drawBrush, ((MAP_MAX_SIZE / 2) - 1) * BLOCK_SIZE, MAP_MAX_SIZE / 2 * BLOCK_SIZE, drawFormat);
            if (MainPlayer)
            {
                g.DrawString(player2Score, drawFont, drawBrush, (MAP_MAX_SIZE - 3) * BLOCK_SIZE, ((MAP_MAX_SIZE / 2) - 3) * BLOCK_SIZE, drawFormat);
                g.DrawString(player1Score, drawFont, drawBrush, (MAP_MIN_SIZE + 1) * BLOCK_SIZE, ((MAP_MAX_SIZE / 2) - 3) * BLOCK_SIZE, drawFormat);
            }
            else
            {
                g.DrawString(player1Score, drawFont, drawBrush, (MAP_MAX_SIZE - 3) * BLOCK_SIZE, ((MAP_MAX_SIZE / 2) - 3) * BLOCK_SIZE, drawFormat);
                g.DrawString(player2Score, drawFont, drawBrush, (MAP_MIN_SIZE + 1) * BLOCK_SIZE, ((MAP_MAX_SIZE / 2) - 3) * BLOCK_SIZE, drawFormat);
            }

            DrawShapeHandler rectangleHandler = new RectangleShape();
            DrawShapeHandler ovalHandler = new OvalShape();
            DrawShapeHandler triangleHandler = new TriangleShape();
            DrawShapeHandler inventoryHandler = new InventoryShape();

            rectangleHandler.SetNext(ovalHandler);
            ovalHandler.SetNext(triangleHandler);
            triangleHandler.SetNext(inventoryHandler);

            int itemId = -1;
                    
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    int blockId;
                    GraphicalElement element;
                    if (MainPlayer)
                    {
                        blockId = MapMatrix[i, j];
                        element = ObjectMatrix[i, j];
                    }
                    else
                    {
                        blockId = MapMatrix[MAP_MAX_SIZE - i, j];
                        element = ObjectMatrix[MAP_MAX_SIZE - i, j];
                    }

                    if(element is Item)
                    {
                        itemId = (element as Item).GetSpecialItemId();
                    }
                    
                    rectangleHandler.DrawShape(blockId, itemId, i, j);
                }
            }
        }

        private void KeyIsPressed(object sender, KeyPressEventArgs e)
        {
            if (MainPlayer)
            {
                if (e.KeyChar.Equals(' '))
                {
                    UsePlayerItem();
                }
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
            else
            {
                if (e.KeyChar.Equals(' '))
                {
                    UsePlayerItem();
                }
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
                    UpdatePlayerPosition(RIGHT);
                }
                if (e.KeyChar.Equals('d'))
                {
                    UpdatePlayerPosition(LEFT);
                }
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
