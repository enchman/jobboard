using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLib
{
    public class Employee
    {
        public string Name { get; set; }
        public List<Job> PreviousTask = new List<Job> { };
        public Job CurrentTask;

        public Employee()
        {

        }

        public Employee(string name)
        {
            this.Name = name;
        }

        public Employee(Job task)
        {
            AddTask(task);
        }

        public Employee(string name, Job task)
        {
            this.Name = name;
            AddTask(task);
        }

        public void AddTask(Job task)
        {
            PreviousTask.Add(CurrentTask);
            CurrentTask = task;
        }

        public void TaskHistory()
        {

        }
    }
}
