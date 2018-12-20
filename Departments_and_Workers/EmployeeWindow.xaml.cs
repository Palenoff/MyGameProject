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
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public EmployeeWindow(string title,ObservableCollection<Department>departments,IEnumerable departments_source,int selected_department,Employee employee)
        {
            InitializeComponent();
            _employee = employee;
            _departments = departments;
            NameFNameTB.Text = _employee.Name;
            AgeTB.Text = _employee.Age.ToString();
            SalaryTB.Text = _employee.Salary.ToString();
            PositionTB.Text = _employee.Position;
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
            _employee.Name = NameFNameTB.Text;
            _employee.Age = Int32.Parse(AgeTB.Text);
            _employee.Salary = Int32.Parse(SalaryTB.Text);
            _employee.Position = PositionTB.Text;
            try
            {
                _departments.Single(x => x == _employee.Department)?.Employees.Remove(_employee);
            }
            catch (Exception ex) { }
            _employee.Department = _departments.Single(x => x.Name == DepartmentsCB_Employees.Items[DepartmentsCB_Employees.SelectedIndex].ToString());
            _departments.Single(x => x == _employee.Department).Employees.Add(_employee);
            Close();
        }
    }
}
