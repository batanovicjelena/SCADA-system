using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConcentrator
{
    public class Context : DbContext
    {
        private static Context instance;

        public static Context Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Context();
                }
                return instance;
            }
        }
        public DbSet<Digital_output> DigitalOutputs { get; set; }
        public DbSet<Digital_input> DigitalInputs { get; set; }
        public DbSet<Analog_input> AnalogInputs { get; set; }
        public DbSet<Analog_output> AnalogOutputs { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<ActivatedAlarm> ActivatedAlarms { get; set; }
        private Context()
        {
        }

    }
}
