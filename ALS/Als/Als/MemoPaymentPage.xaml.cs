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
using System.Configuration;


namespace Als
{
    /// <summary>
    /// Interaction logic for MemoPaymentPage.xaml
    /// </summary>
    public partial class MemoPaymentPage : Page
    {
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();
        private DataTable table;
        private string moduleName = "Report Memo Payment";

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public MemoPaymentPage()
        {
            InitializeComponent();
            conn = new OleDbConnection(connParam);
            cbVendor_Bind();
            cbGLAccount_Bind();
            cbMonthPeriod_Bind();
            Load_Page();
            CheckAccess();
        }

        private void cbVendor_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "Select ID, Vendor_Name, Vendor_Account_No, Vendor_Account_No + ' - ' + Vendor_Name AS Display From MasterVendor Where 1=1 AND Deleted = 0";
                cmd.CommandType = CommandType.Text;

                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        DataRow row = dt.NewRow();
                        row["Display"] = "-Please Select-";
                        row["ID"] = 0;
                        dt.Rows.Add(row);
                        DataView dv = dt.DefaultView;
                        dv.Sort = "Vendor_Name asc";
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

        private void cbGLAccount_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "Select ID, GL_No, GL_Name, CStr(GL_No) + ' - ' + GL_Name AS Display From MasterGLAccount Where 1=1 AND Deleted = 0";
                cmd.CommandType = CommandType.Text;

                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        DataRow row = dt.NewRow();
                        row["Display"] = "-Please Select-";
                        row["ID"] = 0;
                        dt.Rows.Add(row);
                        DataView dv = dt.DefaultView;
                        dv.Sort = "GL_Name asc";
                        cbGL.ItemsSource = dv;
                        cbGL.SelectedValue = 0;
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

        private void cbMonthPeriod_Bind()
        {
            try
            {
                var months = System.Globalization.DateTimeFormatInfo.InvariantInfo.MonthNames;
                
                using (DataTable dt = new DataTable())
                {
                    dt.Columns.Add("ID");
                    dt.Columns.Add("Display");
                    DataRow row = dt.NewRow();
                    row["Display"] = "-Please Select-";
                    row["ID"] = 0;
                    dt.Rows.Add(row);
                    int i = 1;
                    foreach (var month in months)
                    {
                        DataRow rows = dt.NewRow();
                        rows["Display"] = month;
                        rows["ID"] = i;
                        dt.Rows.Add(rows);
                        i = i + 1;
                    }
                    dt.Rows.RemoveAt(13);
                    DataView dv = dt.DefaultView;
                    //dv.Sort = "ID asc";
                    cbPeriodMonth.ItemsSource = dv;
                    cbPeriodMonth.SelectedValue = 0;
                        
                }
                

            }
            catch { }
        }

        private void btnGenerateMemo_Click(object sender, RoutedEventArgs e)
        {
            if (!validateData())
                return;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                //string id = lblID.Text;
                string invoiceNo = txtInvoiceNo.Text;
                string vendor = cbVendor.SelectedValue.ToString();
                string trfAmount = txtTransferredAmount.Text;
                string GL = cbGL.SelectedValue.ToString();
                string description = txtMemoDescription.Text;
                string periodMonth = cbPeriodMonth.SelectedValue.ToString();
                string periodYear = txtPeriodYear.Text;
                string totalPax = txtTotalPax.Text;
                string checkedBy = txtCheckedBy.Text;
                string acknowledged1 = txtAcknowledgedBy1.Text;
                string acknowledged2 = txtAcknowledgedBy2.Text;
                string approved1 = txtApprovedBy1.Text;
                string approved2 = txtApprovedBy2.Text;
                string createdBy = LoginWindow.LoginInfo.UserID;
                string createdDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                string query = "";

                query = "INSERT INTO MemoPayment (Invoice_No, Vendor_ID, Transferred_Amount, GL_Account_ID, Memo_Description, Period_Month, Period_Year, ";
                query = query + "Total_Pax, Checked_By, Acknowledged_By_1, Acknowledged_By_2, Approved_By_1, Approved_By_2, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate)";
                query = query + " values ('" + invoiceNo + "', " + vendor + ", " + trfAmount + ", " + GL + ", '" + description + "', '" + periodMonth + "', " + periodYear + ", ";
                query = query + "" + totalPax + ", '" + checkedBy + "', '" + acknowledged1 + "', '" + acknowledged2 + "', '" + approved1 + "', '" + approved2 + "', ";
                query = query + "'" + createdBy + "', #" + createdDate + "#, '" + createdBy + "', #" + createdDate + "#)";

                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Insert Failed !!", "ERROR");
            }
            finally
            {
                try
                {
                    conn.Close();
                    pub.Audit_Trail(conn, cmd, moduleName, "View", "Invoice No : " + txtInvoiceNo.Text);
                    MemoPaymentReportWindow report = new MemoPaymentReportWindow();
                    report.ShowDialog(); 
                }
                catch { }
            }
        }

