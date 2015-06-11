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
    /// Interaction logic for CardPage.xaml
    /// </summary>
    public partial class CardPage : Page
    {
        private OleDbConnection conn;
        private OleDbCommand cmd = new OleDbCommand();
        PublicClass pub = new PublicClass();
        private string moduleName = "Master Card Name";

        private string connParam = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private string criteria, query, card_Name;

        public CardPage()
        {
            InitializeComponent();
            conn = new OleDbConnection(connParam);
            cbCardType_Bind();
            dgCard_Bind();
            CheckAccess();
        }

        private void cbCardType_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "Select * From MasterCardType Where 1=1 AND Deleted = 0";
                cmd.CommandType = CommandType.Text;

                using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        DataRow row = dt.NewRow();
                        row["Card_Type_Name"] = "ALL";
                        row["ID"] = 0;
                        dt.Rows.Add(row);
                        DataView dv = dt.DefaultView;
                        dv.Sort = "ID asc";
                        cbCardType.ItemsSource = dv;
                        cbCardType.SelectedValue = 0;
                        
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

        public void RefreshPage()
        {
            dgCard_Bind();
        }

        private void dgCard_Bind()
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                query = "Select mcn.*, mct.Card_Type_Name ";
                query = query + "From MasterCardName mcn LEFT JOIN (Select * From MasterCardType Where Deleted = 0) mct ON mcn.Card_Type_ID = mct.ID Where 1=1 AND mcn.Deleted = 0 " + criteria;
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                dgCard.DataContext = pub.BindDG(cmd);

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
                criteria = " AND Bin_No LIKE '%" + txtBinNo.Text + "%'";
                if (!string.IsNullOrEmpty(txtCardName.Text))
                    criteria = criteria + " AND Card_Name LIKE '%" + txtCardName.Text + "%' ";
                if (cbCardType.SelectedValue.ToString() != "0")
                    criteria = criteria + " AND Card_Type_ID = " + cbCardType.SelectedValue + "";

                dgCard_Bind();
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
            txtBinNo.Text = string.Empty;
            txtCardName.Text = string.Empty;
            cbCardType.SelectedValue = 0;
            criteria = string.Empty;
            dgCard_Bind();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CardDetailWindow cardDetail = new CardDetailWindow(this);
            cardDetail.lblTitle.Text = "Card - Create";
            cardDetail.lblID.Text = string.Empty;
            cardDetail.txtBinNo.Text = string.Empty;
            cardDetail.txtCardName.Text = string.Empty;
            cardDetail.txtCardDescription.Text = string.Empty;
            cardDetail.txtDigitUniqueCard.Text = string.Empty;
            cardDetail.gbDetail.Visibility = Visibility.Hidden;
            cardDetail.ShowDialog();
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ID = ((FrameworkElement)sender).DataContext;
                DataRowView tab = (DataRowView)ID;
                CardDetailWindow cardDetail = new CardDetailWindow(this);
                //fill data
                card_Name = tab["Card_Name"].ToString();
                cardDetail.txtBinNo.Text = tab["Bin_No"].ToString();
                if (tab["Card_Name"] != null)
                    cardDetail.txtCardName.Text = tab["Card_Name"].ToString();
                if (tab["Card_Description"] != null)
                    cardDetail.txtCardDescription.Text = tab["Card_Description"].ToString();
                if (tab["Digit_Unique_Card"] != null)
                    cardDetail.txtDigitUniqueCard.Text = tab["Digit_Unique_Card"].ToString();
                if (tab["Card_Type_ID"] != null)
                    cardDetail.cbCardType.SelectedValue = tab["Card_Type_ID"].ToString();

                if (tab["CreatedBy"] != null)
                    cardDetail.lblCreatedByValue.Text = tab["CreatedBy"].ToString();
                if (tab["CreatedDate"] != null)
                    cardDetail.lblCreatedDateValue.Text = tab["CreatedDate"].ToString();
                if (tab["UpdatedBy"] != null)
                    cardDetail.lblUpdatedByValue.Text = tab["UpdatedBy"].ToString();
                if (tab["UpdatedDate"] != null)
                    cardDetail.lblUpdatedDateValue.Text = tab["UpdatedDate"].ToString();

                cardDetail.lblTitle.Text = "Card - View";
                //enable/disable control
                cardDetail.txtBinNo.IsEnabled = false;
                cardDetail.txtCardName.IsEnabled = false;
                cardDetail.txtCardDescription.IsEnabled = false;
                cardDetail.txtDigitUniqueCard.IsEnabled = false;
                cardDetail.cbCardType.IsEnabled = false;

                cardDetail.gbDetail.Visibility = Visibility.Visible;
                cardDetail.btnSave.Visibility = Visibility.Hidden;

                cardDetail.ShowDialog();
            }
            catch { }
            finally
            {
                try
                {
                    pub.Audit_Trail(conn, cmd, "Master Card Name", "View", "Card Name : " + card_Name);
                    card_Name = string.Empty;
                }
                catch { }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            CardDetailWindow cardDetail = new CardDetailWindow(this);
            //fill data
            cardDetail.lblID.Text = tab["ID"].ToString();
            cardDetail.txtBinNo.Text = tab["Bin_No"].ToString();
            if (tab["Card_Name"] != null)
                cardDetail.txtCardName.Text = tab["Card_Name"].ToString();
            if (tab["Card_Description"] != null)
                cardDetail.txtCardDescription.Text = tab["Card_Description"].ToString();
            if (tab["Digit_Unique_Card"] != null)
                cardDetail.txtDigitUniqueCard.Text = tab["Digit_Unique_Card"].ToString();
            if (tab["Card_Type_ID"] != null)
                cardDetail.cbCardType.SelectedValue = tab["Card_Type_ID"].ToString();

            cardDetail.lblTitle.Text = "Card - Update";
            //enable/disable control
            cardDetail.txtBinNo.IsEnabled = false;
            cardDetail.txtCardName.IsEnabled = true;
            cardDetail.txtCardDescription.IsEnabled = true;
            cardDetail.txtDigitUniqueCard.IsEnabled = true;
            cardDetail.cbCardType.IsEnabled = true;

            cardDetail.gbDetail.Visibility = Visibility.Visible;
            cardDetail.btnSave.Visibility = Visibility.Visible;

            cardDetail.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var ID = ((FrameworkElement)sender).DataContext;
            DataRowView tab = (DataRowView)ID;
            card_Name = tab["Card_Name"].ToString();
            MessageBoxResult result = MessageBox.Show("Are you sure to delete this data ?", "Confirmation Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE MasterCardName SET Deleted = 1 Where ID = " + tab["ID"].ToString();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    dgCard_Bind();
                }
                catch { }
                finally
                {
                    try
                    {
                        pub.Audit_Trail(conn, cmd, "Master Card Name", "Delete", "Card Name : " + card_Name);
                        card_Name = string.Empty;
                    }
                    catch { }
                }
            }
        }

        private void CheckAccess()
        {
            pub.CheckAccess(moduleName, dgCard, btnCreate);
        }
    }
}
