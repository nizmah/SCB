using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using System.Collections.ObjectModel;

namespace Als
{
   
    public class DatabaseHelper
    {

        

        public static SqlQueryResult RestoreDefaultDbBackup(FileInfo bakFileInfo, string dPassword)
        {
            SqlQueryResult result = new SqlQueryResult();
            string commandText =
               "USE master\r\nALTER DATABASE [SamkarHardware] SET SINGLE_USER WITH NO_WAIT";
            string commandText2 =
                 "USE Master\r\nRESTORE DATABASE [SamkarHardware] FROM  DISK = N'" + bakFileInfo.FullName + "'" +
                 " WITH  FILE = 1, PASSWORD = N'" + dPassword + "',  NOUNLOAD,  REPLACE, " +
                 " STATS = 10";
            try
            {
                using (OleDbConnection sqlConn = new OleDbConnection(RegistryHelper.AdminConnectionString))
                {
                    sqlConn.Open();
                    OleDbCommand sqlcmd = new OleDbCommand(commandText, sqlConn);
                    sqlcmd.ExecuteNonQuery();
                    sqlcmd.CommandText = commandText2;
                    sqlcmd.ExecuteNonQuery();
                    sqlConn.Close();
                }
                result.Succeeded = true;
                return result;
            }
            catch (OleDbException sqlExc)
            {                
                result.Succeeded = false;
                result.dErrorMessage = sqlExc.Message;
                result.dErrorCode = "";//sqlExc.Number.ToString();
                result.dResultDataTable = null;
                return result;
            }

            catch (Exception lExc)
            {
                result.dErrorMessage = lExc.Message;
                result.dErrorCode = "1";
                result.dResultDataTable = null;
                return result;
            }
        }

        public static SqlQueryResult DataBaseExists()
        {
            try
            {
                string selectCtr = "use master\r\n select name from sysdatabases";
                SqlQueryResult custResults = ExecuteNonQueryWithResultTable(selectCtr);

                if (custResults == null)
                {
                    custResults = new SqlQueryResult();
                    custResults.Succeeded = false;
                    return custResults;
                }
                else if (custResults.Succeeded)
                    return custResults;

                else
                    return custResults;
            }
            catch
            {
                SqlQueryResult sqle = new SqlQueryResult();
                sqle.Succeeded = false;
                return sqle;
            }
        }

        public static SqlQueryResult CreateDefaultDbBackup(string dPassword, DirectoryInfo dirInf)
        {
            SqlQueryResult result = new SqlQueryResult();
            
           string commandText =
               "BACKUP DATABASE [SamkarHardware] TO  DISK = N'" + dirInf.Parent.FullName + "\\Default.bak'" +
               " WITH NOFORMAT, PASSWORD = N'"+dPassword+"',NOINIT,  NAME = N'SamkarHardware-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, "+
               " STATS = 10";
    try
            {
                using (OleDbConnection sqlConn = new OleDbConnection(RegistryHelper.AdminConnectionString))
                {
                    sqlConn.Open();
                    OleDbCommand sqlcmd = new OleDbCommand(commandText, sqlConn);
                    sqlcmd.ExecuteNonQuery();
                    sqlConn.Close();                    
                }
                result.Succeeded = true;
                    return result;
            }
            catch (OleDbException sqlExc)
            {
                result.Succeeded = false;
                result.dErrorMessage = sqlExc.Message;
                result.dErrorCode = "";// sqlExc.Number.ToString();
                result.dResultDataTable = null;
                return result;
            }
                
            catch (Exception lExc)
            {
                result.dErrorMessage = lExc.Message;
                result.dErrorCode = "1";
                result.dResultDataTable = null;
                return result;
            }
        }

        public static SqlQueryResult ExecuteNonQueryWithResultTable(string commandText)
        {
            SqlQueryResult result = new SqlQueryResult();
            
            try
            {
                using (OleDbConnection sqlConn = new OleDbConnection(RegistryHelper.ConnectionString))
                {
                    sqlConn.Open(); 
                    OleDbDataAdapter dta = new OleDbDataAdapter(commandText, sqlConn);

                    DataSet dtSet = new DataSet();
                    dta.Fill(dtSet);
                    sqlConn.Close();

                    result.dResultDataTable = dtSet.Tables[0];
                    result.Succeeded = true;
                    result.dErrorMessage = "0.0";
                }
                if ((result.dResultDataTable == null) || (result.dResultDataTable.Rows == null) || (result.dResultDataTable.Rows.Count == 0))
                    return null;
                else
                    return result;
            }
            catch (OleDbException sqlExc)
            {
                result.Succeeded = false;
                result.dErrorMessage = sqlExc.Message;
                result.dErrorCode = "";// sqlExc.Number.ToString();
                result.dResultDataTable = null;
                return result;
            }
                
            catch (Exception lExc)
            {
                result.dErrorMessage = lExc.Message;
                result.dErrorCode = "1";
                result.dResultDataTable = null;
                return result;
            }
            
        }

        
        public static SqlQueryResult ExecuteNonQuery(string commandText)
        {
            SqlQueryResult result = new SqlQueryResult();

            try
            {
                using (OleDbConnection sqlConn = new OleDbConnection(RegistryHelper.ConnectionString))
                {
                    sqlConn.Open();
                    OleDbCommand dta = new OleDbCommand(commandText, sqlConn);
                    dta.ExecuteNonQuery();
                    sqlConn.Close();

                    result.dResultDataTable = null;
                    result.Succeeded = true;
                    result.dErrorMessage = "0.0";
                }
                
                    return result;
            }
            catch (OleDbException sqlExc)
            {
                result.Succeeded = false;
                result.dErrorMessage = sqlExc.Message;
                result.dErrorCode = "";// sqlExc.Number.ToString();
                result.dResultDataTable = null;
                return result;
            }

            catch (Exception lExc)
            {
                result.dErrorMessage = lExc.Message;
                result.dErrorCode = "1";
                result.dResultDataTable = null;
                return result;
            }

        }

