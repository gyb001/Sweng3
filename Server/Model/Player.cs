using ConsoleApp8.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Model
{
    class Player
    {
        public int x;
        public int y;
        public int id;
        public PlayerType playerType;
        public ConsoleColor color;
        public Player(int x, int y, int id, PlayerType playerType)
        {
            this.x = x;
            this.y = y;
            this.id = id;
            this.playerType = playerType;
            if (playerType == PlayerType.Red)
                color = ConsoleColor.Red;
            if (playerType == PlayerType.Blue)
                color = ConsoleColor.Blue;
            if (playerType == PlayerType.Ball)
                color = ConsoleColor.Yellow;
        }
    }
}
