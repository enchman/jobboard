using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLib
{
    public class Employee
    {
        public int Id { get { return employeeId; } }
        public string Name { get; set; }
        public List<Job> PreviousTask = new List<Job> { };
        public Job CurrentTask;

        private int employeeId = 0;

        public Employee()
        {

        }

        public Employee(int id)
        {
            this.employeeId = id;
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

        public Employee(int id, string name)
        {
            this.employeeId = id;
            this.Name = name;
        }

        public Employee(int id, Job task)
        {
            this.employeeId = id;
            AddTask(task);
        }

        public Employee(int id, string name, Job task)
        {

        }

        public void AddTask(Job task)
        {
            PreviousTask.Add(CurrentTask);
            CurrentTask = task;
        }

        public void TaskHistory()
        {

        }

        public void Add()
        {

        }
        public void Add(string name)
        {

        }


        public void Edit()
        {

        }
        public void Edit(string name)
        {

        }

        public void Remove()
        {
            
        }
    }
}
