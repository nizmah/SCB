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
    /// Interaction logic for CardTypePage.xaml
    /// </summary>
    public partial class CardTypePage : Page
    {
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();
        private string moduleName = "Master Card Type";

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, Card_Type;

        public CardTypePage()
        {
            InitializeComponent();
            conn = new OleDbConnection(connParam);
            dgCardType_Bind();
            CheckAccess();
        }

        public void RefreshPage()
        {
            dgCardType_Bind();
        }

        private void dgCardType_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "Select * From MasterCardType Where 1=1 AND Deleted = 0" + criteria;
                cmd.CommandType = CommandType.Text;

                dgCardType.DataContext = pub.BindDG(cmd);
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
            txtCardTypeName.Text = string.Empty;
            criteria = string.Empty;
            dgCardType_Bind();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                criteria = string.Empty;
                criteria = " AND Card_Type_Name LIKE '%" + txtCardTypeName.Text + "%'";

                dgCardType_Bind();
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
            CardTypeDetailWindow cardTypeDetail = new CardTypeDetailWindow(this);
            cardTypeDetail.lblTitle.Text = "Card Type - Create";
            cardTypeDetail.lblID.Text = string.Empty;
            cardTypeDetail.txtCardTypeName.Text = string.Empty;
            cardTypeDetail.txtCardTypeDescription.Text = string.Empty;
            cardTypeDetail.gbDetail.Visibility = Visibility.Hidden;
            cardTypeDetail.ShowDialog();
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ID = ((FrameworkElement)sender).DataContext;
                DataRowView tab = (DataRowView)ID;
                CardTypeDetailWindow cardTypeDetail = new CardTypeDetailWindow(this);
                //fill data
                Card_Type = tab["Card_Type_Name"].ToString();
                cardTypeDetail.txtCardTypeName.Text = tab["Card_Type_Name"].ToString();
                if (tab["Card_Type_Description"] != null)
                    cardTypeDetail.txtCardTypeDescription.Text = tab["Card_Type_Description"].ToString();

                if (tab["CreatedBy"] != null)
                    cardTypeDetail.lblCreatedByValue.Text = tab["CreatedBy"].ToString();
                if (tab["CreatedDate"] != null)
                    cardTypeDetail.lblCreatedDateValue.Text = tab["CreatedDate"].ToString();
                if (tab["UpdatedBy"] != null)
                    cardTypeDetail.lblUpdatedByValue.Text = tab["UpdatedBy"].ToString();
                if (tab["UpdatedDate"] != null)
                    cardTypeDetail.lblUpdatedDateValue.Text = tab["UpdatedDate"].ToString();

                cardTypeDetail.lblTitle.Text = "Card Type - View";
                //enable/disable control
                cardTypeDetail.txtCardTypeName.IsEnabled = false;
                cardTypeDetail.txtCardTypeDescription.IsEnabled = false;

                cardTypeDetail.gbDetail.Visibility = Visibility.Visible;
                cardTypeDetail.btnSave.Visibility = Visibility.Hidden;

                cardTypeDetail.ShowDialog();
            }
            catch { }
            finally
            {
                try
                {
                    pub.Audit_Trail(conn, cmd, "Master Card Type", "View", "Card Type Name : " + Card_Type);
                    Card_Type = string.Empty;
                }
                catch { }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            CardTypeDetailWindow cardTypeDetail = new CardTypeDetailWindow(this);
            //fill data
            cardTypeDetail.lblID.Text = tab["ID"].ToString();
            cardTypeDetail.txtCardTypeName.Text = tab["Card_Type_Name"].ToString();
            if (tab["Card_Type_Description"] != null)
                cardTypeDetail.txtCardTypeDescription.Text = tab["Card_Type_Description"].ToString();

            cardTypeDetail.lblTitle.Text = "Card Type - Update";
            //enable/disable control
            cardTypeDetail.txtCardTypeName.IsEnabled = false;
            cardTypeDetail.txtCardTypeDescription.IsEnabled = true;

            cardTypeDetail.gbDetail.Visibility = Visibility.Visible;
            cardTypeDetail.btnSave.Visibility = Visibility.Visible;

            cardTypeDetail.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            Card_Type = tab["Card_Type_Name"].ToString();
            MessageBoxResult result = MessageBox.Show("Are you sure to delete this data ?", "Confirmation Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE MasterCardType SET Deleted = 1 Where ID = " + tab["ID"].ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    dgCardType_Bind();
                }
                catch { }
                finally
                {
                    try
                    {
                        pub.Audit_Trail(conn, cmd, "Master Card Type", "Delete", "Card Type Name : " + Card_Type);
                        Card_Type = string.Empty;
                    }
                    catch { }
                }
            }
        }

        private void CheckAccess()
        {
            pub.CheckAccess(moduleName, dgCardType, btnCreate);
        }
    }
}
