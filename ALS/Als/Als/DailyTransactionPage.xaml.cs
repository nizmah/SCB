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
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Als
{
    /// <summary>
    /// Interaction logic for CardPage.xaml
    /// </summary>
    public partial class DailyTransactionPage : Page
    {
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        OleDbTransaction transaction = null;
        private List<string> ListAls = new List<string>();
        //[DllImport("kernel32")]
        //static extern int AllocConsole();

        //[DllImport("Kernel32")]
        //public static extern void FreeConsole();

        static class TipeProses
        {
            public const int DUI = 0;
            public const int LUI = 1;
            public const int REWARD = 2;
            
        };

        public DailyTransactionPage()
        {
            InitializeComponent();

            using (conn = new OleDbConnection(connParam))
            {
                conn.Open();
                populateLastUploadedInfo(TipeProses.DUI);
                populateLastUploadedInfo(TipeProses.LUI);
                populateLastUploadedInfo(TipeProses.REWARD);
               
            }

        }

        void populateLastUploadedInfo( int iType )
        {

            string query = "";
            string sType = iType.ToString();
            try
            {
               
                cmd.Connection = conn;
                query = "Select top 1 * from daily_transaction_header Where type = " + sType + " order by running_no_header desc";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            if (iType == TipeProses.DUI)
                            {
                                txtLastUploadFileNameDui.Text = dr["file_name"].ToString();
                                txtLastUploadDateDui.Text = dr["uploaded_date"].ToString();
                                txtLastUploadByDui.Text = dr["uploaded_by"].ToString();
                                txtApprovedDateDui.Text = dr["approved_date"].ToString();
                                txtApprovedByDui.Text = dr["approved_by"].ToString();
                            }

                            if (iType == TipeProses.LUI)
                            {
                                txtLastUploadFileNameLui.Text = dr["file_name"].ToString();
                                txtLastUploadDateLui.Text = dr["uploaded_date"].ToString();
                                txtLastUploadByLui.Text = dr["uploaded_by"].ToString();
                                txtApprovedDateLui.Text = dr["approved_date"].ToString();
                                txtApprovedByLui.Text = dr["approved_by"].ToString();
                            }

                            if (iType == TipeProses.REWARD)
                            {
                                txtLastUploadFileNameRwd.Text = dr["file_name"].ToString();
                                txtLastUploadDateRwd.Text = dr["uploaded_date"].ToString();
                                txtLastUploadByRwd.Text = dr["uploaded_by"].ToString();
                                txtApprovedDateRwd.Text = dr["approved_date"].ToString();
                                txtApprovedByRwd.Text = dr["approved_by"].ToString();
                            }
                        }

                    }
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.InnerException.Message); }
            
        }

        private void btnProses_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            string errorMsg = string.Empty;
            string outputMsg = string.Empty;
            
            //bool bresult = false;
            if (!string.IsNullOrEmpty(txtupload_dui.Text)&&!string.IsNullOrEmpty(txtupload_lui.Text))
            {
                using (conn = new OleDbConnection(connParam))
                {
                    try
                    {
                        conn.Open();
                        ListAls = getAL();
                        transaction = conn.BeginTransaction();
                        if (DeleteRunningNoCurrent())
                        {
                            proses(TipeProses.DUI, txtupload_dui.Text, out outputMsg);
                            MessageBox.Show(outputMsg);
                            

                            proses(TipeProses.LUI, txtupload_lui.Text, out outputMsg);
                            MessageBox.Show(outputMsg);

                            transaction.Commit();
                            txtupload_dui.Text = string.Empty;
                            txtupload_lui.Text = string.Empty;
                        }
                        else
                        {
                            MessageBox.Show("Something wrong. Please call administrator.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Upload failed !\n" + ex.Message);
                        transaction.Rollback();
                    }
                }

            }
            else
            {
                MessageBox.Show("Please select file first");
            }
            Mouse.OverrideCursor = null;
            
        }

        private void btnProsesRwd_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            if (!string.IsNullOrEmpty(txtupload_reward.Text))
            {
                string outputMsg = string.Empty;
                string errorMsg = string.Empty;
                prosesreward(txtupload_reward.Text, out outputMsg, out errorMsg);

                if (string.IsNullOrEmpty(errorMsg))
                    MessageBox.Show(outputMsg);
                else
                {
                    MessageBox.Show("Upload Point Reward failed. \n"+errorMsg);
                }
            }
            else
            {
                MessageBox.Show("Please select file first");
            }

            Mouse.OverrideCursor = null;
            
        }

        void proses(int tipe, string sfile, out string msgOutput)
        {
            msgOutput = string.Empty;

            int totalAllLines = 0;
            int totalLinesAffected = 0;
            string namaTipe = tipe == TipeProses.DUI ? "DUI" : (tipe == TipeProses.LUI ? "LUI" : "Reward");

            DateTime startProcess = DateTime.Now;
            string sRunningNo = GetRunningNo(tipe);

            if (!InsertToRunningNoCurrent(sRunningNo))
                throw new Exception("Insert Running No Current failed !");

            if (!InsertToDailyTransactionHeader(sRunningNo, tipe, sfile))
                throw new Exception("Insert Daily Transaction Header failed !");
            

            List<string> allLines = new List<string>();
            using (StreamReader sr = File.OpenText(sfile))
            {
                while (!sr.EndOfStream)
                {
                    allLines.Add(sr.ReadLine());
                }
            }

            totalAllLines = allLines.Count;
            foreach (string x in allLines.Where(t => ListAls.Any(s => t.Contains(s)) && !t.Contains("----")))
            {

                List<string> ln = x.Trim().Split(new char[0]).Where(t => !string.IsNullOrEmpty(t)).ToList();
                inserttodailytransactiontable(sRunningNo, ln[1], ln[4], ln[5], ln[6], ln[7].Replace("-", "").Replace(",", "").Replace(".",""));
               
                totalLinesAffected++;
            }

            DateTime endProcess = DateTime.Now;

            StringBuilder sbOutput = new StringBuilder();
            sbOutput.AppendFormat("Data {0} success uploading. Start Process : {1} and End Process {2}", namaTipe, startProcess, endProcess);
            sbOutput.AppendLine();
            sbOutput.AppendFormat("Process time : {0}", (endProcess - startProcess).Seconds);
            sbOutput.AppendLine();
            sbOutput.AppendFormat("Total lines : {0}", totalAllLines);
            sbOutput.AppendLine();
            sbOutput.AppendFormat("Total affected : {0}", totalLinesAffected);
            sbOutput.AppendLine();

            msgOutput = sbOutput.ToString();
        }

        string GetRunningNo(int iType)
        {

            string sRunningno = "";
            if (iType == TipeProses.DUI)
            {
                sRunningno = "DUI" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
            }
            if (iType == TipeProses.LUI)
            {
                sRunningno = "LUI" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
            }
            if (iType == TipeProses.REWARD)
            {
                sRunningno = "RWD" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
            }

            return sRunningno;
        }

        bool InsertToDailyTransactionHeader(string sRunningNo, int iType, string sFileName)
        {
            string query = "";
            bool success = false;
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                query = "INSERT INTO daily_transaction_header (running_no_header, file_name, Type, uploaded_date, uploaded_by, approved)";
                query = query + " values ('" + sRunningNo + "', '" + sFileName + "', '" + iType.ToString() + "', '" + DateTime.Now.ToString() + "', '" + LoginWindow.LoginInfo.UserID + "', 0 )";

                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                success = true;
            }
            catch
            { }

            return success;
        }

        bool InsertToRunningNoCurrent(string sRunningNo)
        {
            bool success = false;
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.CommandText = string.Format(@"INSERT INTO RunningNoCurrent (running_no_current) values('{0}')", sRunningNo);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                success = true;
            }
            catch { }
            return success;
        }

        bool DeleteRunningNoCurrent()
        {
            string query = "";
            bool valid = false;
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                query = "Delete from RunningNoCurrent ";

                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                valid = true;
            }
            catch { }
            return valid;
        }

        bool prosestruncatereward()
        {
            bool success = false;
            try
            {
                Console.WriteLine("Try truncating data reward");
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.CommandText = "DELETE FROM pointreward";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();


                cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.CommandText = "ALTER TABLE pointreward  ALTER COLUMN running_no COUNTER (1, 1)";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                success = true;

            }
            catch { }
            
            return success;

        }

        void prosesreward(string sfile, out string outputMessage, out string errorMsg)
        {
            outputMessage = string.Empty;
            errorMsg = string.Empty;
            //string str = string.Empty;
            int totalAllLines = 0;
            int totalAffected = 0;
            DateTime startProcess = DateTime.Now;
            string sRunningNo = GetRunningNo(TipeProses.REWARD); //2 = point reward
            using (conn = new OleDbConnection(connParam))
            {
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                    if (prosestruncatereward())
                    {

                        InsertToDailyTransactionHeader(sRunningNo, TipeProses.REWARD, sfile);
                        List<string> allLine = new List<string>();
                        using (StreamReader sr = File.OpenText(sfile))
                        {
                            while (!sr.EndOfStream)
                            {
                                allLine.Add(sr.ReadLine().Trim());

                            }
                        }
                        totalAllLines = allLine.Count;

                        foreach (string x in allLine.Where(t => t.Length == 130 && !t.Contains(":") && t.Contains("190")))
                        {
                            List<string> ln = x.Split(new char[0]).Where(t => !string.IsNullOrEmpty(t)).ToList();
                            inserttablepointreward(sRunningNo, ln[0], ln[5].Replace("-", "").Replace(",", "").Replace(".",""));
                            totalAffected++;
                        }
                        DateTime endProcess = DateTime.Now;
                        transaction.Commit();

                        StringBuilder sbOutput = new StringBuilder();
                        sbOutput.AppendFormat("Data Point Reward success uploading. Start Process : {0} and End Process {1}.", startProcess, endProcess);
                        sbOutput.AppendLine();
                        sbOutput.AppendFormat("Process time : {0} s", (endProcess - startProcess).Seconds);
                        sbOutput.AppendLine();
                        sbOutput.AppendFormat("Total lines : {0}", totalAllLines);
                        sbOutput.AppendLine();
                        sbOutput.AppendFormat("Total affected : {0}", totalAffected);
                        sbOutput.AppendLine();

                        outputMessage = sbOutput.ToString();
                    }
                    else
                    {
                        throw new Exception("Process truncate point reward failed !");
                    }

                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                    transaction.Rollback();
                }
            }
            

            txtupload_reward.Text = string.Empty;
        }

        void inserttablepointreward(string runningnoheader, string cardno, string currentbalanceamount)
        {
            
            string query = string.Empty;
            if (conn.State == ConnectionState.Closed) conn.Open();
            query = @"INSERT INTO pointreward(running_no_header, Card_no, current_balance_amount) VALUES('" + runningnoheader + "', '" + cardno + "', " + currentbalanceamount + ")";
            cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.Transaction = transaction;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

        }

        List<string> getAL()
        {
            
            List<string> list = new List<string>();
            if (conn.State == ConnectionState.Closed) conn.Open();

            using (OleDbDataAdapter sda = new OleDbDataAdapter("Select Merchant_Code from MasterMerchant Where Deleted = false ",conn))
            {
                
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(Convert.ToString(dr[0]));
                }
                   
            }
            return list;
        }

        void inserttodailytransactiontable(string sRunningNo, string smerchantid, string scardno, string sdate
                                            , string stc, string samount)
        {
            string query = "";
            if (conn.State == ConnectionState.Closed) conn.Open();
            cmd = new OleDbCommand();
            cmd.Connection = conn;

            query = "INSERT INTO daily_transaction_detail (running_no_header, merchant_id, card_no, transaction_date, tc, transaction_amount)";
            query = query + " values ('" + sRunningNo + "','" + smerchantid + "', '" + scardno + "', '" + sdate + "', '" + stc + "', " + Convert.ToInt32(samount) + " )";
            cmd.Transaction = transaction;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }

        void inserttodailytransactionrewardtable(string scardno, string samount)
        {
            string query = "";

            bool flag = true;


            try
            {
                if(conn.State == ConnectionState.Closed) conn.Open();
                cmd.Connection = conn;


                query = "INSERT INTO PointReward (Card_no, current_balance_amount)";
                query = query + " values ('" + scardno + "',  " + Convert.ToInt32(samount) + " )";

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


        private void btnUpload_dui_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();


            dlg.DefaultExt = ".txt";
            dlg.Filter = "File Text (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                txtupload_dui.Text = filename;
            }
        }

        private void btnUpload_lui_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();


            dlg.DefaultExt = ".txt";
            dlg.Filter = "File Text (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                txtupload_lui.Text = filename;
            }
        }

        private void btnUpload_reward_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();


                dlg.DefaultExt = ".txt";
                dlg.Filter = "File Text (.txt)|*.txt";

               
                if (dlg.ShowDialog() == true)
                {
                    string filename = dlg.FileName;
                    txtupload_reward.Text = filename;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void GeneratedDraftOutput_Click(object sender, RoutedEventArgs e)
        {
            GeneratedDraftOutputWindow gdopage = new GeneratedDraftOutputWindow();
            gdopage.ShowDialog();   
        }

        private void GeneratedApprovedOutput_Click(object sender, RoutedEventArgs e)
        {
            GeneratedApprovedOutputWindow gaopage = new GeneratedApprovedOutputWindow();
            gaopage.ShowDialog();
        }
    }
}
