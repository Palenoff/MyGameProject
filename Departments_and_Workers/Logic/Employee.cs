using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments_and_Workers.Logic
{
    public class Employee : INotifyPropertyChanged
    {
        private string _name, _position;
        private int _age, _salary;
        private Department _department;
        public Employee()
        {
        }

        public Employee(string name, int age, int salary, string position, ObservableCollection<Department> departments, string department)
        {
            Name = name;
            Age = age;
            Salary = salary;
            Position = position;
            Department = departments.Single(x => x.DepartmentName == department);
        }

        public string Name
        {
            get
            { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }
        public int Age
        {
            get
            { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    this.NotifyPropertyChanged("Age");
                }
            }
        }
        public int Salary
        {
            get
            { return _salary; }
            set
            {
                if (_salary != value)
                {
                    _salary = value;
                    this.NotifyPropertyChanged("Salary");
                }
            }
        }
        public string Position
        {
            get
            { return _position; }
            set
            {
                if (_position != value)
                {
                    _position = value;
                    this.NotifyPropertyChanged("Position");
                }
            }
        }
        public Department Department
        {
            get
            { return _department; }
            set
            {
                if (_department != value)
                {
                    _department?.Employees.Remove(this);
                    _department = value;
                    _department.Employees.Add(this);
                    this.NotifyPropertyChanged("Department");
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
