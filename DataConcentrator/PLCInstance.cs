using PLCSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataConcentrator
{
    public class PLCInstance: PLCSimulatorManager
    {

        public static PLCSimulatorManager instance;
        public PLCInstance()
        {
        }

        public static PLCSimulatorManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PLCSimulatorManager();
                    instance.StartPLCSimulator(); // start pa se generisu inputi
                }
                return instance;
            }
        }

    }
}
