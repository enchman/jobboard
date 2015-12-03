using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobEngine
{
    public class Operation
    {
        /// <summary>
        /// Time cycle of work process in milisecond
        /// </summary>
        const int TIME_CYCLE = 1000;

        #region Properties

        private List<Machine> stations = new List<Machine> { };

        protected int machineId = 0;
        protected string machineName = null;
        protected int machineDuration = 0;
        protected int numParts = 0;

        public int Id
        {
            get
            {
                return machineId;
            }
        }

        public string Name
        {
            get
            {
                return machineName;
            }
        }

        public int Duration
        {
            get
            {
                return machineDuration;
            }
        }

        public int Part
        {
            get
            {
                return numParts;
            }
        }
        #endregion

        #region Constructor

        public Operation()
        {

        }

        public Operation(int id, string name, int duration)
        {
            machineId = id;
            machineName = name;
            machineId = duration;
            stations.Add(new Machine());
        }

        public Operation(int id, string name, int duration, int amount)
        {
            machineId = id;
            machineName = name;
            machineId = duration;
            for(int i = 0; i < amount; i++)
            {
                stations.Add(new Machine());
            }
        }

        public Operation(int id, string name, int duration, List<Machine> items)
        {
            machineId = id;
            machineName = name;
            machineId = duration;
            stations = items;
        }
        #endregion

        public bool SetWorker(Employee worker)
        {
            int index = stations.FindIndex(x => x.Worker == null);
            if(index != -1)
            {
                return stations[index].CheckWorkerShift(worker);
            }
            else
            {
                return false;
            }
        }

        public int GetPart()
        {
            if(numParts != 0)
            {
                numParts--;
                return numParts;
            }
            else
            {
                return 0;
            }
        }

        public int GetPart(int amount)
        {
            if (numParts >= amount)
            {
                numParts = numParts - amount;
                return numParts;
            }
            else if(numParts > 0)
            {
                int temp = numParts;
                numParts = 0;
                return temp;
            }
            else
            {
                return 0;
            }
        }
    }
}
