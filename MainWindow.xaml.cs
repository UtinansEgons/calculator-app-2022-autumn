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

        int bracketOpened = 0;
        private void BracketButton_Click(object sender, RoutedEventArgs e)
        {
            if (bracketOpened == 0 && !numberTyped)
            {
                bracketOpened++;
                Display.Text += "(";
                numberTyped=false;
            } 
            else if (bracketOpened > 0 && numberTyped)
            {
                bracketOpened--;
                Display.Text += ")";
            }
        }

        bool blackThemeOn = false;

        SolidColorBrush colorOne;
        SolidColorBrush colorTwo;
        byte gridBgOne = 255;
        byte gridBackgroundValTwo = 65;
        byte gridBgTwo = 240;
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
                while (gridBgOne != 65)
                {
                    gridBgOne -= 10;
                    gridBackgroundValTwo += 10;

                    gridBgTwo -= 10;
                    gridTwoValTwo += 10;

                    await Task.Delay(10);

                    colorOne = new SolidColorBrush(Color.FromRgb(gridBgOne,gridBgOne,gridBgOne));
                    colorTwo = new SolidColorBrush(Color.FromRgb(gridBgTwo, gridBgTwo, gridBgTwo));
                    gridBackground.Background = colorOne;

                    themeBtn.Background = new SolidColorBrush(Color.FromRgb(gridBackgroundValTwo, gridBackgroundValTwo, gridBackgroundValTwo));
                    changeButtonColor(new SolidColorBrush(Color.FromRgb(gridBgOne, gridBgOne, gridBgOne)), new SolidColorBrush(Colors.White));
                    themeBtn.Foreground = colorOne;
                    otherGrid.Background = colorTwo;
                }
                blackThemeOn = true;
                themeBtn.Content = "☀️";
            }
            else
            {
                while (gridBgOne != 255)
                {
                    gridBgOne += 10;
                    gridBackgroundValTwo -= 10;

                    gridBgTwo += 10;
                    gridTwoValTwo -= 10;

                    await Task.Delay(10);
                    colorOne = new SolidColorBrush(Color.FromRgb(gridBgOne, gridBgOne, gridBgOne));
                    colorTwo = new SolidColorBrush(Color.FromRgb(gridBgTwo, gridBgTwo, gridBgTwo));

                    gridBackground.Background = colorOne;

                    themeBtn.Background = new SolidColorBrush(Color.FromRgb(gridBackgroundValTwo,gridBackgroundValTwo,gridBackgroundValTwo));
                    themeBtn.Foreground = colorOne;
                    changeButtonColor(colorOne, new SolidColorBrush(Colors.Black));
                    otherGrid.Background = colorTwo;
                }
                blackThemeOn = false;
                themeBtn.Content = "🌙";
            }
        }


        private void changeButtonColor(SolidColorBrush color, SolidColorBrush color2)
        {

            Button[] buttons = { ButtonZero, ButtonOne, ButtonTwo, ButtonThree, ButtonFour, ButtonFive, ButtonSix, ButtonSeven, ButtonEight, ButtonNine, ButtonPeriod, ButtonDel };
            Button[] specialButtons = { ButtonParan, ButtonPerc, ButtonDiv, ButtonMult, ButtonPlus, ButtonMin, ButtonEquals };
            TextBox[] textboxes = { Display, Result };


            foreach (Button button in buttons)
            {
                button.Background = color;
                button.Foreground = color2;
                button.BorderBrush = color;
            }

            foreach (Button button in specialButtons)
            {
                button.Background = color;
                button.BorderBrush = color;
            }

            foreach (TextBox textbox in textboxes)
            {
                textbox.Background = color;
                textbox.Foreground = color2;
                textbox.BorderBrush = color;
            }
        }
    }
}

