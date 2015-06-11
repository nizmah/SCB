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
    /// Interaction logic for VendorPage.xaml
    /// </summary>
    public partial class VendorPage : Page
    {
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();
        private string moduleName = "Master Vendor";

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, query, vendor_Name;

        public VendorPage()
        {
            InitializeComponent();
            conn = new OleDbConnection(connParam);
            dgVendor_Bind();
            CheckAccess();
        }

        public void RefreshPage()
        {
            dgVendor_Bind();
        }

        private void dgVendor_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                query = "Select mv.*, mb.Bank_Name, mb.Bank_Branch ";
                query = query + "From MasterVendor mv LEFT JOIN (Select * FROM MasterBank WHERE Deleted = 0) mb ON mv.Bank_ID = mb.ID Where 1=1 AND mv.Deleted = 0 " + criteria;
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

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            VendorDetailWindow vendorDetail = new VendorDetailWindow(this);
            vendorDetail.lblTitle.Text = "Vendor - Create";
            vendorDetail.lblID.Text = string.Empty;
            vendorDetail.txtVendorName.Text = string.Empty;
            vendorDetail.txtBank.Text = string.Empty;
            vendorDetail.txtVendorDescription.Text = string.Empty;
            vendorDetail.txtVendorAccNo.Text = string.Empty;
            vendorDetail.txtBank.IsEnabled = false;
            vendorDetail.gbDetail.Visibility = Visibility.Hidden;
            vendorDetail.ShowDialog();
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ID = ((FrameworkElement)sender).DataContext;
                DataRowView tab = (DataRowView)ID;
                VendorDetailWindow vendorDetail = new VendorDetailWindow(this);
                //fill data
                vendor_Name = tab["Vendor_Name"].ToString();
                vendorDetail.txtVendorName.Text = tab["Vendor_Name"].ToString();
                if (tab["Bank_Name"] != null)
                {
                    vendorDetail.txtBank.Text = tab["Bank_Name"].ToString();
                    if (tab["Bank_Branch"] != null)
                        vendorDetail.txtBank.Text = vendorDetail.txtBank.Text + " - " + tab["Bank_Branch"].ToString();

                }
                if (tab["Vendor_Description"] != null)
                    vendorDetail.txtVendorDescription.Text = tab["Vendor_Description"].ToString();
                if (tab["Vendor_Account_No"] != null)
                    vendorDetail.txtVendorAccNo.Text = tab["Vendor_Account_No"].ToString();

                if (tab["CreatedBy"] != null)
                    vendorDetail.lblCreatedByValue.Text = tab["CreatedBy"].ToString();
                if (tab["CreatedDate"] != null)
                    vendorDetail.lblCreatedDateValue.Text = tab["CreatedDate"].ToString();
                if (tab["UpdatedBy"] != null)
                    vendorDetail.lblUpdatedByValue.Text = tab["UpdatedBy"].ToString();
                if (tab["UpdatedDate"] != null)
                    vendorDetail.lblUpdatedDateValue.Text = tab["UpdatedDate"].ToString();

                vendorDetail.lblTitle.Text = "Vendor - View";
                //enable/disable control
                vendorDetail.txtVendorName.IsEnabled = false;
                vendorDetail.txtBank.IsEnabled = false;
                vendorDetail.txtVendorDescription.IsEnabled = false;
                vendorDetail.txtVendorAccNo.IsEnabled = false;
                vendorDetail.btnLookupBank.IsEnabled = false;

                vendorDetail.gbDetail.Visibility = Visibility.Visible;
                vendorDetail.btnSave.Visibility = Visibility.Hidden;

                vendorDetail.ShowDialog();
            }
            catch { }
            finally
            {
                try
                {
                    pub.Audit_Trail(conn, cmd, "Master Vendor", "View", "Vendor Name : " + vendor_Name);
                    vendor_Name = string.Empty;
                }
                catch { }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            VendorDetailWindow vendorDetail = new VendorDetailWindow(this);
            //fill data
            vendorDetail.lblID.Text = tab["ID"].ToString();
            vendorDetail.lblBankID.Text = tab["Bank_ID"].ToString();
            vendorDetail.txtVendorName.Text = tab["Vendor_Name"].ToString();
            if (tab["Bank_Name"] != null)
            {
                vendorDetail.txtBank.Text = tab["Bank_Name"].ToString();
                if (tab["Bank_Branch"] != null)
                    vendorDetail.txtBank.Text = vendorDetail.txtBank.Text + " - " + tab["Bank_Branch"].ToString();

            }
            if (tab["Vendor_Description"] != null)
                vendorDetail.txtVendorDescription.Text = tab["Vendor_Description"].ToString();
            if (tab["Vendor_Account_No"] != null)
                vendorDetail.txtVendorAccNo.Text = tab["Vendor_Account_No"].ToString();

            if (tab["CreatedBy"] != null)
                vendorDetail.lblCreatedByValue.Text = tab["CreatedBy"].ToString();
            if (tab["CreatedDate"] != null)
                vendorDetail.lblCreatedDateValue.Text = tab["CreatedDate"].ToString();
            if (tab["UpdatedBy"] != null)
                vendorDetail.lblUpdatedByValue.Text = tab["UpdatedBy"].ToString();
            if (tab["UpdatedDate"] != null)
                vendorDetail.lblUpdatedDateValue.Text = tab["UpdatedDate"].ToString();

            vendorDetail.lblTitle.Text = "Vendor - Update";
            //enable/disable control
            vendorDetail.txtVendorName.IsEnabled = false;
            vendorDetail.txtBank.IsEnabled = false;
            vendorDetail.txtVendorDescription.IsEnabled = true;
            vendorDetail.txtVendorAccNo.IsEnabled = true;
            vendorDetail.btnLookupBank.IsEnabled = true;

            vendorDetail.gbDetail.Visibility = Visibility.Visible;
            vendorDetail.btnSave.Visibility = Visibility.Visible;

            vendorDetail.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            vendor_Name = tab["Vendor_Name"].ToString(); 
            MessageBoxResult result = MessageBox.Show("Are you sure to delete this data ?", "Confirmation Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE MasterVendor SET Deleted = 1 Where ID = " + tab["ID"].ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    dgVendor_Bind();
                }
                catch { }
                finally
                {
                    try
                    {
                        pub.Audit_Trail(conn, cmd, "Master Vendor", "Delete", "Vendor Name : " + vendor_Name);
                        vendor_Name = string.Empty;
                    }
                    catch { }
                }
            }
        }

        private void CheckAccess()
        {
            pub.CheckAccess(moduleName, dgVendor, btnCreate);
        }
    }
}
