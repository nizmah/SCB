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
    /// Interaction logic for CardTypeDetailWindow.xaml
    /// </summary>
    public partial class CardTypeDetailWindow : Window
    {
        private CardTypePage cardTypePage;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public CardTypeDetailWindow(CardTypePage cardTypePage)
        {
            InitializeComponent();
            this.cardTypePage = cardTypePage;
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
                    string cardTypeName = txtCardTypeName.Text;
                    string cardTypeDescription = txtCardTypeDescription.Text;
                    string createdBy = LoginWindow.LoginInfo.UserID;
                    string createdDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    string query = "";
                    if (string.IsNullOrEmpty(id))
                    {
                        if (!pub.validateName(cmd, "MasterCardType", "Card_Type_Name", cardTypeName))
                        {
                            flag = false;
                            MessageBox.Show("Card Type Name already exist !!", "ERROR");
                            return;
                        }
                        query = "INSERT INTO MasterCardType (Card_Type_Name, Card_Type_Description, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)";
                        query = query + " values ('" + cardTypeName + "', '" + cardTypeDescription + "', '" + createdBy + "', #" + createdDate + "#, '" + createdBy + "', #" + createdDate + "#)";
                    }
                    else
                    {
                        query = "UPDATE MasterCardType SET Card_Type_Description = '" + cardTypeDescription + "',";
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
                                pub.Audit_Trail(conn, cmd, "Master Card Type", "Create", "Card Type Name : " + txtCardTypeName.Text);
                            else
                                pub.Audit_Trail(conn, cmd, "Master Card Type", "Update", "Card Type Name : " + txtCardTypeName.Text);

                            cardTypePage.RefreshPage();
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
            if (string.IsNullOrEmpty(txtCardTypeName.Text))
            {
                MessageBox.Show("Please fill Card Type Name !!", "WARNING");
                return false;
            }
            return true;
        }
    }
}
