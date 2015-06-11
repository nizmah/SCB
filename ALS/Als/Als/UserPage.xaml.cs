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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Configuration;

namespace Als
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();
        private string moduleName = "Master User";

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, user_ID;

        public UserPage()
        {
            InitializeComponent();
            conn = new OleDbConnection(connParam);
            dgUser_Bind();
            CheckAccess();
        }

        public void RefreshPage()
        {
            dgUser_Bind();
        }

        private void dgUser_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "Select * From MasterUser Where 1=1 AND Deleted = 0" + criteria + " order by ID desc";
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
            txtJobRole.Text = string.Empty;
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
                criteria = " AND User_ID LIKE '%" + txtUserId.Text + "%'";
                if(!string.IsNullOrEmpty(txtUserName.Text))
                    criteria = criteria + " AND User_Name LIKE '%" + txtUserName.Text + "%' ";
                if(!string.IsNullOrEmpty(txtNoExtention.Text))
                    criteria = criteria + " AND User_No_Ext LIKE '%" + txtNoExtention.Text + "%'";
                if(!string.IsNullOrEmpty(txtJobRole.Text))
                    criteria = criteria + " AND Job_Role LIKE '%" + txtJobRole.Text + "%'";

                dgUser_Bind();
            }
            catch { }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            UserDetailWindow userDetail = new UserDetailWindow(this);
            userDetail.lblTitle.Text = "User - Create";
            userDetail.lblID.Text = string.Empty;
            userDetail.txtUserId.Text = string.Empty;
            userDetail.txtPassword.Text = string.Empty;
            userDetail.txtUserName.Text = string.Empty;
            userDetail.txtNoExtention.Text = string.Empty;
            userDetail.txtJobRole.Text = string.Empty;
            userDetail.txtLimitApproval.Text = "0";
            userDetail.gbDetail.Visibility = Visibility.Hidden;
            userDetail.ShowDialog();
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ID = ((FrameworkElement)sender).DataContext;
                DataRowView tab = (DataRowView)ID;
                UserDetailWindow userDetail = new UserDetailWindow(this);
                //fill data
                userDetail.txtUserId.Text = tab["User_ID"].ToString();
                user_ID = tab["User_ID"].ToString(); 
                userDetail.txtPassword.Text = tab["User_Password"].ToString();
                if (tab["User_Name"] != null)
                    userDetail.txtUserName.Text = tab["User_Name"].ToString();
                if (tab["User_No_Ext"] != null)
                    userDetail.txtNoExtention.Text = tab["User_No_Ext"].ToString();
                if (tab["Job_Role"] != null)
                    userDetail.txtJobRole.Text = tab["Job_Role"].ToString();
                if (tab["Limit_Approval"] != null)
                    userDetail.txtLimitApproval.Text = tab["Limit_Approval"].ToString();

                if (tab["CreatedBy"] != null)
                    userDetail.lblCreatedByValue.Text = tab["CreatedBy"].ToString();
                if (tab["CreatedDate"] != null)
                    userDetail.lblCreatedDateValue.Text = tab["CreatedDate"].ToString();
                if (tab["UpdatedBy"] != null)
                    userDetail.lblUpdatedByValue.Text = tab["UpdatedBy"].ToString();
                if (tab["UpdatedDate"] != null)
                    userDetail.lblUpdatedDateValue.Text = tab["UpdatedDate"].ToString();

                userDetail.lblTitle.Text = "User - View";
                //enable/disable control
                userDetail.txtUserId.IsEnabled = false;
                userDetail.txtUserName.IsEnabled = false;
                userDetail.txtPassword.IsEnabled = false;
                userDetail.txtNoExtention.IsEnabled = false;
                userDetail.txtJobRole.IsEnabled = false;
                userDetail.txtLimitApproval.IsEnabled = false;

                userDetail.gbDetail.Visibility = Visibility.Visible;
                userDetail.btnSave.Visibility = Visibility.Hidden;

                userDetail.ShowDialog();
            }
            catch { }
            finally
            {
                try {
                    pub.Audit_Trail(conn, cmd, "Master User", "View", "User ID : " + user_ID);
                    user_ID = string.Empty;
                }
                catch { }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            UserDetailWindow userDetail = new UserDetailWindow(this);
            //fill data
            userDetail.lblID.Text = tab["ID"].ToString();
            userDetail.txtUserId.Text = tab["User_ID"].ToString();
            userDetail.txtPassword.Text = tab["User_Password"].ToString();
            if (tab["User_Name"] != null)
                userDetail.txtUserName.Text = tab["User_Name"].ToString();
            if (tab["User_No_Ext"] != null)
                userDetail.txtNoExtention.Text = tab["User_No_Ext"].ToString();
            if (tab["Job_Role"] != null)
                userDetail.txtJobRole.Text = tab["Job_Role"].ToString();
            if (tab["Limit_Approval"] != null)
                userDetail.txtLimitApproval.Text = tab["Limit_Approval"].ToString();

            userDetail.lblTitle.Text = "User - Update";
            //enable/disable control
            userDetail.txtUserId.IsEnabled = false;
            userDetail.txtUserName.IsEnabled = true;
            userDetail.txtPassword.IsEnabled = true;
            userDetail.txtNoExtention.IsEnabled = true;
            userDetail.txtJobRole.IsEnabled = true;
            userDetail.txtLimitApproval.IsEnabled = true;

            userDetail.gbDetail.Visibility = Visibility.Visible;
            userDetail.btnSave.Visibility = Visibility.Visible;

            userDetail.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //ConfirmationWindow confirm = new ConfirmationWindow();
            //confirm.lblTitle.Text = "Confirmation Delete";
            //confirm.lblConfirmation.Text = "Are you sure to delete this data ?";
            //confirm.ShowDialog();
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            user_ID = tab["User_ID"].ToString();
            MessageBoxResult result = MessageBox.Show("Are you sure to delete this data ?", "Confirmation Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE MasterUser SET Deleted = 1 Where ID = " + tab["ID"].ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    dgUser_Bind();
                }
                catch { }
                finally
                {
                    try
                    {
                        pub.Audit_Trail(conn, cmd, "Master User", "Delete", "User ID : " + user_ID);
                        user_ID = string.Empty;
                    }
                    catch { }
                }
            }
        }

        private void CheckAccess()
        {
            pub.CheckAccess(moduleName, dgUser, btnCreate);

        }
    }
}
