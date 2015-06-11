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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;

namespace Als
{
    /// <summary>
    /// Interaction logic for MemoPaymentReportWindow.xaml
    /// </summary>
    public partial class MemoPaymentReportWindow : Window
    {
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, query;

        private DataTable table;

        public MemoPaymentReportWindow()
        {
            InitializeComponent();
            conn = new OleDbConnection(connParam);
            _reportViewer.Load += ReportViewer_Load;
        }

        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                fillTable();

                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                Microsoft.Reporting.WinForms.ReportDataSource();
                Database1DataSet dataset = new Database1DataSet();

                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1";
                //Name of the report dataset in our .RDLC file
                this._reportViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                reportDataSource1.Value = table;
                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);

                this._reportViewer.LocalReport.ReportPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Report\\ReportMemoPayment.rdlc";
                dataset.EndInit();
                //fill data into WpfApplication4DataSet
             
                this._reportViewer.LocalReport.DataSources.Clear();
                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                _reportViewer.RefreshReport();
                _isReportViewerLoaded = true;
            }
        }

        private void fillTable()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                query = "SELECT a.ID, a.Invoice_No,a.Vendor_ID, b.Vendor_Name, b.Vendor_Account_No, a.Transferred_Amount, ";
                query = query + "c.Bank_Name, c.Bank_Branch, d.GL_Dept, d.GL_Unit, d.GL_No, a.Memo_Description, a.Total_Pax, e.User_ID, ";
                query = query + "e.User_Name, e.User_No_Ext, a.Checked_By, a.Acknowledged_By_1, a.Acknowledged_By_2, a.Approved_By_1, a.Approved_By_2 ";
                query = query + "FROM ((((MemoPayment a ";
                query = query + "LEFT JOIN MasterVendor b ON a.Vendor_ID = b.ID) ";
                query = query + "LEFT JOIN MasterBank c ON b.Bank_ID = c.ID) ";
                query = query + "LEFT JOIN MasterGLAccount d ON a.GL_Account_ID = d.ID) ";
                query = query + "LEFT JOIN MasterUser e ON e.User_ID = a.CreatedBy) ";
                query = query + "WHERE a.ID = (SELECT Max(ID) FROM MemoPayment)  AND b.Deleted = 0 ";
                query = query + "AND c.Deleted = 0 AND d.Deleted = 0 AND e.Deleted = 0";
                
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (table = new DataTable())
                    {
                        sda.Fill(table);

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
    }
}
