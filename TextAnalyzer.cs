using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Threading;

namespace StructuralModelEngine
{
    class TextAnalyzer
    {
        //Передается ли оно по ссылке или все же надо писать ref?
        public TextAnalyzer(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            cts = new CancellationTokenSource();
            token = cts.Token;
        }

        MainWindow mainWindow;

        CancellationTokenSource cts;
        CancellationToken token;

        public void Start()
        {
            Task.Run(() => { LoopAnalyzeText(); }, token);
        }

        public void Stop()
        {
            cts.Cancel();
            
            //Освобождаю какие-то ресурсы...
            cts.Dispose();
        }

        void AnalyzeText()
        {
            #region Распознаем текст, добавляем узлы
            TextRange doc = new TextRange(mainWindow.textEdit.Document.ContentStart, mainWindow.textEdit.Document.ContentEnd);
            mainWindow.structuralModel.nodes.Clear();

            mainWindow.Dispatcher.Invoke(() => { mainWindow.modelVisual3D.Children.Clear(); });

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

                        mainWindow.structuralModel.AddNode(x, y, z);
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }
            }


            foreach (var n in mainWindow.structuralModel.nodes)
            {
                mainWindow.AddSphere(n.x, n.y, n.z, 0.2);
            }

            #endregion
        }
        
        void LoopAnalyzeText()
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    //DebugMsg("LoopAnalyzeText canceled");
                    break;
                }
                AnalyzeText();
                System.Threading.Thread.Sleep(2000);
            }
        }

    }
}
