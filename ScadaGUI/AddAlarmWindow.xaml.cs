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
    /// Interaction logic for AddAlarmWindow.xaml
    /// </summary>
    public partial class AddAlarmWindow : Window
    {
        private new Alarm newAlarm = new Alarm();
        public AddAlarmWindow()
        {
            InitializeComponent();
            this.type.ItemsSource = new List<AlarmType> { AlarmType.LOW, AlarmType.HIGH };
            this.DataContext = newAlarm;
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                newAlarm.Type = (AlarmType)this.type.SelectedItem;
                newAlarm.Limit = this.limit.Text;
                newAlarm.Message = this.message.Text;
                Context.Instance.Alarms.Add(newAlarm);
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
            // MESSAGE
            if (String.IsNullOrWhiteSpace(message.Text))
            {
                message.BorderBrush = Brushes.Red;
                messageVal.Visibility = Visibility.Visible;
                retVal = false;
            }
            else
            {
                message.ClearValue(Border.BorderBrushProperty);
                messageVal.Visibility = Visibility.Hidden;
            }
            // TYPE
            if (type.SelectedIndex == -1)
            {
                retVal = false;
                typeVal.Visibility = Visibility.Visible;
            }
            else
            {
                typeVal.Visibility = Visibility.Hidden;
            }
            // LIMIT
            if (limit.Text.Any(char.IsLetter) || Int32.Parse(limit.Text) <= 0)
            {
                limit.BorderBrush = Brushes.Red;
                limitVal.Visibility = Visibility.Visible;
                retVal = false;
            }
            else
            {
                limit.ClearValue(Border.BorderBrushProperty);
                limitVal.Visibility = Visibility.Hidden;
            }
            return retVal;
        }
        #endregion
    }
}
