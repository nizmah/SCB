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
    /// Interaction logic for RolePage.xaml
    /// </summary>
    public partial class RolePage : Page
    {
        private MainWindow main;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();
        private string moduleName = "Master Role";

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, Role_Name;

        public RolePage(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            conn = new OleDbConnection(connParam);
            dgRole_Bind();
            CheckAccess();
        }

        public void RefreshPage()
        {
            dgRole_Bind();
        }

        private void dgRole_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "Select * From MasterRole Where 1=1 AND Deleted = 0" + criteria + " order by ID desc";
                cmd.CommandType = CommandType.Text;

                dgRole.ItemsSource = pub.BindDG(cmd).DefaultView;
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
            txtRoleName.Text = string.Empty;
            criteria = string.Empty;
            dgRole_Bind();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                criteria = string.Empty;
                criteria = " AND Role_Name LIKE '%" + txtRoleName.Text + "%'";

                dgRole_Bind();
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
            RoleDetailWindow roleDetail = new RoleDetailWindow(this);
            roleDetail.lblTitle.Text = "Role - Create";
            roleDetail.lblID.Text = string.Empty;
            roleDetail.txtRoleName.Text = string.Empty;
            roleDetail.txtRoleDescription.Text = string.Empty;
            roleDetail.gbDetail.Visibility = Visibility.Hidden;
            roleDetail.ShowDialog();
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ID = ((FrameworkElement)sender).DataContext;
                DataRowView tab = (DataRowView)ID;
                RoleDetailWindow roleDetail = new RoleDetailWindow(this);
                //fill data
                Role_Name = tab["Role_Name"].ToString();
                roleDetail.txtRoleName.Text = tab["Role_Name"].ToString();
                if (tab["Role_Description"] != null)
                    roleDetail.txtRoleDescription.Text = tab["Role_Description"].ToString();

                if (tab["CreatedBy"] != null)
                    roleDetail.lblCreatedByValue.Text = tab["CreatedBy"].ToString();
                if (tab["CreatedDate"] != null)
                    roleDetail.lblCreatedDateValue.Text = tab["CreatedDate"].ToString();
                if (tab["UpdatedBy"] != null)
                    roleDetail.lblUpdatedByValue.Text = tab["UpdatedBy"].ToString();
                if (tab["UpdatedDate"] != null)
                    roleDetail.lblUpdatedDateValue.Text = tab["UpdatedDate"].ToString();

                roleDetail.lblTitle.Text = "Role - View";
                //enable/disable control
                roleDetail.txtRoleName.IsEnabled = false;
                roleDetail.txtRoleDescription.IsEnabled = false;

                roleDetail.gbDetail.Visibility = Visibility.Visible;
                roleDetail.btnSave.Visibility = Visibility.Hidden;

                roleDetail.ShowDialog();
            }
            catch { }
            finally
            {
                try
                {
                    pub.Audit_Trail(conn, cmd, "Master Role", "View", "Role Name : " + Role_Name);
                    Role_Name = string.Empty;
                }
                catch { }
            }

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            RoleDetailWindow roleDetail = new RoleDetailWindow(this);
            //fill data
            
            roleDetail.lblID.Text = tab["ID"].ToString();
            roleDetail.txtRoleName.Text = tab["Role_Name"].ToString();
            if (tab["Role_Description"] != null)
                roleDetail.txtRoleDescription.Text = tab["Role_Description"].ToString();

            roleDetail.lblTitle.Text = "Role - Update";
            //enable/disable control
            roleDetail.txtRoleName.IsEnabled = false;
            roleDetail.txtRoleDescription.IsEnabled = true;

            roleDetail.gbDetail.Visibility = Visibility.Visible;
            roleDetail.btnSave.Visibility = Visibility.Visible;

            roleDetail.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //ConfirmationWindow confirm = new ConfirmationWindow();
            //confirm.lblTitle.Text = "Confirmation Delete";
            //confirm.lblConfirmation.Text = "Are you sure to delete this data ?";
            //confirm.ShowDialog();
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            Role_Name = tab["Role_Name"].ToString();
            MessageBoxResult result = MessageBox.Show("Are you sure to delete this data ?", "Confirmation Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE MasterRole SET Deleted = 1 Where ID = " + tab["ID"].ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    dgRole_Bind();
                }
                catch { }
                finally
                {
                    try
                    {
                        pub.Audit_Trail(conn, cmd, "Master Role", "Delete", "Role Name : " + Role_Name);
                        Role_Name = string.Empty;
                    }
                    catch { }
                }
            }
        }

        private void Member_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ID = ((FrameworkElement)sender).DataContext;
                DataRowView tab = (DataRowView)ID;
                Role_Name = tab["Role_Name"].ToString();
                main.Member_Role(this, tab["ID"].ToString(), tab["Role_Name"].ToString());
            }
            catch { }
            finally
            {
                try
                {
                    pub.Audit_Trail(conn, cmd, "Master User Role", "View", "Role Name : " + Role_Name);
                    Role_Name = string.Empty;
                }
                catch { }
            }
        }

        private void RoleAccess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ID = ((FrameworkElement)sender).DataContext;
                DataRowView tab = (DataRowView)ID;
                Role_Name = tab["Role_Name"].ToString();
                main.Role_Access(this, tab["ID"].ToString(), tab["Role_Name"].ToString());
            }
            catch { }
            finally
            {
                try
                {
                    pub.Audit_Trail(conn, cmd, "Master Role Access", "View", "Role Name : " + Role_Name);
                    Role_Name = string.Empty;
                }
                catch { }
            }
        }

        private void CheckAccess()
        {
            pub.CheckAccess(moduleName, dgRole, btnCreate);

        }
    }
}
