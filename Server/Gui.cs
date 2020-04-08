using ConsoleApp8.Model;
using Server.Model;
using Sweng3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class Gui
    {
        void arrayToString(Player[] inp)
        {
            foreach (var i in inp)
            { try
                { Console.Write(" "+i.id.ToString()+" ", Console.ForegroundColor= i.color); }
                catch (Exception e)
                { Console.Write("   "); }
            }
         

        }
        public async Task Show()
        {
            Console.Clear();
            Console.Write("--------------------------------------------------------------\n|", Console.ForegroundColor = ConsoleColor.White);
            for (int i = Field.Instance.yLenght; i >= 0; i--)
            {
                var val = new Player[Field.Instance.xLenght];
                var vals = Field.Instance.players.Where(x => x.y == i).OrderBy(z => z.y).ToList<Player>();
                foreach (var k in vals)
                {
                    val[k.x] = k;
                }
                arrayToString(val);
                Console.Write("|\n|", Console.ForegroundColor = ConsoleColor.White);
            }
            Console.WriteLine("--------------------------------------------------------------", Console.ForegroundColor = ConsoleColor.White);

        }
    }
}


/* Console.WriteLine(program.clients[0].id, Console.ForegroundColor = ConsoleColor.Blue);
 Console.WriteLine(program.clients[1].id, Console.ForegroundColor = ConsoleColor.Red);
 for (int k = 0; k < Field.Instance.width * 8; k++)
     Console.Write("_", Console.ForegroundColor = ConsoleColor.Green);
 foreach (var i in Field.Instance.playerTypes)
 {
     if (Field.Instance.playerTypes.IndexOf(i) > (Field.Instance.width / 2) - 2 && Field.Instance.playerTypes.IndexOf(i) < (Field.Instance.width / 2) + 2)
         Console.Write("\r\n|", Console.ForegroundColor = ConsoleColor.Yellow);
     Console.Write("\r\n|", Console.ForegroundColor = ConsoleColor.Green);
     foreach (var j in i)
     {
         if (j.IsEmpty)
         {
             Console.Write("\t");
             continue;
         }
         ConsoleColor clr = new ConsoleColor();
         switch (j.Color)
         {
             case PlayerType.Blue:
                 clr = ConsoleColor.Blue;
                 break;
             case PlayerType.Red:
                 clr = ConsoleColor.Red;
                 break;
             case PlayerType.Ball:
                 clr = ConsoleColor.Yellow;
                 Console.Write("X\t", Console.ForegroundColor = clr);
                 continue;
         }
         Console.Write(j.Id + "\t", Console.ForegroundColor = clr);
     }

     if (Field.Instance.playerTypes.IndexOf(i) > (Field.Instance.width / 2) - 2 && Field.Instance.playerTypes.IndexOf(i) < (Field.Instance.width / 2) + 2)
         Console.Write( "|", Console.ForegroundColor = ConsoleColor.Yellow);
     Console.Write("|", Console.ForegroundColor = ConsoleColor.Green);
 }
 Console.Write("\n");
 for (int k = 0; k < Field.Instance.width * 8; k++)
     Console.Write("_", Console.ForegroundColor = ConsoleColor.Green);
 await Task.Delay(1000);
 Console.Clear();



}
}
}*/
