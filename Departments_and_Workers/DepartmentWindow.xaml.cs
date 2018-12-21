using Departments_and_Workers.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для DepartmentWindows.xaml
    /// </summary>
    public partial class DepartmentWindow : Window
    {
        ObservableCollection<Department> _departments;
        Department _department;
        public DepartmentWindow(ObservableCollection<Department> departments)
        {
            InitializeComponent();
            _departments = departments;
        }
        public DepartmentWindow(ObservableCollection<Department> departments, Department department)
        {
            InitializeComponent();
            _departments = departments;
            _department = department;
        }


        private void SaveDepartmentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_department == null)
                _departments.Add(new Department(DepartmentTB.Text));
            else
                _department.DepartmentName = DepartmentTB.Text;
            Close();
        }
    }
}
