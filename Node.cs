using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelixToolkit.Wpf;

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

            csv3d = new CoordinateSystemVisual3D();
        }

        public double x, y, z;
        public int number;

        public CoordinateSystemVisual3D csv3d;
    }
}
