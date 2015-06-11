using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Als
{
    class PermissionHelper
    {
        public static void VerifyWindowOpen(ObjectAccessDeniedException acc, string objectName)
        {
            LoginBox lgb = new LoginBox(objectName, acc.RequiredPermissionLevel);
            lgb.ShowDialog();
            if (!lgb.IsVerified)
            {
                MessageBox.Show("Ensure you have sufficient permissions to perform this action. "
                    + "Please contact your system administrator for further assistance.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
