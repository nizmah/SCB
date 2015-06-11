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
    /// Interaction logic for CardDetailWindow.xaml
    /// </summary>
    public partial class CardDetailWindow : Window
    {
        private CardPage cardPage;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public CardDetailWindow(CardPage cardPage)
        {
            InitializeComponent();
            this.cardPage = cardPage;
            conn = new OleDbConnection(connParam);
            cbCardType_Bind();
        }

        private void cbCardType_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "Select * From MasterCardType Where 1=1 AND Deleted = 0";
                cmd.CommandType = CommandType.Text;

                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        DataRow row = dt.NewRow();
                        row["Card_Type_Name"] = "-Please Select-";
                        row["ID"] = 0;
                        dt.Rows.Add(row);
                        DataView dv = dt.DefaultView;
                        dv.Sort = "ID asc";
                        cbCardType.ItemsSource = dv;
                        cbCardType.SelectedValue = 0;
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
                    string binNo = txtBinNo.Text;
                    string cardName = txtCardName.Text;
                    string cardDescription = txtCardDescription.Text;
                    string unique = txtDigitUniqueCard.Text;
                    string cardType = cbCardType.SelectedValue.ToString(); ;
                    string createdBy = LoginWindow.LoginInfo.UserID;
                    string createdDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    string query = "";
                    if (string.IsNullOrEmpty(id))
                    {
                        if (!pub.validateName(cmd, "MasterCardName", "Bin_No", binNo))
                        {
                            flag = false;
                            MessageBox.Show("Card No already exist !!", "ERROR");
                            return;
                        }
                        if (!pub.validateName(cmd, "MasterCardName", "Card_Name", cardName))
                        {
                            flag = false;
                            MessageBox.Show("Card Name already exist !!", "ERROR");
                            return;
                        }
                        query = "INSERT INTO MasterCardName (Bin_No, Card_Name, Card_Description, Digit_Unique_Card, Card_Type_ID, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)";
                        query = query + " values ('" + binNo + "','" + cardName + "', '" + cardDescription + "', '" + unique + "', " + cardType + ", '" + createdBy + "', #" + createdDate + "#, '" + createdBy + "', #" + createdDate + "#)";
                    }
                    else
                    {
                        query = "UPDATE MasterCardName SET Card_Name = '" + cardName + "', Card_Description = '" + cardDescription + "',";
                        query = query + "Digit_Unique_Card = '" + unique + "', Card_Type_ID = " + cardType + ",";
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
                                pub.Audit_Trail(conn, cmd, "Master Card Name", "Create", "Card Name : " + txtCardName.Text);
                            else
                                pub.Audit_Trail(conn, cmd, "Master Card Name", "Update", "Card Name : " + txtCardName.Text);

                            cardPage.RefreshPage();
                            this.Close();
                        }
                    }
                    catch { }
                }
            }
        }

        private bool validateData()
        {
            if (string.IsNullOrEmpty(txtBinNo.Text))
            {
                MessageBox.Show("Please fill BIN No !!", "WARNING");
                return false;
            }
            if (string.IsNullOrEmpty(txtCardName.Text))
            {
                MessageBox.Show("Please fill Card Name !!", "WARNING");
                return false;
            }
            if (string.IsNullOrEmpty(txtDigitUniqueCard.Text))
            {
                MessageBox.Show("Please fill Digit Unique Card !!", "WARNING");
                return false;
            }
            if (cbCardType.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please select Card Type !!", "WARNING");
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