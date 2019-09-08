using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralModelEngine
{
    class Node
    {
        public Node(int Number)
        {
            x = 0f;
            y = 0f;
            z = 0f;
            number = Number;
        }

        public float x, y, z;
        public int number;
    }
}
