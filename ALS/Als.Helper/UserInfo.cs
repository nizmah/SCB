using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Als
{
    public class RegUserInfo : INotifyPropertyChanged
    {
        string employeeIdHash;
        ObjectPermissionAccess permissionAccess;
        string firstName;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public RegUserInfo()
        {
            PropertyChanged += new PropertyChangedEventHandler(RegUserInfo_PropertyChanged);
        }
        public string EmployeeIdHash
        {
            get { return employeeIdHash; }
            set
            {
                if (value != this.employeeIdHash)

                    this.employeeIdHash = value;
                NotifyPropertyChanged("EmployeeIdHash");
            }
        }
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (value != this.employeeIdHash)

                    this.firstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }
        public ObjectPermissionAccess PermissionAccess
        {
            get { return permissionAccess; }
            set
            {
                if (value != this.permissionAccess)

                    this.permissionAccess = value;
                NotifyPropertyChanged("PermissionAccess");
            }
        }
        private void RegUserInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "EmployeeIdHash")
            {
                if (String.IsNullOrWhiteSpace(employeeIdHash))
                {
                    employeeIdHash = null;
                    firstName = null;
                }
            }
            if (e.PropertyName == "FirstName")
            {
                if (String.IsNullOrWhiteSpace(firstName))
                {
                    employeeIdHash = null;
                    firstName = null;
                }
            }
        }
    }

    
}
