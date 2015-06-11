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
    /// Interaction logic for BankDetailWindow.xaml
    /// </summary>
    public partial class BankDetailWindow : Window
    {
        private BankPage bankPage;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public BankDetailWindow(BankPage bankPage)
        {
            InitializeComponent();
            this.bankPage = bankPage;
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
                    string bankName = txtBankName.Text;
                    string bankBranch = txtBankBranch.Text;
                    string bankDescription = txtBankDescription.Text;
                    string bankCity = txtBankCity.Text;
                    string createdBy = LoginWindow.LoginInfo.UserID;
                    string createdDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    string query = "";
                    if (string.IsNullOrEmpty(id))
                    {
                        string[] bankCategory = new string[3];
                        string[] bankValue = new string[3];
                        bankCategory[0] = "Bank_Name"; bankCategory[1] = "Bank_Branch"; bankCategory[2] = "Bank_City";
                        bankValue[0] = "'" + bankName + "'"; bankValue[1] = "'" + bankBranch + "'"; bankValue[2] = "'" + bankCity + "'";

                        if (!pub.validateNameCategories(cmd, "MasterBank", bankCategory, bankValue))
                        {
                            flag = false;
                            MessageBox.Show("Data already exist !!", "ERROR");
                            return;
                        }
                        query = "INSERT INTO MasterBank (Bank_Name, Bank_Branch, Bank_City, Bank_Description, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)";
                        query = query + " values ('" + bankName + "','" + bankBranch + "', '" + bankCity + "', '" + bankDescription + "', '" + createdBy + "', #" + createdDate + "#, '" + createdBy + "', #" + createdDate + "#)";
                    }
                    else
                    {
                        query = "UPDATE MasterBank SET Bank_Branch = '" + bankBranch + "', Bank_City = '" + bankCity + "',";
                        query = query + "Bank_Description = '" + bankDescription + "',";
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
                                pub.Audit_Trail(conn, cmd, "Master Bank", "Create", "Bank Category : " + txtBankName.Text + " - " + txtBankBranch.Text + " - " + txtBankCity.Text);
                            else
                                pub.Audit_Trail(conn, cmd, "Master Bank", "Update", "Bank Category : " + txtBankName.Text + " - " + txtBankBranch.Text + " - " + txtBankCity.Text);

                            bankPage.RefreshPage();
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
            if (string.IsNullOrEmpty(txtBankName.Text))
            {
                MessageBox.Show("Please fill Bank Name !!", "WARNING");
                return false;
            }
            if (string.IsNullOrEmpty(txtBankBranch.Text))
            {
                MessageBox.Show("Please fill Bank Branch !!", "WARNING");
                return false;
            }
            if (string.IsNullOrEmpty(txtBankCity.Text))
            {
                MessageBox.Show("Please fill Bank City !!", "WARNING");
                return false;
            }
            return true;
        }
    }
}
