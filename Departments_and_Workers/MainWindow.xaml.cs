using Departments_and_Workers.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
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
        DataSet ds;
        SqlDataAdapter adapterDepartments, adapterPeople;
        static string connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Lesson7; Integrated Security = True";
        SqlConnection connection = new SqlConnection(connectionString);

        private void UpdateListBox(object sender, EventArgs e)
        {
            DataRow [] arr = ds.Tables["People"].Select($"Department = {((sender as ComboBox).SelectedItem as DataRowView).Row["ID"]}");
            EmployeesLV.ItemsSource = arr.CopyToDataTable().DefaultView;
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            ds = new DataSet();

            #region


            //string createStoredProcDepartment = @"CREATE PROCEDURE [dbo].[sp_GetDepartment] AS SELECT * FROM Departments;";
            //string createStoredProcPeople = @"CREATE PROCEDURE [dbo].[sp_GetPeople] AS SELECT * FROM People;";
            //string sqlExpressionDepartments = @"INSERT INTO Departments (Name) VALUES ( N'IT');
            //                                    INSERT INTO Departments (Name) VALUES ( N'Бухгалтер');";
            //string sqlExpression = @"INSERT INTO People (Name, Age,Salary,Position,Department) VALUES ( N'Аркадий Васильевич', '28', '150', N'Middle-программист', '1' );
            //                         INSERT INTO People (Name, Age,Salary,Position,Department) VALUES ( N'Геннадий Геннадьевич', '30', '220', 'Team-Lead', '1' );
            //                         INSERT INTO People (Name, Age,Salary,Position,Department) VALUES ( N'Алексей Валерьевич', '35', '300', N'Архитектор БД', '1' );
            //                         INSERT INTO People (Name, Age,Salary,Position,Department) VALUES ( N'Степан Евграфович', '22', '100', N'Junior-программист', '1' );
            //                         INSERT INTO People (Name, Age,Salary,Position,Department) VALUES ( N'Анастасия Федотовна', '40', '150', N'Главный бухгалтер', '2' );
            //                         INSERT INTO People (Name, Age,Salary,Position,Department) VALUES ( N'Евгения Максимовна', '42', '70', N'Бухгалтер', '2' );
            //                         INSERT INTO People (Name, Age,Salary,Position,Department) VALUES ( N'Валерий Никифорович', '55', '130', N'Заместитель главного бухгалтера', '2' );";

            //string sqlExpression = "SELECT * FROM People";

            //connection.Open();
            //SqlCommand command = new SqlCommand(sqlExpression, connection);
            //// Указываем, что команда представляет хранимую процедуру
            //command.CommandType = System.Data.CommandType.StoredProcedure;
            //SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            //if (reader.HasRows)       // Если есть данные
            //{
            //    while (reader.Read()) // Построчно считываем данные
            //    {
            //        var vId = Convert.ToInt32(reader.GetValue(0));
            //        var vFIO = reader.GetString(1);
            //        var vEmail = reader["Email"];
            //        var vPhone = reader.GetString(reader.GetOrdinal("Phone"));
            //    }
            //}
            //reader.Close();


            //string sql = "SELECT * FROM People";
            //SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
            //DataSet ds = new DataSet();
            //adapter.Fill(ds);






            adapterPeople = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM People", connection);
            adapterPeople.SelectCommand = command;
            // insert
            command = new SqlCommand("INSERT INTO People (Name, Age,Salary,Position,Department) VALUES (@Name, @Age, @Salary, @Position, @Department); SET @ID = @@IDENTITY;", connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Age", SqlDbType.Int, -1, "Age");
            command.Parameters.Add("@Salary", SqlDbType.Int, -1, "Salary");
            command.Parameters.Add("@Position", SqlDbType.NVarChar, -1, "Position");
            command.Parameters.Add("@Department", SqlDbType.Int, -1, "Department");
            SqlParameter param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;
            adapterPeople.InsertCommand = command;
            // update
            command = new SqlCommand(@"UPDATE People SET Name = @Name, Age = @Age, Salary = @Salary, Position = @Position, Department = @Department WHERE ID = @ID", connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Age", SqlDbType.Int, -1, "Age");
            command.Parameters.Add("@Salary", SqlDbType.Int, -1, "Salary");
            command.Parameters.Add("@Position", SqlDbType.NVarChar, -1, "Position");
            command.Parameters.Add("@Department", SqlDbType.Int, -1, "Department");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapterPeople.UpdateCommand = command;
            // delete
            command = new SqlCommand("DELETE FROM People WHERE ID = @ID", connection);
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapterPeople.DeleteCommand = command;
            ds.Tables.Add(new DataTable("People"));
            adapterPeople.Fill(ds.Tables["People"]);







            adapterDepartments = new SqlDataAdapter();
            command = new SqlCommand("SELECT * FROM Departments", connection);
            adapterDepartments.SelectCommand = command;
            // insert
            command = new SqlCommand("INSERT INTO Departments (Name) VALUES (@Name); SET @ID = @@IDENTITY;", connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.Direction = ParameterDirection.Output;
            adapterDepartments.InsertCommand = command;
            // update
            command = new SqlCommand(@"UPDATE Departments SET Name = @Name WHERE ID = @ID", connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapterDepartments.UpdateCommand = command;
            // delete
            command = new SqlCommand("DELETE FROM Departments WHERE ID = @ID", connection);
            param = command.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            param.SourceVersion = DataRowVersion.Original;
            adapterDepartments.DeleteCommand = command;
            ds.Tables.Add(new DataTable("Departments"));
            adapterDepartments.Fill(ds.Tables["Departments"]);

            #endregion

            //DataContext = ds.Tables["Departments"].DefaultView;
            //DepartmentsCB.DataContext = ds.Tables["Departments"].DefaultView;
            //DepartmentsCB.ItemsSource = ds.Tables["Departments"].Rows[];
            DepartmentsCB.ItemsSource = ds.Tables["Departments"].DefaultView;














        }

        private void EmployeesLV_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)EmployeesLV.SelectedItem;
            newRow.BeginEdit();
            EmployeeWindow editWindow = new EmployeeWindow(newRow.Row, (DepartmentsCB.SelectedItem as DataRowView).Row, ds.Tables["Departments"].DefaultView, adapterDepartments);
            editWindow.ShowDialog();
            if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
            {
                newRow.EndEdit();
                adapterPeople.Update(ds.Tables["People"]);
            }
            else
            {
                newRow.CancelEdit();
            }
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            //new EmployeeWindow("Создание сотрудника", DataContext, Departments, DepartmentsCB.ItemsSource, -1, new Employee()).Show();
            // Добавим новую строку
            DataRow newRow = ds.Tables["People"].NewRow();
            EmployeeWindow editWindow = new EmployeeWindow(newRow, (DepartmentsCB.SelectedItem as DataRowView).Row, ds.Tables["Departments"].DefaultView, adapterDepartments);
            editWindow.ShowDialog();
            if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
            {
                ds.Tables["People"].Rows.Add(editWindow.Row);
                adapterPeople.Update(ds.Tables["People"]);
            }

        }

        private void AddDepartmentBtn_Click(object sender, RoutedEventArgs e)
        {
            //new DepartmentWindow(Departments).Show();
        }

        private void DeleteEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)EmployeesLV.SelectedItem;
            newRow.Row.Delete();
            adapterPeople.Update(ds.Tables["People"]);
        }

        private void EditDepartmentBtn_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)DepartmentsCB.SelectedItem;
            newRow.BeginEdit();
            DepartmentWindow editWindow = new DepartmentWindow(newRow.Row);
            editWindow.ShowDialog();
            if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
            {
                newRow.EndEdit();
                adapterPeople.Update(ds.Tables["Departments"]);
            }
            else
            {
                newRow.CancelEdit();
            }
        }
    }
}
