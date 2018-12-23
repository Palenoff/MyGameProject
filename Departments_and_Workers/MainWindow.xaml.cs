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
        private static ObservableCollection<Department> departments = new ObservableCollection<Department>()
        {
            new Department("IT", new ObservableCollection<Employee>()),
            new Department("Бухгалтерия", new ObservableCollection<Employee>())
        };
        private ObservableCollection<Employee> employees = new ObservableCollection<Employee>()
        {
            new Employee("Аркадий Васильевич", 28, 150, "Middle-программист", Departments, "IT"),
            new Employee("Геннадий Геннадьевич", 30, 220, "Team-Lead", Departments, "IT"),
            new Employee("Алексей Валерьевич", 35, 300, "Архитектор БД", Departments, "IT"),
            new Employee("Степан Евграфович", 22, 100, "Junior-программист", Departments, "IT"),
            new Employee("Анастасия Федотовна", 40, 150, "Главный бухгалтер", Departments, "Бухгалтерия"),
            new Employee("Евгения Максимовна", 42, 70, "Бухгалтер", Departments, "Бухгалтерия"),
            new Employee("Валерий Никифорович", 55, 130, "Заместитель главного бухгалтера", Departments, "Бухгалтерия")
        };
        public static ObservableCollection<Department> Departments { get => departments; set => departments = value; }
        public ObservableCollection<Employee> Employees { get => employees; set => employees = value; }

        int _selected_department_index;
        private void UpdateListBox(object sender, EventArgs e)
        {
            _selected_department_index = (sender as ComboBox).SelectedIndex;
            EmployeesLV.ItemsSource = (DepartmentsCB.SelectedItem as Department)?.Employees;
            //Departments[_selected_department_index].Employees.CollectionChanged += new NotifyCollectionChangedEventHandler(EmployeesCollectionChanged);
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void EmployeesLB_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (EmployeesLV.SelectedItem != null)
            {
                new EmployeeWindow("Редактирование сотрудника", DataContext, Departments,DepartmentsCB.ItemsSource,_selected_department_index,Departments[_selected_department_index].Employees[EmployeesLV.SelectedIndex]).Show();
            }
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            new EmployeeWindow("Создание сотрудника", DataContext, Departments, DepartmentsCB.ItemsSource, -1, new Employee()).Show();
        }

        private void AddDepartmentBtn_Click(object sender, RoutedEventArgs e)
        {
            new DepartmentWindow(Departments).Show();
        }

        private void EditDepartmentBtn_Click(object sender, RoutedEventArgs e)
        {
            new DepartmentWindow(DepartmentsCB.SelectedItem as Department).Show();
        }
    }
}
