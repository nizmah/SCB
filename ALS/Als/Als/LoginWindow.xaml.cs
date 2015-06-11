using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes; 
using System.Security.Permissions;
using System.Data.OleDb;
using System.Data;
using System.Configuration;

namespace Als
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>

    [ObjectPermissionAttribute(SecurityAction.Demand, RequiredPermissionAccess = ObjectPermissionAccess.Guest)]

    public partial class LoginWindow 
    {
        public static class LoginInfo
        {
            public static string UserID;
            public static DataTable accessTable;
        }

        PublicClass pub = new PublicClass();
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public LoginWindow()
        {

            InitializeComponent();
            var uri = new Uri(@"pack://application:,,,/Als;component/Images/StandartChartered.jpg");
            img.Source = new BitmapImage(uri);
            conn = new OleDbConnection(connParam);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string userID = txtUsr.Text;
            string pass = txtPwd.Password;
            string deleted = string.Empty;

            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Please insert User ID / Password");
                return;
            }

            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "Select Deleted From MasterUser Where User_ID = '" + userID + "' AND User_Password = '" + pass + "'";
            cmd.CommandType = CommandType.Text;
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                deleted = reader.GetValue(0).ToString();
            }
            reader.Close();

            if (deleted == "False")
            {
                string query = "Select d.Module_Name, c.Create_Check,c.Update_Check, c.View_Check, c.Delete_Check, c.Upload_Check ";
                query = query + "FROM ((((MasterUserRole a ";
                query = query + "INNER JOIN MasterRole b ON a.Role_ID = b.ID) ";
                query = query + "INNER JOIN MasterUser e ON a.User_ID = e.ID) ";
                query = query + "INNER JOIN MasterRoleAccess c ON b.ID = c.Role_ID) ";
                query = query + "LEFT JOIN MasterModule d ON c.Module_ID = d.ID) ";
                query = query + "WHERE a.Deleted = 0 AND b.Deleted = 0 AND e.User_ID = '" + userID + "'";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                //LoginInfo.accessTable = new DataTable();
                LoginInfo.accessTable = pub.BindDG(cmd);

                LoginInfo.UserID = userID;
                MainWindow form = new MainWindow();
                form.Show();
                //HomePage pages = new HomePage();
                //pages.nav;
                this.Hide();
                
            }
            else
            {
                MessageBox.Show("User does not exist or already deleted");
                txtUsr.Text = string.Empty;
                txtPwd.Clear();
            }
            conn.Close();
        
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //private void Hyperlink_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("To enhance system security, automatic password recovery is disabled. " +
        //        "Please contact you system administrator to recover your password.",
        //        "Password Recovery", MessageBoxButton.OK, MessageBoxImage.Information);
        //}
    }
}
