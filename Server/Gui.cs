using ConsoleApp8.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class Gui
    {
        public async Task Show()
        {
            while (true)
            {
                foreach (var i in Field.Instance._field)
                {
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
                            case Color.Blue:
                                clr = ConsoleColor.Blue;
                                break;
                            case Color.Red:
                                clr = ConsoleColor.Red;
                                break;
                            case Color.Yellow:
                                clr = ConsoleColor.Yellow;
                                Console.Write("X\t", Console.ForegroundColor = clr);
                                continue;
                                break;
                        }
                        Console.Write(j.Id + "\t", Console.ForegroundColor = clr);
                    }
                    Console.Write("\r\n");
                }
                await Task.Delay(1000);
                Console.Clear();
            }
        }
    }
}
