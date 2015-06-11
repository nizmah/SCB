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
    /// Interaction logic for MonthlyReportWindow.xaml
    /// </summary>
    public partial class MonthlyReportWindow : Window
    {
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, criteriaM, query, code, periodFrom, periodTo, reportType;

        private DataTable table;

        public MonthlyReportWindow(string code, string periodFrom, string periodTo, string reportType)
        {
            InitializeComponent();
            conn = new OleDbConnection(connParam);
            this.code = code;
            this.periodFrom = periodFrom;
            this.periodTo = periodTo;
            this.reportType = reportType;
            _reportViewer.Load += ReportViewer_Load;
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            try
            {

                if (reportType == "Monthly")
                {
                    menuHeader.Text = "MONTHLY REPORT";
                    
                    fillTable();
                }
                else
                {
                    menuHeader.Text = "MONTHLY REPORT";
                    fillTableComparison();
                }

                Microsoft.Reporting.WinForms.ReportParameter[] parameters = new Microsoft.Reporting.WinForms.ReportParameter[3];

                parameters[0] = new Microsoft.Reporting.WinForms.ReportParameter("PeriodFrom", periodFrom);
                parameters[1] = new Microsoft.Reporting.WinForms.ReportParameter("PeriodTo", periodTo);
                if(reportType == "Monthly")
                    parameters[2] = new Microsoft.Reporting.WinForms.ReportParameter("RemarksVisibility", "False");
                else
                    parameters[2] = new Microsoft.Reporting.WinForms.ReportParameter("RemarksVisibility", "True");

                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                Microsoft.Reporting.WinForms.ReportDataSource();
                Database1DataSet dataset = new Database1DataSet();

                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1";
                //Name of the report dataset in our .RDLC file
                this._reportViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                reportDataSource1.Value = table;

                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);

                this._reportViewer.LocalReport.ReportPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Report\\ReportMonthlyReport.rdlc";


                dataset.EndInit();
                //fill data into WpfApplication4DataSet

                this._reportViewer.LocalReport.DataSources.Clear();
                this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this._reportViewer.LocalReport.SetParameters(parameters);
                _reportViewer.RefreshReport();
            }
            catch (Exception ex)
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
                if (!string.IsNullOrEmpty(code))
                    criteria = criteria + " AND Merchant_Number='" + code + "'";

                if (!string.IsNullOrEmpty(periodFrom) && string.IsNullOrEmpty(periodTo))
                    criteria = criteria + " AND Report_Date >= #" + periodFrom + " 0:00:01#";
                else if (!string.IsNullOrEmpty(periodTo) && string.IsNullOrEmpty(periodFrom))
                    criteria = criteria + " AND Report_Date <= #" + periodTo + " 23:59:59#";
                else if (!string.IsNullOrEmpty(periodTo) && !string.IsNullOrEmpty(periodFrom))
                    criteria = criteria + " AND Report_Date >= #" + periodFrom + " 0:00:01# AND Report_Date <= #" + periodTo + " 23:59:59#";

                query = "SELECT * FROM View_MonthlyReport ";
                query = query + criteria;

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

        private void fillTableComparison()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                DataTable Unmatched;
                DataTable Monthly;
                criteria = "WHERE 1=1 AND TC = '40'";
                if (!string.IsNullOrEmpty(code))
                {
                    criteria = criteria + " AND Merchant_Number='" + code + "'";
                }

                if (!string.IsNullOrEmpty(periodTo) && !string.IsNullOrEmpty(periodFrom))
                {
                    criteria = criteria + " AND Report_Date >= #" + periodFrom + " 0:00:01# AND Report_Date <= #" + periodTo + " 23:59:59#";
                }

                query = "SELECT * FROM View_UnMatchComparison ";
                query = query + criteria;

                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (Unmatched = new DataTable())
                    {
                        sda.Fill(Unmatched);

                    }
                }

                query = "SELECT * FROM View_MonthlyComparison ";
                query = query + criteria;

                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (Monthly = new DataTable())
                    {
                        sda.Fill(Monthly);

                    }
                }

                foreach (DataRow row in Unmatched.Rows)
                {
                    if (Monthly.Select("Report_Date = '" + row["Report_Date"] + "' OR Merchant_Number = '" + row["Merchant_Number"] + "' OR Card_Holder_Number = '" + row["Card_Holder_Number"] + "' OR Transaction_Date = '" + row["Transaction_Date"] + "' OR Amount = '" + row["Amount"] + "'").FirstOrDefault() == null)
                    {
                        row["Remarks"] = "This Transaction not exists in vendor file";
                        Unmatched.AcceptChanges();
                    }
                    else
                    {
                        if (Monthly.Select("Report_Date = '" + row["Report_Date"] + "'").FirstOrDefault() != null)
                        {
                            if (Monthly.Select("Merchant_Number = '" + row["Merchant_Number"] + "'").FirstOrDefault() != null)
                            {
                                if (Monthly.Select("Card_Holder_Number = '" + row["Card_Holder_Number"] + "'").FirstOrDefault() != null)
                                {
                                    if (Monthly.Select("Transaction_Date = '" + row["Transaction_Date"] + "'").FirstOrDefault() != null)
                                    {
                                        if (Monthly.Select("Amount = '" + row["Amount"] + "'").FirstOrDefault() != null)
                                        {
                                            row["Remarks"] = " ";
                                            Unmatched.AcceptChanges();
                                        }
                                        else
                                        {
                                            row["Remarks"] = "Amount Invalid";
                                            Unmatched.AcceptChanges();
                                        }
                                    }
                                    else
                                    {
                                        row["Remarks"] = "Transaction Date Invalid";
                                        Unmatched.AcceptChanges();
                                    }
                                }
                                else
                                {
                                    row["Remarks"] = "Card Holder Number Invalid";
                                    Unmatched.AcceptChanges();
                                }
                            }
                            else
                            {
                                row["Remarks"] = "Merchant Number Invalid";
                                Unmatched.AcceptChanges();
                            }
                        }
                        else
                        {
                            row["Remarks"] = "Report Date Invalid";
                            Unmatched.AcceptChanges();
                        }
                    }
                    
                }



                table = Unmatched;

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
