using PLCSimulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataConcentrator
{
   // public string TypeAddress  { ADDR010, ADDR012, ADDR014, ADDR016 };
    public class Digital_input : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region fleids
        
        private string name;
        private string description;
        private string address;
        private int scanTime;
        private string scan;
        private bool currentValue;
        // nit za svaki input jer se menja !!!
        private readonly object lockerDigital = new object();
        private Thread DigitalThread { get; set; }
        //public static PLCSimulatorManager instance;
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
        public bool CurrentValue
        {
            get { return currentValue; }
            set
            {
                currentValue = value;
                OnPropertyChanged("CurrentValue");
            }
        }
        #endregion
        #region constructors
        public Digital_input()
        {
        }
        
        public Digital_input(string name, string description, string address, int scanTime)
        {
            Name = name;
            Description = description;
            Address = address;
            ScanTime = scanTime;
            Scan = "OFF";
        }
        #endregion
        #region SCAN READ
        public void StartScan()
        {
            DigitalThread = new Thread(ScanRead); 
            DigitalThread.Start(PLCInstance.Instance);
        }
        
        public void ScanRead(object obj)
        {
            while (true)
            {
                Thread.Sleep(ScanTime*1000); 
                lock (lockerDigital)
                {
                    if (scan.Equals("ON"))
                    {
                        bool valuePLC = ((PLCSimulatorManager)obj).GetDigitalValue(Address) ; 
                        if (valuePLC != CurrentValue)
                        {
                            CurrentValue = valuePLC;
                        }
                    }
                }  
            }
        }
        public void StopScan()
        {
            DigitalThread.Abort();
        }
        #endregion
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
