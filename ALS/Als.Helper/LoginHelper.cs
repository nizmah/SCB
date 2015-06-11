using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Als
{
    public class LoginHelper : INotifyPropertyChanged
    {
        RegUserInfo regInfo = null;
        string employeeIdHash;
        ObjectPermissionAccess permissionAccess;
        string firstName;
        bool isValidUser = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginHelper(string userName, string password)
        {

            PropertyChanged += new PropertyChangedEventHandler(LoginHelper_PropertyChanged);
        }
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
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
        public bool IsValidUser
        {
            get { return isValidUser; }
            set
            {
                if (value != this.isValidUser)

                    this.isValidUser = value;
                NotifyPropertyChanged("IsValidUser");
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
        private void LoginHelper_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

    public class LoginInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string employeeIdHash;
        ObjectPermissionAccess permissionAccess;
        bool isValidUser = false;
        string firstName;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
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
        public bool IsValidUser
        {
            get { return isValidUser; }
            set
            {
                if (value != this.isValidUser)

                    this.isValidUser = value;
                NotifyPropertyChanged("IsValidUser");
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
    }
}