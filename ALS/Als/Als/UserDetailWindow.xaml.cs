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
    /// Interaction logic for UserDetailWindow.xaml
    /// </summary>
    public partial class UserDetailWindow : Window
    {
        private UserPage userPage;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public UserDetailWindow(UserPage userPage)
        {
            InitializeComponent();
            this.userPage = userPage;
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
                    string userId = txtUserId.Text;
                    string userPassword = txtPassword.Text;
                    string username = txtUserName.Text;
                    string userNoExt = txtNoExtention.Text;
                    string jobRole = txtJobRole.Text;
                    string limit = txtLimitApproval.Text;
                    string createdBy = LoginWindow.LoginInfo.UserID;
                    string createdDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    string query = "";
                    if (string.IsNullOrEmpty(id))
                    {
                        if (!pub.validateName(cmd, "MasterUser", "User_ID", userId))
                        {
                            flag = false;
                            MessageBox.Show("User ID already exist !!", "ERROR");
                            return;
                        }
                        query = "INSERT INTO MasterUser (User_ID, User_Password, User_Name, User_No_Ext, Job_Role, Limit_Approval, Deleted, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)";
                        query = query + " values ('" + userId + "', '" + userPassword + "', '" + username + "', '" + userNoExt + "', '" + jobRole + "', '" + limit + "',";
                        query = query + " 0, '" + createdBy + "', #" + createdDate + "#, '" + createdBy + "', #" + createdDate + "#)";
                    }
                    else
                    {
                        query = "UPDATE MasterUser SET User_Password = '" + userPassword + "', User_Name = '" + username + "', User_No_Ext = '" + userNoExt + "',";
                        query = query + " Job_Role = '" + jobRole + "', Limit_Approval = '" + limit + "', UpdatedBy = '" + createdBy + "', UpdatedDate = #" + createdDate + "#";
                        query = query + " WHERE ID = " + id;

                    }
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException ex) {
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
                                pub.Audit_Trail(conn, cmd, "Master User", "Create", "User ID : " + txtUserId.Text);
                            else
                                pub.Audit_Trail(conn, cmd, "Master User", "Update", "User ID : " + txtUserId.Text);

                            userPage.RefreshPage();
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

        private void txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void CheckIsNumeric(TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result)))
            {
                e.Handled = true;
            }
        }

        private bool validateData()
        {
            if (string.IsNullOrEmpty(txtUserId.Text))
            {
                MessageBox.Show("Please fill User ID !!", "WARNING");
                return false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please fill Password !!", "WARNING");
                return false;
            }
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("Please fill User Name !!", "WARNING");
                return false;
            }
            if (string.IsNullOrEmpty(txtLimitApproval.Text))
            {
                MessageBox.Show("Please fill Limit Approval !!", "WARNING");
                return false;
            }
            return true;
        }
    }
}
