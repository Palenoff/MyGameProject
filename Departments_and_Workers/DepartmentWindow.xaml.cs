using Departments_and_Workers.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        public DataRow Row { get; set; }

        public DepartmentWindow(Department department)
        {
            InitializeComponent();
        }

        public DepartmentWindow(DataRow row)
        {
            this.Row = row;
        }



        private void SaveDepartmentBtn_Click(object sender, RoutedEventArgs e)
        {
            Row["Name"] = DepartmentTB.Text;
            this.DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //DepartmentTB.Text = Row["Name"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