        public static SqlQueryResult SQLExecuteNonQuery(string commandText)
        {
            SqlQueryResult result = new SqlQueryResult();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(RegistryHelper.SQLConnectionString))
                {
                    sqlConn.Open();
                    SqlDataAdapter dta = new SqlDataAdapter(commandText, sqlConn);

                    DataSet dtSet = new DataSet();
                    dta.Fill(dtSet);
                    sqlConn.Close();

                    result.dResultDataTable = dtSet.Tables[0];
                    result.Succeeded = true;
                    result.dErrorMessage = "0.0";
                }
                if ((result.dResultDataTable == null) || (result.dResultDataTable.Rows == null) || (result.dResultDataTable.Rows.Count == 0))
                    return null;
                else
                    return result;
            }
            catch (SqlException sqlExc)
            {
                result.Succeeded = false;
                result.dErrorMessage = sqlExc.Message;
                result.dErrorCode = "";// sqlExc.Number.ToString();
                result.dResultDataTable = null;
                return result;
            }

            catch (Exception lExc)
            {
                result.dErrorMessage = lExc.Message;
                result.dErrorCode = "1";
                result.dResultDataTable = null;
                return result;
            }

        }

        public static SqlQueryResult RunAsAdminExecuteNonQuery(string commandText)
        {
            SqlQueryResult result = new SqlQueryResult();
            
            try
            {
                using (OleDbConnection sqlConn = new OleDbConnection(RegistryHelper.AdminConnectionString))
                {
                    sqlConn.Open();
                    OleDbDataAdapter dta = new OleDbDataAdapter(commandText,sqlConn);

                    DataSet dtSet = new DataSet();
                    dta.Fill(dtSet);
                    
                    sqlConn.Close();

                    result.dResultDataTable = dtSet.Tables[0];
                    result.Succeeded = true;
                    result.dErrorMessage = "0.0";
                }
                if ((result.dResultDataTable == null) || (result.dResultDataTable.Rows == null) || (result.dResultDataTable.Rows.Count == 0))
                return null; 
                else
                    return result;
            }
            catch (OleDbException sqlExc)
            {
                System.Windows.MessageBox.Show(sqlExc.Message);
                result.Succeeded = false;
                result.dErrorMessage = sqlExc.Message;
                result.dErrorCode = "";// sqlExc.Number.ToString();
                result.dResultDataTable = null;
                return result;
            }
                
            catch (Exception lExc)
            {
                
                result.dErrorMessage = lExc.Message;
                result.dErrorCode = "1";
                result.dResultDataTable = null;
                return result;
            }
            
        }

        public static ScalarQueryResult ExecuteScalar(string commandText)
        {
            ScalarQueryResult result = new ScalarQueryResult();
            try
            {
                using (OleDbConnection sqlConn = new OleDbConnection(RegistryHelper.ConnectionString))
                {
                    OleDbCommand sqlcmd = new OleDbCommand(commandText, sqlConn);
                    sqlConn.Open();
                    string tempStr = sqlcmd.ExecuteScalar().ToString();
                    result.sResult = tempStr;
                    sqlConn.Close();
                }
                return result;
            }
            catch (OleDbException sqlExc)
            {
                result.dErrorMessage = sqlExc.Message;
                result.dErrorCode = "";// sqlExc.Number.ToString();
                result.sResult = "";
                return result;
            }
            catch (Exception lExc)
            {
                result.dErrorMessage = lExc.Message;
                result.dErrorCode = "1";
                result.sResult = null;
                return result;
            }
        }
        public static ScalarQueryResult RunAsAdminExecuteScalar(string commandText)
        {
            ScalarQueryResult result = new ScalarQueryResult();
            try
            {
                using (OleDbConnection sqlConn = new OleDbConnection(RegistryHelper.AdminConnectionString))
                {
                    OleDbCommand sqlcmd = new OleDbCommand(commandText, sqlConn);
                    sqlConn.Open();
                    string tempStr = (string)sqlcmd.ExecuteScalar();
                    result.sResult = tempStr;
                    sqlConn.Close();
                }
                return result;
            }
            catch (OleDbException sqlExc)
            {
                result.dErrorMessage = sqlExc.Message;
                result.dErrorCode = "";// sqlExc.Number.ToString();
                result.sResult = "";
                return result;
            }
            catch (Exception lExc)
            {
                result.dErrorMessage = lExc.Message;
                result.dErrorCode = "1";
                result.sResult = null;
                return result;
            }
        }
        public ObservableCollection<string> xCopyFromDatabaseTableToObservableCollection(string selectString)
        {
            try
            {
                ObservableCollection<string> top50List = new ObservableCollection<string>();
                DataTable dtSet = DatabaseHelper.ExecuteNonQueryWithResultTable(selectString).dResultDataTable;
                if (dtSet != null)
                {
                    for (int i = 0; i < dtSet.Rows.Count; i++)
                    {
                        top50List.Add(dtSet.Rows[i][0].ToString());
                    }
                    return top50List;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        
        public static ObservableCollection<string> CopyFromDatabaseTableToObservableCollection(string selectString)
        {
            try
            {               
                ObservableCollection<string> top50List = new ObservableCollection<string>();
                DataTable dtSet = DatabaseHelper.ExecuteNonQueryWithResultTable(selectString).dResultDataTable;
                if (dtSet != null)
                {
                    for (int i = 0; i < dtSet.Rows.Count; i++)
                    {
                        top50List.Add(dtSet.Rows[i][0].ToString());
                    }
                    return top50List;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        public static ObservableCollection<string> RunAsAdminCopyFromDatabaseTableToObservableCollection(string selectString)
        {
            try
            {               
                ObservableCollection<string> top50List = new ObservableCollection<string>();
                DataTable dtSet = DatabaseHelper.RunAsAdminExecuteNonQuery(selectString).dResultDataTable;
                if (dtSet != null)
                {
                    for (int i = 0; i < dtSet.Rows.Count; i++)
                    {
                        top50List.Add(dtSet.Rows[i][0].ToString());
                    }
                    return top50List;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static ScalarQueryResult ExecuteScalarWithOpenConn(string commandText, ref OleDbConnection sqlConn)
        {
            ScalarQueryResult result = new ScalarQueryResult();
            try
            {
                    OleDbCommand sqlcmd = new OleDbCommand(commandText, sqlConn);
                    string tempStr = (string)sqlcmd.ExecuteScalar();
                    result.sResult = tempStr;
                
                return result;
            }
            catch (OleDbException sqlExc)
            {
                result.dErrorMessage = sqlExc.Message;
                result.dErrorCode = "";// sqlExc.Number.ToString();
                result.sResult = "";
                return result;
            }
            catch (Exception lExc)
            {
                result.dErrorMessage = lExc.Message;
                result.dErrorCode = "1";
                result.sResult = null;
                return result;
            }
        }
        public static SqlQueryResult ExecuteNonQueryWithOpenConn(string commandText, ref OleDbConnection sqlConn)
        {
            SqlQueryResult result = new SqlQueryResult();

            try
            {
                OleDbCommand dta = new OleDbCommand(commandText, sqlConn);
                    dta.ExecuteNonQuery();                    

                    result.dResultDataTable = null;
                    result.Succeeded = true;
                    result.dErrorMessage = "0.0";
                

                return result;
            }
            catch (OleDbException sqlExc)
            {
                result.Succeeded = false;
                result.dErrorMessage = sqlExc.Message;
                result.dErrorCode = "";// sqlExc.Number.ToString();
                result.dResultDataTable = null;
                return result;
            }

            catch (Exception lExc)
            {
                result.dErrorMessage = lExc.Message;
                result.dErrorCode = "1";
                result.dResultDataTable = null;
                return result;
            }

        }
        public static bool ValidateUser(string userName, string passord)
        {
            return false;
        }

        public static string GetNewID(string tableName)
        {
            //ft = sd.Substring(0, 5) + "." + sd.Substring(5, sd.Length - 5);
            string prefix = "SMK-";
            string prefix2 = tableName.Substring(5, 3) + "-";            
            string selectStr="USE SamkarHardware\r\n"+
            "SELECT COUNT(*) FROM "+tableName;
            if (!tableName.StartsWith("dbo."))
            {
                ScalarQueryResult fg = ExecuteScalar(selectStr);
                string finalStr = prefix + prefix2 + fg.sResult.PadLeft(12, '0'); ;
                return finalStr;
            }
            else
            {
                ScalarQueryResult fg = RunAsAdminExecuteScalar(selectStr);
                string finalStr = prefix + prefix2 + fg.sResult.PadLeft(12, '0'); 
                return finalStr;
            }
            
            
           
            
        }

        
    }
}
