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
    /// Interaction logic for VendorLookupWindow.xaml
    /// </summary>
    public partial class VendorLookupWindow : Window
    {
        private MerchantDetailWindow merchantDetail;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, query;

        public VendorLookupWindow(MerchantDetailWindow merchantDetail)
        {
            InitializeComponent();
            this.merchantDetail = merchantDetail;
            conn = new OleDbConnection(connParam);
            dgVendor_Bind();
        }

        private void dgVendor_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                query = "Select mv.*, mb.Bank_Name, mb.Bank_Branch ";
                query = query + "From MasterVendor mv LEFT JOIN (SELECT * FROM MasterBank WHERE Deleted = 0) mb ON mv.Bank_ID = mb.ID Where 1=1 AND mv.Deleted = 0 " + criteria;
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                dgVendor.DataContext = pub.BindDG(cmd);

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
                criteria = " AND Vendor_Name LIKE '%" + txtVendorName.Text + "%'";
                if (!string.IsNullOrEmpty(txtBankBranch.Text))
                    criteria = criteria + " AND Bank_Branch LIKE '%" + txtBankBranch.Text + "%' ";
                if (!string.IsNullOrEmpty(txtBankName.Text))
                    criteria = criteria + " AND Bank_Name LIKE '%" + txtBankName.Text + "%' ";
                if (!string.IsNullOrEmpty(txtVendorAccNo.Text))
                    criteria = criteria + " AND Vendor_Account_No LIKE '%" + txtVendorAccNo.Text + "%' ";

                dgVendor_Bind();
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
            txtVendorName.Text = string.Empty;
            txtBankBranch.Text = string.Empty;
            txtVendorAccNo.Text = string.Empty;
            txtBankName.Text = string.Empty;
            criteria = string.Empty;
            dgVendor_Bind();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            //fill data
            merchantDetail.lblVendorID.Text = tab["ID"].ToString();
            //if (tab["Vendor_Name"] != null)
            //    merchantDetail.txtVendorName.Text = tab["Vendor_Name"].ToString();

            this.Close();
        }
    }
}
