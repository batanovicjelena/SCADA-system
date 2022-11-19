using DataConcentrator;
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
using System.Windows.Shapes;

namespace ScadaGUI
{
    /// <summary>
    /// Interaction logic for AddDigitalOutputWindow.xaml
    /// </summary>
    public partial class AddDigitalOutputWindow : Window
    {
        private new Digital_output newDigitalOutput = new Digital_output();
        #region GetUnusedOutputs
        public List<string> getUnusedOutputs()
        {
            List<string> addressTemp = new List<string> { "ADDR010", "ADDR012", "ADDR014", "ADDR016" };
            List<string> addressTempTemp = new List<string> { "ADDR010", "ADDR012", "ADDR014", "ADDR016" };
            var temp = Context.Instance.DigitalOutputs.ToList();
            foreach (Digital_output v in temp)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (v.Address == addressTemp[i])
                    {
                        addressTempTemp.Remove(addressTemp[i]);
                    }
                }
            }
            return addressTempTemp;
        }
        #endregion
        public AddDigitalOutputWindow()
        {
            InitializeComponent();
            this.address.ItemsSource = getUnusedOutputs(); 
            this.initialValue.ItemsSource = new List<bool> { true, false };
            this.DataContext= newDigitalOutput;
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                newDigitalOutput.PLCWrite();
                Context.Instance.DigitalOutputs.Add(newDigitalOutput);
                Context.Instance.SaveChanges();
                this.Close();
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #region VALIDATE
        private bool ValidateInput()
        {
            bool retVal = true;
            //NAME
            if (String.IsNullOrWhiteSpace(name.Text))
            {
                name.BorderBrush = Brushes.Red;
                nameVal.Visibility = Visibility.Visible;
                retVal = false;
            }
            else
            {
                name.ClearValue(Border.BorderBrushProperty);
                nameVal.Visibility = Visibility.Hidden;
            }
            // DESCRIPTION
            if (String.IsNullOrWhiteSpace(desription.Text))
            {
                desription.BorderBrush = Brushes.Red;
                descriptionVal.Visibility = Visibility.Visible;
                retVal = false;
            }
            else
            {
                desription.ClearValue(Border.BorderBrushProperty);
                descriptionVal.Visibility = Visibility.Hidden;
            }
            // ADDRESS
            if (initialValue.SelectedIndex == -1)
            {
                retVal = false;
                addressVal.Visibility = Visibility.Visible;
            }
            else
            {
                addressVal.Visibility = Visibility.Hidden;
            }
            // INITIAL VALUE
            if (initialValue.SelectedIndex == -1)
            {
                retVal = false;
                initialValueVal.Visibility = Visibility.Visible;
            }
            else
            {
                initialValueVal.Visibility = Visibility.Hidden;
            }
            return retVal;
        }
        #endregion
    }
}
