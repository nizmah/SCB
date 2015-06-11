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
    /// Interaction logic for MerchantPriceListDetailWindow.xaml
    /// </summary>
    public partial class MerchantPriceListDetailWindow : Window
    {
        private MerchantPriceListPage merchantPriceListPage;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public MerchantPriceListDetailWindow(MerchantPriceListPage merchantPriceListPage)
        {
            InitializeComponent();
            this.merchantPriceListPage = merchantPriceListPage;
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
                    string merchantID = lblMerchantID.Text;
                    string cardTypeID = lblCardTypeID.Text;
                    string totalGuest = txtTotalGuest.Text;
                    string priceAmount = txtPriceAmount.Text;
                    string pointAmount = txtPointAmount.Text;
                    string createdBy = LoginWindow.LoginInfo.UserID;
                    string createdDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    string query = "";
                    if (string.IsNullOrEmpty(id))
                    {
                        string[] category = new string[3];
                        string[] value = new string[3];
                        category[0] = "Merchant_ID"; category[1] = "Card_Type_ID"; category[2] = "Guest_Amount";
                        value[0] = merchantID; value[1] = cardTypeID; value[2] = "'" + totalGuest + "'";

                        if (!pub.validateNameCategories(cmd, "MasterMerchantPrice", category, value))
                        {
                            flag = false;
                            MessageBox.Show("Total Guest already exist !!", "ERROR");
                            return;
                        }
                        query = "INSERT INTO MasterMerchantPrice (Merchant_ID, Card_Type_ID, Guest_Amount, Price_Amount, Point_Amount, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)";
                        query = query + " values (" + merchantID + ", " + cardTypeID + ", " + totalGuest + ", " + priceAmount + ", " + pointAmount + ", '" + createdBy + "', #" + createdDate + "#, '" + createdBy + "', #" + createdDate + "#)";
                    }
                    else
                    {
                        query = "UPDATE MasterMerchantPrice SET Guest_Amount = " + totalGuest + ", Price_Amount = " + priceAmount + ", Point_Amount = " + pointAmount + ",";
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
                            string[] mCode = lblMerchantNameValue.Text.Split('-');
                            if (string.IsNullOrEmpty(lblID.Text))
                                pub.Audit_Trail(conn, cmd, "Master Merchant Price List", "Create", "Merchant Code : " + mCode[0]);
                            else
                                pub.Audit_Trail(conn, cmd, "Master Merchant Price List", "Update", "Merchant Code : " + mCode[0]);

                            merchantPriceListPage.RefreshPage();
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
            if (string.IsNullOrEmpty(txtTotalGuest.Text))
            {
                MessageBox.Show("Please fill Total Guest !!", "WARNING");
                return false;
            }
            if (string.IsNullOrEmpty(txtPriceAmount.Text))
            {
                MessageBox.Show("Please fill Price Amount !!", "WARNING");
                return false;
            }
            if (string.IsNullOrEmpty(txtPointAmount.Text))
            {
                MessageBox.Show("Please fill Point Amount !!", "WARNING");
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
