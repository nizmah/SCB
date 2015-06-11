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
    public partial class MerchantPriceListPage : Page
    {
        private MerchantPage merchantPage;
        private MainWindow main;
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();
        private string moduleName = "Master Merchant Price List";

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, query;

        public MerchantPriceListPage(MainWindow main, MerchantPage merchantPage, string merchantID, string merchantCode)
        {
            InitializeComponent();
            this.main = main;
            this.merchantPage = merchantPage;
            conn = new OleDbConnection(connParam);
            checkMerchant(merchantID, merchantCode);
            cbCardType_Bind();
            cbMerchant_Bind();
            dgMerchantPriceList_Bind();
            CheckAccess();
        }

        private void checkMerchant(string merchantID, string merchantCode)
        {
            if (string.IsNullOrEmpty(merchantID))
            {
                lblMerchantNameValue.Visibility = Visibility.Hidden;
                cbMerchant.Visibility = Visibility.Visible;
                //txtMerchantName.Visibility = Visibility.Visible;
                //btnLookupMerchant.Visibility = Visibility.Visible;
                btnCancel.Visibility = Visibility.Hidden;
                lblMerchantID.Text = string.Empty;
                lblMerchantNameValue.Text = string.Empty;
            }
            else
            {
                lblMerchantNameValue.Visibility = Visibility.Visible;
                cbMerchant.Visibility = Visibility.Hidden;
               // txtMerchantName.Visibility = Visibility.Hidden;
                //btnLookupMerchant.Visibility = Visibility.Hidden;
                btnCancel.Visibility = Visibility.Visible;
                lblMerchantID.Text = merchantID;
                lblMerchantNameValue.Text = merchantCode;
            }
        }

        private void cbCardType_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "Select * From MasterCardType Where 1=1 AND Deleted = 0";
                cmd.CommandType = CommandType.Text;

                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        DataRow row = dt.NewRow();
                        row["Card_Type_Name"] = "-Please Select-";
                        row["ID"] = 0;
                        dt.Rows.Add(row);
                        DataView dv = dt.DefaultView;
                        dv.Sort = "ID asc";
                        cbCardType.ItemsSource = dv;
                        cbCardType.SelectedValue = 0;
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

        public void RefreshPage()
        {
            dgMerchantPriceList_Bind();
        }

        private void dgMerchantPriceList_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                query = "Select * ";
                query = query + "From MasterMerchantPrice Where 1=1 AND Deleted = 0 " + criteria + " order by Guest_Amount";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;


                dgMerchantPriceList.DataContext = pub.BindDG(cmd);

            }
            catch { }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            MerchantPriceListDetailWindow merchantPriceListDetail = new MerchantPriceListDetailWindow(this);
            merchantPriceListDetail.lblTitle.Text = "Merchant Price List - Create";
            if (string.IsNullOrEmpty(lblMerchantNameValue.Text))
            {
                merchantPriceListDetail.lblMerchantID.Text = cbMerchant.SelectedValue.ToString();
                merchantPriceListDetail.lblMerchantNameValue.Text = cbMerchant.Text.ToString();
            }
            else
            {
                merchantPriceListDetail.lblMerchantID.Text = lblMerchantID.Text.ToString();
                merchantPriceListDetail.lblMerchantNameValue.Text = lblMerchantNameValue.Text.ToString();
            }
            
            merchantPriceListDetail.lblCardTypeNameValue.Text = cbCardType.Text.ToString();
            merchantPriceListDetail.lblCardTypeID.Text = cbCardType.SelectedValue.ToString();
            merchantPriceListDetail.txtTotalGuest.Text = string.Empty;
            merchantPriceListDetail.txtPriceAmount.Text = string.Empty;
            merchantPriceListDetail.txtPointAmount.Text = string.Empty;

            merchantPriceListDetail.gbDetail.Visibility = Visibility.Hidden;
            merchantPriceListDetail.ShowDialog();
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!validateData())
                    return;

                criteria = string.Empty;
                if (string.IsNullOrEmpty(lblMerchantNameValue.Text))
                {
                    criteria = " AND Merchant_ID = " + cbMerchant.SelectedValue.ToString();
                }
                else
                {
                    criteria = " AND Merchant_ID = " + lblMerchantID.Text;
                }

                criteria = criteria + " AND Card_Type_ID = " + cbCardType.SelectedValue;

                dgMerchantPriceList_Bind();
                groupBox2.Visibility = Visibility.Visible;
            }
            catch { }
            finally
            {
                try
                {
                    string[] mCode = lblMerchantNameValue.Text.Split('-');
                    pub.Audit_Trail(conn, cmd, "Master Merchant Price List", "View", "Merchant Code : " + mCode[0]);
                }
                catch { }
            }
        }

        private bool validateData()
        {
            if (string.IsNullOrEmpty(lblMerchantNameValue.Text))
            {
                if (cbMerchant.SelectedValue.ToString() == "0")
                {
                    MessageBox.Show("Please select Merchant !!", "WARNING");
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(lblMerchantID.Text))
                {
                    MessageBox.Show("Please select Merchant !!", "WARNING");
                    return false;
                }
            }
            if (cbCardType.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please select Card Type !!", "WARNING");
                return false;
            }
            return true;
        }

        private void btnLookupMerchant_Click(object sender, RoutedEventArgs e)
        {
            MerchantLookupWindow merchantLookup = new MerchantLookupWindow("1",this, new MonthlyReportPage("Monthly"));
            merchantLookup.ShowDialog();
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            MerchantPriceListDetailWindow merchantPriceListDetail = new MerchantPriceListDetailWindow(this);
            //fill data
            if (string.IsNullOrEmpty(lblMerchantNameValue.Text))
            {
                merchantPriceListDetail.lblMerchantNameValue.Text = cbMerchant.Text.ToString();
                merchantPriceListDetail.lblMerchantID.Text = cbMerchant.SelectedValue.ToString();
            }
            else
            {
                merchantPriceListDetail.lblMerchantNameValue.Text = lblMerchantNameValue.Text.ToString();
                merchantPriceListDetail.lblMerchantID.Text = lblMerchantID.Text.ToString();
            }
            merchantPriceListDetail.lblCardTypeNameValue.Text = cbCardType.Text.ToString();
            merchantPriceListDetail.lblCardTypeID.Text = cbCardType.SelectedValue.ToString();
            if (tab["Guest_Amount"] != null)
                merchantPriceListDetail.txtTotalGuest.Text = tab["Guest_Amount"].ToString();
            if (tab["Price_Amount"] != null)
                merchantPriceListDetail.txtPriceAmount.Text = tab["Price_Amount"].ToString();
            if (tab["Point_Amount"] != null)
                merchantPriceListDetail.txtPointAmount.Text = tab["Point_Amount"].ToString();

            if (tab["CreatedBy"] != null)
                merchantPriceListDetail.lblCreatedByValue.Text = tab["CreatedBy"].ToString();
            if (tab["CreatedDate"] != null)
                merchantPriceListDetail.lblCreatedDateValue.Text = tab["CreatedDate"].ToString();
            if (tab["UpdatedBy"] != null)
                merchantPriceListDetail.lblUpdatedByValue.Text = tab["UpdatedBy"].ToString();
            if (tab["UpdatedDate"] != null)
                merchantPriceListDetail.lblUpdatedDateValue.Text = tab["UpdatedDate"].ToString();

            merchantPriceListDetail.lblTitle.Text = "Merchant Price List - View";
            //enable/disable control
            merchantPriceListDetail.txtTotalGuest.IsEnabled = false;
            merchantPriceListDetail.txtPriceAmount.IsEnabled = false;
            merchantPriceListDetail.txtPointAmount.IsEnabled = false;

            merchantPriceListDetail.gbDetail.Visibility = Visibility.Visible;
            merchantPriceListDetail.btnSave.Visibility = Visibility.Hidden;

            merchantPriceListDetail.ShowDialog();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            MerchantPriceListDetailWindow merchantPriceListDetail = new MerchantPriceListDetailWindow(this);
            //fill data
            merchantPriceListDetail.lblID.Text = tab["ID"].ToString();
            if (string.IsNullOrEmpty(lblMerchantNameValue.Text))
            {
                merchantPriceListDetail.lblMerchantNameValue.Text = cbMerchant.Text.ToString();
                merchantPriceListDetail.lblMerchantID.Text = cbMerchant.SelectedValue.ToString();
            }
            else
            {
                merchantPriceListDetail.lblMerchantNameValue.Text = lblMerchantNameValue.Text.ToString();
                merchantPriceListDetail.lblMerchantID.Text = lblMerchantID.Text.ToString();
            }
            merchantPriceListDetail.lblCardTypeNameValue.Text = cbCardType.Text.ToString();
            merchantPriceListDetail.lblCardTypeID.Text = cbCardType.SelectedValue.ToString();
            if (tab["Guest_Amount"] != null)
                merchantPriceListDetail.txtTotalGuest.Text = tab["Guest_Amount"].ToString();
            if (tab["Price_Amount"] != null)
                merchantPriceListDetail.txtPriceAmount.Text = tab["Price_Amount"].ToString();
            if (tab["Point_Amount"] != null)
                merchantPriceListDetail.txtPointAmount.Text = tab["Point_Amount"].ToString();

            if (tab["CreatedBy"] != null)
                merchantPriceListDetail.lblCreatedByValue.Text = tab["CreatedBy"].ToString();
            if (tab["CreatedDate"] != null)
                merchantPriceListDetail.lblCreatedDateValue.Text = tab["CreatedDate"].ToString();
            if (tab["UpdatedBy"] != null)
                merchantPriceListDetail.lblUpdatedByValue.Text = tab["UpdatedBy"].ToString();
            if (tab["UpdatedDate"] != null)
                merchantPriceListDetail.lblUpdatedDateValue.Text = tab["UpdatedDate"].ToString();

            merchantPriceListDetail.lblTitle.Text = "Merchant Price List - Update";
            //enable/disable control
            merchantPriceListDetail.txtTotalGuest.IsEnabled = true;
            merchantPriceListDetail.txtPriceAmount.IsEnabled = true;
            merchantPriceListDetail.txtPointAmount.IsEnabled = true;

            merchantPriceListDetail.gbDetail.Visibility = Visibility.Visible;
            merchantPriceListDetail.btnSave.Visibility = Visibility.Visible;

            merchantPriceListDetail.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;

            MessageBoxResult result = MessageBox.Show("Are you sure to delete this data ?", "Confirmation Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE MasterMerchantPrice SET Deleted = 1 Where ID = " + tab["ID"].ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    dgMerchantPriceList_Bind();
                }
                catch { }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            main.mainFrame.Navigate(merchantPage);
        }

        private void CheckAccess()
        {
            pub.CheckAccess(moduleName, dgMerchantPriceList, btnCreate);
        }
    }
}
