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
    /// Interaction logic for AddOrRemoveAlarmWindow.xaml
    /// </summary>
    public partial class AddOrRemoveAlarmWindow : Window
    {
        public List<Alarm> AllAlarms = new List<Alarm>();
        public List<Alarm> UsedAlarms { get; set; }
        public List<Alarm> UnusedAlarms { get; set; }
        public Alarm uAlarm { get; set; }
        public Alarm alarm { get; set; }
        public Analog_input tempAnalogInput { get; set; }
        public AddOrRemoveAlarmWindow(Analog_input SelectedAnalogInput)
        {
            tempAnalogInput = SelectedAnalogInput;
            tempAnalogInput.Name = SelectedAnalogInput.Name;
            tempAnalogInput.Address = SelectedAnalogInput.Address;
            tempAnalogInput.Description = SelectedAnalogInput.Description;
            tempAnalogInput.Scan = SelectedAnalogInput.Scan;
            tempAnalogInput.ScanTime = SelectedAnalogInput.ScanTime;
            tempAnalogInput.LowLimit = SelectedAnalogInput.LowLimit;
            tempAnalogInput.HighLimit = SelectedAnalogInput.HighLimit;
            tempAnalogInput.CurrentValue = SelectedAnalogInput.CurrentValue;
            AllAlarms = Context.Instance.Alarms.ToList();
            if (SelectedAnalogInput.Alarms != null)
            {
                tempAnalogInput.Alarms = SelectedAnalogInput.Alarms;
            }
            else
            {
                tempAnalogInput.Alarms = new List<Alarm>();
            }
            UsedAlarms = new List<Alarm>(); // vec pripadaju nekom inputu
            UnusedAlarms = new List<Alarm>(); // nekorisceni
            foreach (Analog_input ai in Context.Instance.AnalogInputs.Local)
            {
                foreach(Alarm a in ai.Alarms)
                {
                    UsedAlarms.Add(a);
                }
            }
            UnusedAlarms = AllAlarms;
            UnusedAlarms.RemoveAll(item => UsedAlarms.Contains(item));
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

            if (ValidateInput())
            {
                var updatedAnalogInput = (from k in Context.Instance.AnalogInputs.Local
                                          where k.Name == tempAnalogInput.Name
                                          select k).FirstOrDefault();

                updatedAnalogInput.Name = tempAnalogInput.Name;
                updatedAnalogInput.Scan= tempAnalogInput.Scan;
                updatedAnalogInput.ScanTime = tempAnalogInput.ScanTime;
                updatedAnalogInput.LowLimit = tempAnalogInput.LowLimit;
                updatedAnalogInput.HighLimit= tempAnalogInput.HighLimit;
                updatedAnalogInput.Units = tempAnalogInput.Units;
                updatedAnalogInput.Address = tempAnalogInput.Address;
                updatedAnalogInput.CurrentValue= tempAnalogInput.CurrentValue;
                updatedAnalogInput.Description = tempAnalogInput.Description;
                updatedAnalogInput.Alarms.Add(alarm);
                updatedAnalogInput.Alarms.Remove(uAlarm);
                
                Context.Instance.Entry(updatedAnalogInput).State = System.Data.Entity.EntityState.Modified;
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
            if (alarm != null)
            {
                if (alarm.Analog_input != null)
                {
                    retVal = false;
                    usedAlarmVal.Visibility = Visibility.Visible;
                }
                else
                {
                    usedAlarmVal.Visibility = Visibility.Hidden;
                }
            }

            return retVal;
        }
        #endregion
    }
}
