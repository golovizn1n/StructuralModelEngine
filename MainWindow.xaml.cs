﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using System.Text.RegularExpressions;
using HelixToolkit.Wpf;

namespace StructuralModelEngine
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            meshMain.Positions.Clear();
            meshMain.TriangleIndices.Clear();

            DebugMsg("Program started.");

            TextAnalyzer textAnalyzer = new TextAnalyzer(this);
            textAnalyzer.Start();
        } 

        public StructuralModel structuralModel = new StructuralModel();
       
        public void AddSphere(double x, double y, double z, double r)
        {
            this.Dispatcher.Invoke(() =>
            {
                var s = new SphereVisual3D();
                s.Center = new Point3D(x, y, z);
                s.Radius = r;
                s.Material = Materials.Red;
                modelVisual3D.Children.Add(s);
                
            });

            //Этот способ создаст сферу в том же мэше, что возможно ускорит отрисовку
           /* mesh.Positions.Add(new Point3D(x + 5.0, y, z));
            mesh.Positions.Add(new Point3D(x, y + 5.0, z));
            mesh.Positions.Add(new Point3D(x, y, z));

            var n = mesh.Positions.Count;

            mesh.TriangleIndices.Add(n - 3);
            mesh.TriangleIndices.Add(n - 2);
            mesh.TriangleIndices.Add(n - 1);*/

        }

        public void AddCs(double x, double y, double z)
        {
            this.Dispatcher.Invoke(() =>
            {
                var t = new CoordinateSystemVisual3D();

               // Transform3DGroup tg = new Transform3DGroup();
                
                //tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1.0, 0.0, 0.0), 45.0)));
               // tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0.0, 1.0, 0.0), 45.0)));
               // tg.Children.Add(new TranslateTransform3D(x, y, z));

                //t.Transform = tg;
                modelVisual3D.Children.Add(t);
            });
        }

        public void AddCs(Vector3D position, Vector3D target)
        {
            this.Dispatcher.Invoke(() =>
            {
                var ld = (target - position);
                ld.Normalize();
                Vector3D x = ld;
                Vector3D z = new Vector3D(0.0, 0.0, 1.0);
                Vector3D y = Vector3D.CrossProduct(x, z);
                z = Vector3D.CrossProduct(x, y);

                var t = new CoordinateSystemVisual3D();
                var cm = BuildCosinesMatrix(x, y, z);

               Transform3DGroup tg = new Transform3DGroup();
                
                tg.Children.Add(new MatrixTransform3D(cm));
                tg.Children.Add(new TranslateTransform3D(position.X, position.Y, position.Z));

               t.Transform = tg;
                modelVisual3D.Children.Add(t);
            });
        }

        //Выводим некторое сообщение в лог
        void DebugMsg(string msg)
        {
            TextRange doc = new TextRange(debugOutput.Document.ContentStart, debugOutput.Document.ContentEnd);

            Dispatcher.Invoke(()=> 
            {
                doc.Text += msg + Environment.NewLine;
            });
            
        }

        public Matrix3D BuildCosinesMatrix(Vector3D x1, Vector3D y1, Vector3D z1)
        {
            Matrix3D matrix = new Matrix3D();

            x1.Normalize();
            y1.Normalize();
            z1.Normalize();            

            Vector3D x0 = new Vector3D(1.0, 0.0, 0.0);
            Vector3D y0 = new Vector3D(0.0, 1.0, 0.0);
            Vector3D z0 = new Vector3D(0.0, 0.0, 1.0);
            
            matrix.M11 = VecCos(x0, x1);
            matrix.M21 = -VecCos(x0, y1); //-
            matrix.M31 = -VecCos(x0, z1); //-

            matrix.M12 = VecCos(y0, x1);
            matrix.M22 = -VecCos(y0, y1); //-
            matrix.M32 = -VecCos(y0, z1); //-

            matrix.M13 = VecCos(z0, x1);
            matrix.M23 = VecCos(z0, y1);
            matrix.M33 = -VecCos(z0, z1); //-

            #region Из старого проекта
            /* OLD
              matrix.M11 = VecCos(x0, x1);
            matrix.M22 = -VecCos(y0, y1); //-
            matrix.M33 = -VecCos(z0, z1); //-

            matrix.M21 = VecCos(y0, x1);
            matrix.M31 = VecCos(z0, x1);

            matrix.M12 = -VecCos(x0, y1); //-
            matrix.M32 = VecCos(z0, y1);
            matrix.M13 = -VecCos(x0, z1); //-

            matrix.M23 = -VecCos(y0, z1); //-
             */
            #endregion

            return matrix;
        }

        //Векторы д/б нормализованы
        static double VecCos(Vector3D v1, Vector3D v2)
        {
            return Vector3D.DotProduct(v1, v2);
        }
        
    }
}
