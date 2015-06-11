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
using System.Configuration;

namespace Als
{
    /// <summary>
    /// Interaction logic for AuditTrailPage.xaml
    /// </summary>
    public partial class AuditTrailPage : Page
    {
        PublicClass pub = new PublicClass();
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string moduleName = "Report Audit Trail";

        public AuditTrailPage()
        {
            InitializeComponent();
            conn = new OleDbConnection(connParam);
            CheckAccess();
        }

        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AuditTrailReportWindow report = new AuditTrailReportWindow(hdnID.Text, txtUserId.Text, dpPeriodFrom.Text, dpPeriodTo.Text);
                report.ShowDialog();
            }
            catch (OleDbException ex)
            {
            }
            finally { pub.Audit_Trail(conn, cmd, moduleName, "View", "User ID : " + txtUserId.Text); }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            dpPeriodFrom.Text = string.Empty;
            dpPeriodTo.Text = string.Empty;
            txtUserId.Text = string.Empty;
            txtUserName.Text = string.Empty;
            hdnID.Text = string.Empty;
        }

        private void btnLookupser_Click(object sender, RoutedEventArgs e)
        {
            UserLookupWindow2 userLookup = new UserLookupWindow2(this);
            userLookup.ShowDialog();
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
