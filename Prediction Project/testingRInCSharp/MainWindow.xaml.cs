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
        REngine engine;
        public MainWindow()
        {
            InitializeComponent();
            
            REngine.SetEnvironmentVariables();           
            engine = REngine.GetInstance();
            engine.Initialize();

            var rScript = File.ReadAllText(@"..\..\Model\RScript.R");           
            engine.Evaluate(rScript);
            labelAdjRSquared.Content = "Adjusted R-squared : " + string.Format("{0:0.####}",engine.Evaluate("model.summary$adj.r.squared").AsNumeric()[0]);
        }

        public bool CheckTextBoxes()
        {
            return textBoxRadiation.Text.Length != 0 && textBoxPressure.Text.Length != 0 &&
                    textBoxHumidity.Text.Length != 0 && textBoxSpeed.Text.Length != 0 &&
                    textBoxTimeOfSun.Text.Length != 0;
        }

        /// <summary>
        /// очистка параметров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            textBoxRadiation.Text = string.Empty;
            textBoxPressure.Text = string.Empty;
            textBoxHumidity.Text = string.Empty;
            textBoxSpeed.Text = string.Empty;
            textBoxTimeOfSun.Text = string.Empty;
            labelResult.Content = string.Empty;
        }

        /// <summary>
        /// кнопка расчета
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckTextBoxes())
                MessageBox.Show("Введены не все значения параметров!!!");
            else
            {
                labelResult.Content=string.Format("Рассчитанное значение температуры: {0}", FahrenheitToCelsius(EvaluateTemperature()));
            }
        }

        public double EvaluateTemperature()
        {
            //для игнорирования ввода точки или запятой
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            var dataIn = string.Format("data.frame(radiation={0}, pressure={1}, humidity={2}, wind_speed={3}, day_time={4})",
                textBoxRadiation.Text,
                (double.Parse(textBoxPressure.Text) / 25.4).ToString(),
                textBoxHumidity.Text,
                (double.Parse(textBoxSpeed.Text) * 3600 / 1609.34).ToString(),
                textBoxTimeOfSun.Text);
            var expressionToEvaluate = string.Format("predict(linear.model.half.1,{0})",dataIn);
            //return engine.Evaluate(expressionToEvaluate).AsCharacter()[0];
            return engine.Evaluate(expressionToEvaluate).AsNumeric()[0];
        }

        public string FahrenheitToCelsius(double fahrenheit)
        {
            return string.Format("{0:0.####}", (fahrenheit-32)*5/9);
        }
    }
}
