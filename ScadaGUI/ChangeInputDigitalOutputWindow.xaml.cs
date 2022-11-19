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
    /// Interaction logic for ChangeInputDigitalOutputWindow.xaml
    /// </summary>
    public partial class ChangeInputDigitalOutputWindow : Window
    {
        public Digital_output tempOutput { get; set; }
        public ChangeInputDigitalOutputWindow(Digital_output selected)
        {
            tempOutput = selected;
            tempOutput.Name = selected.Name;
            tempOutput.Address = selected.Address;
            tempOutput.Description = selected.Description;
            tempOutput.InitialValue = selected.InitialValue;
            tempOutput.CurrentValue = selected.CurrentValue;
            tempOutput.PLCWrite();
            InitializeComponent();
            this.initialValue.ItemsSource = new List<bool> { true, false };
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var updatedOutput = (from k in Context.Instance.DigitalOutputs.Local
                                    where k.Name == tempOutput.Name
                                    select k).FirstOrDefault();
                
                Context.Instance.DigitalOutputs.Attach(updatedOutput);
                updatedOutput.PLCWrite();
                Context.Instance.Entry(updatedOutput).Property(x => x.CurrentValue).IsModified = true;
                Context.Instance.Entry(updatedOutput).Property(x => x.InitialValue).IsModified = true;
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

            // INITIAL VALUE
            if (initialValue.SelectedIndex == -1)
            {
                retVal = false;
                initialVal.Visibility = Visibility.Visible;
            }
            else
            {
                initialVal.Visibility = Visibility.Hidden;
            }
            return retVal;
        }
        #endregion

    }
}
