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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NCalc;


namespace calculator_app_2022_autumn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        bool numberTyped = true;
        private void NumericButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (Display.Text == "0")
            {
                Display.Text = (string)button.Content;
            }
            else
            {
                Display.Text += (string)button.Content;
            }
            //Display.Text += (string)button.Content;
            numberTyped = true;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Display.Text = "0";
            numberTyped = false;
        }

        private void OperationsButton_Click(object sender, RoutedEventArgs e)
        {
            if (numberTyped)
            {
                Button button = (Button)sender;
                Display.Text += button.Content;
                numberTyped = false;
            }
        }

        private void ResultsButton_Click(object sender, RoutedEventArgs e)
        {
            //double result;
            //String equation = Display.Text;
            //result = Evaluate(equation);
            //Display.Text = result.ToString();

            var expression = new NCalc.Expression(Display.Text);
            Func<double> func = expression.ToLambda<double>();
            Result.Text = func().ToString();

        }

        public static double Evaluate(string expression)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("expression", string.Empty.GetType(), expression);
            System.Data.DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);

        }

        private void SqrtButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Display.Text += "pow(";
            bracketOpened++;
        }

        int bracketOpened = 0;
        private void BracketButton_Click(object sender, RoutedEventArgs e)
        {
            if (bracketOpened == 0)
            {
                bracketOpened++;
                Display.Text += "(";
            } else
            {
                bracketOpened--;
                Display.Text += ")";
            }
        }

        private void SqRootButton_Click(object sender, RoutedEventArgs e)
        {
            //Button button = (Button)sender;
            //Display.Text += "Sqrt(";
            //bracketOpened = true;

            Display.Text = "(Sqrt(4)*2)";
        }


        bool blackThemeOn = false;

        SolidColorBrush blackTheme = new SolidColorBrush(Color.FromRgb(40, 40, 40));
        SolidColorBrush whiteTheme = new SolidColorBrush(Color.FromRgb(240, 240, 240));
        SolidColorBrush color;
        SolidColorBrush colorTwo;
        byte gridBackgroundValOne = 255;
        byte gridBackgroundValTwo = 65;
        byte gridTwoValOne = 240;
        byte gridTwoValTwo = 40;
        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            changeColor();
        }

        private async void changeColor()
        {
            if (!blackThemeOn)
            {
                while (gridBackgroundValOne != 65)
                {
                    gridBackgroundValOne -= 10;
                    gridBackgroundValTwo += 10;

                    gridTwoValOne -= 10;
                    gridTwoValTwo += 10;

                    await Task.Delay(10);
                    color = new SolidColorBrush(Color.FromRgb(gridBackgroundValOne,gridBackgroundValOne,gridBackgroundValOne));
                    colorTwo = new SolidColorBrush(Color.FromRgb(gridTwoValOne, gridTwoValOne, gridTwoValOne));
                    gridBackground.Background = color;
                    themeBtn.Background = new SolidColorBrush(Color.FromRgb(gridBackgroundValTwo, gridBackgroundValTwo, gridBackgroundValTwo));
                    themeBtn.Foreground = color;
                    otherGrid.Background = colorTwo;
                }
                blackThemeOn = true;
                themeBtn.Content = "☀️";
            }
            else
            {
                while (gridBackgroundValOne != 255)
                {
                    gridBackgroundValOne += 10;
                    gridBackgroundValTwo -= 10;

                    gridTwoValOne += 10;
                    gridTwoValTwo -= 10;

                    await Task.Delay(10);
                    color = new SolidColorBrush(Color.FromRgb(gridBackgroundValOne, gridBackgroundValOne, gridBackgroundValOne));
                    colorTwo = new SolidColorBrush(Color.FromRgb(gridTwoValOne, gridTwoValOne, gridTwoValOne));
                    gridBackground.Background = color;
                    themeBtn.Background = new SolidColorBrush(Color.FromRgb(gridBackgroundValTwo,gridBackgroundValTwo,gridBackgroundValTwo));
                    themeBtn.Foreground = color;
                    otherGrid.Background = colorTwo;
                }
                blackThemeOn = false;
                themeBtn.Content = "🌙";
            }
        }

        private void changeButtonColor(Color color)
        {
        }
    }
}
