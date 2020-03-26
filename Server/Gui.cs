using ConsoleApp8.Model;
using Sweng3;
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
                Console.WriteLine(program.clients[0].id, Console.ForegroundColor = ConsoleColor.Blue);
                Console.WriteLine(program.clients[1].id, Console.ForegroundColor = ConsoleColor.Red);
                for (int k = 0; k < Field.Instance.width * 8; k++)
                    Console.Write("_", Console.ForegroundColor = ConsoleColor.Green);
                foreach (var i in Field.Instance._field)
                {
                    if (Field.Instance._field.IndexOf(i) > (Field.Instance.width / 2) - 2 && Field.Instance._field.IndexOf(i) < (Field.Instance.width / 2) + 2)
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
                        }
                        Console.Write(j.Id + "\t", Console.ForegroundColor = clr);
                    }

                    if (Field.Instance._field.IndexOf(i) > (Field.Instance.width / 2) - 2 && Field.Instance._field.IndexOf(i) < (Field.Instance.width / 2) + 2)
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
    }
}
