using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConcentrator
{
   
    public class ActivatedAlarm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region fields
        private int alarmId;
        private AlarmType type;
        private string limit;
        private string message;
        // kad je aktivan da se upise u drugu bazu a DateTime da se osvezi
        private DateTime? time;
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
        public string Limit
        {
            get { return limit; }
            set
            {
                limit = value;
                OnPropertyChanged("Limit");
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
        #endregion
        public ActivatedAlarm()
        {
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
