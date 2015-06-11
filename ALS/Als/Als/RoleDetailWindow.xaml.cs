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
using System.Configuration;

namespace Als
{
    /// <summary>
    /// Interaction logic for RoleDetailWindow.xaml
    /// </summary>
    public partial class RoleDetailWindow : Window
    {
        private RolePage rolePage;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand("SELECT @@IDENTITY");
        PublicClass pub = new PublicClass();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public RoleDetailWindow(RolePage rolePage)
        {
            InitializeComponent();
            this.rolePage = rolePage;
            conn = new OleDbConnection(connParam);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string confirm = "Do you want to save ?";
            string action = "Confirmation Create";
            bool flag = true;
            if (!string.IsNullOrEmpty(lblID.Text))
            {
                confirm = "Previous data will be changed, do you want save ?";
                action = "Confirmation Update";
            }
            if (!validateData())
                return;
            MessageBoxResult result = MessageBox.Show(confirm, action, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    string id = lblID.Text;
                    string roleName = txtRoleName.Text;
                    string roleDescription = txtRoleDescription.Text;
                    string createdBy = LoginWindow.LoginInfo.UserID;
                    string createdDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    string query = "";
                    
                    if (string.IsNullOrEmpty(id))
                    {
                        if (!pub.validateName(cmd, "MasterRole", "Role_Name", roleName))
                        {
                            flag = false;
                            MessageBox.Show("Role Name already exist !!", "ERROR");
                            return;
                        }
                        query = "INSERT INTO MasterRole (Role_Name, Role_Description, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)";
                        query = query + " values ('" + roleName + "', '" + roleDescription + "', '" + createdBy + "', #" + createdDate + "#, '" + createdBy + "', #" + createdDate + "#)";
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        query = "SELECT @@IDENTITY";
                        cmd.CommandText = query;
                        int roleID = (int)cmd.ExecuteScalar();
                        query = "INSERT INTO MasterRoleAccess (Module_ID, Role_ID, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)";
                        query = query + "SELECT ID, " + roleID + ", '" + createdBy + "', #" + createdDate + "#, '" + createdBy + "', #" + createdDate + "# FROM MasterModule";
                    }
                    else
                    {
                        query = "UPDATE MasterRole SET Role_Description = '" + roleDescription + "',";
                        query = query + " UpdatedBy = '" + createdBy + "', UpdatedDate = #" + createdDate + "#";
                        query = query + " WHERE ID = " + id;
                    }

                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException ex)
                {
                    flag = false;
                    MessageBox.Show("Insert Failed !!", "ERROR");
                }
                finally
                {
                    try
                    {
                        conn.Close();
                        if (flag)
                        {
                            if (string.IsNullOrEmpty(lblID.Text))
                                pub.Audit_Trail(conn, cmd, "Master Role", "Create", "Role Name : " + txtRoleName.Text);
                            else
                                pub.Audit_Trail(conn, cmd, "Master Role", "Update", "Role Name : " + txtRoleName.Text);

                            rolePage.RefreshPage();
                            this.Close();
                            if (string.IsNullOrEmpty(lblID.Text))
                                pub.SuccessMessage("Inserted.");
                            else
                                pub.SuccessMessage("Updated.");
                        }
                    }
                    catch { }
                }
            }
        }

        private bool validateData()
        {
            if (string.IsNullOrEmpty(txtRoleName.Text))
            {
                MessageBox.Show("Please fill Role Name !!", "WARNING");
                return false;
            }
            return true;
        }
    }
}
