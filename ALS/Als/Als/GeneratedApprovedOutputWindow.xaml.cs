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
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Collections;

namespace Als
{
    /// <summary>
    /// Interaction logic for GeneratedApprovedOutputWindow.xaml
    /// </summary>
    public partial class GeneratedApprovedOutputWindow : Window
    {
        private OleDbConnection conn;
        private DailyTransactionPage dtp;
        private OleDbCommand cmd = new OleDbCommand();
        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, query;
        private DataTable tableSettlement;
        private DataTable tableTC40;
        private DataTable tableTC41;
        private DataTable tableUPGL;
        private DataTable tableDeduct;
        private DataTable tableCharging;
        private DataTable tablePSGL;
        //private DataTable table;
        //private bool _rvSettlement;
        //private bool _rvTC40;

        public GeneratedApprovedOutputWindow()
        {
            InitializeComponent();
            //this.dtp = _dtp;
            rvSettlement.Load += new EventHandler(ReportViewer_Load);

        }

        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            conn = new OleDbConnection(connParam);
            if (!_isReportViewerLoaded)
            {

                tableSettlement = fillTable(1);
                tableTC40 = fillTable(2);
                tableTC41 = fillTable(3);
                tableUPGL = fillTable(4);
                tableDeduct = fillTable(5);
                tableCharging = fillTable(6);
                tablePSGL = fillTable(7);
                Microsoft.Reporting.WinForms.ReportDataSource rdsrcSettlement = new Microsoft.Reporting.WinForms.ReportDataSource();
                Microsoft.Reporting.WinForms.ReportDataSource rdsrcTC40 = new Microsoft.Reporting.WinForms.ReportDataSource();
                Microsoft.Reporting.WinForms.ReportDataSource rdsrcTC41 = new Microsoft.Reporting.WinForms.ReportDataSource();
                Microsoft.Reporting.WinForms.ReportDataSource rdsrcUPGL = new Microsoft.Reporting.WinForms.ReportDataSource();
                Microsoft.Reporting.WinForms.ReportDataSource rdsrcDeduct = new Microsoft.Reporting.WinForms.ReportDataSource();
                Microsoft.Reporting.WinForms.ReportDataSource rdsrcCharging = new Microsoft.Reporting.WinForms.ReportDataSource();
                Microsoft.Reporting.WinForms.ReportDataSource rdsrcPSGL = new Microsoft.Reporting.WinForms.ReportDataSource();
                Database1DataSet dataset = new Database1DataSet();

                dataset.BeginInit();

                //settlement
                rdsrcSettlement.Name = "DataSet1";
                //Name of the report dataset in our .RDLC file
                this.rvSettlement.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdsrcSettlement.Value = tableSettlement;
                this.rvSettlement.ShowParameterPrompts = false;
                this.rvSettlement.LocalReport.DataSources.Add(rdsrcSettlement);
                this.rvSettlement.LocalReport.ReportPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Report\\RptSettlementVendor.rdlc";
                this.rvSettlement.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("IsDraft", "false"));

                //tc40
                rdsrcTC40.Name = "DataSet1";
                this.rvTC40.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdsrcTC40.Value = tableTC40;
                this.rvTC40.ShowParameterPrompts = false;
                this.rvTC40.LocalReport.DataSources.Add(rdsrcTC40);
                this.rvTC40.LocalReport.ReportPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Report\\RptTC40MemorandumDraft.rdlc";
                List<Microsoft.Reporting.WinForms.ReportParameter> listParameterTC40 = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                listParameterTC40.Add(new Microsoft.Reporting.WinForms.ReportParameter("TC", "TC40"));
                listParameterTC40.Add(new Microsoft.Reporting.WinForms.ReportParameter("IsDraft", "false"));
                this.rvTC40.LocalReport.SetParameters(listParameterTC40);

                //tc41
                rdsrcTC41.Name = "DataSet1";
                this.rvTC41.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdsrcTC41.Value = tableTC41;
                this.rvTC41.ShowParameterPrompts = false;
                this.rvTC41.LocalReport.DataSources.Add(rdsrcTC41);
                this.rvTC41.LocalReport.ReportPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Report\\RptTC40MemorandumDraft.rdlc";
                List<Microsoft.Reporting.WinForms.ReportParameter> listParameterTC41 = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                listParameterTC41.Add(new Microsoft.Reporting.WinForms.ReportParameter("TC", "TC41"));
                listParameterTC41.Add(new Microsoft.Reporting.WinForms.ReportParameter("IsDraft", "false"));
                this.rvTC41.LocalReport.SetParameters(listParameterTC41);

