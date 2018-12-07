using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using RDotNet;
using System.IO;



namespace testingRInCSharp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
            REngine.SetEnvironmentVariables();           
            REngine engine = REngine.GetInstance();
            engine.Initialize();
            //var rScript = File.ReadAllText(@"..\..\Model\RScript.R");
            //MessageBox.Show(engine.Evaluate("2 + 2").AsCharacter().ToString());
            //label1.Content = engine.Evaluate(rScript).AsCharacter()[0];
            
           // MessageBox.Show(Directory.GetCurrentDirectory());

           var rScript = File.ReadAllText(@"..\..\Model\RScript.R");
            engine.Evaluate("library(datasets)");
           engine.Evaluate(rScript);
           //DataFrame data = engine.Cre
           label1.Content = engine.Evaluate(rScript).AsCharacter()[0];
        }
    }
}
