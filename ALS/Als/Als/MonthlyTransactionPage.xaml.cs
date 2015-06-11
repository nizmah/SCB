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
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;

namespace Als
{
    /// <summary>
    /// Interaction logic for CardPage.xaml
    /// </summary>
    public partial class MonthlyTransactionPage : Page
    {
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        PublicClass pub = new PublicClass();
        private string moduleName = "Transaction Monthly";

        private enum tipe
        {
            dui = 0,
            lui = 1,
            reward = 2
        };

        public MonthlyTransactionPage()
        {
            InitializeComponent();

            conn = new OleDbConnection(connParam);
            cbMerchant_Bind();
            CheckAccess();
        }

        
        private void btnProses_Click(object sender, RoutedEventArgs e)
        {
            if (GetTotalLine() <= 1)
            {
                MessageBox.Show("There is something wrong with the file.");
                return;
            }

            string sRunningno = "";
            sRunningno = "MR" + DateTime.Now.ToString("yyyyMMdd-HHmmss");

            Excel.Application xlapp = new Excel.Application();
            Excel.Workbook xlworkbook = null;
            Excel.Worksheet xlworksheet = null;
            Excel.Range range = null;
            string scard_no = "";
            string stransaction_date = "";
            string stransaction_amount = "";
            string scard_holder_name = "";
            string sPax = "";
            DateTime dtTestContent;
            string cekAngka = "";
            xlworkbook = xlapp.Workbooks.Open(txtupload.Text);
            try
            {
                InsertMonthlyTransactionHeader(sRunningno, txtupload.Text, cbMerchant.Text);

                xlworksheet = (Excel.Worksheet)xlworkbook.Worksheets.get_Item(1);
                range = xlworksheet.UsedRange;
                for (int ccnt = 1; ccnt <= range.Rows.Count; ccnt++)
                {
                    try
                    {
                        //cekAngka = ((range.Cells[ccnt, 1] as Excel.Range).Value).ToString();
                        //if (cekAngka)
                        stransaction_date = ((range.Cells[ccnt, 2] as Excel.Range).Value).ToString();
                                                
                        dtTestContent = Convert.ToDateTime(stransaction_date);

                        scard_holder_name = ((range.Cells[ccnt, 3] as Excel.Range).Value).ToString();
                        scard_no = ((range.Cells[ccnt, 4] as Excel.Range).Value).ToString().Replace(" ","");
                        sPax = ((range.Cells[ccnt, 7] as Excel.Range).Value).ToString();
                        stransaction_amount = ((range.Cells[ccnt, 8] as Excel.Range).Value).ToString();
                        insertMonthlyTransactionDetail(sRunningno, stransaction_date, scard_no, scard_holder_name, sPax, stransaction_amount);
                    }
                    catch (Exception ex2)
                    { }
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                xlworkbook.Close();
                pub.Audit_Trail(conn, cmd, moduleName, "Upload", "File Name : " + txtupload.Text);
            }

            xlapp.Quit();

            MessageBox.Show("Uploaded is success.");
        }

        int GetTotalLine()
        {
            int iTotalBaris = 0;            
            DateTime dtTransDate;
            Excel.Application xlapp = new Excel.Application();
            Excel.Workbook xlworkbook = null;
            Excel.Worksheet xlworksheet = null;
            Excel.Range range = null;
            xlworkbook = xlapp.Workbooks.Open(txtupload.Text);
            try
            {
                xlworksheet = (Excel.Worksheet)xlworkbook.Worksheets.get_Item(1);
                range = xlworksheet.UsedRange;
                for (int ccnt = 1; ccnt <= range.Rows.Count; ccnt++)
                {
                    try
                    {
                        if ((range.Cells[ccnt, 2] as Excel.Range).Value.ToString() != "")
                        {
                            dtTransDate = Convert.ToDateTime(((range.Cells[ccnt, 2] as Excel.Range).Value));

                            iTotalBaris++;
                        }
                    }
                    catch (Exception ex2)
                    {

                    }
                }

            }
            catch (Exception ex)
            { }
            finally
            {
                xlworkbook.Close();
            }

            xlapp.Quit();

            return iTotalBaris;

        }
        
        /*running_no_header
        file_name
        merchant_id
        uploaded_date
        uploaded_by*/

        void InsertMonthlyTransactionHeader(string sRunningNo, string sfile_name,
            string smerchant_id)
        {
            string query = "";

            bool flag = true;

            try
            {
                conn.Open();
                cmd.Connection = conn;

                query = "INSERT INTO Monthly_transaction_header (running_no_header, file_name, merchant_id, ";
                query += " uploaded_date, uploaded_by ) VALUES ";
                query += "('" + sRunningNo +"', '" + sfile_name +"', '" + smerchant_id +"', ";
                query += "'" + DateTime.Now.ToString("MM/dd/yyyy") +"', '" + LoginWindow.LoginInfo.UserID +"' )";

                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
            catch (OleDbException ex)
            {
                flag = false;
                if (ex.ErrorCode == -2147467259)
                    MessageBox.Show("Bank Name already exist !!", "ERROR");
                else
                    MessageBox.Show("Insert Failed !!", "ERROR");
            }
            finally
            {
                try
                {
                    conn.Close();
                    if (flag)
                    {
                        //dailytransaction.RefreshPage();
                        //this.Close();
                    }
                }
                catch { }
            }
        }

        void insertMonthlyTransactionDetail(string sRunningNo, string sTransaction_Date,
            string sCard_No, string sCard_Holder_Name, string sPax, string sTransaction_amount)
        {
            string query = "";

            bool flag = true;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                query = "INSERT INTO Monthly_transaction_Detail (running_no_header, Transaction_Date, Card_No, ";
                query += " card_holder_name, Pax, transaction_amount)";
                query += " values ('" + sRunningNo + "', '" + sTransaction_Date + "', '" + sCard_No + "', ";
                query += " '" + sCard_Holder_Name + "', '" + sPax + "', '" + sTransaction_amount + "')";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                flag = false;
                if (ex.ErrorCode == -2147467259)
                    MessageBox.Show("Bank Name already exist !!", "ERROR");
                else
                    MessageBox.Show("Insert Failed !!", "ERROR");
            }
            finally
            {
                try
                {
                    conn.Close();
                    if (flag)
                    {
                        //dailytransaction.RefreshPage();
                        //this.Close();
                    }
                }
                catch { }
            }
        }



        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();


            dlg.DefaultExt = ".xls|.xlsx";
            dlg.Filter = "Excel File (.xls)|*.xls|Excel File 2007 (.xlsx)|*.xlsx";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                txtupload.Text = filename;
            }

        }


        private void cbMerchant_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "Select * From MasterMerchant Where 1=1 AND Deleted = 0";
                cmd.CommandType = CommandType.Text;

                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        DataRow row = dt.NewRow();
                        row["Merchant_Name"] = "-Please Select-";
                        row["ID"] = 0;
                        dt.Rows.Add(row);
                        DataView dv = dt.DefaultView;
                        dv.Sort = "ID asc";
                        cbMerchant.ItemsSource = dv;
                        cbMerchant.SelectedValue = 0;
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

        //Jimmy 26-05-2015
        private void CheckAccess()
        {
            DataRow row = pub.Access(moduleName);

            if (row == null || string.IsNullOrEmpty(row[1].ToString()))
            {
                btnProses.Visibility = Visibility.Hidden;
            }
            else
            {
                if (!(bool)row["Upload_Check"])
                {
                    btnProses.Visibility = Visibility.Hidden;
                }
            }
        }
    }


}
