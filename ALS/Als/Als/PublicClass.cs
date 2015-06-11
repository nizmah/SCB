using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Windows.Input;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Windows.Controls;
using System.Windows;

namespace Als
{
    public class PublicClass
    {

        public bool validateName(OleDbCommand cmd, string tableName, string columnName, string Value)
        {
            cmd.CommandText = "SELECT TOP 1 ID FROM " + tableName + " WHERE " + columnName + " = " + Value + " AND deleted = 0";
            if (cmd.ExecuteScalar() == null)
                return true;
            else
            {
                return false;
            }
        }

        public bool validateNameCategories(OleDbCommand cmd, string tableName, string[] columnName, string[] Value)
        {
            string query = "SELECT TOP 1 ID FROM " + tableName + " WHERE deleted = 0  ";

            for (int a = 0; a < columnName.Count(); a++ )
            {
                query = query + " AND " + columnName[a] + " = " + Value[a];
            }
            cmd.CommandText = query;
            if (cmd.ExecuteScalar() == null)
                return true;
            else
            {
                return false;
            }
        }

        public DataTable BindDG(OleDbCommand cmd)
        {
            using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
            {
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    DataTable table = new DataTable();
                    DataColumn col = table.Columns.Add("RowNo", typeof(int));
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrement = true;
                    table.Load(dt.CreateDataReader());

                    return table;
                }
            }
        }


        public DataTable DataViewAsDataTable(DataView dv)
        {
            DataTable dt = dv.Table.Clone();
            foreach (DataRowView drv in dv)
            {
                dt.ImportRow(drv.Row);
            }
            return dt;
        }

        public DataTable DataViewAsDataTableWithNo(DataView dv)
        {
            DataTable dt = dv.Table.Clone();
            foreach (DataRowView drv in dv)
            {
                dt.ImportRow(drv.Row);
            }
            DataTable table = new DataTable();
            DataColumn col = table.Columns.Add("RowNo", typeof(int));
            col.AutoIncrementSeed = 1;
            col.AutoIncrement = true;
            table.Load(dt.CreateDataReader());

            return table;
        }

        public void CheckIsNumeric(TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result)))
            {
                e.Handled = true;
            }
        }

        //Audit Trail function
        public void Audit_Trail(OleDbConnection conn, OleDbCommand cmd, string actMod, string actName, string actDesc)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;

                string createdBy = LoginWindow.LoginInfo.UserID;
                string createdDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                string query = "";

                query = "INSERT INTO UserLog (UserID, User_ID, User_Name, Action_Module, Action_Name, Action_Description, Action_Time) ";
                query = query + " SELECT ID, '" + createdBy + "', User_Name, '" + actMod + "', '" + actName + "', '" + actDesc + "', #" + createdDate + "# ";
                query = query + " FROM MasterUser WHERE Deleted = 0 AND User_ID = '" + createdBy + "'";

                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
            }
            finally
            {
                try
                {
                    conn.Close();
                }
                catch { }
            }
        }

        public void CheckAccess(string module_name, DataGrid gv, Button btnCreate)
        {
            if (LoginWindow.LoginInfo.UserID.ToLower() == "admin")
                return;

            DataRow row = LoginWindow.LoginInfo.accessTable.NewRow();
            if (LoginWindow.LoginInfo.accessTable.Rows.Count > 0)
            {
                row = LoginWindow.LoginInfo.accessTable.Select("Module_Name = '" + module_name + "'").FirstOrDefault();
            }

            //DataRow row = pub.Access(moduleName);
            if (row == null || string.IsNullOrEmpty(row[1].ToString()))
            {
                btnCreate.Visibility = Visibility.Hidden;
                gv.Columns[gv.Columns.Count - 1].Visibility = Visibility.Hidden;
                gv.Columns[gv.Columns.Count - 2].Visibility = Visibility.Hidden;
                gv.Columns[gv.Columns.Count - 3].Visibility = Visibility.Hidden;
            }
            else
            {
                if (!(bool)row["Create_Check"])
                    btnCreate.Visibility = Visibility.Hidden;
                if (!(bool)row["Update_Check"])
                    gv.Columns[gv.Columns.Count - 3].Visibility = Visibility.Hidden;
                if (!(bool)row["Delete_Check"])
                    gv.Columns[gv.Columns.Count - 1].Visibility = Visibility.Hidden;
                if (!(bool)row["View_Check"])
                    gv.Columns[gv.Columns.Count - 2].Visibility = Visibility.Hidden;
            }
            //return row;
        }

        public DataRow Access(string module_name)
        {
            DataRow row = LoginWindow.LoginInfo.accessTable.NewRow();
            if (LoginWindow.LoginInfo.UserID.ToLower() == "admin")
            {
                row[0] = 0; row[1] = module_name; row[2] = true; row[3] = true; row[4] = true; row[5] = true; row[6] = true;
                return row;
            }
            
            if (LoginWindow.LoginInfo.accessTable.Rows.Count > 0)
            {
                row = LoginWindow.LoginInfo.accessTable.Select("Module_Name = '" + module_name + "'").FirstOrDefault();
            }

            return row;
        }

        public void SuccessMessage(string action)
        {
            MessageBox.Show("Data "+ action);
        }
    }
}
