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

            cts = new CancellationTokenSource();
            token = cts.Token;

            Task.Run(()=> 
            {
               LoopAnalyzeText();
            }, token);

            cts.Cancel();
            DebugMsg("Cancellation requested.");

            //Освобождаю какие-то ресурсы...
            cts.Dispose();

        }

        CancellationTokenSource cts;
        CancellationToken token;

        MeshGeometry3D mesh = new MeshGeometry3D();

        Model m = new Model();
        
       
        void AddSphere(double x, double y, double z, double r)
        {
            this.Dispatcher.Invoke(() =>
            {
                var s = new SphereVisual3D();
                s.Center = new Point3D(x, y, z);
                s.Radius = r;
                s.Material = Materials.Red;
                model.Children.Add(s);
            });
           /* mesh.Positions.Add(new Point3D(x + 5.0, y, z));
            mesh.Positions.Add(new Point3D(x, y + 5.0, z));
            mesh.Positions.Add(new Point3D(x, y, z));

            var n = mesh.Positions.Count;

            mesh.TriangleIndices.Add(n - 3);
            mesh.TriangleIndices.Add(n - 2);
            mesh.TriangleIndices.Add(n - 1);*/

        }

        void AnalyzeText()
        {
            DebugMsg("AnalyzeText() поток(" + Thread.CurrentThread.ManagedThreadId + ")");
            
            #region Распознаем текст, добавляем узлы
            TextRange doc = new TextRange(textEdit.Document.ContentStart, textEdit.Document.ContentEnd);
            m.nodes.Clear();

            Dispatcher.Invoke(()=> { model.Children.Clear(); });
            
            var lines = doc.Text.Split('\n');

            foreach (var l in lines)
            {
                var words = l.Split(" ".ToCharArray());

                if (words[0] == "node")
                {
                    try
                    {
                        var x = Convert.ToSingle(words[1]);
                        var y = Convert.ToSingle(words[2]);
                        var z = Convert.ToSingle(words[3]);

                        m.AddNode(x, y, z);
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }
            }

            
            foreach (var n in m.nodes)
            {
                AddSphere(n.x, n.y, n.z, 0.2);
            }

            #endregion

        }
        
        void DebugMsg(string msg)
        {
            TextRange doc = new TextRange(debugOutput.Document.ContentStart, debugOutput.Document.ContentEnd);

            Dispatcher.Invoke(()=> 
            {
                doc.Text += msg + Environment.NewLine;
            });
            
        }

        void LoopAnalyzeText()
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    DebugMsg("LoopAnalyzeText canceled");
                    break;
                }
                AnalyzeText();
                System.Threading.Thread.Sleep(2000);
            }
        }

    }
}
