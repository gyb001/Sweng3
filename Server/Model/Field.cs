using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8.Model
{
    //a gombfoci asztal mérete: 55x93x3 cm
    //játékos: , 19 mm-es forgácslap
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
                        instance = new Field(10,10);
                    }
                    return instance;
                }
            }
        }
        int height;
        int width;
        int numOfplayer = 22;
        public List<List<Cell>> _field;

        public Field(int height, int width)
        {
            this.height = height;
            this.width = width;
            generateField();
            RandomPalyerBlue();
            RandomPalyerred();

            _field[height / 2][width / 2].IsEmpty = false;
            _field[height / 2][width / 2].Id = -1;
            _field[height / 2][width / 2].Color = Color.Yellow;
        }

        private void generateField()
        {

            _field = new List<List<Cell>>();
            for (int i = 0; i < height; i++)
            {
                _field.Add(new List<Cell>());
                for (int j = 0; j < width; j++)
                {
                    _field[i].Add(new Cell() { IsEmpty=true});
                }
            }
        }

        public void RandomPalyerBlue()
        {
            for (int i = 0; i < 22; i++)
            {
                int x = new Random().Next(height / 2-1);
                int y = new Random().Next(width);
                _field[y][x].IsEmpty = false;
                _field[y][x].Id = i;
                _field[y][x].Color = Color.Blue;
            }
        }
        public void RandomPalyerred()
        {
            for (int i = 0; i < 22; i++)
            {
                int x = new Random().Next(height / 2+1,height);
                int y = new Random().Next(width);
                _field[y][x].IsEmpty = false;
                _field[y][x].Id = i;
                _field[y][x].Color = Color.Red;
            }
        }
        public void jsonTOobject(string JSON)
        {
            instance._field = JsonConvert.DeserializeObject<List<List<Cell>>>(JSON);
        }
        public  string objectTOjson()
        {
            return JsonConvert.SerializeObject(instance._field);
        }
    }
}
