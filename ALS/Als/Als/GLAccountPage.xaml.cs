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
    /// Interaction logic for GLAccountPage.xaml
    /// </summary>
    public partial class GLAccountPage : Page
    {
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();
        private string moduleName = "Master GL Account";

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, query, gl_No;

        public GLAccountPage()
        {
            InitializeComponent();
            conn = new OleDbConnection(connParam);
            //btnCreate.Visibility = Visibility.Hidden;
            dgGLAccount_Bind();
            CheckAccess();
        }

        public void RefreshPage()
        {
            dgGLAccount_Bind();
        }

        private void dgGLAccount_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                query = "Select * ";
                query = query + "From MasterGLAccount Where 1=1 AND Deleted = 0 " + criteria;
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                dgGLAccount.DataContext = pub.BindDG(cmd);

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
                criteria = " AND GL_No LIKE '%" + txtGLAccountNo.Text + "%'";
                criteria = criteria + " AND GL_Name LIKE '%" + txtGLAccountName.Text + "%'";

                dgGLAccount_Bind();
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
            txtGLAccountNo.Text = string.Empty;
            txtGLAccountName.Text = string.Empty;
            criteria = string.Empty;
            dgGLAccount_Bind();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            GLAccountDetailWindow glAccountDetail = new GLAccountDetailWindow(this);
            glAccountDetail.lblTitle.Text = "GL Account - Create";
            glAccountDetail.lblID.Text = string.Empty;
            glAccountDetail.txtGLAccountNo.Text = string.Empty;
            glAccountDetail.txtGLAccountName.Text = string.Empty;
            glAccountDetail.cbTC.SelectedValue = "40";
            glAccountDetail.cbAccEntry.SelectedValue = "Debit";
            glAccountDetail.txtGLProd.Text = string.Empty;
            glAccountDetail.txtGLDept.Text = string.Empty;
            glAccountDetail.txtGLUnit.Text = string.Empty;
            glAccountDetail.txtGLClass.Text = string.Empty;
            glAccountDetail.txtGLDescription.Text = string.Empty;
            glAccountDetail.cbBinNo.SelectedValue = 0;
            glAccountDetail.gbDetail.Visibility = Visibility.Hidden;
            glAccountDetail.ShowDialog();
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ID = ((FrameworkElement)sender).DataContext;
                DataRowView tab = (DataRowView)ID;
                GLAccountDetailWindow glAccountDetail = new GLAccountDetailWindow(this);
                //fill data
                gl_No = tab["GL_No"].ToString();
                glAccountDetail.txtGLAccountNo.Text = tab["GL_No"].ToString();
                glAccountDetail.txtGLAccountName.Text = tab["GL_Name"].ToString();
                glAccountDetail.cbTC.SelectedValue = tab["TC"].ToString();
                glAccountDetail.cbAccEntry.SelectedValue = tab["Acc_Entry"].ToString();
                glAccountDetail.cbBinNo.SelectedValue = tab["Bin_No"].ToString();
                if (tab["GL_Prod"] != null)
                    glAccountDetail.txtGLProd.Text = tab["GL_Prod"].ToString();
                if (tab["GL_Dept"] != null)
                    glAccountDetail.txtGLDept.Text = tab["GL_Dept"].ToString();
                if (tab["GL_Unit"] != null)
                    glAccountDetail.txtGLUnit.Text = tab["GL_Unit"].ToString();
                if (tab["GL_Class"] != null)
                    glAccountDetail.txtGLClass.Text = tab["GL_Class"].ToString();
                if (tab["GL_Description"] != null)
                    glAccountDetail.txtGLDescription.Text = tab["GL_Description"].ToString();

                if (tab["CreatedBy"] != null)
                    glAccountDetail.lblCreatedByValue.Text = tab["CreatedBy"].ToString();
                if (tab["CreatedDate"] != null)
                    glAccountDetail.lblCreatedDateValue.Text = tab["CreatedDate"].ToString();
                if (tab["UpdatedBy"] != null)
                    glAccountDetail.lblUpdatedByValue.Text = tab["UpdatedBy"].ToString();
                if (tab["UpdatedDate"] != null)
                    glAccountDetail.lblUpdatedDateValue.Text = tab["UpdatedDate"].ToString();

                glAccountDetail.lblTitle.Text = "GL Account - View";
                //enable/disable control
                glAccountDetail.txtGLAccountNo.IsEnabled = false;
                glAccountDetail.txtGLAccountName.IsEnabled = false;
                glAccountDetail.cbTC.IsEnabled = false;
                glAccountDetail.cbAccEntry.IsEnabled = false;
                glAccountDetail.txtGLProd.IsEnabled = false;
                glAccountDetail.txtGLDept.IsEnabled = false;
                glAccountDetail.txtGLUnit.IsEnabled = false;
                glAccountDetail.txtGLClass.IsEnabled = false;
                glAccountDetail.cbBinNo.IsEnabled = false;
                glAccountDetail.txtGLDescription.IsEnabled = false;

                glAccountDetail.gbDetail.Visibility = Visibility.Visible;
                glAccountDetail.btnSave.Visibility = Visibility.Hidden;

                glAccountDetail.ShowDialog();
            }
            catch { }
            finally
            {
                try
                {
                    pub.Audit_Trail(conn, cmd, "Master GL Account", "View", "GL No : " + gl_No);
                    gl_No = string.Empty;
                }
                catch { }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            GLAccountDetailWindow glAccountDetail = new GLAccountDetailWindow(this);
            //fill data
            glAccountDetail.lblID.Text = tab["ID"].ToString();
            glAccountDetail.txtGLAccountNo.Text = tab["GL_No"].ToString();
            glAccountDetail.txtGLAccountName.Text = tab["GL_Name"].ToString();
            glAccountDetail.cbTC.SelectedValue = tab["TC"].ToString();
            glAccountDetail.cbAccEntry.SelectedValue = tab["Acc_Entry"].ToString();
            glAccountDetail.cbBinNo.SelectedValue = tab["Bin_No"].ToString();
            if (tab["GL_Prod"] != null)
                glAccountDetail.txtGLProd.Text = tab["GL_Prod"].ToString();
            if (tab["GL_Dept"] != null)
                glAccountDetail.txtGLDept.Text = tab["GL_Dept"].ToString();
            if (tab["GL_Unit"] != null)
                glAccountDetail.txtGLUnit.Text = tab["GL_Unit"].ToString();
            if (tab["GL_Class"] != null)
                glAccountDetail.txtGLClass.Text = tab["GL_Class"].ToString();
            if (tab["GL_Description"] != null)
                glAccountDetail.txtGLDescription.Text = tab["GL_Description"].ToString();


            glAccountDetail.lblTitle.Text = "GL Account - Update";
            //enable/disable control
            glAccountDetail.txtGLAccountNo.IsEnabled = false;
            glAccountDetail.txtGLAccountName.IsEnabled = true;
            glAccountDetail.cbTC.IsEnabled = true;
            glAccountDetail.cbAccEntry.IsEnabled = true;
            glAccountDetail.txtGLProd.IsEnabled = true;
            glAccountDetail.txtGLDept.IsEnabled = true;
            glAccountDetail.txtGLUnit.IsEnabled = true;
            glAccountDetail.txtGLClass.IsEnabled = true;
            glAccountDetail.cbBinNo.IsEnabled = true;
            glAccountDetail.txtGLDescription.IsEnabled = true;

            glAccountDetail.gbDetail.Visibility = Visibility.Visible;
            glAccountDetail.btnSave.Visibility = Visibility.Visible;

            glAccountDetail.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            gl_No = tab["GL_No"].ToString();
            MessageBoxResult result = MessageBox.Show("Are you sure to delete this data ?", "Confirmation Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE MasterGLAccount SET Deleted = 1 Where ID = " + tab["ID"].ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    dgGLAccount_Bind();
                }
                catch { }
                finally
                {
                    try
                    {
                        pub.Audit_Trail(conn, cmd, "Master GL Account", "View", "GL No : " + gl_No);
                        gl_No = string.Empty;
                    }
                    catch { }
                }
            }
        }

        private void CheckAccess()
        {
            pub.CheckAccess(moduleName, dgGLAccount, btnCreate);
        }
    }
}
