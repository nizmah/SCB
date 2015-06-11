using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Collections.ObjectModel;

namespace Als
{
    ////public partial class DatabaseHelper
    //{
        public class Stock
        {
           /* public static int GetAllInStock(string ItemID)
            {


                DataSet SaleSet = new DataSet();
                DataSet StockSet = new DataSet();

                int tries = 0;
                bool IsConnecting = true;
                #region //get total stock

                while (IsConnecting)
                {
                    tries++;
                    try
                    {
                        string SEARCH_STR = "SELECT Quantity FROM Stock WHERE ItemID = '" + ItemID + "'";
                       // string constr = ConnString.GetConnectionString();
                        OleDbConnection sqlConn = new OleDbConnection(constr);
                        //bool h = SqlContext.IsAvailable;
                        OleDbCommand sqlCmd = new OleDbCommand("USE HSBFS_Restorante" + Environment.NewLine + SEARCH_STR, sqlConn);
                        sqlConn.Open();
                        sqlCmd.ExecuteNonQuery();
                        OleDbDataAdapter dt = new OleDbDataAdapter(sqlCmd);
                        dt.Fill(StockSet, "Stock");
                        IsConnecting = false;
                    }
                    catch
                    {
                        if (tries > 10)
                        {
                            IsConnecting = false;
                        }
                    }
                }

                int sumStockSet = 0;
                //get sum in stock table
                if (StockSet.Tables.Count != 0)
                {
                    if (StockSet.Tables[0].Rows.Count != 0)
                    {
                        for (int i = 0; i < StockSet.Tables[0].Rows.Count; i++)
                        {
                            sumStockSet = sumStockSet + Convert.ToInt32(StockSet.Tables[0].Rows[i][0].ToString());
                        }
                    }
                }
                #endregion
                tries = 0;
                IsConnecting = true;
                #region//get total Sales
                while (IsConnecting)
                {
                    tries++;

                    try
                    {
                        string SEARCH_STR = "SELECT Quantity FROM Sales WHERE ItemID = '" + ItemID + "'";
                        if (ConnString.CheckConfigurationFile())
                        {
                            string constr = ConnString.GetConnectionString();
                            OleDbConnection sqlConn = new OleDbConnection(constr);
                            OleDbCommand sqlCmd = new OleDbCommand("USE HSBFS_Restorante" + Environment.NewLine + SEARCH_STR, sqlConn);
                            sqlConn.Open();
                            sqlCmd.ExecuteNonQuery();
                            OleDbDataAdapter dt = new OleDbDataAdapter(sqlCmd);
                            dt.Fill(SaleSet, "Sales");
                            IsConnecting = false;
                        }
                    }
                    catch
                    {
                        if (tries > 10)
                        {
                            IsConnecting = false;
                        }
                    }
                }
                int sumSalesSet = 0;


                if (SaleSet.Tables.Count != 0)
                {
                    if (SaleSet.Tables[0].Rows.Count != 0)
                    {
                        for (int i = 0; i < SaleSet.Tables[0].Rows.Count; i++)
                        {
                            sumSalesSet = sumSalesSet + Convert.ToInt32(SaleSet.Tables[0].Rows[i][0].ToString());
                        }
                    }
                }
                #endregion

                return sumStockSet - sumSalesSet;
            }*/
        }
   // }
}
