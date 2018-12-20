using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments_and_Workers.Logic
{
    public class Employee
    {
        public Employee()
        {
        }

        public Employee(string name, int age, int salary, string position)
        {
            Name = name;
            Age = age;
            Salary = salary;
            Position = position;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public string Position { get; set; }
        public Department Department { get; set; }
    }
}