                //UPGL
                rdsrcUPGL.Name = "DataSet1";
                this.rvUPGL.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdsrcUPGL.Value = tableUPGL;
                this.rvUPGL.ShowParameterPrompts = false;
                this.rvUPGL.LocalReport.DataSources.Add(rdsrcUPGL);
                this.rvUPGL.LocalReport.ReportPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Report\\RptUPGLDraft.rdlc";
                this.rvUPGL.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("IsDraft", "false"));

                //DeductionList
                rdsrcDeduct.Name = "DataSet1";
                this.rvDeduct.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdsrcDeduct.Value = tableDeduct;
                this.rvDeduct.ShowParameterPrompts = false;
                this.rvDeduct.LocalReport.DataSources.Add(rdsrcDeduct);
                this.rvDeduct.LocalReport.ReportPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Report\\RptDeductDraft.rdlc";
                this.rvDeduct.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("IsDraft", "false"));

                //ChargingList
                rdsrcCharging.Name = "DataSet1";
                this.rvCharging.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdsrcCharging.Value = tableCharging;
                this.rvCharging.ShowParameterPrompts = false;

                this.rvCharging.LocalReport.DataSources.Add(rdsrcCharging);
                this.rvCharging.LocalReport.ReportPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Report\\RptChargingDraft.rdlc";
                this.rvCharging.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("IsDraft", "false"));

                //PSGL
                rdsrcPSGL.Name = "DataSet1";
                this.rvPSGL.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdsrcPSGL.Value = tablePSGL;
                this.rvPSGL.ShowParameterPrompts = false;
                this.rvPSGL.LocalReport.DataSources.Add(rdsrcPSGL);
                this.rvPSGL.LocalReport.ReportPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Report\\RptPSGLDraft.rdlc";
                this.rvPSGL.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("IsDraft", "false"));

                dataset.EndInit();
                //fill data into WpfApplication4DataSet

                //this.rvSettlement.LocalReport.DataSources.Clear();
                //this.rvSettlement.LocalReport.DataSources.Add(rdsrcSettlement);

                //this.rvTC40.LocalReport.DataSources.Clear();
                //this.rvTC40.LocalReport.DataSources.Add(rdsrcTC40);

                //this.rvUPGL.LocalReport.DataSources.Clear();
                //this.rvUPGL.LocalReport.DataSources.Add(rdsrcUPGL);

                rvSettlement.RefreshReport();
                rvTC40.RefreshReport();
                rvTC41.RefreshReport();
                rvUPGL.RefreshReport();
                rvDeduct.RefreshReport();
                rvCharging.RefreshReport();
                rvPSGL.RefreshReport();

                _isReportViewerLoaded = true;
            }
        }

        private DataTable fillTable(int jenisreport)
        {
            DataTable table = null;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                if (jenisreport == 1) //settlement
                {
                    query = "SELECT * FROM qry_settlement2 where approved = true ";
                }
                else if (jenisreport == 2) //tc40
                {
                    query = "SELECT * FROM qry_Memorandom_TC40_Draft WHERE daily_transaction_detail.tc='40' AND daily_transaction_header.Approved=true";
                }
                else if (jenisreport == 3) //tc41
                {
                    query = "SELECT * FROM qry_Memorandom_TC40_Draft WHERE daily_transaction_detail.tc='41' AND daily_transaction_header.Approved=true";
                }
                else if (jenisreport == 4) //upgl
                {
                    query = "SELECT * FROM qry_upgl where approved = true";
                }
                else if (jenisreport == 5)//Deduction List
                {
                    query = "SELECT * FROM qry_deductionlist WHERE daily_transaction_header.Approved=true";
                }
                else if (jenisreport == 6)//Charging List
                {
                    query = "SELECT * FROM qry_charginglist WHERE daily_transaction_header.Approved=true";
                }
                else if (jenisreport == 7)//PSGL
                {
                    query = "SELECT * FROM qry_psgl Where daily_transaction_header.Approved=true";
                }

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

            return table;
        }
    }
}
