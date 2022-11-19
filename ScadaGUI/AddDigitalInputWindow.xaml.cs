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
    /// Interaction logic for AddDigitalInputWindow.xaml
    /// </summary>
    public partial class AddDigitalInputWindow : Window
    {
        private new Digital_input newDigitalInput = new Digital_input();
        
        public AddDigitalInputWindow()
        {
            InitializeComponent();
            this.address.ItemsSource = new List<string> { "ADDR009", "ADDR011", "ADDR013", "ADDR015" }; 
            this.scan.ItemsSource = new List<string> { "ON", "OFF" };
            this.DataContext = newDigitalInput;
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                newDigitalInput.Address = this.address.SelectedItem.ToString();
                newDigitalInput.ScanTime = Int32.Parse(this.scanTime.Text);
                newDigitalInput.StartScan();

                Context.Instance.DigitalInputs.Add(newDigitalInput);
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
            if (address.SelectedIndex == -1)
            {
                retVal = false;
                addressVal.Visibility = Visibility.Visible;
            }
            else
            {
                addressVal.Visibility = Visibility.Hidden;
            }
            // SCAN
            if (scan.SelectedIndex == -1)
            {
                retVal = false;
                scanVal.Visibility = Visibility.Visible;
            }
            else
            {
                scanVal.Visibility = Visibility.Hidden;
            }
            // SCAN TIME
            if (scanTime.Text.Any(char.IsLetter) || Int32.Parse(scanTime.Text) <= 0)
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
            return retVal;
        }
        #endregion
    }
}
