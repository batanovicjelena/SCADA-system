using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConcentrator
{
    public enum AlarmType { LOW, HIGH};
    public class Alarm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region fields
        private int alarmId;
        private AlarmType type;
        private string limit;
        private string message;
        private bool isActivated;
        private int writeAlarm;
        private DateTime? time;
        //[ForeignKey("Analog_input")]
        //public string Analog_input_Name { get; set; }
        public virtual Analog_input Analog_input { get; set; }
        #endregion
        [Key]
        #region methods
        public int AlarmId
        {
            get { return alarmId; }
            set
            {
                alarmId = value;
                OnPropertyChanged("AlarmId");
            }
        }
        public AlarmType Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("AlarmType");
            }
        }
        /*public Analog_input Analog_input
        {
            get { return analog_input; }
            set
            {
                analog_input = value;
                OnPropertyChanged("Analog_input");
            }
        }*/
        public string Limit
        {
            get { return limit; }
            set
            {
                limit = value;
                OnPropertyChanged("Limit");
            }
        }
        public bool IsActivated
        {
            get { return isActivated; }
            set
            {
                isActivated = value;
                OnPropertyChanged("IsActivated");
            }
        }
        public DateTime? Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }
        public int WriteAlarm
        {
            get { return writeAlarm; }
            set
            {
                writeAlarm = value;
                OnPropertyChanged("WriteAlarm");
            }
        }
        #endregion
        public Alarm()
        {
            WriteAlarm= 0;
            IsActivated = false;
        }

        public override string ToString()
        {
            return $"Alarm Id: {AlarmId}\nType: {Type}\nLimit: {Limit}\n";
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
