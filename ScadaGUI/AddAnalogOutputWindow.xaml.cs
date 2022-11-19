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
    /// Interaction logic for AddAnalogOutputWindow.xaml
    /// </summary>
    public partial class AddAnalogOutputWindow : Window
    {
        private new Analog_output newAnalogOutput = new Analog_output();
        #region GetUnusedOutputs
        public List<string> getUnusedOutputs()
        {
            List<string> addressTemp = new List<string> { "ADDR005", "ADDR006", "ADDR007", "ADDR008" };
            List<string> addressTempTemp = new List<string> { "ADDR005", "ADDR006", "ADDR007", "ADDR008" };
            var temp = Context.Instance.AnalogOutputs.ToList();
            foreach (Analog_output v in temp)
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
        public AddAnalogOutputWindow()
        {
            InitializeComponent();
            this.address.ItemsSource = getUnusedOutputs();
            this.DataContext = newAnalogOutput;
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                newAnalogOutput.Address = this.address.SelectedItem.ToString();
                newAnalogOutput.HighLimit = this.highLimit.Text;
                newAnalogOutput.LowLimit = this.lowLimit.Text;
                newAnalogOutput.Description = this.description.Text;
                newAnalogOutput.Name = this.name.Text;
                newAnalogOutput.Units = this.units.Text;
                newAnalogOutput.CurrentValue = Double.Parse(this.initialValue.Text);
                newAnalogOutput.PLCWrite();
                Context.Instance.AnalogOutputs.Add(newAnalogOutput);
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
            if (String.IsNullOrWhiteSpace(description.Text))
            {
                description.BorderBrush = Brushes.Red;
                descriptionVal.Visibility = Visibility.Visible;
                retVal = false;
            }
            else
            {
                description.ClearValue(Border.BorderBrushProperty);
                descriptionVal.Visibility = Visibility.Hidden;
            }
            // ADDRESS
            if (address.SelectedIndex == -1)
            {
                retVal = false;
                addressVal.Visibility = Visibility.Visible;
            }
            else
            {
                addressVal.Visibility = Visibility.Hidden;
            }
            
            // LOW LIMIT
            if (String.IsNullOrWhiteSpace(lowLimit.Text))
            {
                lowLimit.BorderBrush = Brushes.Red;
                lowLimitVal.Visibility = Visibility.Visible;
                retVal = false;
            }
            else if (lowLimit.Text.Any(char.IsLetter) || Int32.Parse(lowLimit.Text) <= 0 || Int32.Parse(lowLimit.Text) > Int32.Parse(highLimit.Text))
            {
                lowLimit.BorderBrush = Brushes.Red;
                lowLimitVal.Visibility = Visibility.Visible;
                retVal = false;
            }
            else
            {
                lowLimit.ClearValue(Border.BorderBrushProperty);
                lowLimitVal.Visibility = Visibility.Hidden;
            }
            // HIGH LIMIT
            if (String.IsNullOrWhiteSpace(highLimit.Text))
            {
                highLimit.BorderBrush = Brushes.Red;
                highLimitVal.Visibility = Visibility.Visible;
                retVal = false;
            }
            else if (highLimit.Text.Any(char.IsLetter) || Int32.Parse(highLimit.Text) <= 0 || Int32.Parse(lowLimit.Text) > Int32.Parse(highLimit.Text))
            {
                highLimit.BorderBrush = Brushes.Red;
                highLimitVal.Visibility = Visibility.Visible;
                retVal = false;
            }
            else
            {
                highLimit.ClearValue(Border.BorderBrushProperty);
                highLimitVal.Visibility = Visibility.Hidden;
            }
            // UNITS
            if (String.IsNullOrWhiteSpace(units.Text))
            {
                units.BorderBrush = Brushes.Red;
                unitsVal.Visibility = Visibility.Visible;
                retVal = false;
            }
            else
            {
                units.ClearValue(Border.BorderBrushProperty);
                unitsVal.Visibility = Visibility.Hidden;
            }
            // INITIAL VALUE
            if (String.IsNullOrWhiteSpace(initialValue.Text))
            {
                initialValue.BorderBrush = Brushes.Red;
                initialValueVal.Visibility = Visibility.Visible;
                retVal = false;
            }
            else if (initialValue.Text.Any(char.IsLetter) || Int32.Parse(initialValue.Text) <= 0)
            {
                initialValue.BorderBrush = Brushes.Red;
                initialValueVal.Visibility = Visibility.Visible;
                retVal = false;
            }
            else
            {
                initialValue.ClearValue(Border.BorderBrushProperty);
                initialValueVal.Visibility = Visibility.Hidden;
            }
            
            return retVal;
        }
#endregion
    }
}
