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
using System.Configuration;

namespace Als
{
    /// <summary>
    /// Interaction logic for DeleteAuditTrailPage.xaml
    /// </summary>
    public partial class DeleteAuditTrailPage : Page
    {
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, query;
        PublicClass pub = new PublicClass();
        private string moduleName = "Report Audit Trail";

        public DeleteAuditTrailPage()
        {
            InitializeComponent();
            conn = new OleDbConnection(connParam);
            CheckAccess();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string checkValid = validate();
                if (checkValid != string.Empty)
                {
                    MessageBox.Show(checkValid);
                    return;
                }
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    conn.Open();
                    cmd.Connection = conn;
                    criteria = "WHERE 1=1";
                    string periodFrom = dpPeriodFrom.Text + " 0:00:01";
                    string periodTo = dpPeriodTo.Text + " 23:59:59";
                    criteria = criteria + " AND Action_Time >= #" + periodFrom + "# AND Action_Time <= #" + periodTo + "#";

                    query = "DELETE FROM UserLog " + criteria;

                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Delete data success");
                }
            }
            catch { }
            finally
            {
                try {  }
                catch { }
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            dpPeriodFrom.Text = string.Empty;
            dpPeriodTo.Text = string.Empty;
        }

        private string validate()
        {
            if (string.IsNullOrEmpty(dpPeriodFrom.Text))
                return "Please Fill Period From";
            else if (string.IsNullOrEmpty(dpPeriodTo.Text))
                return "Please Fill Period To";
            else
                return string.Empty;
        }

        //Jimmy 26-05-2015
        private void CheckAccess()
        {
            DataRow row = pub.Access(moduleName);

            if (row == null || string.IsNullOrEmpty(row[1].ToString()))
            {
                btnDelete.Visibility = Visibility.Hidden;
            }
            else
            {
                if (!(bool)row["Delete_Check"])
                {
                    btnDelete.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
