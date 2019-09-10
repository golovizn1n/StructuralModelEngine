using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralModelEngine
{
    public class Node
    {
        public Node(int Number)
        {
            x = 0.0;
            y = 0.0;
            z = 0.0;
            number = Number;
        }

        public double x, y, z;
        public int number;
    }
}
