using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Wpf_Kurs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<string, Operator> _operatorMap = new()
        {
            { "*", Operator.Multiplication },
            { "-", Operator.Substracting },
            { "+", Operator.Adding },
            { "/", Operator.Dividing },
        };
        internal double lastNumber, result;
        internal Operator _operator = Operator.Default;
        internal bool isDecimal = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            resultLabel.Content = resultLabel.Content.ToString() == "0" ? button.Content : $"{resultLabel.Content}{button.Content}";

        }

        private void AcButtonAClick(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            SetDefaultValues();
        }

        private void PlusMinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = (-lastNumber).ToString();
            }
        }

        private void PercButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = (-lastNumber).ToString();
            }
        }

        private void OperatorBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var content = button.Content.ToString() ?? "";
            _operator = _operatorMap.ContainsKey(content) ? _operatorMap[content] : Operator.Default;
            if (_operator != Operator.Default)
            {
                var labelContent = resultLabel.Content.ToString() ?? "";
                lastNumber = double.Parse(labelContent);
                resultLabel.Content = "0";
            }
        }

        private void DotButton_Click(object sender, RoutedEventArgs e)
        {
            var content = resultLabel.Content.ToString() ?? "";
            if (content.Contains(',')) return;
            isDecimal = true;
            resultLabel.Content += ",";
        }

        private void ResultBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(resultLabel.Content.ToString(), out double currentValue))
            {
                resultLabel.Content = "ERROR!";
                return;
            }
            switch (_operator)
            {
                case Operator.Dividing:
                    if(currentValue == 0)
                    {
                        MessageBox.Show("You cannot divide by 0!", MainWindow.TitleProperty.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    result = lastNumber / currentValue;
                    break;
                case Operator.Multiplication:
                    result = lastNumber * currentValue;
                    break;
                case Operator.Adding:
                    result = lastNumber + currentValue;
                    break;
                case Operator.Substracting:
                    result = lastNumber - currentValue;
                    break;
                default:
                    return;
            }
            resultLabel.Content = result.ToString();
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            lastNumber = result;
            _operator = Operator.Default;
            isDecimal = false;
        }
    }

    public enum Operator
    {
        Default,
        Dividing,
        Multiplication,
        Adding,
        Substracting        
    }
}
