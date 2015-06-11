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
    /// Interaction logic for MerchantLookupWindow.xaml
    /// </summary>
    public partial class MerchantLookupWindow : Window
    {
        private MerchantPriceListPage merchantPriceList;
        private MonthlyReportPage monthlyReport;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, query, type;

        public MerchantLookupWindow(string type, MerchantPriceListPage merchantPriceList, MonthlyReportPage monthlyReport)
        {
            InitializeComponent();
            this.type = type;
            this.merchantPriceList = merchantPriceList;
            this.monthlyReport = monthlyReport;
            conn = new OleDbConnection(connParam);
            dgMerchant_Bind();
        }

        private void dgMerchant_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                query = "Select mm.*, mv.Vendor_Name ";
                query = query + "From MasterMerchant mm LEFT JOIN (SELECT * FROM MasterVendor WHERE Deleted = 0) mv ON mm.Vendor_ID = mv.ID Where 1=1 AND mm.Deleted = 0 " + criteria;
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                dgMerchant.DataContext = pub.BindDG(cmd);

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
                criteria = " AND Merchant_Code LIKE '%" + txtMerchantCode.Text + "%'";
                criteria = criteria + " AND Merchant_Name LIKE '%" + txtMerchantName.Text + "%'";
                if (!string.IsNullOrEmpty(txtVendorName.Text))
                    criteria = criteria + " AND Vendor_Name LIKE '%" + txtVendorName.Text + "%' ";

                dgMerchant_Bind();
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
            txtMerchantName.Text = string.Empty;
            txtMerchantCode.Text = string.Empty;
            criteria = string.Empty;
            dgMerchant_Bind();
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
            if (type == "1")
            {
                merchantPriceList.lblMerchantID.Text = tab["ID"].ToString();
                //if (tab["Merchant_Name"] != null)
                 //   merchantPriceList.txtMerchantName.Text = tab["Merchant_Name"].ToString();
            }
            else
            {
                monthlyReport.hdnID.Text = tab["ID"].ToString();
                if (tab["Merchant_Name"] != null)
                    monthlyReport.txtMerchantName.Text = tab["Merchant_Name"].ToString();
                if (tab["Merchant_Code"] != null)
                    monthlyReport.txtMerchantCode.Text = tab["Merchant_Code"].ToString();
            }
            this.Close();
        }
    }
}
