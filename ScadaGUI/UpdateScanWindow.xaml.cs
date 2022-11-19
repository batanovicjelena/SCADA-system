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
    /// Interaction logic for UpdateScanWindow.xaml
    /// </summary>
    public partial class UpdateScanWindow : Window
    {
        public Digital_input tempInput { get; set; }
        public UpdateScanWindow(Digital_input selected)
        {
            tempInput= selected;
            tempInput.Scan=selected.Scan;
            tempInput.ScanTime = selected.ScanTime;
            tempInput.Name = selected.Name;
            tempInput.Address = selected.Address;
            tempInput.Description = selected.Description;
            tempInput.CurrentValue = selected.CurrentValue;
            InitializeComponent();
            this.scan.ItemsSource = new List<string> { "ON", "OFF" };
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var updatedInput = (from k in Context.Instance.DigitalInputs.Local
                                    where k.Name == tempInput.Name
                                    select k).FirstOrDefault();
                Context.Instance.DigitalInputs.Attach(updatedInput); 
                Context.Instance.Entry(updatedInput).Property(x => x.CurrentValue).IsModified = true;
                Context.Instance.Entry(updatedInput).Property(x => x.Scan).IsModified = true;
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
            return retVal;
        }
        #endregion
    }
}
