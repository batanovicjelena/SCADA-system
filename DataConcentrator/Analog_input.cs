using PLCSimulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// napravi listu aktivnih u okviru inputa za svaki
namespace DataConcentrator
{
    public delegate void AlarmActivatedHandler(int AlarmId);
    public class Analog_input : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region fields
        private string name;
        private string description;
        private string address;
        private int scanTime;
        private string scan;
        private string lowLimit;
        private string highLimit;
        public double currentValue;
        private string units;
        public event AlarmActivatedHandler AlarmActivated; // event kad se aktivira alarm
        private readonly object lockerAnalog = new object();
        private Thread AnalogThread { get; set; }
        private List<Alarm> alarms;
        private List<Alarm> alarmsActivated;
        private bool alarmTemp;
        private bool pokusaj;
        
        private int brojacAktivnih { get; set; }
        public List<Alarm> ActiveAlarms { get; set; }
        #endregion

        [Key]
        #region methods
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        public int ScanTime
        {
            get { return scanTime; }
            set
            {
                scanTime = value;
                OnPropertyChanged("ScanTime");
            }
        }
        public string Scan
        {
            get { return scan; }
            set
            {
                scan = value;
                OnPropertyChanged("Scan");
            }
        }
        public double CurrentValue
        {
            get { return currentValue; }
            set
            {
                currentValue = value;
                OnPropertyChanged("CurrentValue");
            }
        }
        public string LowLimit
        {
            get { return lowLimit; }
            set
            {
                lowLimit = value;
                OnPropertyChanged("LowLimit");
            }
        }
        public string HighLimit
        {
            get { return highLimit; }
            set
            {
                highLimit = value;
                OnPropertyChanged("HighLimit");
            }
        }
        public string Units
        {
            get { return units; }
            set
            {
                units = value;
                OnPropertyChanged("Units");
            }
        }
        public List<Alarm> Alarms
        {
            get { return alarms; }
            set
            {
                alarms = value;
                OnPropertyChanged("Alarms");
            }
        }
        public List<Alarm> AlarmsActivated
        {
            get { return alarmsActivated; }
            set
            {
                alarmsActivated = value;
                OnPropertyChanged("AlarmsActivated");
            }
        }
        public bool AlarmTemp
        {
            get { return alarmTemp; }
            set
            {
                alarmTemp = value;
                OnPropertyChanged("AlarmTemp");
            }
        }
        public bool Pokusaj
        {
            get { return pokusaj; }
            set
            {
                pokusaj = value;
                OnPropertyChanged("Pokusaj");
            }
        }
        #endregion
        #region constructors
        public Analog_input()
        {
            CurrentValue = Convert.ToDouble(LowLimit);
            Pokusaj = false;
            brojacAktivnih = 0;
            Alarms = new List<Alarm>();
            ActiveAlarms = new List<Alarm>();
            AlarmsActivated= new List<Alarm>();
        }
        public Analog_input(string name, string description, string address, int scanTime, string scan, string lowLimit, string highLimit, string units)
        {
            Name = name;
            Description = description;
            Address = address;
            ScanTime = scanTime;
            Scan = scan;
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Units = units;
            Alarms = new List<Alarm>();
            //ActiveAlarms = new List<Alarm>();
            CurrentValue = Double.Parse(lowLimit);
        }
        #endregion
        #region SCAN READ
        public void StartScan()
        {
            AnalogThread = new Thread(ScanRead);
            AnalogThread.Start(PLCInstance.Instance);
        }
        public void ScanRead(object obj)
        {
            
            while (true)
            {
                Thread.Sleep(ScanTime * 1000);
                lock (lockerAnalog)
                {
                    
                    if (scan.Equals("ON"))
                    {
                        // da trenutna vrednost bude u granicama
                        double valuePLC = ((PLCSimulatorManager)obj).GetAnalogValue(Address);
                        if (valuePLC != CurrentValue)
                        {
                            CurrentValue = valuePLC;
                        }
                        if (CurrentValue < Double.Parse(LowLimit))
                        {
                            CurrentValue = Double.Parse(LowLimit);
                        }
                        else if(CurrentValue > Double.Parse(HighLimit))
                        {
                            CurrentValue = Double.Parse(HighLimit);
                        }
                       // alarmi
                        //Pokusaj = false;
                        if (!Alarms.Contains(null)) // javljalo grešku da je kolekcija izmenjena i jeste, ali je nekako registrovalo kao null
                        {
                            foreach (Alarm alarm in Alarms)
                            {
                                if(alarm != null){ 
                                    if ( alarm.Type == AlarmType.LOW)
                                    {
                                        if (Double.Parse(alarm.Limit) >= CurrentValue && alarm.IsActivated == false)
                                        {
                                            alarm.IsActivated = true;
                                            AlarmActivated?.Invoke(alarm.AlarmId);
                                            if (!AlarmsActivated.Contains(alarm))
                                            {
                                                AlarmsActivated.Add(alarm);
                                            }
                                        }
                                        else if (Double.Parse(alarm.Limit) < CurrentValue && alarm.IsActivated == true)
                                        {
                                            alarm.IsActivated = false;
                                        }
                                    }
                                    else
                                    {
                                        if (Double.Parse(alarm.Limit) <= CurrentValue && alarm.IsActivated == false)
                                        {
                                            alarm.IsActivated = true;
                                            AlarmActivated?.Invoke(alarm.AlarmId);
                                            if (!AlarmsActivated.Contains(alarm))
                                            {
                                                AlarmsActivated.Add(alarm);
                                            }
                                        }
                                        else if (Double.Parse(alarm.Limit) > CurrentValue && alarm.IsActivated == true)
                                        {
                                            alarm.IsActivated = false;
                                        }
                                    }
                               
                                    if (alarm.IsActivated)
                                    {
                                        alarm.WriteAlarm++;
                                    }
                                    else
                                    {
                                        alarm.WriteAlarm = 0;
                                    }  
                                    if(alarm.WriteAlarm == 1)
                                    {
                                        alarm.Time= DateTime.Now; 
                                    }
                                    // AlarmTemp mi je true kad je prvi put aktivan ako je za vise vrednosti aktivan bice false
                                    if (alarm.WriteAlarm == 1) 
                                    {
                                        AlarmTemp = true;
                                    }
                                    else { AlarmTemp = false; }
                                    }
                                else
                                {
                                    Alarms.Remove(alarm);
                                }
                            }
                            // promenljiva pokusaj kada je true-signalizacija da je alarm aktivan(crveno polje alarm), kada je false- neaktivan(zeleno polje)
                            var rez = (from a in alarms
                                       where a.IsActivated
                                       select a).Any();
                            if (rez)
                            {
                                Pokusaj = true;
                            }
                            else
                            {
                                Pokusaj = false;
                            }
                            
                        }
                    }
                }
            } 
            
        }
        public void StopScan()
        {
            AnalogThread.Abort();
        }
        #endregion
        
        
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
