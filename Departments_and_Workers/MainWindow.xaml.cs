using Departments_and_Workers.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Departments_and_Workers
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Department> departments = new ObservableCollection<Department>()
        {
            new Department("IT", new ObservableCollection<Employee>()
            {
                new Employee("Аркадий Васильевич", 28, 150, "Middle-программист"),
                new Employee("Геннадий Геннадьевич", 30, 220, "Team-Lead"),
                new Employee("Алексей Валерьевич", 35, 300, "Архитектор БД"),
                new Employee("Степан Евграфович", 22, 100, "Junior-программист")
            }),
            new Department("Бухгалтерия", new ObservableCollection<Employee>()
            {
                new Employee("Анастасия Федотовна", 40, 150, "Главный бухгалтер"),
                new Employee("Евгения Максимовна", 42, 70, "Бухгалтер"),
                new Employee("Валерий Никифорович", 55, 130, "Заместитель главного бухгалтера")
            })
        };
        int _selected_department_index;
        private void UpdateListBox(object sender, EventArgs e)
        {
            _selected_department_index = (sender as ComboBox).SelectedIndex;
            EmployeesLB.ItemsSource = departments[_selected_department_index].Employees.Select(x => x.Name);
            departments[_selected_department_index].Employees.CollectionChanged += new NotifyCollectionChangedEventHandler(EmployeesCollectionChanged);
        }

        public MainWindow()
        {
            InitializeComponent();
            DepartmentsCB.ItemsSource = departments.Select(x => x.Name);
            departments.CollectionChanged += new NotifyCollectionChangedEventHandler(DepartmentsCollectionChanged);
        }

        private void DepartmentsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            DepartmentsCB.ItemsSource = departments.Select(x => x.Name);
        }

        private void EmployeesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            EmployeesLB.ItemsSource = departments[_selected_department_index].Employees.Select(x => x.Name);
        }

        private void EmployeesLB_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (EmployeesLB.SelectedItem != null)
            {
                new EmployeeWindow("Редактирование сотрудника",departments,DepartmentsCB.ItemsSource,_selected_department_index,departments[_selected_department_index].Employees[EmployeesLB.SelectedIndex]).Show();
            }
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            new EmployeeWindow("Создание сотрудника", departments, DepartmentsCB.ItemsSource, -1, new Employee()).Show();
        }

        private void AddDepartmentBtn_Click(object sender, RoutedEventArgs e)
        {
            new DepartmentWindow(departments).Show();
        }
    }
}
