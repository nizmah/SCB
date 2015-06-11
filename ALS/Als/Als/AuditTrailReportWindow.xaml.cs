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
    /// Interaction logic for AuditTrailReportWindow.xaml
    /// </summary>
    public partial class AuditTrailReportWindow : Window
    {
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, query, id, userId, periodFrom, periodTo;

        private DataTable table;

        public AuditTrailReportWindow(string id, string userId, string periodFrom, string periodTo)
        {
            InitializeComponent();
            conn = new OleDbConnection(connParam);
            this.id = id;
            this.userId = userId;
            this.periodFrom = periodFrom;
            this.periodTo = periodTo;
            _reportViewer.Load += ReportViewer_Load;
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            try
            {
                fillTable();

                Microsoft.Reporting.WinForms.ReportParameter[] parameters = new Microsoft.Reporting.WinForms.ReportParameter[3];
                parameters[0] = new Microsoft.Reporting.WinForms.ReportParameter("User", userId == "" ? "ALL" : userId);
                parameters[1] = new Microsoft.Reporting.WinForms.ReportParameter("PeriodFrom", periodFrom);
                parameters[2] = new Microsoft.Reporting.WinForms.ReportParameter("PeriodTo", periodTo);

                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                Microsoft.Reporting.WinForms.ReportDataSource();
                Database1DataSet dataset = new Database1DataSet();

                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1";
                //Name of the report dataset in our .RDLC file
                this._reportViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                reportDataSource1.Value = table;

                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);

                this._reportViewer.LocalReport.ReportPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Report\\ReportAuditTrail.rdlc";


                dataset.EndInit();
                //fill data into WpfApplication4DataSet

                this._reportViewer.LocalReport.DataSources.Clear();
                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this._reportViewer.LocalReport.SetParameters(parameters);
                _reportViewer.RefreshReport();
            }
            catch (OleDbException ex)
            {
            }
        }

        private void fillTable()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                criteria = "WHERE 1=1";
                if (!string.IsNullOrEmpty(id))
                    criteria = criteria + " AND UserID=" + id;

                if (!string.IsNullOrEmpty(periodFrom) && string.IsNullOrEmpty(periodTo))
                    criteria = criteria + " AND Action_Time >= #" + periodFrom + " 0:00:01#";
                else if (!string.IsNullOrEmpty(periodTo) && string.IsNullOrEmpty(periodFrom))
                    criteria = criteria + " AND Action_Time <= #" + periodTo + " 23:59:59#";
                else if (!string.IsNullOrEmpty(periodTo) && !string.IsNullOrEmpty(periodFrom))
                    criteria = criteria + " AND Action_Time >= #" + periodFrom + " 0:00:01# AND Action_Time <= #" + periodTo + " 23:59:59#";

                query = "SELECT User_ID, User_Name, Action_Module, Action_Name, Action_Description, Action_Time ";
                query = query + "FROM UserLog " + criteria;

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
