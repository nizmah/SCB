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
    /// Interaction logic for GLAccountDetailWindow.xaml
    /// </summary>
    public partial class GLAccountDetailWindow : Window
    {
        private GLAccountPage glAccount;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria;

        public GLAccountDetailWindow(GLAccountPage glAccount)
        {
            InitializeComponent();
            this.glAccount = glAccount;
            conn = new OleDbConnection(connParam);
            bindCB();
            cbBinNo_Bind();
        }

        private void bindCB()
        {
            cbTC.DisplayMemberPath = "Key";
            cbTC.SelectedValuePath = "Value";
            cbTC.Items.Add(new KeyValuePair<string, string>("40", "40"));
            cbTC.Items.Add(new KeyValuePair<string, string>("41", "41"));

            cbAccEntry.DisplayMemberPath = "Key";
            cbAccEntry.SelectedValuePath = "Value";
            cbAccEntry.Items.Add(new KeyValuePair<string, string>("Debit", "Debit"));
            cbAccEntry.Items.Add(new KeyValuePair<string, string>("Credit", "Credit"));
        }

        private void cbBinNo_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "Select Bin_No, Bin_No + ' - ' + Card_Name AS Display From MasterCardName Where 1=1 AND Deleted = 0";
                cmd.CommandType = CommandType.Text;

                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        DataRow row = dt.NewRow();
                        row["Display"] = "-Please Select-";
                        row["Bin_No"] = 0;
                        dt.Rows.Add(row);
                        DataView dv = dt.DefaultView;
                        dv.Sort = "Bin_No asc";
                        cbBinNo.ItemsSource = dv;
                        cbBinNo.SelectedValue = 0;
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
                    string id = lblID.Text;                    string glAccNo = txtGLAccountNo.Text;
                    string glAccName = txtGLAccountName.Text;
                    string tc = cbTC.SelectedValue.ToString();
                    string accEntry = cbAccEntry.SelectedValue.ToString();
                    string glProd = txtGLProd.Text;
                    string glDept = txtGLDept.Text;
                    string glUnit = txtGLUnit.Text;
                    string glClass = txtGLClass.Text;
                    string glDesc = txtGLDescription.Text;
                    string binNo = cbBinNo.SelectedValue.ToString();
                    string createdBy = LoginWindow.LoginInfo.UserID;
                    string createdDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    string query = "";
                    if (string.IsNullOrEmpty(id))
                    {
                        if (!pub.validateName(cmd, "MasterGLAccount", "GL_No", glAccNo))
                        {
                            flag = false;
                            MessageBox.Show("GL Account No already exist !!", "ERROR");
                            return;
                        }
                        if (!pub.validateName(cmd, "MasterGLAccount", "GL_Name", "'"+glAccName+"'"))
                        {
                            flag = false;
                            MessageBox.Show("GL Account Name already exist !!", "ERROR");
                            return;
                        }
                        query = "INSERT INTO MasterGLAccount (GL_No, GL_Name, TC, Acc_Entry, GL_Prod, GL_Dept, GL_Unit, GL_Class, Bin_No, GL_Description, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)";
                        query = query + " values (" + glAccNo + ", '" + glAccName + "', '" + tc + "', '" + accEntry + "', '" + glProd + "', '" + glDept + "', '" + glUnit + "', '" + glClass + "', '" + binNo + "', '" + glDesc + "', ";
                        query = query + "'" + createdBy + "', #" + createdDate + "#, '" + createdBy + "', #" + createdDate + "#)";
                    }
                    else
                    {
                        query = "UPDATE MasterGLAccount SET GL_Name = '" + glAccName + "', TC = '" + tc + "', Acc_Entry = '" + accEntry + "', GL_Prod = '" + glProd + "',";
                        query = query + " GL_Dept = '" + glDept + "', GL_Unit = '" + glUnit + "', GL_Class = '" + glClass + "', Bin_No = '" + binNo + "', GL_Description = '" + glDesc + "', UpdatedBy = '" + createdBy + "', UpdatedDate = #" + createdDate + "#";
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
                                pub.Audit_Trail(conn, cmd, "Master GL Account", "Create", "GL No : " + txtGLAccountNo.Text);
                            else
                                pub.Audit_Trail(conn, cmd, "Master GL Account", "Update", "GL No : " + txtGLAccountNo.Text);

                            glAccount.RefreshPage();
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
            if (string.IsNullOrEmpty(txtGLAccountNo.Text))
            {
                MessageBox.Show("Please fill GL Account No !!", "WARNING");
                return false;
            }
            if (string.IsNullOrEmpty(txtGLAccountName.Text))
            {
                MessageBox.Show("Please fill GL Account Name !!", "WARNING");
                return false;
            }
            if (cbBinNo.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please select Bin No !!", "WARNING");
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            pub.CheckIsNumeric(e);
        }
    }
}
