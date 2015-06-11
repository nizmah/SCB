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
    /// Interaction logic for BankPage.xaml
    /// </summary>
    public partial class BankPage : Page
    {
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();
        private string moduleName = "Master Bank";

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, query, bank_Category;

        public BankPage()
        {
            InitializeComponent();
            conn = new OleDbConnection(connParam);
            dgBank_Bind();
            CheckAccess();
        }

        public void RefreshPage()
        {
            dgBank_Bind();
        }

        private void dgBank_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                query = "Select * ";
                query = query + "From MasterBank Where 1=1 AND Deleted = 0 " + criteria;
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                dgBank.DataContext = pub.BindDG(cmd);

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
                criteria = " AND Bank_Name LIKE '%" + txtBankName.Text + "%'";
                if (!string.IsNullOrEmpty(txtBankBranch.Text))
                    criteria = criteria + " AND Bank_Branch LIKE '%" + txtBankBranch.Text + "%' ";
                if (!string.IsNullOrEmpty(txtCityofBranch.Text))
                    criteria = criteria + " AND Bank_City LIKE '%" + txtCityofBranch.Text + "%' ";

                dgBank_Bind();
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
            txtBankName.Text = string.Empty;
            txtBankBranch.Text = string.Empty;
            txtCityofBranch.Text = string.Empty;
            criteria = string.Empty;
            dgBank_Bind();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            BankDetailWindow bankDetail = new BankDetailWindow(this);
            bankDetail.lblTitle.Text = "Bank - Create";
            bankDetail.lblID.Text = string.Empty;
            bankDetail.txtBankName.Text = string.Empty;
            bankDetail.txtBankBranch.Text = string.Empty;
            bankDetail.txtBankCity.Text = string.Empty;
            bankDetail.txtBankDescription.Text = string.Empty;
            bankDetail.gbDetail.Visibility = Visibility.Hidden;
            bankDetail.ShowDialog();
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ID = ((FrameworkElement)sender).DataContext;
                DataRowView tab = (DataRowView)ID;
                BankDetailWindow bankDetail = new BankDetailWindow(this);
                //fill data
                bank_Category = tab["Bank_Name"].ToString() + " - " + tab["Bank_Branch"].ToString() + " - " + tab["Bank_City"].ToString();
                bankDetail.txtBankName.Text = tab["Bank_Name"].ToString();
                if (tab["Bank_Branch"] != null)
                    bankDetail.txtBankBranch.Text = tab["Bank_Branch"].ToString();
                if (tab["Bank_City"] != null)
                    bankDetail.txtBankCity.Text = tab["Bank_City"].ToString();
                if (tab["Bank_Description"] != null)
                    bankDetail.txtBankDescription.Text = tab["Bank_Description"].ToString();

                if (tab["CreatedBy"] != null)
                    bankDetail.lblCreatedByValue.Text = tab["CreatedBy"].ToString();
                if (tab["CreatedDate"] != null)
                    bankDetail.lblCreatedDateValue.Text = tab["CreatedDate"].ToString();
                if (tab["UpdatedBy"] != null)
                    bankDetail.lblUpdatedByValue.Text = tab["UpdatedBy"].ToString();
                if (tab["UpdatedDate"] != null)
                    bankDetail.lblUpdatedDateValue.Text = tab["UpdatedDate"].ToString();

                bankDetail.lblTitle.Text = "Bank - View";
                //enable/disable control
                bankDetail.txtBankName.IsEnabled = false;
                bankDetail.txtBankBranch.IsEnabled = false;
                bankDetail.txtBankCity.IsEnabled = false;
                bankDetail.txtBankDescription.IsEnabled = false;

                bankDetail.gbDetail.Visibility = Visibility.Visible;
                bankDetail.btnSave.Visibility = Visibility.Hidden;

                bankDetail.ShowDialog();
            }
            catch { }
            finally
            {
                try
                {
                    pub.Audit_Trail(conn, cmd, "Master Bank", "View", "Bank Category : " + bank_Category);
                    bank_Category = string.Empty;
                }
                catch { }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            BankDetailWindow bankDetail = new BankDetailWindow(this);
            //fill data
            bankDetail.lblID.Text = tab["ID"].ToString();
            bankDetail.txtBankName.Text = tab["Bank_Name"].ToString();
            if (tab["Bank_Branch"] != null)
                bankDetail.txtBankBranch.Text = tab["Bank_Branch"].ToString();
            if (tab["Bank_City"] != null)
                bankDetail.txtBankCity.Text = tab["Bank_City"].ToString();
            if (tab["Bank_Description"] != null)
                bankDetail.txtBankDescription.Text = tab["Bank_Description"].ToString();

            bankDetail.lblTitle.Text = "Bank - Update";
            //enable/disable control
            bankDetail.txtBankName.IsEnabled = false;
            bankDetail.txtBankBranch.IsEnabled = true;
            bankDetail.txtBankCity.IsEnabled = true;
            bankDetail.txtBankDescription.IsEnabled = true;

            bankDetail.gbDetail.Visibility = Visibility.Visible;
            bankDetail.btnSave.Visibility = Visibility.Visible;

            bankDetail.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            bank_Category = tab["Bank_Name"].ToString() + " - " + tab["Bank_Branch"].ToString() + " - " + tab["Bank_City"].ToString();
            MessageBoxResult result = MessageBox.Show("Are you sure to delete this data ?", "Confirmation Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE MasterBank SET Deleted = 1 Where ID = " + tab["ID"].ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    dgBank_Bind();
                }
                catch { }
                finally
                {
                    try
                    {
                        pub.Audit_Trail(conn, cmd, "Master Bank", "Delete", "Bank Category : " + bank_Category);
                        bank_Category = string.Empty;
                    }
                    catch { }
                }
            }
        }

        private void CheckAccess()
        {
            pub.CheckAccess(moduleName, dgBank, btnCreate);

        }
    }
}
