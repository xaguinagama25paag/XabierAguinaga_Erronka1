using System;
using System.Collections.Generic;
using System.Data;
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

namespace Erronka1
{
    /// <summary>
    /// Interaction logic for Kalkulagaiua.xaml
    /// </summary>
    public partial class Kalkulagaiua : UserControl
    {
        int number2 = 0;
        int number1 = 0;
        public string calculation = "0";
        Boolean check = false;
        Boolean check2 = false;
        Boolean check3 = false;
        string sign = string.Empty;
        public Kalkulagaiua()
        {
            InitializeComponent();
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Content)
            {
                case "1":
                    Checking(1);
                    break;
                case "2":
                    Checking(2);
                    break;
                case "3":
                    Checking(3);
                    break;
                case "4":
                    Checking(4);
                    break;
                case "5":
                    Checking(5);
                    break;
                case "6":
                    Checking(6);
                    break;
                case "7":
                    Checking(7);
                    break;
                case "8":
                    Checking(8);
                    break;
                case "9":
                    Checking(9);
                    break;
                case "0":
                    Checking(0);
                    break;
                case "+":
                    check = true;
                    if (!check2)
                    {
                        sign = "+";
                        Checking(0);
                    }
                    break;
                case "-":
                    check = true;
                    if (!check2)
                    {
                        sign = "-";
                        Checking(0);
                    }
                    break;
                case "X":
                    check = true;
                    if (!check2)
                    {
                        sign = "*";
                        Checking(0);
                    }
                    break;
                case "/":
                    check = true;
                    if (!check2)
                    {
                        sign = "/";
                        Checking(0);
                    }
                    break;
                case "%":
                    check = true;
                    if (!check2)
                    {
                        sign = "%";
                        Checking(0);
                    }
                    break;
                case "=":
                    check = true;
                    if (check2)
                    {
                        check3 = true;
                        Checking(0);
                    }
                    break;
                default:
                    break;
            }
            boxa.Text = calculation + ".";
        }
        private void Checking(int number)
        {
            if (check)
            {
                if (check2)
                {
                    if (check3)
                    {
                        calculation = new DataTable().Compute(calculation, null).ToString();
                        check = false;
                        check2 = false;
                        check3 = false;
                    }
                    else
                    {
                        if (number2 != 0)
                        {
                            calculation += number.ToString();
                            number2 = number;
                        }
                        else
                        {
                            number2 = number;
                            calculation += number2.ToString();
                        }
                    }
                }
                else
                {
                    calculation += sign;
                    check2 = true;
                }
            }
            else
            {
                if (number1 != 0)
                {


                    calculation += number.ToString();
                    number1 = number;
                }
                else
                {
                    number1 = number;
                    calculation = number1.ToString();
                }
            }
            //(calculation + "+" + number1.ToString());
            //new DataTable().Compute("1+2", null);
        }
    }

    }
