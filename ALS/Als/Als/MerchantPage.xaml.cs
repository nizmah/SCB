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
    /// Interaction logic for MerchantPage.xaml
    /// </summary>
    public partial class MerchantPage : Page
    {
        private MainWindow main;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();
        private string moduleName = "Master Merchant";

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, query, merchant_Code;

        public MerchantPage(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            conn = new OleDbConnection(connParam);
            dgMerchant_Bind();
            CheckAccess();
        }

        public void RefreshPage()
        {
            dgMerchant_Bind();
        }

        private void dgMerchant_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                query = "Select mm.*, mv.Vendor_Name ";
                query = query + "From MasterMerchant mm LEFT JOIN (Select * FROM MasterVendor WHERE Deleted = 0) mv ON mm.Vendor_ID = mv.ID Where 1=1 AND mm.Deleted = 0 " + criteria;
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

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            MerchantDetailWindow merchantDetail = new MerchantDetailWindow(this);
            merchantDetail.lblTitle.Text = "Merchant - Create";
            merchantDetail.lblID.Text = string.Empty;
            merchantDetail.cbVendor.SelectedValue = 0;
            //merchantDetail.txtVendorName.Text = string.Empty;
            merchantDetail.txtMerchantCode.Text = string.Empty;
            merchantDetail.txtMerchantName.Text = string.Empty;
            merchantDetail.txtMerchantDescription.Text = string.Empty;
            //merchantDetail.txtVendorName.IsEnabled = false;
            merchantDetail.gbDetail.Visibility = Visibility.Hidden;
            merchantDetail.ShowDialog();
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ID = ((FrameworkElement)sender).DataContext;
                DataRowView tab = (DataRowView)ID;
                MerchantDetailWindow merchantDetail = new MerchantDetailWindow(this);
                //fill data
                merchant_Code = tab["Merchant_Code"].ToString();
                merchantDetail.txtMerchantCode.Text = tab["Merchant_Code"].ToString();
                if (tab["Vendor_Name"] != null)
                    merchantDetail.cbVendor.Text = tab["Vendor_Name"].ToString();
                if (tab["Merchant_Description"] != null)
                    merchantDetail.txtMerchantDescription.Text = tab["Merchant_Description"].ToString();
                if (tab["Merchant_Name"] != null)
                    merchantDetail.txtMerchantName.Text = tab["Merchant_Name"].ToString();

                if (tab["CreatedBy"] != null)
                    merchantDetail.lblCreatedByValue.Text = tab["CreatedBy"].ToString();
                if (tab["CreatedDate"] != null)
                    merchantDetail.lblCreatedDateValue.Text = tab["CreatedDate"].ToString();
                if (tab["UpdatedBy"] != null)
                    merchantDetail.lblUpdatedByValue.Text = tab["UpdatedBy"].ToString();
                if (tab["UpdatedDate"] != null)
                    merchantDetail.lblUpdatedDateValue.Text = tab["UpdatedDate"].ToString();

                merchantDetail.lblTitle.Text = "Merchant - View";
                //enable/disable control
                //merchantDetail.txtVendorName.IsEnabled = false;
                merchantDetail.cbVendor.IsEnabled = false;
                merchantDetail.txtMerchantCode.IsEnabled = false;
                merchantDetail.txtMerchantDescription.IsEnabled = false;
                merchantDetail.txtMerchantName.IsEnabled = false;
                //merchantDetail.btnLookupVendor.IsEnabled = false;

                merchantDetail.gbDetail.Visibility = Visibility.Visible;
                merchantDetail.btnSave.Visibility = Visibility.Hidden;

                merchantDetail.ShowDialog();
            }
            catch { }
            finally
            {
                try
                {
                    pub.Audit_Trail(conn, cmd, "Master Merchant", "View", "Merchant Code : " + merchant_Code);
                    merchant_Code = string.Empty;
                }
                catch { }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            MerchantDetailWindow merchantDetail = new MerchantDetailWindow(this);
            //fill data
            merchantDetail.lblID.Text = tab["ID"].ToString();
            merchantDetail.lblVendorID.Text = tab["Vendor_ID"].ToString();
            merchantDetail.txtMerchantCode.Text = tab["Merchant_Code"].ToString();
            if (tab["Vendor_Name"] != null)
                merchantDetail.cbVendor.SelectedValue = tab["Vendor_ID"].ToString();
            if (tab["Merchant_Description"] != null)
                merchantDetail.txtMerchantDescription.Text = tab["Merchant_Description"].ToString();
            if (tab["Merchant_Name"] != null)
                merchantDetail.txtMerchantName.Text = tab["Merchant_Name"].ToString();

            if (tab["CreatedBy"] != null)
                merchantDetail.lblCreatedByValue.Text = tab["CreatedBy"].ToString();
            if (tab["CreatedDate"] != null)
                merchantDetail.lblCreatedDateValue.Text = tab["CreatedDate"].ToString();
            if (tab["UpdatedBy"] != null)
                merchantDetail.lblUpdatedByValue.Text = tab["UpdatedBy"].ToString();
            if (tab["UpdatedDate"] != null)
                merchantDetail.lblUpdatedDateValue.Text = tab["UpdatedDate"].ToString();

            merchantDetail.lblTitle.Text = "Merchant - View";
            //enable/disable control
            //merchantDetail.txtVendorName.IsEnabled = false;
            merchantDetail.cbVendor.IsEnabled = true;
            merchantDetail.txtMerchantCode.IsEnabled = false;
            merchantDetail.txtMerchantDescription.IsEnabled = true;
            merchantDetail.txtMerchantName.IsEnabled = true;
            //merchantDetail.btnLookupVendor.IsEnabled = true;

            merchantDetail.gbDetail.Visibility = Visibility.Visible;
            merchantDetail.btnSave.Visibility = Visibility.Visible;

            merchantDetail.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            merchant_Code = tab["Merchant_Code"].ToString();
            MessageBoxResult result = MessageBox.Show("Are you sure to delete this data ?", "Confirmation Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE MasterMerchant SET Deleted = 1 Where ID = " + tab["ID"].ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    dgMerchant_Bind();
                }
                catch { }
                finally
                {
                    try
                    {
                        pub.Audit_Trail(conn, cmd, "Master Merchant", "Delete", "Merchant Code : " + merchant_Code);
                        merchant_Code = string.Empty;
                    }
                    catch { }
                }
            }
        }

        private void MerchantPriceList_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            main.Merchant_Price_List(this, tab["ID"].ToString(), tab["Merchant_Code"].ToString() + " - " + tab["Merchant_Name"].ToString());
        }

        private void CheckAccess()
        {
            pub.CheckAccess(moduleName, dgMerchant, btnCreate);
        }
    }
}
