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

namespace WPF_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String Stroke_up = null;
        private String One = null;
        private String Two = null;
        private String Sign = null;
        private Double First_Number = 0;
        private Double Second_Number = 0;
        private bool NumberF = true;
        private bool FractionF = true;
        private bool EqualAfterF = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        // any button click event
        private void Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Content.ToString() == "/" || button.Content.ToString() == "*" ||
                button.Content.ToString() == "-" || button.Content.ToString() == "+")
            {
                if (NumberF)
                { Sign = button.Content.ToString(); Up_Display(); NumberF = false; EqualAfterF = false;
                    Two = null; FractionF = true; Second_Number = 0; }
                else
                {
                    Operations();
                    Sign = null;
                    if (String.IsNullOrEmpty(Sign)) { Sign = button.Content.ToString(); Up_Display(); }
                    EqualAfterF = false;
                    Two = null;
                    Second_Number = 0;
                    FractionF = true;
                }
            }
            else if (button.Content.ToString() == "CE")
            {
                if (NumberF) { One = null; First_Number = 0; Display_down.Text = "0";}
                else
                { Two = null; Second_Number = 0; Display_down.Text = "0"; }
            }
            else if (button.Content.ToString() == "C")
            {
                NumberF = true;
                One = null;
                Two = null;
                First_Number = 0;
                Second_Number = 0;
                Display_down.Text = "0";
                Display_up.Text = null;
                Stroke_up = null;
            }
            else if (button.Content.ToString() == "=")
            {
                if (String.IsNullOrEmpty(Two)) { return; }
                Operations();
                NumberF = true;
                EqualAfterF = true;
                Display_up.Text = null;
                Stroke_up = null;
            }
            else if (button.Name == "RemoveDigit")
            {
                if (NumberF)
                {
                    if (String.IsNullOrEmpty(One)) { return; }
                    if ((One.Length - 1) == 0) { First_Number = 0; Display_down.Text = "0"; One = null; }
                    else { One = One.Remove(One.Length - 1, 1); First_Number = Convert.ToDouble(One);
                        Display_down.Text = One; }
                }
                else
                {
                    if (String.IsNullOrEmpty(Two)) { return; }
                    if ((Two.Length - 1) == 0) { Second_Number = 0; Display_down.Text = "0"; Two = null; }
                    else { Two = Two.Remove(Two.Length - 1, 1); Second_Number = Convert.ToDouble(Two);
                        Display_down.Text = Two; }
                }
            }
            else if (button.Content.ToString() == ",")
            {
                if (FractionF)
                {
                    if (NumberF)
                    {
                        if (String.IsNullOrEmpty(One)) { One = "0" + button.Content.ToString(); }
                        else { One += button.Content.ToString(); }
                        Display_down.Text = One;
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(Two)) { Two = "0" + button.Content.ToString(); }
                        else { Two += button.Content.ToString(); }
                        Display_down.Text = Two;
                    }
                    FractionF = false;
                }
            }
            else
            {
                if (EqualAfterF)
                {
                    One = null;
                    EqualAfterF = false;
                    FractionF = true;
                }
                if (NumberF)
                {
                    if (One == "0") { One = null; }
                    One += button.Content.ToString();
                    Display_down.Text = One;
                    First_Number = Convert.ToDouble(One);
                }
                else
                {
                    if (Two == "0") { Two = null; }
                    Two += button.Content.ToString();
                    Display_down.Text = Two;
                    Second_Number = Convert.ToDouble(Two);
                }
            }
        }
        // operations with digits
        private void Operations()
        {
            if (Sign == "+")
            {
                First_Number += Second_Number;
                One = First_Number.ToString();
                Display_down.Text = One;
            }
            else if (Sign == "-")
            {
                First_Number -= Second_Number;
                One = First_Number.ToString();
                Display_down.Text = One;
            }
            else if (Sign == "*")
            {
                if (String.IsNullOrEmpty(Two)) { return; }
                First_Number *= Second_Number;
                One = First_Number.ToString();
                Display_down.Text = One;
            }
            else if (Sign == "/")
            {
                if (String.IsNullOrEmpty(Two)) { return; }
                First_Number /= Second_Number;
                One = First_Number.ToString();
                Display_down.Text = One;
            }
        }
        // display digits and operations on Up display
        private void Up_Display()
        {
            if (NumberF)
            {
                if (String.IsNullOrEmpty(One)) { One = "0"; }
                Stroke_up += One + Sign;
                Display_up.Text = Stroke_up;
            }
            else
            {
                if (String.IsNullOrEmpty(Two)) { Stroke_up = Stroke_up.Trim("+-/*".ToCharArray()); }
                Stroke_up += Two + Sign;
                Display_up.Text = Stroke_up;
            }
        }
    }
}
