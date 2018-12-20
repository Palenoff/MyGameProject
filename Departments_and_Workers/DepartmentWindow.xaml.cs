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
        public DepartmentWindow(ObservableCollection<Department> departments)
        {
            InitializeComponent();
            _departments = departments;
        }

        private void AddDepartmentBtn_Click(object sender, RoutedEventArgs e)
        {
            _departments.Add(new Department(AddDepartmentTB.Text));
            Close();
        }
    }
}
