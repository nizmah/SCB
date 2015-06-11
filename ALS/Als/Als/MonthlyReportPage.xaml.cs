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
using System.Configuration;
using System.Data.OleDb;

namespace Als
{
    /// <summary>
    /// Interaction logic for MonthlyReportPage.xaml
    /// </summary>
    public partial class MonthlyReportPage : Page
    {
        private string reportType;
        PublicClass pub = new PublicClass();
        private string moduleName = "Report Monthly";
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public MonthlyReportPage(string reportType)
        {
            InitializeComponent();
            conn = new OleDbConnection(connParam);
            this.reportType = reportType;
            if (reportType == "Comparison")
            {
                
                this.moduleName = "Report Comparison Transaction";
                this.Title = "Comparison Transaction Report";
                this.gbHeader.Text = "Comparison Transaction Report";
               
            }
            CheckAccess();
        }

        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MonthlyReportWindow report = new MonthlyReportWindow(txtMerchantCode.Text, dpPeriodFrom.Text, dpPeriodTo.Text, reportType);
                report.ShowDialog();
            }
            catch
            {

            }
            finally { pub.Audit_Trail(conn, cmd, moduleName, "View", "Merchant Code : " + txtMerchantCode.Text); }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            dpPeriodFrom.Text = string.Empty;
            dpPeriodTo.Text = string.Empty;
            txtMerchantCode.Text = string.Empty;
            txtMerchantName.Text = string.Empty;
            hdnID.Text = string.Empty;
        }

        private void btnLookupMerchant_Click(object sender, RoutedEventArgs e)
        {
            MerchantPriceListPage mpl = new MerchantPriceListPage(new MainWindow() , new MerchantPage(new MainWindow()) , "","");
            MerchantLookupWindow MerchantLookup = new MerchantLookupWindow("2", mpl, this);
            MerchantLookup.ShowDialog();
        }

        //Jimmy 26-05-2015
        private void CheckAccess()
        {
            DataRow row = pub.Access(moduleName);

            if (row == null || string.IsNullOrEmpty(row[1].ToString()))
            {
                btnGenerateReport.Visibility = Visibility.Hidden;
            }
            else
            {
                if (!(bool)row["View_Check"])
                {
                    btnGenerateReport.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
