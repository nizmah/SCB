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
    /// Interaction logic for UserRolePage.xaml
    /// </summary>
    public partial class UserRolePage : Page
    {
        private MainWindow main;
        private RolePage rolePage;
        private string RoleID, RoleName, criteria, checkQuery;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        public DataView dataView;
        PublicClass pub = new PublicClass();
        private string moduleName = "Master User Role";

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public UserRolePage(MainWindow main, RolePage rolePage,string RoleID, string RoleName)
        {
            InitializeComponent();
            this.main = main;
            this.rolePage = rolePage;
            this.RoleID = RoleID;
            this.RoleName = RoleName;
            lblRoleNameValue.Text = this.RoleName;
            conn = new OleDbConnection(connParam);
            criteria = " AND b.Role_ID = " + RoleID;
            checkQuery = "Switch(b.Role_ID = " + RoleID + ", 'True', b.Role_ID <> " + RoleID + ", 'False', b.Role_ID is null, 'False')";
            RefreshPageDate();
            dgUserRole_Bind();
            CheckAccess();
        }

        public void RefreshPageDate()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                string query = "SELECT TOP 1 CreatedBy, CreatedDate ";
                query = query + " FROM MasterUserRole b Where 1=1" + criteria ;
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
                            lblUpdatedByValue.Text = dt.Rows[0]["CreatedBy"].ToString();
                            lblUpdatedDateValue.Text = dt.Rows[0]["CreatedDate"].ToString();
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

        private void dgUserRole_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;

                string query = "SELECT a.ID, a.User_ID, a.User_Name, a.User_No_Ext, " + checkQuery + " as Flag ";
                query = query + "FROM MasterUser a LEFT JOIN MasterUserRole b ON a.ID = b.User_ID Where 1 =1 AND a.Deleted = 0 order by a.ID desc";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);

                        DataTable table = null;
                        var row = dt.Select("Flag = 'True'").ToList();
                        if (row.Any())
                        {
                            table = row.CopyToDataTable();

                            DataTable tableRow = new DataTable();
                            DataColumn col = tableRow.Columns.Add("RowNo", typeof(int));
                            col.AutoIncrementSeed = 1;
                            col.AutoIncrement = true;
                            tableRow.Load(table.CreateDataReader());

                            dgUserRole.DataContext = tableRow;
                        }

                        if (dataView == null)
                        {
                            DataTable tableRow = new DataTable();
                            DataColumn col = tableRow.Columns.Add("RowNo", typeof(int));
                            col.AutoIncrementSeed = 1;
                            col.AutoIncrement = true;
                            tableRow.Load(dt.CreateDataReader());

                            dataView = tableRow.DefaultView;
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;

            MessageBoxResult result = MessageBox.Show("Are you sure to delete this data ?", "Confirmation Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    DataTable table = pub.DataViewAsDataTable(dataView);
                    DataRow row = table.Select("ID = " + tab["ID"]).Single();
                    row["Flag"] = "False";
                    DataView dv = table.DefaultView;
                    DataTable dt = null;
                    var rowTable = table.Select("Flag = 'True'").ToList();
                    if (rowTable.Any())
                    {
                        dt = rowTable.CopyToDataTable();
                        dt.Columns.Remove("RowNo");
                        DataTable tableRow = new DataTable();
                        DataColumn col = tableRow.Columns.Add("RowNo", typeof(int));
                        col.AutoIncrementSeed = 1;
                        col.AutoIncrement = true;
                        tableRow.Load(dt.CreateDataReader());
                        dgUserRole.ItemsSource = tableRow.DefaultView;
                    }
                    else
                    {
                        dgUserRole.ItemsSource = null;
                        dgUserRole.Items.Refresh();
                    }
                    dataView = dv;
                }
                catch { }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            UserLookupWindow userLookup = new UserLookupWindow(this,RoleID);
            if (dataView != null)
                userLookup.dgUser.ItemsSource = dataView;
            userLookup.ShowDialog();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            main.mainFrame.Navigate(rolePage);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string confirm = "Do you want to save ?";
            string action = "Confirmation Create";

            MessageBoxResult result = MessageBox.Show(confirm, action, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "Delete From MasterUserRole Where Role_ID = " + RoleID;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    string query = "";
                    string createdBy = LoginWindow.LoginInfo.UserID;
                    string createdDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                    foreach (DataRowView drv in dgUserRole.ItemsSource)
                    {
                        query = "INSERT INTO MasterUserRole (Role_ID, User_ID, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)";
                        query = query + " values ('" + RoleID + "', '" + drv.Row["ID"] + "', '" + createdBy + "', #" + createdDate + "#, '" + createdBy + "', #" + createdDate + "#)";
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }


                }
                catch { }
                finally
                {
                    try { conn.Close();
                    pub.Audit_Trail(conn, cmd, "Master User Role", "Update", "Role Name : " + RoleName);
                    RoleName = string.Empty;
                    main.mainFrame.Navigate(rolePage);
                    pub.SuccessMessage("Updated.");
                    }
                    catch { }
                }
            }
        }

        private void CheckAccess()
        {
            DataRow row = pub.Access(moduleName);
            if (row == null || string.IsNullOrEmpty(row[1].ToString()))
            {
                btnAdd.Visibility = Visibility.Hidden;
                dgUserRole.Columns[dgUserRole.Columns.Count - 1].Visibility = Visibility.Hidden;
            }
            else
            {
                if (!(bool)row["Create_Check"])
                    btnAdd.Visibility = Visibility.Hidden;
                if (!(bool)row["Delete_Check"])
                    dgUserRole.Columns[dgUserRole.Columns.Count - 1].Visibility = Visibility.Hidden;
            }

        }
    }
}
