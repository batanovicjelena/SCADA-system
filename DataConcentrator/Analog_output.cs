using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConcentrator
{
    public class Analog_output : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region fields
        private string name;
        private string description;
        private string address;
        private double initialValue;
        private string lowLimit;
        private string highLimit;
        private string units;
        private double currentValue;
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
        public double InitialValue
        {
            get { return initialValue; }
            set
            {
                initialValue = value;
                currentValue = value;
                OnPropertyChanged("InitialValue");
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
        #endregion
        #region constructors
        public Analog_output()
        {
        }
        public Analog_output(string name, string description, string address, double initialValue, string lowLimit, string highLimit, string units)
        {
            Name = name;
            Description = description;
            Address = address;
            InitialValue = initialValue;
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Units = units;
            PLCWrite();
        }
        #endregion
        #region WRITE
        public void PLCWrite()
        {
            if (CurrentValue > Double.Parse(HighLimit))
            {
                CurrentValue = Double.Parse(HighLimit);
            }else if(CurrentValue < Double.Parse(LowLimit))
            {
                CurrentValue = Double.Parse(LowLimit);
            }
            PLCInstance.Instance.SetDigitalValue(Address, Convert.ToDouble(CurrentValue));
        }
        #endregion
        
        
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
