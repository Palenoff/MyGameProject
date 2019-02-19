using Departments_and_Workers.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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
        public DataRow Row { get; set; }
        public DataRow DepartmentRow { get; set; }
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

        public EmployeeWindow(DataRow Row, DataRow department, DataView dataView, SqlDataAdapter sqlDataAdapter)
        {
            InitializeComponent();
            this.Row = Row;
            this.DataContext = Row;
            DepartmentRow = department;
            DepartmentsCB_Employees.ItemsSource = dataView;
            sqlDataAdapter.Fill(DepartmentRow.Table);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            //Employee.Name = NameFNameTB.Text;
            //Employee.Age = Int32.Parse(AgeTB.Text);
            //Employee.Salary = Int32.Parse(SalaryTB.Text);
            //Employee.Position = PositionTB.Text;
            //Employee.Department = DepartmentsCB_Employees.SelectedItem as Department;


            Row["Age"] = Int32.Parse(AgeTB.Text);
            Row["Name"] = NameFNameTB.Text;
            Row["Salary"] = Int32.Parse(SalaryTB.Text);
            Row["Position"] = PositionTB.Text;
            Row["Department"] = DepartmentsCB_Employees.SelectedItem; //тут должен быть ключ, но я хз как его взять
            this.DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AgeTB.Text = Row["Age"].ToString();
            NameFNameTB.Text = Row["Name"].ToString();
            SalaryTB.Text = Row["Salary"].ToString();
            PositionTB.Text = Row["Position"].ToString();
            DataRow[] arr = DepartmentRow.Table.Select($"ID = {Row["Department"]}");
            Console.WriteLine(arr[0]["Name"]);
            //DepartmentsCB_Employees.SelectedItem = arr[0]["Name"]; //тут должен быть ключ, но я хз как его взять из комбобокса

        }
    }
}
