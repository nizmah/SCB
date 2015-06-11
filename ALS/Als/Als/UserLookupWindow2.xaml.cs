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
    /// Interaction logic for UserLookupWindow2.xaml
    /// </summary>
    public partial class UserLookupWindow2 : Window
    {
        private AuditTrailPage auditTrailPage;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, checkQuery;
        

        public UserLookupWindow2(AuditTrailPage auditTrailPage)
        {
            InitializeComponent();
            this.auditTrailPage = auditTrailPage;
            conn = new OleDbConnection(connParam);
            dgUser_Bind();
        }

        private void dgUser_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                string query = "SELECT a.ID, a.User_ID, a.User_Name, a.User_No_Ext ";
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
        
        private void Select_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            //fill data
            auditTrailPage.hdnID.Text = tab["ID"].ToString();
            auditTrailPage.txtUserId.Text = tab["User_ID"].ToString();
            if (tab["User_Name"] != null)
            {
                auditTrailPage.txtUserName.Text = tab["User_Name"].ToString();

            }
            this.Close();
        }
    }
}
