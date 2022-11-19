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
    /// Interaction logic for AddAnalogInputWindow.xaml
    /// </summary>
    public partial class AddAnalogInputWindow : Window
    {
        private new Analog_input newAnalogInput = new Analog_input();
        private new List<Alarm> listAlarms = new List<Alarm>();
        public AddAnalogInputWindow()
        {
            InitializeComponent();
            this.address.ItemsSource = new List<string> { "ADDR001", "ADDR002", "ADDR003", "ADDR004" };
            this.scan.ItemsSource = new List<string> { "ON", "OFF" };
            this.DataContext= newAnalogInput;
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                newAnalogInput.Address = this.address.SelectedItem.ToString();
                newAnalogInput.Scan = this.scan.SelectedItem.ToString();
                newAnalogInput.HighLimit = this.highLimit.Text;
                newAnalogInput.LowLimit = this.lowLimit.Text;
                newAnalogInput.ScanTime = Int32.Parse(this.scanTime.Text);
                newAnalogInput.Description= this.description.Text;
                newAnalogInput.Name = this.name.Text;
                newAnalogInput.Units = this.units.Text;
                newAnalogInput.CurrentValue= Double.Parse(this.lowLimit.Text);
                newAnalogInput.StartScan();

                Context.Instance.AnalogInputs.Add(newAnalogInput);
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
            /*if (address.SelectedIndex == -1)
            {
                retVal = false;
                addressVal.Visibility = Visibility.Visible;
            }
            else
            {
                addressVal.Visibility = Visibility.Hidden;
            }*/
            // SCAN
            if (scan.SelectedIndex == -1)
            {
                scan.BorderBrush = Brushes.Red;
                scanVal.Visibility = Visibility.Visible;
                retVal = false;
            }
            else
            {
                scanVal.Visibility = Visibility.Hidden;
            }
            // SCAN TIME
            if (String.IsNullOrWhiteSpace(scanTime.Text))
            {
                scanTime.BorderBrush = Brushes.Red;
                scanTimeVal.Visibility = Visibility.Visible;
                retVal = false;
            }
            else if (scanTime.Text.Any(char.IsLetter) || Int32.Parse(scanTime.Text) <= 0 )
            {
                scanTime.BorderBrush = Brushes.Red;
                scanTimeVal.Visibility = Visibility.Visible;
                retVal = false;
            }
            else
            {
                scanTime.ClearValue(Border.BorderBrushProperty);
                scanTimeVal.Visibility = Visibility.Hidden;
            }
            // LOW LIMIT
            if (String.IsNullOrWhiteSpace(lowLimit.Text))
            {
                lowLimit.BorderBrush = Brushes.Red;
                lowLimitVal.Visibility = Visibility.Visible;
                retVal = false;
            }
            else if(lowLimit.Text.Any(char.IsLetter) || Int32.Parse(lowLimit.Text) <= 0 || Int32.Parse(lowLimit.Text) > Int32.Parse(highLimit.Text))
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
            return retVal;
        }
        #endregion
    }
}
