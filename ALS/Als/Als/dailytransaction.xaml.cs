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
namespace Als
{
    /// <summary>
    /// Interaction logic for dailytransaction.xaml
    /// </summary>
    public partial class dailytransaction : Page
    {

        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public dailytransaction()
        {
            InitializeComponent();

            conn = new OleDbConnection(connParam);

        }

        private void btnProses_Click(object sender, RoutedEventArgs e)
        {

            bool bresult = false;
            if (txtupload_dui.Text != "")
            {
                bresult = proses(txtupload_dui.Text);
                if (bresult)
                    MessageBox.Show("Upload dui success");
                else
                {
                    MessageBox.Show("Upload dui failed");
                    return;
                }
            }

            bresult = false;
            if (txtupload_lui.Text != "")
            {
                bresult = proses(txtupload_lui.Text);
                if (bresult)
                    MessageBox.Show("Upload lui success");
                else
                {
                    MessageBox.Show("Upload lui failed");
                    return;
                }
            }

            //prosesreward
            bresult = false;
            string sawal = DateTime.Now.ToLongTimeString();
            if (txtupload_reward.Text != "")
            {
                bresult = prosesreward(txtupload_reward.Text);
                if (bresult)
                    MessageBox.Show("Upload reward success. Start time : " + sawal + ". End time : " + DateTime.Now.ToLongTimeString()) ;
                else
                {
                    MessageBox.Show("Upload reward failed");
                    return;
                }
            }

        }

        bool proses(string sfile)
        {
            bool isok = false;
            ArrayList ls = new ArrayList();
            ls = getAL();
            bool ismerchantcode = false;

            FileStream fs = new FileStream(sfile, FileMode.Open);

            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string str = "";
            string strmerchantid = "";
            string strcardno = "";
            string strdate = "";
            string strtc = "";
            string stramount = "";
            while ((str = sr.ReadLine()) != null)
            {
                if (str.Trim() != "")
                {
                    strmerchantid = str.Substring(10, 12);
                    ismerchantcode = ls.Contains(strmerchantid);
                    if (ismerchantcode)
                    {
                        strcardno = str.Substring(60, 16);
                        strdate = str.Substring(86, 2) + "/" + str.Substring(83, 2) + "/" + str.Substring(80, 2);
                        strtc = str.Substring(91, 2);
                        stramount = str.Substring(93, 16).Replace(".", "").Replace(",", "");
                        inserttodailytransactiontable(strmerchantid, strcardno, strdate, strtc, stramount);

                        isok = true;
                    }
                }
            }

            return isok;
        }

        bool prosestruncatereward()
        {

            //            Dim strSql As String
            //strSql = "DELETE FROM tblFoo;"
            //CurrentProject.Connection.Execute strSql
            //strSql = "ALTER TABLE tblFoo ALTER COLUMN id COUNTER (1, 1);"
            //CurrentProject.Connection.Execute strSql
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
        bool prosesreward(string sfile)
        {
            bool isok = false;


            if (!prosestruncatereward())
            {
                return false;
            }

            int j = 0;
            string strx = "";
            string strcustno190x = "";
            
            FileStream fs2 = new FileStream(sfile, FileMode.Open);
            using (StreamReader r = new StreamReader(fs2,Encoding.Default))
            {
                while ((strx = r.ReadLine()) != null) {

                    if (strx.Trim() != "")
                    {
                        if (strx.Length > 40)
                        {
                            strcustno190x = strx.Substring(22, 3);
                            if (strcustno190x == "190")
                            {
                                j++;
                            }
                        }
                    }
                }                
            }

            FileStream fs = new FileStream(sfile, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);

            string str = "";
            string strcardno = "";
            string strcustno190 = "";
            string stramount = "";

            DataTable table = new DataTable();

            table.Columns.Add(new DataColumn("Card_no"));
            table.Columns.Add(new DataColumn("current_balance_amount"));
            int i = 0;
            
            while ((str = sr.ReadLine()) != null)
            {
                if (str.Trim() != "")
                {
                    if (str.Length > 40)
                    {

                        strcustno190 = str.Substring(22, 3);
                        if (strcustno190 == "190")
                        {
                            i++;
                            strcardno = str.Substring(1, 16);
                            stramount = str.Substring(85, 13).Replace(".", "").Replace(",", "");

                            //tutup sementara
                            //inserttodailytransactionrewardtable(strcardno, stramount);

                            table.Rows.Add(new object[] { strcardno, stramount });
                         
                            if (i % 10000 == 0 || i == j)
                            {
                                inserttablepointreward(table);
                                table.Clear();
                            }
                            
                            isok = true;
                        }
                    }
                }
            }
            table.Dispose();

            //PointReward (Card_no, current_balance_amount
            
            return isok;

        }

        bool inserttablepointreward( DataTable table )
        {
            try
            {
                using (OleDbDataAdapter sda = new OleDbDataAdapter())
                {
                    string INSERT = " INSERT INTO pointreward(Card_no, current_balance_amount) VALUES(@X1, @X2) ";
                    sda.InsertCommand = new OleDbCommand(INSERT);
                    sda.InsertCommand.Parameters.Add("@X1", OleDbType.VarChar, 255, "Card_no");
                    sda.InsertCommand.Parameters.Add("@X2", OleDbType.Numeric, 18, "current_balance_amount");
                    sda.InsertCommand.Connection = conn;
                    sda.InsertCommand.Connection.Open();
                    sda.Update(table);
                    sda.InsertCommand.Connection.Close();

                }
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }
        ArrayList getAL()
        {
            string query = "";
            ArrayList list = new ArrayList();
                            
            try
            {
                conn.Open();
                cmd.Connection = conn;
                query = "Select Merchant_Code from MasterMerchant Where Deleted = 0 ";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);

                        foreach (DataRow dr in dt.Rows)
                        {
                            list.Add(dr[0].ToString());

                        }
                    }
                }

            }
            catch { }
            finally
            {
                try { conn.Close(); }
                catch { }
            }

            return list;
        }

        void inserttodailytransactiontable(string smerchantid,string scardno, string sdate
                                            ,string stc, string samount)
        {
            string query = "";

            bool flag = true;

           
            try
            {
                conn.Open();
                cmd.Connection = conn;
                
                query = "INSERT INTO daily_transaction_detail (merchant_id, card_no, transaction_date, tc, transaction_amount)";
                query = query + " values ('" + smerchantid + "', '" + scardno + "', '" + sdate + "', '" + stc + "', " + samount + " )";
                
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

        void inserttodailytransactionrewardtable( string scardno, string samount)
        {
            string query = "";

            bool flag = true;


            try
            {
                conn.Open();
                cmd.Connection = conn;


                query = "INSERT INTO PointReward (Card_no, current_balance_amount)";
                query = query + " values ('" + scardno + "',  " + samount + " )";

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
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();


            dlg.DefaultExt = ".txt";
            dlg.Filter = "File Text (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                txtupload_reward.Text = filename;
            }
        }
    }
}
