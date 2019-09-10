using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralModelEngine
{
    class Vector3
    {
        public double x, y, z;
        public double w;

        public Vector3(Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
            w = v.w;
        }

        public Vector3()
        {
            x = 0.0;
            y = 0.0;
            z = 0.0;
            w = 1.0;
        }

        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            this.w = 1.0;
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vector3 operator *(Vector3 v1, double f)
        {
            return new Vector3(v1.x * f, v1.y * f, v1.z * f);
        }
        public static Vector3 operator /(Vector3 v1, double f)
        {
            return new Vector3(v1.x / f, v1.y / f, v1.z / f);
        }

        public static double DP(Vector3 v1, Vector3 v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }
        public static Vector3 CP(Vector3 v1, Vector3 v2)
        {
            Vector3 res = new Vector3();

            res.x = v1.y * v2.z - v1.z * v2.y;
            res.y = v1.z * v2.x - v1.x * v2.z;
            res.z = v1.x * v2.y - v1.y * v2.x;
            
            return res;
        }

        public double DP(Vector3 v)
        {
            return x * v.x + y * v.y + z * v.z;
        }
        public Vector3 CP(Vector3 v)
        {
            Vector3 res = new Vector3();

            res.x = y * v.z - z * v.y;
            res.y = z * v.x - x * v.z;
            res.z = x * v.y - y * v.x;

            return res;
        }

        public double Length { get { return Math.Sqrt(x * x + y * y + z * z); } }

        public Vector3 Normalized
        {
            get
            {
                double l = Length;
                return new Vector3(x / l, y / l, z / l);
            }
        }

                
        public void Translate(double x, double y, double z)
        {
            this.x += x;
            this.y += y;
            this.z += z;
        }

        public void Translate(Vector3 v)
        {
            x += v.x;
            y += v.y;
            z += v.z;
        }

        public Vector3 ScaledIntoView(double width, double height)
        {
            Vector3 res = new Vector3(this);

            if (w != 0.0)
            {
                res.x /= w;
                res.y /= w;
                res.z /= w;
            }

            res.Translate(1.0, 1.0, 0.0);
            res.x *= 0.5 * width;
            res.y *= 0.5 * height;

            return res;
        }
        
        public override string ToString()
        {
            return x.ToString("0.00") + " " + y.ToString("0.00") + " " + z.ToString("0.00");
        }
    }
}
