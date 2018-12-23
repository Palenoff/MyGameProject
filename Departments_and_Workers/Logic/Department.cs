using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments_and_Workers.Logic
{
    public class Department : INotifyPropertyChanged
    {
        private string _name;
        private ObservableCollection<Employee> _employees;
        public delegate void ChangedEventHandler();
        public Department(string name)
        {
            DepartmentName = name;
            Employees = new ObservableCollection<Employee>();
        }

        public Department(string name, ObservableCollection<Employee> employees)
        {
            DepartmentName = name;
            Employees = employees;
            foreach (Employee employee in Employees)
            {
                employee.Department = this;
            }
        }

        public string DepartmentName
        {
            get
            { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    this.NotifyPropertyChanged("DepartmentName");
                }
            }
        }
        public ObservableCollection<Employee> Employees
        {
            get
            { return _employees; }
            set
            {
                if (_employees != value)
                {
                    _employees = value;
                    this.NotifyPropertyChanged("Employeees");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
