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
    /// Interaction logic for MerchantDetailWindow.xaml
    /// </summary>
    public partial class MerchantDetailWindow : Window
    {
        private MerchantPage merchantPage;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public MerchantDetailWindow(MerchantPage merchantPage)
        {
            InitializeComponent();
            this.merchantPage = merchantPage;
            conn = new OleDbConnection(connParam);
            cbVendor_Bind();
        }

        private void cbVendor_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "Select * From MasterVendor Where 1=1 AND Deleted = 0";
                cmd.CommandType = CommandType.Text;

                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        DataRow row = dt.NewRow();
                        row["Vendor_Name"] = "-Please Select-";
                        row["ID"] = 0;
                        dt.Rows.Add(row);
                        DataView dv = dt.DefaultView;
                        dv.Sort = "ID asc";
                        
                        cbVendor.ItemsSource = dv;
                        cbVendor.SelectedValue = 0;
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
                    string id = lblID.Text;
                    string merchantCode = txtMerchantCode.Text;
                    string merchantName = txtMerchantName.Text;
                    string vendorID = cbVendor.SelectedValue.ToString();
                    string merchantDescription = txtMerchantDescription.Text;
                    string createdBy = LoginWindow.LoginInfo.UserID;
                    string createdDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    string query = "";
                    if (string.IsNullOrEmpty(id))
                    {
                        if (!pub.validateName(cmd, "MasterMerchant", "Merchant_Code", merchantCode))
                        {
                            flag = false;
                            MessageBox.Show("Merchant Code already exist !!", "ERROR");
                            return;
                        }
                        query = "INSERT INTO MasterMerchant (Merchant_Code, Merchant_Name, Vendor_ID, Merchant_Description, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)";
                        query = query + " values ('" + merchantCode + "', '" + merchantName + "', '" + vendorID + "', '" + merchantDescription + "', '" + createdBy + "', #" + createdDate + "#, '" + createdBy + "', #" + createdDate + "#)";
                    }
                    else
                    {
                        query = "UPDATE MasterMerchant SET Merchant_Name = '" + merchantName + "', Vendor_ID = '" + vendorID + "', Merchant_Description = '" + merchantDescription + "',";
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
                                pub.Audit_Trail(conn, cmd, "Master Merchant", "Create", "Merchant Code : " + txtMerchantCode.Text);
                            else
                                pub.Audit_Trail(conn, cmd, "Master Merchant", "Update", "Merchant Code : " + txtMerchantCode.Text);

                            merchantPage.RefreshPage();
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
            if (string.IsNullOrEmpty(txtMerchantCode.Text))
            {
                MessageBox.Show("Please fill Merchant Code !!", "WARNING");
                return false;
            }
            if (cbVendor.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please select Vendor !!", "WARNING");
                return false;
            }
            if (string.IsNullOrEmpty(txtMerchantName.Text))
            {
                MessageBox.Show("Please fill Merchant Name !!", "WARNING");
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLookupVendor_Click(object sender, RoutedEventArgs e)
        {
            VendorLookupWindow vendorLookup = new VendorLookupWindow(this);
            vendorLookup.ShowDialog();
        }

        private void txtMerchantCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            pub.CheckIsNumeric(e);
        }
    }
}
