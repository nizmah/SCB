using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Als
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public RegUserInfo CurrentUserInfo
        {
            get;
            set;
        }
        public void LogoffUser()
        {
        }

        public bool LogonUser(RegUserInfo regUser)
        {
            return false;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            RegUserInfo guestUser = new RegUserInfo();
            guestUser.EmployeeIdHash = "GuestIDNo." + (new Random().Next(1000) * new Random().Next(1000) * new Random().Next(1000)).ToString("0000000000");
            guestUser.PermissionAccess = ObjectPermissionAccess.SystemAdmin;
            guestUser.FirstName = LoginWindow.LoginInfo.UserID; 
            
            CurrentUserInfo = guestUser;
            try
            {
                LoginWindow log = new LoginWindow();
                log.Show();
                
                //MainWindow mn = new MainWindow();
                //mn.Show();
            }
            catch (ObjectAccessDeniedException jk)
            {
                MessageBox.Show(jk.Message);
                Application.Current.Shutdown(5);
            }
            catch
            {
                MessageBox.Show("other error");
            }
        }
    }
    
}
