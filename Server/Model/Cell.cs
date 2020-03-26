using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp8.Model
{
    class Cell
    {
        bool isEmpty;
        int id;
        Color _color;

        public bool IsEmpty { get => isEmpty; set => isEmpty = value; }
        public int Id { get => id; set => id = value; }
        public Color Color { get => _color; set => _color = value; }
    }
}
