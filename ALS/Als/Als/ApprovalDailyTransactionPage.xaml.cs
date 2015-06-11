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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Configuration;

namespace Als
{
    /// <summary>
    /// Interaction logic for MerchantPriceListPage.xaml
    /// </summary>
    public partial class ApprovalDailyTransactionPage : Page
    {
        //private MerchantPage merchantPage;
        //private MainWindow main;
        //private string criteria, query;
        //private string sRunningNoHeaderDui, sRunningNoHeaderLui, sRunningNoHeaderRwd;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string sUploadedDate;
        
        public ApprovalDailyTransactionPage()
        {
            InitializeComponent();
            conn = new OleDbConnection(connParam);
            populateLastUploadedInfo();
            
        }

        void populateLastUploadedInfo()
        {

            string query = "";
            try
            {
                conn.Open();
                cmd.Connection = conn;
                query = "Select top 1 * from daily_transaction_header Where type = 0 order by running_no_header desc";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);

                        DataRow dr = dt.Rows[0];
                        
                        txtLastUploadDate.Text = Convert.ToDateTime(dr["uploaded_date"]).ToString("dd-MM-yyyy HH:mm:ss");
                        txtLastUploadBy.Text = dr["uploaded_by"].ToString();
                        txtLastTransactionDate.Text = getLastTransactionDate(dr["running_no_header"].ToString());
                        sUploadedDate = Convert.ToDateTime(dr["uploaded_date"]).ToString("MM/dd/yyyy");
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


        string getLastTransactionDate(string sRunningNo)
        {
            string sLastTrxDate = "";
            string query = "";
            try
            {
                conn.Open();
                cmd.Connection = conn;
                query = "Select top 1 transaction_date from daily_transaction_detail ";
                query += " Where approved = 0 and";
                query += " running_no_header = '" + sRunningNo + "' order by running_no_detail desc";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);

                        DataRow dr = dt.Rows[0];

                        sLastTrxDate = Convert.ToDateTime(dr["transaction_date"]).ToString("dd-MM-yyyy HH:mm");
                    }
                }

            }
            catch { }
            finally
            {
                try { conn.Close(); }
                catch { }
            }

            return sLastTrxDate;
        }

        bool prosesDeleteReward()
        {

            try
            {
                conn.Open();
                cmd.Connection = conn;

                string query = "DELETE FROM pointreward";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                query = "ALTER TABLE pointreward  ALTER COLUMN running_no COUNTER (1, 1)";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                try
                {
                    conn.Close();

                }
                catch { }
            }
            return true;

        }

        bool prosesDeleteDTH(string sRunningNo)
        {

            try
            {
                conn.Open();
                cmd.Connection = conn;

                string query = "DELETE FROM daily_transaction_header where running_no_header = '" + sRunningNo + "'";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                try
                {
                    conn.Close();

                }
                catch { }
            }
            return true;

        }

        bool prosesDeleteDTL(string sRunningNo)
        {

            try
            {
                conn.Open();
                cmd.Connection = conn;

                string query = "DELETE FROM daily_transaction_Detail where running_no_header = '" + sRunningNo + "'";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                try
                {
                    conn.Close();

                }
                catch { }
            }
            return true;

        }
        private void btnReject_Click(object sender, RoutedEventArgs e)
        {

            string sRunningNoDui = getRunningNo(sUploadedDate, "0");
            if (prosesDeleteDTL(sRunningNoDui))
            {
                if (prosesDeleteDTH(sRunningNoDui))
                {
                    MessageBox.Show("Delete data Dui success.");
                }
            }

            string sRunningNoLui = getRunningNo(sUploadedDate, "1");
            if (prosesDeleteDTL(sRunningNoLui))
            {
                if (prosesDeleteDTH(sRunningNoLui))
                {
                    MessageBox.Show("Delete data Lui success.");
                }
            }

            string sRunningNoRwd = getRunningNo(sUploadedDate, "2");
            if (prosesDeleteReward())
            {
                if (prosesDeleteDTH(sRunningNoRwd))
                {
                    MessageBox.Show("Delete data Reward success.");
                }
            }

        }


        string getRunningNo(string sDate, string stipe)
        {
            string query = "";
            string srunningno = "";
            try
            {
                conn.Open();
                cmd.Connection = conn;
                query = string.Format(@"Select top 1 running_no_header from daily_transaction_header 
                                        Where approved = 0 and type = {0} and uploaded_date >= #{1}# 
                                        order by uploaded_date", stipe, sDate);
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        DataRow dr = dt.Rows[0];
                        srunningno = dr["running_no_header"].ToString();
                    }
                }

            }
            catch { }
            finally
            {
                try { conn.Close(); }
                catch { }
            }

            return srunningno;
        }

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            string sRunningNoDui = getRunningNo(sUploadedDate, "0");
            if (!prosesUpdateDTH(sRunningNoDui))
            {
                MessageBox.Show("Update Dui failed.");
                return;
            }

            string sRunningNoLui = getRunningNo(sUploadedDate, "1");
            if (!prosesUpdateDTH(sRunningNoLui))
            {
                MessageBox.Show("Update LUI failed.");
                return;
            }

            string sRunningNoRwd = getRunningNo(sUploadedDate, "2");
            if (!prosesUpdateDTH(sRunningNoRwd))
            {
                MessageBox.Show("Update Reward point failed.");
                return;
            }

            MessageBox.Show("Approved data success");

        }

        bool prosesUpdateDTH(string sRunningNo)
        {

            try
            {
                conn.Open();
                cmd.Connection = conn;

                string query = "UPDATE daily_transaction_header SET approved = 1, approved_date = #" + DateTime.Now.ToString("MM/dd/yyyy") + "#, ";
                query += "approved_by = '" + LoginWindow.LoginInfo.UserID + "' where running_no_header = '" + sRunningNo + "'";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                try
                {
                    conn.Close();

                }
                catch { }
            }
            return true;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GeneratedDraftOutputWindow gdopage = new GeneratedDraftOutputWindow();
            gdopage.ShowDialog();   
        }

    }
}
