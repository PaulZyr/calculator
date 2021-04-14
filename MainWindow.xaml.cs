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

namespace Home0409Calculator
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
        private bool _point = false;
        private bool _first = true;
        private bool _numberInputReady = true;
        private string _number = "";
        private char _operation = '$';
        private string _doneOperations = "";
        private double _result = 0;

        private void label1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PrepareNum("1");
        }

        private void label2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PrepareNum("2");
        }

        private void label3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PrepareNum("3");
        }

        private void label4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PrepareNum("4");
        }

        private void label5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PrepareNum("5");
        }

        private void label6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PrepareNum("6");
        }

        private void label7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PrepareNum("7");
        }

        private void label8_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PrepareNum("8");
        }

        private void label9_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PrepareNum("9");
        }

        private void label0_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PrepareNum("0");
        }

        private void pointLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(!_point)
            {
                PrepareNum(",");
                _point = true;
            }
        }

        private void divLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddOperation('/');        
        }

        private void multLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddOperation('*');
        }

        private void minLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddOperation('-');
        }

        private void plusLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddOperation('+');
        }

        private void leftLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_number.Length > 0) _number = _number.Remove(_number.Length - 1);
            else if (_operation != '$')
            {
                _operation = '$';
                _numberInputReady = false;
            }

            ShowProceduresText();
        }

        private void PrepareNum(string s)
        {
            if (!_numberInputReady) return;

            if (_number.Length == 1 && _number[0] == '0' && s != ",") _number = s;
            else _number += s;

            ShowProceduresText();
        }

        private void AddOperation(char op)
        {
            double t = 0;
            if (_first)
            {
                Double.TryParse(_number, out t);
                _result += t;
                _doneOperations += t.ToString();
                _number = "";
                _first = false;
            }

            if(_operation == '$')
            {
                _operation = op;
                _point = false;
            }

            _numberInputReady = true;
            ShowProceduresText();
        }

        private void equalLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            double t;
            if (!_first && _number.Length > 0)
            {

                Double.TryParse(_number, out t);
                switch (_operation)
                {
                    case '+': _result += t; break;
                    case '*': _result *= t; break;
                    case '/':
                        if (t != 0) _result /= t;
                        else
                        {
                            MessageBox.Show("ERROR:\nForbidden Operation - Divide by Zero!");
                            _operation = '$';
                            _number = "";
                            ShowProceduresText();
                            return;
                        }

                        break;
                    case '-': _result -= t; break;
                }
                _doneOperations += " " + _operation + " " + t.ToString();
                _number = "";
                _operation = '$';
                ShowProceduresText();
            }
                
            _numberInputReady = false;
        }

        private void ShowProceduresText()
        {
            string str = "";
            str += _doneOperations;
            if(_operation != '$') str += " " + _operation + " ";
            str += _number;
            procTextBox.Text = str;
            ShowResult();
        }
        private void ShowResult()
        {
            resultTextBox.Text = _result.ToString();
        }

        private void ceLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _operation = '$';
            _number = "";
            ShowProceduresText();
        }

        private void cLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _number = "";
            ShowProceduresText();
        }
    }
}
