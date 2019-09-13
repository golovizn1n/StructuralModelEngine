using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
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
        public double xt, yt, zt;

        public int number;

        public CoordinateSystemVisual3D csv3d;

       /* public void RotateCsByVector3D(Vector3D v)
        {
            Vector3D x = new Vector3D(v.X, 0.0, 0.0);
            Vector3D y = new Vector3D(0.0, v.Y, 0.0);
            Vector3D z = new Vector3D(0.0, 0.0, v.Z);

            var m3d = MainWindow.BuildCosinesMatrix(x, y, z);
            MatrixTransform3D mt3d = new MatrixTransform3D(m3d);

            csv3d.Transform = mt3d;
        }*/
    }
}
