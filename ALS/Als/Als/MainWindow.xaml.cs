using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
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
using System.Security.Permissions;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading;


namespace Als
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   [ObjectPermissionAttribute(SecurityAction.Demand, RequiredPermissionAccess = ObjectPermissionAccess.Guest)]
    public partial class MainWindow : CustomChromeLibrary.CustomChromeWindow
    {
        BackgroundWorker appThread2 = new BackgroundWorker();
        
        //string selectStr = "USE SamkarHardware\r\n" + "SELECT EmployeeIdHash FROM master.Hashes";
        public MainWindow()
        {
            try
            {

                appThread2.DoWork += new DoWorkEventHandler(appThread2_DoWork);
                appThread2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(appThread2_RunWorkerCompleted);
                appThread2.WorkerSupportsCancellation = true;
                appThread2.WorkerReportsProgress = false;

                InitializeComponent();
                RegistryHelper.CreateKeys();
                textBlock3.DataContext = ((App)Application.Current).CurrentUserInfo;
            }
            catch
            {
                MessageBox.Show("An error occurred when loading application modules. The application will now terminate. \r\n"+
                    "Try restarting the application. If it persists contact your system administrator. \r\nERROR DETAILS: "+
                    "File load Error; Main Window InitFailure.");
                Application.Current.Shutdown(10);

            }

        }

        private void VerifyWindowOpen(ObjectAccessDeniedException acc,string objectName)
        {
            LoginBox lgb = new LoginBox(objectName, acc.RequiredPermissionLevel);
            lgb.ShowDialog();
            if (!lgb.IsVerified)
            {
                MessageBox.Show("Ensure you have sufficient permissions to perform this action. "
                    + "Please contact your system administrator for further assistance.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnClick_User(object sender, RoutedEventArgs e)
        {
            try
            {
                mainFrame.Navigate(new UserPage());
            }
            catch { }
        }

        private void OnClick_Role(object sender, RoutedEventArgs e)
        {
            try
            {
                mainFrame.Navigate(new RolePage(this));
            }
            catch { }
        }

        private void OnClick_DeleteAuditTrail(object sender, RoutedEventArgs e)
        {
            try
            {
                mainFrame.Navigate(new DeleteAuditTrailPage());
            }
            catch { }
        }
       
        private void OnClick_Report(object sender, RoutedEventArgs e)
        {
            
        }

        private void OnClick_CardType(object sender, RoutedEventArgs e)
        {
            try
            {
                mainFrame.Navigate(new CardTypePage());
            }
            catch { }
        }

        private void OnClick_Card(object sender, RoutedEventArgs e)
        {
            try
            {
                mainFrame.Navigate(new CardPage());
            }
            catch { }
        }

        private void OnClick_Bank(object sender, RoutedEventArgs e)
        {
            try
            {
                mainFrame.Navigate(new BankPage());
            }
            catch { }
        }

        private void OnClick_Vendor(object sender, RoutedEventArgs e)
        {
            try
            {
                mainFrame.Navigate(new VendorPage());
            }
            catch { }
        }

        private void OnClick_Merchant(object sender, RoutedEventArgs e)
        {
            try
            {
                mainFrame.Navigate(new MerchantPage(this));
            }
            catch { }
        }

        private void OnClick_MerchantPriceList(object sender, RoutedEventArgs e)
        {
            try
            {
                MerchantPage merchantPage = new MerchantPage(this);
                mainFrame.Navigate(new MerchantPriceListPage(this, merchantPage, "", ""));
            }
            catch { }
        }

        private void OnClick_GLAccount(object sender, RoutedEventArgs e)
        {
            try
            {
                mainFrame.Navigate(new GLAccountPage());
            }
            catch { }
        }

        private void OnClick_UploadExcel(object sender, RoutedEventArgs e)
        {
            try
            {
                mainFrame.Navigate(new MonthlyTransactionPage());
            }
            catch { }
        }

        private void OnClick_Uploadtext(object sender, RoutedEventArgs e)
        {
            try
            {
                mainFrame.Navigate(new DailyTransactionPage());
            }
            catch { }
        }

        private void OnClick_ApproveTransactiontext(object sender, RoutedEventArgs e)
        {
            try
            {
                mainFrame.Navigate(new ApprovalDailyTransactionPage());
            }
            catch { }
        }

        public void Member_Role(RolePage rolePage, string RoleID, string RoleName)
        {
            try
            {
                mainFrame.Navigate(new UserRolePage(this, rolePage, RoleID, RoleName));
            }
            catch { }
        }

        public void Role_Access(RolePage roleAccessPage, string RoleID, string RoleName)
        {
            try
            {
                mainFrame.Navigate(new RoleAccessPage(this, roleAccessPage, RoleID, RoleName));
            }
            catch { }
        }

        public void Merchant_Price_List(MerchantPage merchantPage, string merchantID, string merchantCode)
        {
            try
            {
                mainFrame.Navigate(new MerchantPriceListPage(this, merchantPage, merchantID, merchantCode));
            }
            catch { }
        }

        public void OnClick_MonthlyReport(object sender, RoutedEventArgs e)
        {
            try
            {
                mainFrame.Navigate(new MonthlyReportPage("Monthly"));
            }
            catch { }
        }

        public void OnClick_ComparisonTransactionReport(object sender, RoutedEventArgs e)
        {
            try
            {
                mainFrame.Navigate(new MonthlyReportPage("Comparison"));
            }
            catch { }
        }

        public void OnClick_MemoPayment(object sender, RoutedEventArgs e)
        {
            try
            {
                mainFrame.Navigate(new MemoPaymentPage());
            }
            catch { }
        }

        public void OnClick_AuditTrail(object sender, RoutedEventArgs e)
        {
            try
            {
                mainFrame.Navigate(new AuditTrailPage());
            }
            catch { }
        }

        private void OnClick_File_Exit(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch { }
        }

        private void CustomChromeWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Are you sure you would like to Exit?. \r " +
                "Any unsaved changes will be lost.\r Click NO to cancel this request.", this.Title, MessageBoxButton.YesNo,
                MessageBoxImage.Warning))
            {
                Application.Current.Shutdown(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void CustomChromeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //var uri = new Uri(@"pack://application:,,,/Als;component/Images/StandartChartered.jpg");
            //img.Source = new BitmapImage(uri);
            //textBlock3.Text = LoginWindow.LoginInfo.UserID; 
            
            
            //LoginWindow hy = new LoginWindow();
            //hy.ShowDialog();
            //Thread.Sleep(1000);
            //appThread2.RunWorkerAsync(selectStr);
            //RegUserInfo usrinf = new RegUserInfo();
            //usrinf.FirstName = LoginWindow.LoginInfo.UserID;
            //usrinf.PermissionAccess = ObjectPermissionAccess.Guest;
            //usrinf.EmployeeIdHash = Security.GetMd5Hash("randomHash");

            //RegistryHelper.CreateUser(usrinf);

            //RegUserInfo ghy =  RegistryHelper(usrinf.EmployeeIdHash);
            
        }
      
        private void appThread2_DoWork(object sender, DoWorkEventArgs e)
        {
            ObservableCollection<string> sqlresult;
            while (true)
            {
               sqlresult = DatabaseHelper.CopyFromDatabaseTableToObservableCollection((string)e.Argument);
            }
            //e.Result = sqlresult;
        }
        private void appThread2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //progressBar1.Visibility = System.Windows.Visibility.Hidden;
            //tbLoadingStatusBar.Visibility = System.Windows.Visibility.Hidden;

            if ((!e.Cancelled) && (e.Error == null))
            {
                if (e.Result != null)
                {
                    ObservableCollection<string> sqlResult = (ObservableCollection<string>)e.Result;
                    //((App)Application.Current).EmployeeIdHashCollection = sqlResult;
                }
            }            
        }

      

        private void textBlock3_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            if (textBlock3.Text != "Click here to Login")
            {
                textBlock3.Foreground = new SolidColorBrush(Colors.White);
                textBlock3.Cursor = Cursors.Arrow;
            }
            else
            {
                
                textBlock3.Foreground = new SolidColorBrush(Colors.LightPink);
                textBlock3.Cursor = Cursors.Hand;
            }
        }

        private void textBlock3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //LoginBox lgy = new LoginBox();
            //lgy.ShowDialog();
        }
        
       

        

        

        

       
        

        
        
        
    }
}
