using System;
using System.Security;
using System.Collections.Generic;
using Microsoft.Win32;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Data.OleDb;
using System.Collections.ObjectModel;
using System.Windows;


namespace Als
{
    public class RegistryHelper
    {
        public enum CriticalPropName
        {
            EmployeeIdHash = 1, Salt = 2, FirstName = 3, PermissionAccess = 4
        }
        public static string GetMostRecentBackup()
        {
            string tempStr = "";
            try
            {
                RegistryKey MainKey =
                    Registry.CurrentUser.OpenSubKey("Software", true).OpenSubKey("Samkar Hardware");
                RegistryKey
                        ProgKey = MainKey.OpenSubKey("Settings");
                using (RegistryKey
                     trialKey = ProgKey.OpenSubKey("BackupRestore", true))
                {
                    tempStr = trialKey.GetValue("RCBackup").ToString();
                }
                return tempStr;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// Gets the Property values of Critical Properties:
        /// 1. EmplyeeIDHash
        /// 2. Salt (This is the keyname of the hashing subkey) and Salt Valuename
        /// 3. First Name
        /// 4. PermissionAccess 
        /// NB: In creating a CriticalProperty atleast two keys are created and at most 3. 
        /// </summary>
        /// <param name="propertynumber"></param>
        /// <param name="keySaltHashKey"></param>
        /// <returns></returns>
        private static ObservableCollection<string> GetRawCgriticalPropertyValues(int propertynumber)
        {
            try
            {
                using (RegistryKey MainKey =
                    Registry.CurrentUser.OpenSubKey("Software", true).OpenSubKey("Samkar Hardware"))
                {
                    RegistryKey
                            ProgKey = MainKey.OpenSubKey("CritiCalPropertyStore");

                    string[] existingUsres = ProgKey.GetSubKeyNames();
                    ObservableCollection<string> criticalProps = new ObservableCollection<string>();
                    if (existingUsres != null)
                        if (existingUsres.Length != 0)
                            foreach (string kename in existingUsres)
                            {
                                if (ProgKey.OpenSubKey(kename).GetValueNames().Length == 6)
                                {
                                    criticalProps.Add((string)ProgKey.OpenSubKey(kename).GetValue(
                                         ProgKey.OpenSubKey(kename).GetValueNames()[propertynumber]));
                                }
                            }
                    return criticalProps;
                }
            }
            catch { return null; }
        }

        /// <summary>
        /// Gets the salt key name for the critical property
        /// </summary>
        /// <param name="criticalProprtykeyname"></param>
        /// <returns></returns>
        private static string GetSaltKeyNameForCriticalProperty(string criticalProprtykeyname)
        {
            string[] tempStr = null;
            string saltTempKyName = null;

            using (RegistryKey MainKey =
               Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Samkar Hardware"))
            {
                RegistryKey
                    ProgKey = MainKey.OpenSubKey("CritiCalPropertyStore");
                RegistryKey
                        trialKey = ProgKey.OpenSubKey(criticalProprtykeyname);


                tempStr = trialKey.GetValueNames();
                if (tempStr == null)
                { return null; }
                if (tempStr.Length < 3)
                { return null; }

                saltTempKyName = (string)trialKey.GetValue(tempStr[1]);

                if (String.IsNullOrWhiteSpace(saltTempKyName))
                    return null;

            }
            return saltTempKyName;
        }
        /// <summary>
        /// Gets the salt key array containing the Key
        /// </summary>
        /// <param name="criticalProprtykeyname"></param>
        /// <returns></returns>
        private static byte[] GetSaltKeyForCriticalProperty(string criticalProprtykeyname)
        {
            
            try
            {
                string saltTempKyName = GetSaltKeyNameForCriticalProperty(criticalProprtykeyname);
                byte[] tempStr = null;
                string saltIVvalueName = "";

                using (RegistryKey MainKey =
                   Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Samkar Hardware"))
                {
                    RegistryKey
                               ProgKey = MainKey.OpenSubKey("CritiCalPropertyStore");
                    RegistryKey
                            trialKey = ProgKey.OpenSubKey(saltTempKyName);

                    saltIVvalueName = trialKey.GetValueNames()[0];
                    tempStr = (byte[])trialKey.GetValue(saltIVvalueName);
                }
                return tempStr;
            }
            catch (Exception hj)
            {
                MessageBox.Show("GetSaltKeyForCriticalProperty:" + hj.Message);
                return null;
            }
        }
        /// <summary>
        /// Gets the salt array containing the IV
        /// </summary>
        /// <param name="criticalProprtykeyname"></param>
        /// <returns></returns>
        private static byte[] GetSaltIVForCriticalProperty(string criticalProprtykeyname)
        {
            try
            {
                string saltTempKyName = GetSaltKeyNameForCriticalProperty(criticalProprtykeyname);

                byte[] tempStr = null;
                string saltIVvalueName = "";

                using (RegistryKey MainKey =
                   Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Samkar Hardware"))
                {
                    RegistryKey
                               ProgKey = MainKey.OpenSubKey("CritiCalPropertyStore");
                    RegistryKey
                            trialKey = ProgKey.OpenSubKey(saltTempKyName);

                    saltIVvalueName = trialKey.GetValueNames()[1];
                    tempStr = (byte[])trialKey.GetValue(saltIVvalueName);
                }
                return tempStr;
            }
            catch (Exception hj)
            {
                MessageBox.Show("GetSaltIVForCriticalProperty:" + hj.Message);
                return null;
            }
        }

        /// <summary>
        /// Accepts value 1 to 4 which are the critical property values to obtain and throws an exception for any other values
        /// </summary>
        /// <param name="propertynumber"></param>
        /// <param name="keyName"></param>
        /// <param name="valueName"></param>
        /// <returns></returns>
        private static object GetCriticalPropertyValue(CriticalPropName propertynumber, string keyName, string valueName)
        {

            object tempStr = null;
            try
            {
                RegistryKey MainKey =
                    Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Samkar Hardware");
                RegistryKey
                        ProgKey = MainKey.OpenSubKey("CritiCalPropertyStore");
                using (RegistryKey
                        trialKey = ProgKey.OpenSubKey(keyName))
                {
                    switch (propertynumber)
                    {
                        case CriticalPropName.EmployeeIdHash:
                            {
                                tempStr = trialKey.GetValue(valueName);
                                return tempStr;
                            }

                        case CriticalPropName.FirstName:
                            {
                                byte[] keybytes = GetSaltKeyForCriticalProperty(keyName);
                                byte[] IVbytes = GetSaltIVForCriticalProperty(keyName);

                                tempStr = Security.DecryptBytesToString((byte[])trialKey.GetValue(valueName), keybytes, IVbytes);
                                return tempStr;
                            }

                        case CriticalPropName.Salt:
                            {
                                tempStr = trialKey.GetValue(valueName);
                                return tempStr;
                            }

                        case CriticalPropName.PermissionAccess:
                            {
                                byte[] keybytes = GetSaltKeyForCriticalProperty(keyName);
                                byte[] IVbytes = GetSaltIVForCriticalProperty(keyName);

                                tempStr = Security.DecryptBytesToString((byte[])trialKey.GetValue(valueName), keybytes, IVbytes);
                                return tempStr;
                            }

                        default: break;
                    }
                }
                return tempStr;
            }
            catch (Exception hj)
            {
                MessageBox.Show("GetCriticalPropertyValue:" + hj.Message);
                return null;
            }

        }
        private static string GetCriticalPropertyValueName(CriticalPropName propertyIndex, string keyName)
        {
            string tempStr = null;
            string[] tempStrcoll = null;
            try
            {
                RegistryKey MainKey =
                    Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Samkar Hardware");
                RegistryKey
                        ProgKey = MainKey.OpenSubKey("CritiCalPropertyStore");
                using (RegistryKey
                        trialKey = ProgKey.OpenSubKey(keyName))
                {
                    tempStrcoll = trialKey.GetValueNames();

                    string[] allUsers = trialKey.GetValueNames();
                    if (allUsers != null)
                    {
                        if ((allUsers.Length > 2) && (allUsers.Length <= 4))
                        {
                            string tryValueName = allUsers[Int32.Parse(Enum.Format(typeof(CriticalPropName), propertyIndex, "d")) - 1];
                            return tryValueName;

                        }
                    }


                }
                return tempStr;
            }
            catch (Exception hj)
            {
                MessageBox.Show("GetCriticalPropertyValueName:" + hj.Message);
                return null;
            }

        }
        /// <summary>
        /// Gets the Name of the Registry SubKey  containing the specified value and value name.
        /// </summary>
        /// <param name="criticalPrValue"></param>
        /// <param name="criticalPrValueName"></param>
        /// <returns></returns>
        private static string GetCriticalPropertyKeyName(string criticalPrValue, string criticalPrValueName)
        {
            ObservableCollection<string> tempcoll = GetCriticalPropertyKeyNames();
            try
            {
                using (RegistryKey MainKey =
                    Registry.CurrentUser.OpenSubKey("Software", true).OpenSubKey("Samkar Hardware"))
                {
                    RegistryKey
                            ProgKey = MainKey.OpenSubKey("CritiCalPropertyStore");
                    foreach (string stu in tempcoll)
                    {
                        RegistryKey
                                trialkey = ProgKey.OpenSubKey(stu);
                        foreach (string gy in trialkey.GetValueNames())
                        {
                            if ((gy == criticalPrValueName) && (criticalPrValue == trialkey.GetValue(gy).ToString()))
                            {
                                return stu;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception hj)
            {
                MessageBox.Show("GetCriticalPropertyKeyName:" + hj.Message);
                return null;
            }

        }
        private static string GetCriticalPropertyKeyName(string criticalPrValue)
        {
            ObservableCollection<string> tempcoll = GetCriticalPropertyKeyNames();
            try
            {
                using (RegistryKey MainKey =
                    Registry.CurrentUser.OpenSubKey("Software", true).OpenSubKey("Samkar Hardware"))
                {
                    RegistryKey
                            ProgKey = MainKey.OpenSubKey("CritiCalPropertyStore");
                    foreach (string stu in tempcoll)
                    {
                        RegistryKey
                                trialkey = ProgKey.OpenSubKey(stu);
                        foreach (string gy in trialkey.GetValueNames())
                        {
                            string pv = trialkey.GetValue(gy).ToString();
                            if (criticalPrValue == pv)
                            {
                                return stu;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception hj)
            {
                MessageBox.Show("GetCriticalPropertyKeyName:" + hj.Message);
                return null;
            }

        }
        private static string GetCriticalPropertyKeyName(CriticalPropName propertyIndex, string criticalPrValue)
        {
            ObservableCollection<string> tempcoll = GetCriticalPropertyKeyNames();
            try
            {
                using (RegistryKey MainKey =
                    Registry.CurrentUser.OpenSubKey("Software", true).OpenSubKey("Samkar Hardware"))
                {
                    RegistryKey
                            ProgKey = MainKey.OpenSubKey("CritiCalPropertyStore");
                    foreach (string stu in tempcoll)
                    {
                        RegistryKey
                                trialkey = ProgKey.OpenSubKey(stu);
                        string[] allUsers = trialkey.GetValueNames();
                        if (allUsers != null)
                        {
                            if ((allUsers.Length > 2) && (allUsers.Length <= 4))
                            {
                                string tryValueName = allUsers[Int32.Parse(Enum.Format(typeof(CriticalPropName), propertyIndex, "d")) - 1];
                                if (criticalPrValue == trialkey.GetValue(tryValueName).ToString())
                                {
                                    return stu;
                                }
                            }
                        }

                    }
                }
                return null;
            }
            catch (Exception hj)
            {
                MessageBox.Show("GetCriticalPropertyKeyName(Enum):" + hj.Message);
                return null;
            }

        }
        private static ObservableCollection<string> GetCriticalPropertyKeyNames()
        {
            ObservableCollection<string> tempObs = new ObservableCollection<string>();
            try
            {
                using (RegistryKey MainKey =
                    Registry.CurrentUser.OpenSubKey("Software", true).OpenSubKey("Samkar Hardware"))
                {
                    RegistryKey
                            ProgKey = MainKey.OpenSubKey("CritiCalPropertyStore");

                    string[] existingUsres = ProgKey.GetSubKeyNames();
                    if (existingUsres != null)
                        if (existingUsres.Length != 0)
                            foreach (string kename in existingUsres)
                            {
                                tempObs.Add(kename);
                            }
                    return tempObs;
                }
            }
            catch (Exception hj)
            {
                MessageBox.Show("GetCriticalPropertyKeyNames:" + hj.Message);
                return null;
            }
        }
        public static RegUserInfo GetUserInfo(string userHash)
        {
            string currUserHash = userHash;
            string currUserKeyName = GetCriticalPropertyKeyName(CriticalPropName.EmployeeIdHash, currUserHash);
            string permAcValName = GetCriticalPropertyValueName(CriticalPropName.PermissionAccess, currUserKeyName);
            string FirstNmValName = GetCriticalPropertyValueName(CriticalPropName.FirstName, currUserKeyName);
            string permAccstr = GetCriticalPropertyValue(CriticalPropName.PermissionAccess, currUserKeyName, permAcValName).ToString();

            RegUserInfo userInf = new RegUserInfo();
            userInf.EmployeeIdHash = currUserHash;
            userInf.FirstName = GetCriticalPropertyValue(CriticalPropName.FirstName, currUserKeyName, FirstNmValName).ToString();
            userInf.PermissionAccess = (ObjectPermissionAccess)Enum.Parse(typeof(ObjectPermissionAccess), permAccstr, true);
            return userInf;
        }
        
        public static ObjectPermissionAccess GetUserPermission(RegUserInfo userinf)
        {
            string currUserKeyName = GetCriticalPropertyKeyName(CriticalPropName.EmployeeIdHash, userinf.EmployeeIdHash);
            string permAcValName = GetCriticalPropertyValueName(CriticalPropName.PermissionAccess, currUserKeyName);
            string permAccstr = GetCriticalPropertyValue(CriticalPropName.PermissionAccess, currUserKeyName, permAcValName).ToString();
            ObjectPermissionAccess temp = (ObjectPermissionAccess)Enum.Parse(typeof(ObjectPermissionAccess), permAccstr, true);
            return temp;
        }
        private static string CreateNewCriticalPropertyKey()
        {
            string keyName = Guid.NewGuid().ToString();
            try
            {
                using (RegistryKey MainKey =
                    Registry.CurrentUser.OpenSubKey("Software", true).OpenSubKey("Samkar Hardware"))
                {
                    RegistryKey
                            trialKey = MainKey.OpenSubKey("CriticalPropertyStore", true);
                    RegistryKey
                            trialKey2 = trialKey.CreateSubKey(keyName);


                }
                return keyName;
            }
            catch (Exception hj)
            {
                MessageBox.Show("CreateNewCriticalPropertyKey:" + hj.Message);
                return null;
            }
        }
        public static bool CreateKeys()
        {
            try
            {
                RegistryKey MainKey =
                    Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("Samkar Hardware");
                RegistryKey
                        ProgKey = MainKey.CreateSubKey("Settings");
                RegistryKey
                        ProgKey2 = MainKey.CreateSubKey("CriticalPropertyStore");
                RegistryKey
                   trial3Key = ProgKey.CreateSubKey("BackupRestore");
                using (RegistryKey
                      trialxKey = ProgKey.OpenSubKey("BackupRestore", true))
                {
                    trialxKey.SetValue("RCBackup", "");
                }
                RegistryKey
                    trial4Key = ProgKey.CreateSubKey("Users");
                RegistryKey
                     trial44Key = trial4Key.CreateSubKey("Users Roles");
                return true;
            }
            catch (Exception hj)
            {
                MessageBox.Show("CreateKeys:" + hj.Message);
                return false;
            }
        }
        /// <summary>
        /// Writes a new string property vale to an existing Critical Property
        /// </summary>
        /// <param name="WriteEmployeeIdHash"></param>
        /// <returns></returns>

        private static string CreateNewSaltKey(byte[] cryptKey, byte[] cryptIV)
        {
            string keyName = CreateNewCriticalPropertyKey();
            string newKeyName = Guid.NewGuid().ToString();
            try
            {
                using (RegistryKey MainKey =
                    Registry.CurrentUser.OpenSubKey("Software", true).OpenSubKey("Samkar Hardware"))
                {
                    RegistryKey
                            trialKey = MainKey.OpenSubKey("CriticalPropertyStore", true);
                    RegistryKey
                            trialKey2 = trialKey.OpenSubKey(keyName, true);
                    trialKey2.SetValue(newKeyName, cryptKey, RegistryValueKind.Binary);
                    newKeyName = Guid.NewGuid().ToString();
                    trialKey2.SetValue(newKeyName, cryptIV, RegistryValueKind.Binary);
                }
                return keyName;
            }
            catch (Exception hj)
            {
                MessageBox.Show("CreateNewSaltKey:" + hj.Message);
                return null;
            }
        }
        private static bool WriteNewPropertyValue(CriticalPropName propertyNumber, string newPropertyValue, string keyName)
        {
            try
            {
                using (RegistryKey
                 MainKey =
                    Registry.CurrentUser.OpenSubKey("Software", true).OpenSubKey("Samkar Hardware"))
                {
                    RegistryKey
                       trialKey = MainKey.OpenSubKey("CriticalPropertyStore", true);
                    RegistryKey trialKey2 = trialKey.OpenSubKey(keyName, true);

                    switch (propertyNumber)
                    {
                        case CriticalPropName.EmployeeIdHash:
                            {
                                string valueName = Guid.NewGuid().ToString();
                                trialKey2.SetValue(valueName, newPropertyValue, RegistryValueKind.String);
                            }
                            break;
                        case CriticalPropName.FirstName:
                            {
                                string valueName = Guid.NewGuid().ToString();
                                RijndaelManaged rji = new RijndaelManaged();

                                rji.GenerateKey(); rji.GenerateIV();
                                byte[] cryptKey = rji.Key;
                                byte[] cryptIV = rji.IV;
                                string saltKeyNameof = CreateNewSaltKey(cryptKey, cryptIV);
                                trialKey2.SetValue(valueName, saltKeyNameof, RegistryValueKind.String);

                                valueName = Guid.NewGuid().ToString();
                                trialKey2.SetValue(valueName, Security.EncryptStringToBytes(newPropertyValue, cryptKey, cryptIV), RegistryValueKind.Binary);

                            }
                            break;
                        case CriticalPropName.PermissionAccess:
                            {
                                byte[] cryptIV = GetSaltIVForCriticalProperty(keyName);
                                byte[] cryptKey = GetSaltKeyForCriticalProperty(keyName);
                                string valueName = Guid.NewGuid().ToString();
                                trialKey2.SetValue(valueName, Security.EncryptStringToBytes(newPropertyValue, cryptKey, cryptIV), RegistryValueKind.Binary);

                            }
                            break;

                        default: break;
                    }
                }
                return true;
            }
            catch (Exception hj)
            {
                MessageBox.Show("WriteNewPropertyValue:" + hj.Message);
                return false;
            }
        }


        public static string CreateUser(Als.RegUserInfo userInf)
        {
            string keyname = "";
            keyname = CreateNewCriticalPropertyKey();
            WriteNewPropertyValue(CriticalPropName.EmployeeIdHash, userInf.EmployeeIdHash, keyname);
            WriteNewPropertyValue(CriticalPropName.FirstName, userInf.FirstName, keyname);
            WriteNewPropertyValue(CriticalPropName.PermissionAccess, Enum.GetName(typeof(ObjectPermissionAccess),
                userInf.PermissionAccess), keyname);
            return keyname;
        }

        public static bool WriteMostRecentBackup(string pathToBackupRC)
        {
            try
            {
                RegistryKey MainKey =
                    Registry.CurrentUser.OpenSubKey("Software", true).OpenSubKey("Samkar Hardware", true);
                RegistryKey
                        ProgKey = MainKey.OpenSubKey("Settings", true);
                using (RegistryKey
                     trialKey = ProgKey.OpenSubKey("BackupRestore", true))
                {
                    trialKey.DeleteValue("RCBackup");
                    //"Driver={SQL Server};Server=(local);Trusted_Connection=Yes;Database=AdventureWorks;"
                    trialKey.SetValue("RCBackup", pathToBackupRC);
                }
                return true;
            }
            catch (Exception hj)
            {
                MessageBox.Show("WriteMostRecentBackup:" + hj.Message);
                return false;
            }
        }
        public static string ConnectionString
        {
            get { return @"Provider=SQLOLEDB.1;Data Source=Rafael-Hp\SamkarHardware;Password=000002;User ID=SamkarNewUser;Initial Catalog=SamkarHardware"; }

        }
        public static string SQLConnectionString
        {
            get { return @"Data Source=" + Environment.MachineName + "\\SamkarHardware;Password=000002;User ID=SamkarNewUser;Initial Catalog=SamkarHardware"; }

        }
        public static string AdminConnectionString
        {
            get { return @"Provider=SQLOLEDB.1;Data Source=" + Environment.MachineName + "\\SamkarHardware;Password=000002;User ID=sa;Initial Catalog=SamkarHardware"; }

        }

    }
}
