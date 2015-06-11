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
using System.Windows.Shapes; using System.Security.Permissions;

namespace Als
{
    /// <summary>
    /// Interaction logic for LoginBox.xaml
    /// </summary>
    [ObjectPermissionAttribute(SecurityAction.Demand, RequiredPermissionAccess = ObjectPermissionAccess.Guest)]
    public partial class LoginBox : Window
    {
        RegUserInfo currUser;
        ObjectPermissionAccess reqPerm;
        bool isAllowed = false;
        public LoginBox(string moduleName, ObjectPermissionAccess requiredPermission)
        {
            
            InitializeComponent();
            tbModName.Text = moduleName;
            tbReqPerm.Text = requiredPermission.ToString();
            try
            {
                tbCurrentPerm.Text = ((App)Application.Current).CurrentUserInfo.PermissionAccess.ToString();
            }
            catch 
            {
                tbCurrentPerm.Text = "Not Logged in.";
                tbCurrentPerm.Background = new SolidColorBrush(Colors.Red);
            }
            //LoginHelper lgh = new LoginHelper(moduleName,requiredPermission);
            //this.DataContext = lgh.
        }

        private void PasswordHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private bool ValidateUserAuthority(RegUserInfo userInf, ObjectPermissionAccess reqPermission)
        {
            return false;
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public bool IsVerified
        {
            get { return true; }
            set { isAllowed = value; }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            tbIvalidPwd.Visibility = System.Windows.Visibility.Hidden;
            this.Cursor = Cursors.AppStarting;
            //if ((ValidateUserAuthority(currUser, reqPerm)) && (DatabaseHelper.ValidateUser("", "")))
            {
                ((App)Application.Current).LogoffUser();
                ((App)Application.Current).LogonUser(null);
                this.Cursor = Cursors.Arrow;
                this.isAllowed = true;
                this.Visibility = System.Windows.Visibility.Collapsed;
            }
            //else
            {
                //tbIvalidPwd.Visibility = System.Windows.Visibility.Visible;
                //this.Cursor = Cursors.Arrow;
            }
        }
    }
}
