using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConcentrator
{
  //  public enum TypeAddress {ADDR010 , ADDR012, ADDR014, ADDR016 };
    public class Digital_output : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region fields
        private string name;
        private string description;
        private string address;
        private bool initialValue;
        private bool currentValue;
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
        public bool InitialValue
        {
            get { return initialValue; }
            set
            {
                initialValue = value;
                currentValue = value;
                OnPropertyChanged("InitialValue");
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
        public Digital_output()
        {
        }

        public Digital_output(string name, string description, string address, bool initialValue)
        {
            Name = name;
            Description = description;
            Address = address;
            InitialValue = initialValue;
            PLCWrite();
        }
        #endregion
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #region WRITE
        public void PLCWrite()
        {
            PLCInstance.Instance.SetDigitalValue(Address, Convert.ToDouble(CurrentValue));
        }
        #endregion
        public override string ToString()
        {
            return $"Name: {Name}\nDescription: {Description}\nAddress: {Address}\nInitial value: {InitialValue}";
        }
    }
}
