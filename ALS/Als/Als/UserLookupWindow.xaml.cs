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
using System.Data;
using System.Data.OleDb;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows.Forms;

namespace Als
{
    /// <summary>
    /// Interaction logic for UserLookupWindow.xaml
    /// </summary>
    public partial class UserLookupWindow : Window
    {
        private UserRolePage userRolePage;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string Role_ID, criteria, checkQuery;
        

        public UserLookupWindow(UserRolePage userRolePage, string Role_ID)
        {
            InitializeComponent();
            this.userRolePage = userRolePage;
            this.Role_ID = Role_ID;
            conn = new OleDbConnection(connParam);
            checkQuery = "Switch(b.Role_ID = " + Role_ID + ", 'True', b.Role_ID <> " + Role_ID + ", 'False', b.Role_ID is null, 'False')";
            dgUser_Bind();
        }

        private void dgUser_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                string query = "SELECT a.ID, a.User_ID, a.User_Name, a.User_No_Ext, " + checkQuery + " as Flag ";
                query = query + "FROM MasterUser a LEFT JOIN MasterUserRole b ON a.ID = b.User_ID Where 1 =1 AND a.Deleted = 0"+ criteria +" order by a.ID desc";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                dgUser.DataContext = pub.BindDG(cmd);

            }
            catch { }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtNoExtention.Text = string.Empty;
            txtUserId.Text = string.Empty;
            txtUserName.Text = string.Empty;
            criteria = string.Empty;
            dgUser_Bind();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                criteria = string.Empty;
                criteria = " AND a.User_ID LIKE '%" + txtUserId.Text + "%'";
                if (!string.IsNullOrEmpty(txtUserName.Text))
                    criteria = criteria + " AND User_Name LIKE '%" + txtUserName.Text + "%' ";
                if (!string.IsNullOrEmpty(txtNoExtention.Text))
                    criteria = criteria + " AND User_No_Ext LIKE '%" + txtNoExtention.Text + "%'";

                dgUser_Bind();
            }
            catch { }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            DataView view = (DataView)dgUser.ItemsSource;
            DataTable table = DataViewAsDataTable(view);
            userRolePage.dgUserRole.ItemsSource = table.DefaultView;
            userRolePage.dataView = view;
            this.Close();
        }

        private void chkAll_Checked(object sender, RoutedEventArgs e)
        {
            checkQuery = "'True'";
            dgUser_Bind();
        }

        private void chkAll_Unchecked(object sender, RoutedEventArgs e)
        {
            checkQuery = "'False'";
            dgUser_Bind();
            
        }

        public static DataTable DataViewAsDataTable(DataView dv)
        {
            DataTable dt = dv.Table.Clone();
            dt.Columns.Remove("RowNo");
            foreach (DataRowView drv in dv)
            {
                if(drv.Row["Flag"].ToString() == "True")
                    dt.ImportRow(drv.Row);
            }
            DataTable table = new DataTable();
            DataColumn col = table.Columns.Add("RowNo", typeof(int));
            col.AutoIncrementSeed = 1;
            col.AutoIncrement = true;
            table.Load(dt.CreateDataReader());

            return table;
        }
    }
}
