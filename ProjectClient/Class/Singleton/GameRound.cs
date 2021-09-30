using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class
{
     public class GameRound
    {
        //private static GameRound instance = null;

        private int Player1Score { get; set; }
        private int Player2Score { get; set; }

        private static class SingletonHolder
        {
            public static GameRound instance = new GameRound();
        }

        private GameRound()
        {
            //singleton initialized
            this.Player1Score = 0;
            this.Player2Score = 0;
        }

        public static GameRound getInstance()
        {
            //singleton initialized via holder
            return SingletonHolder.instance;
        }

        public int GetPlayerPoints(int playerId)
        {
            switch (playerId)
            {
                case 1:
                    return Player1Score;
                case 2:
                    return Player2Score;
                default:
                    return -1;
            }
        }

        public void AddPoints(int points, int playerId)
        {
            switch (playerId)
            {
                case 1:
                    Player1Score += points;
                    break;
                case 2:
                    Player2Score += points;
                    break;
            }
        }

        public void RemovePoints(int points, int playerId)
        {
            switch (playerId)
            {
                case 1:
                    if ((Player1Score - points) < 0)
                    {
                        Player1Score = 0;
                    }
                    else
                    {
                        Player1Score += points;
                    }
                    break;
                case 2:
                    if ((Player2Score - points) < 0)
                    {
                        Player2Score = 0;
                    }
                    else
                    {
                        Player2Score += points;
                    }
                    break;
            }
        }

    }

    /*
    public class Singleton
    {
        private static class SingletonHolder
        {
            public static Singleton instance = new Singleton();
        }

        public static Singleton getInstance()
        {
            return SingletonHolder.instance;
        }
    }*/
}
