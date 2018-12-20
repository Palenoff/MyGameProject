using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments_and_Workers.Logic
{
    public class Department
    {
        public Department(string name)
        {
            Name = name;
            Employees = new ObservableCollection<Employee>();
        }

        public Department(string name, ObservableCollection<Employee> employees)
        {
            Name = name;
            Employees = employees;
            foreach (Employee employee in Employees)
            {
                employee.Department = this;
            }
        }

        public string Name { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }
    }
}
