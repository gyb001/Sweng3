using Newtonsoft.Json;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8.Model
{

    class Field
    {
        private static Field instance = null;
        private static readonly object padlock = new object();



        public static Field Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Field(20, 10);
                    }
                    return instance;
                }
            }
        }
        public int xLenght;//20
        public int yLenght;//10

        public List<Player> players;

        public Field(int xLenght, int yLenght)
        {
            players = new List<Player>();
            this.xLenght = xLenght;
            this.yLenght = yLenght;

            players.Add(new Player(1, 5, 1, PlayerType.Blue));
            players.Add(new Player(5, 2, 2, PlayerType.Blue));
            players.Add(new Player(5, 8, 3, PlayerType.Blue));
            players.Add(new Player(8, 3, 4, PlayerType.Blue));
            players.Add(new Player(8, 7, 5, PlayerType.Blue));

            players.Add(new Player(xLenght - 1, 5, 1, PlayerType.Red));
            players.Add(new Player(xLenght - 5, 2, 2, PlayerType.Red));
            players.Add(new Player(xLenght - 5, 8, 3, PlayerType.Red));
            players.Add(new Player(xLenght - 8, 3, 4, PlayerType.Red));
            players.Add(new Player(xLenght - 8, 7, 5, PlayerType.Red));

            players.Add(new Player(xLenght / 2, yLenght / 2, 0, PlayerType.Ball));
        }



        public void jsonTOobject(string JSON)
        {
            instance.players = JsonConvert.DeserializeObject<List<Player>>(JSON);
        }
        public string objectTOjson()
        {
            return JsonConvert.SerializeObject(instance.players);
        }
    }
}
