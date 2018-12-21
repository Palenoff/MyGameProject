using Departments_and_Workers.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Departments_and_Workers
{
    /// <summary>
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        Employee _employee;
        ObservableCollection<Department> _departments;

        public Employee Employee { get => _employee; set => _employee = value; }
        public ObservableCollection<Department> Departments { get => _departments; set => _departments = value; }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public EmployeeWindow(string title, object datacontext, ObservableCollection<Department>departments,IEnumerable departments_source,int selected_department,Employee employee)
        {
            InitializeComponent();
            DataContext = Employee = employee;
            DepartmentsCB_Employees.ItemsSource = departments_source;
            DepartmentsCB_Employees.SelectedIndex = selected_department;
            Title = title;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Employee.Name = NameFNameTB.Text;
            Employee.Age = Int32.Parse(AgeTB.Text);
            Employee.Salary = Int32.Parse(SalaryTB.Text);
            Employee.Position = PositionTB.Text;
            Employee.Department = DepartmentsCB_Employees.SelectedItem as Department;
            Close();
        }
    }
}
