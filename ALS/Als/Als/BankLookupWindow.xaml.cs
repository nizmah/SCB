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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Configuration;

namespace Als
{
    /// <summary>
    /// Interaction logic for BankLookupWindow.xaml
    /// </summary>
    public partial class BankLookupWindow : Window
    {
        private VendorDetailWindow vendorDetail;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, query;

        public BankLookupWindow(VendorDetailWindow vendorDetail)
        {
            InitializeComponent();
            this.vendorDetail = vendorDetail;
            conn = new OleDbConnection(connParam);
            dgBank_Bind();
        }

        private void dgBank_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                query = "Select * ";
                query = query + "From MasterBank Where 1=1 AND Deleted = 0 " + criteria;
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                dgBank.DataContext = pub.BindDG(cmd);

            }
            catch { }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                criteria = string.Empty;
                criteria = " AND Bank_Name LIKE '%" + txtBankName.Text + "%'";
                if (!string.IsNullOrEmpty(txtBankBranch.Text))
                    criteria = criteria + " AND Bank_Branch LIKE '%" + txtBankBranch.Text + "%' ";
                if (!string.IsNullOrEmpty(txtCityofBranch.Text))
                    criteria = criteria + " AND Bank_City LIKE '%" + txtCityofBranch.Text + "%' ";

                dgBank_Bind();
            }
            catch { }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtBankName.Text = string.Empty;
            txtBankBranch.Text = string.Empty;
            txtCityofBranch.Text = string.Empty;
            criteria = string.Empty;
            dgBank_Bind();
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            //fill data
            vendorDetail.lblBankID.Text = tab["ID"].ToString();
            if (tab["Bank_Name"] != null)
            {
                vendorDetail.txtBank.Text = tab["Bank_Name"].ToString();
                if (tab["Bank_Branch"] != null)
                    vendorDetail.txtBank.Text = vendorDetail.txtBank.Text + " - " + tab["Bank_Branch"].ToString();

            }
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