        private bool validateData()
        {
            if (string.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                MessageBox.Show("Please fill Invoice No !!", "WARNING");
                return false;
            }
            return true;
        }

        private void txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            pub.CheckIsNumeric(e);
        }

        private void Load_Page()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                string query = string.Empty;
                query = "SELECT ID, Invoice_No,Vendor_ID, Transferred_Amount, ";
                query = query + " GL_Account_ID, Memo_Description, Period_Month, Period_Year,  ";
                query = query + "Total_Pax, Checked_By, Acknowledged_By_1, Acknowledged_By_2, Approved_By_1, Approved_By_2 ";
                query = query + "FROM MemoPayment ";
                query = query + "WHERE ID = (SELECT Max(ID) FROM MemoPayment)";

                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (table = new DataTable())
                    {
                        sda.Fill(table);

                    }
                }

                if (table.Rows.Count > 0)
                {
                    txtInvoiceNo.Text = table.Rows[0]["Invoice_No"].ToString();
                    cbVendor.SelectedValue = table.Rows[0]["Vendor_ID"].ToString();
                    txtTransferredAmount.Text = table.Rows[0]["Transferred_Amount"].ToString();
                    cbGL.SelectedValue = table.Rows[0]["GL_Account_ID"].ToString();
                    txtMemoDescription.Text = table.Rows[0]["Memo_Description"].ToString();
                    cbPeriodMonth.SelectedValue = table.Rows[0]["Period_Month"].ToString();
                    txtPeriodYear.Text = table.Rows[0]["Period_Year"].ToString();
                    txtTotalPax.Text = table.Rows[0]["Total_Pax"].ToString();
                    txtCheckedBy.Text = table.Rows[0]["Checked_By"].ToString();
                    txtAcknowledgedBy1.Text = table.Rows[0]["Acknowledged_By_1"].ToString();
                    txtAcknowledgedBy2.Text = table.Rows[0]["Acknowledged_By_2"].ToString();
                    txtApprovedBy1.Text = table.Rows[0]["Approved_By_1"].ToString();
                    txtApprovedBy2.Text = table.Rows[0]["Approved_By_2"].ToString();
                }
                else
                {
                    txtInvoiceNo.Text = string.Empty;
                    cbVendor.SelectedValue = 0;
                    txtTransferredAmount.Text = string.Empty;
                    cbGL.SelectedValue = 0;
                    txtMemoDescription.Text = string.Empty;
                    cbPeriodMonth.SelectedValue = 0;
                    txtPeriodYear.Text = string.Empty;
                    txtTotalPax.Text = string.Empty;
                    txtCheckedBy.Text = string.Empty;
                    txtAcknowledgedBy1.Text = string.Empty;
                    txtAcknowledgedBy2.Text = string.Empty;
                    txtApprovedBy1.Text = string.Empty;
                    txtApprovedBy2.Text = string.Empty;
                }
            }
            catch (OleDbException ex)
            {
                
            }
            finally
            {
                try
                {
                    conn.Close();
                }
                catch { }
            }
        }

        //Jimmy 26-05-2015
        private void CheckAccess()
        {
            DataRow row = pub.Access(moduleName);

            if (row == null || string.IsNullOrEmpty(row[1].ToString()))
            {
                btnGenerateMemo.Visibility = Visibility.Hidden;
            }
            else
            {
                if (!(bool)row["View_Check"])
                {
                    btnGenerateMemo.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
