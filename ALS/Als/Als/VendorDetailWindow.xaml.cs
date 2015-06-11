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
    /// Interaction logic for VendorDetailWindow.xaml
    /// </summary>
    public partial class VendorDetailWindow : Window
    {
        private VendorPage vendorPage;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public VendorDetailWindow(VendorPage vendorPage)
        {
            InitializeComponent();
            this.vendorPage = vendorPage;
            conn = new OleDbConnection(connParam);
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
                    string vendorName = txtVendorName.Text;
                    string vendorDescription = txtVendorDescription.Text;
                    string bankID = lblBankID.Text;
                    string vendorAcc = txtVendorAccNo.Text;
                    string createdBy = LoginWindow.LoginInfo.UserID;
                    string createdDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    string query = "";
                    if (string.IsNullOrEmpty(id))
                    {
                        if (!pub.validateName(cmd, "MasterVendor", "Vendor_Name", vendorName))
                        {
                            flag = false;
                            MessageBox.Show("Vendor Name already exist !!", "ERROR");
                            return;
                        }
                        query = "INSERT INTO MasterVendor (Vendor_Name, Bank_ID, Vendor_Account_No, Vendor_Description, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)";
                        query = query + " values ('" + vendorName + "', '" + bankID + "', '" + vendorAcc + "', '" + vendorDescription + "', '" + createdBy + "', #" + createdDate + "#, '" + createdBy + "', #" + createdDate + "#)";
                    }
                    else
                    {
                        query = "UPDATE MasterVendor SET Bank_ID = '" + bankID + "', Vendor_Account_No = '" + vendorAcc + "', Vendor_Description = '" + vendorDescription + "',";
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
                                pub.Audit_Trail(conn, cmd, "Master Vendor", "Create", "Vendor Name : " + txtVendorName.Text);
                            else
                                pub.Audit_Trail(conn, cmd, "Master Vendor", "Update", "Vendor Name : " + txtVendorName.Text);

                            vendorPage.RefreshPage();
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
            if (string.IsNullOrEmpty(txtVendorName.Text))
            {
                MessageBox.Show("Please fill Vendor Name !!", "WARNING");
                return false;
            }
            if (string.IsNullOrEmpty(lblBankID.Text))
            {
                MessageBox.Show("Please select Bank !!", "WARNING");
                return false;
            }
            if (string.IsNullOrEmpty(txtVendorAccNo.Text))
            {
                MessageBox.Show("Please fill Vendor Account No !!", "WARNING");
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLookupBank_Click(object sender, RoutedEventArgs e)
        {
            BankLookupWindow bankLookup = new BankLookupWindow(this);
            bankLookup.ShowDialog();
        }

        private void txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //pub.CheckIsNumeric(e);
        }
    }
}
