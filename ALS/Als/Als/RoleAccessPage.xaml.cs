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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace Als
{
    /// <summary>
    /// Interaction logic for RoleAccessPage.xaml
    /// </summary>
    public partial class RoleAccessPage : Page
    {
        private MainWindow main;
        private RolePage rolePage;
        private string RoleID, RoleName;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();
        private string moduleName = "Master Role Access";

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public RoleAccessPage(MainWindow main, RolePage rolePage, string RoleID, string RoleName)
        {
            InitializeComponent();
            this.main = main;
            this.rolePage = rolePage;
            this.RoleID = RoleID;
            this.RoleName = RoleName;
            lblRoleNameValue.Text = this.RoleName;
            conn = new OleDbConnection(connParam);
            RefreshPageDate();
            dgRoleAccess_Bind();
            CheckAccess();
        }

        public void RefreshPageDate()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                string query = "SELECT TOP 1 CreatedBy, CreatedDate, UpdatedBy, UpdatedDate ";
                query = query + " FROM MasterRoleAccess Where 1=1 AND Role_ID = " + RoleID;
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            lblCreatedByValue.Text = dt.Rows[0]["CreatedBy"].ToString();
                            lblCreatedDateValue.Text = dt.Rows[0]["CreatedDate"].ToString();
                            lblUpdatedByValue.Text = dt.Rows[0]["UpdatedBy"].ToString();
                            lblUpdatedDateValue.Text = dt.Rows[0]["UpdatedDate"].ToString();
                        }
                    }
                }

            }
            catch { }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
        }

        private void dgRoleAccess_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                string query = "Select Module_Name, mm.ID as Module_ID, ";
                query = query + "Switch(Create_Check =True, 'True', True , 'False') as Create_Flag, ";
                query = query + "Switch(Update_Check =True, 'True', True , 'False') as Update_Flag, ";
                query = query + "Switch(View_Check = True, 'True', True , 'False') as View_Flag, ";
                query = query + "Switch(Delete_Check =True, 'True', True , 'False') as Delete_Flag, ";
                query = query + "Switch(Upload_Check =True, 'True', True , 'False') as Upload_Flag ";
                query = query + "From MasterModule mm LEFT JOIN MasterRoleAccess mra ON mm.ID = mra.Module_ID Where 1=1 AND mra.Role_ID = " + RoleID;
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                dgRoleAccess.DataContext = pub.BindDG(cmd);

            }
            catch { }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string confirm = "Do you want to save ?";
            string action = "Confirmation Update";

            MessageBoxResult result = MessageBox.Show(confirm, action, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    string query = "";
                    string createdBy = LoginWindow.LoginInfo.UserID;
                    string createdDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                    foreach (DataRowView drv in dgRoleAccess.ItemsSource)
                    {
                        query = "UPDATE MasterRoleAccess SET Create_Check = " + drv.Row["Create_Flag"] + ", Update_Check = " + drv.Row["Update_Flag"];
                        query = query + ", View_Check = " + drv.Row["View_Flag"] + ", Delete_Check = " + drv.Row["Delete_Flag"] + ", Upload_Check = " + drv.Row["Upload_Flag"];
                        query = query + ", UpdatedBy = '" + createdBy + "', UpdatedDate = #" + createdDate + "#";
                        query = query + " WHERE Role_ID = "+ RoleID +" AND  Module_ID =  " + drv.Row["Module_ID"];
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }

                }
                catch { }
                finally
                {
                    try
                    {
                        conn.Close();
                        pub.Audit_Trail(conn, cmd, "Master Role Access", "Update", "Role Name : " + RoleName);
                        RoleName = string.Empty;
                        main.mainFrame.Navigate(rolePage);
                        pub.SuccessMessage("Updated.");
                    }
                    catch { }
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            main.mainFrame.Navigate(rolePage);
        }

        private void CheckAccess()
        {
            
            DataRow row = pub.Access(moduleName);
            if (row == null || string.IsNullOrEmpty(row[1].ToString()))
            {
                btnSave.Visibility = Visibility.Hidden;
            }
            else
            {
                if (!(bool)row["Create_Check"])
                    btnSave.Visibility = Visibility.Hidden;
                if (!(bool)row["Update_Check"])
                    btnSave.Visibility = Visibility.Hidden;
            }

        }
    }
}
