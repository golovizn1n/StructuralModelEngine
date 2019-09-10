using System;
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
         
        //Выводим некторое сообщение в лог
        void DebugMsg(string msg)
        {
            TextRange doc = new TextRange(debugOutput.Document.ContentStart, debugOutput.Document.ContentEnd);

            Dispatcher.Invoke(()=> 
            {
                doc.Text += msg + Environment.NewLine;
            });
            
        }

        
    }
}
