using DataConcentrator;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Threading;

namespace ScadaGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region SELECTED
        public Digital_output SelectedDigitalOutputs { get; set; }
        public Digital_input SelectedDigitalInputs { get; set; }
        public Analog_input SelectedAnalogInputs { get; set; }
        public Analog_output SelectedAnalogOutputs { get; set; }
        public Alarm SelectedAlarms { get; set; }
        public ActivatedAlarm SelectedActivatedAlarms { get; set; }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            #region LOAD
            Context.Instance.DigitalOutputs.Load();
            Context.Instance.DigitalInputs.Load();
            Context.Instance.AnalogInputs.Load();
            Context.Instance.Alarms.Load();
            Context.Instance.ActivatedAlarms.Load();
            #endregion
            foreach (Digital_input di in Context.Instance.DigitalInputs)
            {
                di.StartScan();
            }
            foreach (Analog_input ai in Context.Instance.AnalogInputs)
            {
                ai.AlarmActivated += OnAlarmActivated;
                ai.StartScan();
            }
            #region LOCAL
            DigitalOutputGrid.ItemsSource = Context.Instance.DigitalOutputs.Local;
            DigitalInputGrid.ItemsSource = Context.Instance.DigitalInputs.Local;
            AnalogInputGrid.ItemsSource = Context.Instance.AnalogInputs.Local;
            AnalogOutputGrid.ItemsSource = Context.Instance.AnalogOutputs.Local;
            AlarmGrid.ItemsSource = Context.Instance.Alarms.Local;
            AlarmActivatedGrid.ItemsSource = Context.Instance.ActivatedAlarms.Local;
            #endregion
            this.DataContext = this;
        }
        #region DIGITAL INPUTS 
        private void AddDigitalInputBtn_Click(object sender, RoutedEventArgs e)
        {
            AddDigitalInputWindow addDigitalInputWindow = new AddDigitalInputWindow();
            addDigitalInputWindow.ShowDialog();
        }
        private void DeleteDigitalInputBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                SelectedDigitalInputs.StopScan();
                Context.Instance.DigitalInputs.Remove(SelectedDigitalInputs);
                Context.Instance.SaveChanges();
            }
        }
        private void UpdateScanDigitalInputBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateScanWindow updateScanWindow = new UpdateScanWindow(SelectedDigitalInputs);
            updateScanWindow.ShowDialog();
        }
        
        #endregion
        #region DIGITAL OUTPUT
        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeInputDigitalOutputWindow changeInputDigitalOutputWindow = new ChangeInputDigitalOutputWindow(SelectedDigitalOutputs);
            changeInputDigitalOutputWindow.ShowDialog();
        }
        private void AddDigitalOutputBtn_Click(object sender, RoutedEventArgs e)
        {
            AddDigitalOutputWindow addDigitalOutputWindow = new AddDigitalOutputWindow();
            addDigitalOutputWindow.ShowDialog();
        }
        private void DeleteDigitalOutputBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                Context.Instance.DigitalOutputs.Remove(SelectedDigitalOutputs);
                Context.Instance.SaveChanges();
            }
        }
        #endregion
        #region ANALOG OUTPUT
        private void AddAnalogOutputBtn_Click(object sender, RoutedEventArgs e)
        {
            AddAnalogOutputWindow addAnalogOutputWindow = new AddAnalogOutputWindow();
            addAnalogOutputWindow.ShowDialog();
        }
        private void DeleteAnalogOutputBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                Context.Instance.AnalogOutputs.Remove(SelectedAnalogOutputs);
                Context.Instance.SaveChanges();
            }
        }
        #endregion
        #region ANALOG INPUT
        private void UpdateScanAnalogInputBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateScanAnalogInput updateScanAnalogInputWindow = new UpdateScanAnalogInput(SelectedAnalogInputs);
            updateScanAnalogInputWindow.ShowDialog();
        }
        private void AddAnalogInputBtn_Click(object sender, RoutedEventArgs e)
        {
            AddAnalogInputWindow addAnalogInputWindow = new AddAnalogInputWindow();
            addAnalogInputWindow.ShowDialog();
        }
        private void DeleteAnalogInputBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                SelectedDigitalInputs.StopScan();
                Context.Instance.AnalogInputs.Remove(SelectedAnalogInputs);
                Context.Instance.SaveChanges();
            }
        }
        private void DetailsAnalogInputBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(SelectedAnalogInputs.ToString(), "DETAILS");
        }
        private void AddOrRemoveAlarmBtn_Click(object sender, RoutedEventArgs e)
        {
            AddOrRemoveAlarmWindow addOrRemoveAlarmWindow = new AddOrRemoveAlarmWindow(SelectedAnalogInputs);
            addOrRemoveAlarmWindow.ShowDialog();
        }
        #endregion
        #region ALARMS
        private void AddAlarmBtn_Click(object sender, RoutedEventArgs e)
        {
            AddAlarmWindow addAlarmWindow = new AddAlarmWindow();
            addAlarmWindow.ShowDialog();
        }
        
        private void DeleteAlarmBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                foreach(Analog_input a in Context.Instance.AnalogInputs.Local)
                {
                    if (a.Alarms.Contains(SelectedAlarms))
                    {
                        a.Alarms.Remove(SelectedAlarms);
                    }
                }
                Context.Instance.Alarms.Remove(SelectedAlarms);
                Context.Instance.SaveChanges();
            }
        }
        #endregion
        #region SHUTDOWN
        private void OnWindowclose(object sender, EventArgs e)
        {
            foreach(Digital_input d in Context.Instance.DigitalInputs)
            {
                d.StopScan();
            }
            Environment.Exit(Environment.ExitCode);
        }
        #endregion
        #region OnAlarmActivated
        static void OnAlarmActivated(int alarmId)
        {
            Action WorkAction =   new Action(() =>
                {
                    Alarm alarm =Context.Instance.Alarms.Find(alarmId);
                    ActivatedAlarm alarmTemp= new ActivatedAlarm { AlarmId= alarmId , Limit= alarm.Limit, Message= alarm.Message, Type=alarm.Type, Time=alarm.Time};
                    Context.Instance.ActivatedAlarms.Add(alarmTemp);
                    Context.Instance.SaveChanges();
                });
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, WorkAction); 
        }
        #endregion
    }
}
