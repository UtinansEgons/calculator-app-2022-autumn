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
            double result;
            String equation = Display.Text;
            result = Evaluate(equation);
            Display.Text = result.ToString();

        }

        public static double Evaluate(string expression)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("expression", string.Empty.GetType(), expression);
            System.Data.DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);

        }
    }
}
