using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Windows;
using System.Diagnostics;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices.ComTypes;
using System.ComponentModel;


namespace Als
{
    public class ExcelHelper
    {
        /// <summary>
        /// Variable is used to store Excel's Application Object.
        /// </summary>
        private Excel.Application xlAppObj = null;

        /// <summary>
        /// Variable is used to store Excel's WorkBook Object.
        /// </summary>
        private Excel.Workbook xlWorkBookObj = null;

        /// <summary>
        /// Variable is used to store Excel's WorkSheet Object.
        /// </summary>
        private Excel.Worksheet xlWorkSheetObj = null;

        
        /// <summary>
        /// Variable is used to store excel file's path.
        /// </summary>
        private string s_filePath = null;

        /// <summary>
        /// Variable is used to store excel's worksheet name.
        /// </summary>
        private string s_workSheetName = null;

        /// <summary>
        /// Variable is used to store excel's worksheet number.
        /// </summary>
        private int n_workSheetNo = 0;

        /// <summary>
        /// Generic's List is used to store all the running Excel
        /// process ID's before the creation of new Excel Application object is created.
        /// </summary>
        private List<int> l_initialProcessIds = null;

        /// <summary>
        /// Generic's List is used to store all the running Excel
        /// process ID's after the creation of new Excel Application object is created.
        /// </summary>
        private List<int> l_finalProcessIds = null;

        /// <summary>
        /// Variable is used to store the excel process ID.
        /// </summary>
        private int n_processId = 0;

        public int NumberOfWorksheets
        {
            get{return n_workSheetNo;}
        }

        /// <summary>
        /// Constructor is used to open the default worksheet of the excel file.
        /// </summary>
        /// <param name="filePath"></param>
        public ExcelHelper(string filePath)
        {
            s_filePath = filePath;
            n_workSheetNo = 1;              // "1" Default WorkSheetNo
        }

        /// <summary>
        /// Constructor is used to open specific worksheet in the excel file whose worksheet number 
        /// is specified in the constructor's parameter along with the full path of the excel file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="workSheetNo"></param>
        public ExcelHelper(string filePath, int workSheetNo)
        {
            s_filePath = filePath;
            n_workSheetNo = workSheetNo;
        }
        public int TotalNoOfColumns
        {
            get
            {
                int NumberOfColumns = 0;
                try
                {
                     NumberOfColumns = xlWorkSheetObj.Cells.Find("*", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlWhole, Excel.XlSearchOrder.xlByColumns,
                         Excel.XlSearchDirection.xlPrevious, false, false, Type.Missing).Column;
                    return NumberOfColumns;
                }
                catch
                {
                    return NumberOfColumns;
                }
            }
        }
        public int TotalNoOfRows
        {
            get
            {
                int NumberOfRows = 0;
                try
                {
                     NumberOfRows = xlWorkSheetObj.Cells.Find("*", Type.Missing, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlWhole, Excel.XlSearchOrder.xlByColumns,
                        Excel.XlSearchDirection.xlPrevious, false, false, Type.Missing).Row;
                     return NumberOfRows;
                }
                catch
                {
                    return NumberOfRows;
                }
            }
        }

        /// <summary>
        /// Constructor is used to open specific worksheet in the excel file whose worksheet name
        /// is specified in the constructor's parameter along with the full path of the excel file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="workSheetName"></param>
        public ExcelHelper(string filePath, string workSheetName)
        {
            s_filePath = filePath;
            s_workSheetName = workSheetName;
        }


        public Excel.Worksheet XlWorkSheetObj
        {
            get { return xlWorkSheetObj; }
        }


        /// <summary>
        /// Open Fn() is used to open the specific excel file whose name is provided in the paremeter of 
        /// the constructor and also fetches the process ID of the newly opened Excel process by taking 
        /// snapshot of all the running Excel processes before calling ApplicationObject.Workbooks.Open Fn() 
        /// and then take another snapshot after calling ApplicationObject.Workbooks.Open Fn() and finally
        /// calling the GetExcelProcessId Fn() to finally fetch the ID of the newly created Excel Process.
        /// </summary>
        public bool Open()
        {
            try
            {
                l_initialProcessIds = this.GetExcelProcessIdsSnapshot();
                xlAppObj = new Excel.ApplicationClass();
                xlWorkBookObj = xlAppObj.Workbooks.Open(s_filePath, 0, false, 5, System.Reflection.Missing.Value, System.Reflection.Missing.Value, false, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true);

                if (n_workSheetNo != 0 && s_workSheetName == null)
                {
                    xlWorkSheetObj = (Excel.Worksheet)xlWorkBookObj.Worksheets.get_Item(n_workSheetNo);

                    l_finalProcessIds = this.GetExcelProcessIdsSnapshot();
                    n_processId = this.GetExcelProcessId(l_initialProcessIds, l_finalProcessIds);
                }

                if (s_workSheetName != null && n_workSheetNo == 0)
                {
                    bool b_worksheetNameExist = true;
                    for (int n_loop = 1; n_loop <= xlWorkBookObj.Worksheets.Count; n_loop++)
                    {
                        xlWorkSheetObj = (Excel.Worksheet)xlWorkBookObj.Worksheets.get_Item(n_loop);
                        if (xlWorkSheetObj.Name == s_workSheetName)
                        {
                            b_worksheetNameExist = true;
                            break;
                        }
                        b_worksheetNameExist = false;
                    }

                    if (b_worksheetNameExist == true)
                    {
                        l_finalProcessIds = this.GetExcelProcessIdsSnapshot();
                        n_processId = this.GetExcelProcessId(l_initialProcessIds, l_finalProcessIds);
                    }
                    else
                    {
                        // Exception is on its way........ ting
                    }
                }
                return true;
            }
            catch 
            {                
                MessageBox.Show("Error Opening excel file. Please make sure you have "+
                    "sufficient privilidges to access this file and that it is not readonly or currently in use use in another application",""
                    ,MessageBoxButton.OK,MessageBoxImage.Information);
                return false;
            }
        }

        /// <summary>
        /// Close Fn() saves and closes the open workbook object.
        /// </summary>
        public void Close()
        {
            try
            {
                xlWorkBookObj.Save();
                xlWorkBookObj.Close(true, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
            }
            catch 
            {
                
            }
        }

        public bool UseWorkSheet(int workSheetNo)
        {
            try
            {
                xlWorkSheetObj = (Excel.Worksheet)xlWorkBookObj.Worksheets.get_Item(workSheetNo);
                return true;
            }
            catch
            {
                MessageBox.Show("Error Opening worksheet. Please make sure you have " +
                   "sufficient privilidges to access this file and that it is not readonly or currently in use use in another application", ""
                   , MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
        }

        #region ReleaseObjects

        // The runtime callable wrapper has a reference count that is incremented every time a COM interface 
        // pointer is mapped to it. The ReleaseComObject method decrements the reference count of a runtime callable
        // wrapper. When the reference count reached zero, the runtime releases all its references on the unmanaged 
        // COM object, and throws a System.NullReferenceException if you attempt to use the object further. If the 
        // same COM interface is passed more than once from unmanaged to managed code, the reference count on the 
        // wrapper is incremented every time and calling ReleaseComObject returns the number of remaining references.

        /// <summary>
        /// ReleaseWorkSheetObject Fn() releases the Excel.Worksheet object.
        /// </summary>
        private void ReleaseWorkSheetObject()
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(this.xlWorkSheetObj);
                this.xlWorkSheetObj = null;
            }
            catch 
            {
                
            }
        }

        /// <summary>
        /// ReleaseWorkBookObject Fn() releases the Excel.Workbook object.
        /// </summary>
        private void ReleaseWorkBookObject()
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(this.xlWorkBookObj);
                this.xlWorkBookObj = null;
            }
            catch 
            {
              
            }
        }


        /// <summary>
        /// ReleaseApplicationObject Fn() releases the Excel.Aapplication object.
        /// </summary>        
        private void ReleaseApplicationObject()
        {
            try
            {
                xlAppObj.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(this.xlAppObj);
                this.xlAppObj = null;
            }
            catch 
            {
                
            }
        }

        /// <summary>
        /// ReleaseObjects Fn() releases the Excel.WorkSheet, Excel.WorkBook and Excel.Application object  
        /// before calling the KillExcelProcessById Fn() to finally clean the Application Process.
        /// </summary>
        public void ReleaseObjects()
        {
            if (xlWorkSheetObj != null)
                this.ReleaseWorkSheetObject();

            if (xlWorkBookObj != null)
                this.ReleaseWorkBookObject();

            if (xlAppObj != null)
                this.ReleaseApplicationObject();

            if (n_processId != 0)
                this.KillExcelProcessById(n_processId);
        }

        #endregion

        #region Cleanly Kill Relevant Process

        /// <summary>
        /// GetExcelProcessIdsSnapshot Fn() gets a snapshot of all the Excel Processes 
        /// currently running in the memory when the Fn() is called.
        /// </summary>
        /// <returns>List<int></returns>
        private List<int> GetExcelProcessIdsSnapshot()
        {
            List<int> ProcessIds = new List<int>();

            try
            {
                Process[] Processes = Process.GetProcessesByName("Excel");

                for (int n_loop = 0; n_loop < Processes.Count(); n_loop++)
                    ProcessIds.Add(Processes.ElementAt(n_loop).Id);
            }
            catch 
            {
                
            }

            return ProcessIds;
        }

        /// <summary>
        /// GetExcelProcessId Fn() fetches the specific Excel Process ID which was created
        /// by comparing and substracting the two Generics based before  and after List of Process ID's
        /// </summary>
        /// <param name="l_initialProcessIds"></param>
        /// <param name="l_finalProcessIds"></param>
        /// <returns>int</returns>
        private int GetExcelProcessId(List<int> l_initialProcessIds, List<int> l_finalProcessIds)
        {
            try
            {
                for (int n_loop = 0; n_loop < l_initialProcessIds.Count; n_loop++)
                {
                    int n_PidInitialProcessList = l_initialProcessIds.ElementAt(n_loop);

                    for (int n_innerloop = 0; n_innerloop < l_finalProcessIds.Count; n_innerloop++)
                    {
                        int n_PidFinalProcessList = l_finalProcessIds.ElementAt(n_innerloop);
                        if (n_PidInitialProcessList == n_PidFinalProcessList)
                        {
                            l_finalProcessIds.RemoveAt(n_innerloop);
                            break;
                        }
                    }
                    l_initialProcessIds.RemoveAt(n_loop);
                    n_loop--;
                }
            }
            catch
            {
                
            }

            return l_finalProcessIds.ElementAt(0);
        }

        /// <summary>
        /// KillExcelProcessById Fn() is used to kill the specific process whose Process ID is passed.
        /// </summary>
        /// <param name="n_processId"></param>
        private void KillExcelProcessById(int n_processId)
        {
            try
            {
                Process xlProcess = null;
                xlProcess = Process.GetProcessById(n_processId);
                xlProcess.Kill();
            }
            catch
            {
                  }
        }
        #endregion

        # region Read Operations

        /// <summary>
        /// Fn() is used to read value of type integer, from the cell whose row and column ID is passed.
        /// </summary>
        /// <param name="rowId"></param>
        /// <param name="colId"></param>
        /// <returns>int</returns>
        public int ReadFromCellInteger(int rowId, int colId)
        {
            int cellValue = 0;
            try
            {
                cellValue = Convert.ToInt32(this.ReadFromCell(rowId, colId));
            }
            catch 
            {
                
            }

            return cellValue;
        }

        /// <summary>
        /// Fn() is used to read value of type integer, from the cell whose index location is passed.
        /// </summary>
        /// <param name="indexLoc"></param>
        /// <returns>int</returns>
        public int ReadFromCellInteger(object indexLoc)
        {
            int cellValue = 0;
            try
            {
                cellValue = Convert.ToInt32(this.ReadFromCell(indexLoc));
            }
            catch 
            {
                
            }

            return cellValue;
        }

        /// <summary>
        /// Fn() is used to read value of type double, from the cell whose row and column ID is passed.
        /// </summary>
        /// <param name="rowId"></param>
        /// <param name="colId"></param>
        /// <returns>double</returns>
        public double ReadFromCellDouble(int rowId, int colId)
        {
            double cellValue = 0;
            try
            {
                cellValue = Convert.ToDouble(this.ReadFromCell(rowId, colId));
            }
            catch 
            {
                
            }

            return cellValue;
        }

        /// <summary>
        /// Fn() is used to read value of type double, from the cell whose index location is passed.
        /// </summary>
        /// <param name="indexLoc"></param>
        /// <returns>double</returns>
        public double ReadFromCellDouble(object indexLoc)
        {
            double cellValue = 0;
            try
            {
                cellValue = Convert.ToDouble(this.ReadFromCell(indexLoc));
            }
            catch 
            {
                
            }

            return cellValue;
        }

        /// <summary>
        /// Fn() is used to read value of type string, from the cell whose row and column ID is passed.
        /// </summary>
        /// <param name="rowId"></param>
        /// <param name="colId"></param>
        /// <returns>string</returns>
        public string ReadFromCellString(int rowId, int colId)
        {
            string cellValue = null;
            try
            {
                cellValue = Convert.ToString(this.ReadFromCell(rowId, colId));
            }
            catch 
            {
                
            }

            return cellValue;
        }

        /// <summary>
        /// Fn() is used to read value of type string, from the cell whose index location is passed.
        /// </summary>
        /// <param name="indexLoc"></param>
        /// <returns>string</returns>
        public string ReadFromCellString(object indexLoc)
        {
            string cellValue = null;
            try
            {
                cellValue = Convert.ToString(this.ReadFromCell(indexLoc));
            }
            catch 
            {
                
            }

            return cellValue;
        }

        /// <summary>
        /// Fn() is used to read value of type DateTime, from the cell whose row and column ID is passed.
        /// </summary>
        /// <param name="rowId"></param>
        /// <param name="colId"></param>
        /// <returns>DateTime</returns>
        public DateTime ReadFromCellDateTime(int rowId, int colId)
        {
            DateTime cellValue = DateTime.MinValue;
            try
            {
                cellValue = Convert.ToDateTime(this.ReadFromCell(rowId, colId));
            }
            catch 
            {
                
            }

            return cellValue;
        }

        /// <summary>
        /// Fn() is used to read value of type DateTime, from the cell whose index location is passed.
        /// </summary>
        /// <param name="indexLoc"></param>
        /// <returns>DateTime</returns>
        public DateTime ReadFromCellDateTime(object indexLoc)
        {
            DateTime cellValue = DateTime.MinValue;
            try
            {
                cellValue = Convert.ToDateTime(this.ReadFromCell(indexLoc));
            }
            catch 
            {
                
            }

            return cellValue;
        }

        /// <summary>
        /// Fn() is a general purpose function used to read value and return it as 
        /// object, from the cell whose row and column ID is passed.
        /// </summary>
        /// <param name="rowId"></param>
        /// <param name="colId"></param>
        /// <returns>object</returns>
        private object ReadFromCell(int rowId, int colId)
        {
            object cellValue = null;
            Excel.Range range = null;

            range = (Excel.Range)xlWorkSheetObj.UsedRange;
            cellValue = (object)(range.Cells[rowId, colId] as Excel.Range).Value2;

            return cellValue;
        }

        /// <summary>
        /// Fn() is a general purpose function used to read value and return it as 
        /// object, from the cell whose index location is passed.
        /// </summary>
        /// <param name="indexLoc"></param>
        /// <returns>object</returns>
        private object ReadFromCell(object indexLoc)
        {
            object cellValue = null;
            Excel.Range range = null;

            range = xlWorkSheetObj.get_Range(indexLoc, indexLoc);
            cellValue = range.Value2;

            return cellValue;
        }

        /// <summary>
        /// Fn() is used to read array of values and return as type object array, from the 
        /// cells whose startIndex and endIndex are passed.
        /// </summary>
        /// <param name="startIndexLoc"></param>
        /// <param name="endIndexLoc"></param>
        /// <returns>object</returns>
        public object[,] ReadFromCells(object startIndexLoc, object endIndexLoc)
        {
            object[,] cellValues = null;
            Excel.Range range = null;
            try
            {
                range = xlWorkSheetObj.get_Range(startIndexLoc, endIndexLoc);
                cellValues = (object[,])range.Value2;
            }
            catch 
            {
                MessageBox.Show("invalid range");
            }

            return cellValues;
        }

        #endregion

        # region Write Operations

        /// <summary>
        /// Fn() is used to write value of type integer, to the cell whose row and column ID's are passed.
        /// </summary>
        /// <param name="rowId"></param>
        /// <param name="colId"></param>
        /// <param name="cellValue"></param>
        public void WriteToCell(int rowId, int colId, int cellValue)
        {
            try
            {
                xlWorkSheetObj.Cells[rowId, colId] = cellValue;
            }
            catch 
            {
               
            }
        }

        /// <summary>
        /// Fn() is used to write value of type double, to the cell whose row and column ID's are passed.
        /// </summary>
        /// <param name="rowId"></param>
        /// <param name="colId"></param>
        /// <param name="cellValue"></param>
        public void WriteToCell(int rowId, int colId, double cellValue)
        {
            try
            {
                xlWorkSheetObj.Cells[rowId, colId] = cellValue;
            }
            catch 
            {
                
            }
        }

        /// <summary>
        /// Fn() is used to write value of type string, to the cell whose row and column ID's are passed.
        /// </summary>
        /// <param name="rowId"></param>
        /// <param name="colId"></param>
        /// <param name="cellValue"></param>
        public void WriteToCell(int rowId, int colId, string cellValue)
        {
            try
            {
                xlWorkSheetObj.Cells[rowId, colId] = cellValue;
            }
            catch 
            {
                
            }
        }

        /// <summary>
        /// Fn() is used to write value of type DateTime, to the cell whose row and column ID's are passed.
        /// </summary>
        /// <param name="rowId"></param>
        /// <param name="colId"></param>
        /// <param name="cellValue"></param>
        public void WriteToCell(int rowId, int colId, DateTime cellValue)
        {
            try
            {
                xlWorkSheetObj.Cells[rowId, colId] = cellValue;
            }
            catch
            {
               
            }
        }

        /// <summary>
        /// Fn() is used to write array values of type integers, to the cells whose startIndex and endIndex are passed.
        /// </summary>
        /// <param name="startIndexLoc"></param>
        /// <param name="endIndexLoc"></param>
        /// <param name="cellValues"></param>
        public void WriteToCells(object startIndexLoc, object endIndexLoc, int[,] cellValues)
        {
            Excel.Range range = null;

            try
            {
                range = xlWorkSheetObj.get_Range(startIndexLoc, endIndexLoc);
                range.Value = cellValues;
            }
            catch
            {
                
            }
        }

        /// <summary>
        /// Fn() is used to write array values of type double, to the cells whose startIndex and endIndex are passed.
        /// </summary>
        /// <param name="startIndexLoc"></param>
        /// <param name="endIndexLoc"></param>
        /// <param name="cellValues"></param>
        public void WriteToCells(object startIndexLoc, object endIndexLoc, double[,] cellValues)
        {
            Excel.Range range = null;

            try
            {
                range = xlWorkSheetObj.get_Range(startIndexLoc, endIndexLoc);
                range.Value = cellValues;
            }
            catch 
            {
               
            }
        }

        /// <summary>
        /// Fn() is used to write array values of type string, to the cells whose startIndex and endIndex are passed.
        /// </summary>
        /// <param name="startIndexLoc"></param>
        /// <param name="endIndexLoc"></param>
        /// <param name="cellValues"></param>
        public void WriteToCells(object startIndexLoc, object endIndexLoc, string[,] cellValues)
        {
            Excel.Range range = null;

            try
            {
                range = xlWorkSheetObj.get_Range(startIndexLoc, endIndexLoc);
                range.Value = cellValues;
            }
            catch 
            {
                
            }
        }

        /// <summary>
        /// Fn() is used to write array values of type DateTime, to the cells whose startIndex and endIndex are passed.
        /// </summary>
        /// <param name="startIndexLoc"></param>
        /// <param name="endIndexLoc"></param>
        /// <param name="cellValues"></param>
        public void WriteToCells(object startIndexLoc, object endIndexLoc, DateTime[,] cellValues)
        {
            Excel.Range range = null;

            try
            {
                range = xlWorkSheetObj.get_Range(startIndexLoc, endIndexLoc);
                range.Value = cellValues;
            }
            catch 
            {
                
            }
        }

        #endregion

        /// <summary>
        /// Fn() is used to read values from DataTable and write to excel sheet the values,
        /// starting with the row and column ID's are passed.
        /// </summary>
        /// <param name="startRowId"></param>
        /// <param name="startColId"></param>
        /// <param name="cellValues"></param>
        public void DataTableToExcel(int startRowId, int startColId, System.Data.DataTable cellValues)
        {
            try
            {
                int n_rowCnt = startRowId;
                int n_colCnt = startColId;

                // Writing Column Headings
                foreach (DataColumn dcol in cellValues.Columns)
                {
                    WriteToCell(n_rowCnt, n_colCnt, dcol.ColumnName.ToString());
                    n_colCnt++;
                }

                n_rowCnt++;
                n_colCnt = startColId;

                // Writing Row Data
                foreach (DataRow drows in cellValues.Rows)
                {
                    for (int n_loop = 0; n_loop <= cellValues.Columns.Count - 1; n_loop++)
                    {
                        if (drows[n_loop] == System.DBNull.Value)
                        {
                            WriteToCell(n_rowCnt, n_colCnt + (n_loop), String.Empty);
                        }
                        else
                        {
                            WriteToCell(n_rowCnt, n_colCnt + (n_loop), drows[n_loop].ToString());
                        }
                    }
                    ++n_rowCnt;
                }
            }
            catch 
            {
                
            }
        }


        /// <summary>
        /// Fn() is used to read values from Worksheet and write to DataTable sheet the values,
        /// starting with the row and column ID's are passed.
        /// </summary>
        /// <param name="headingsRowId"></param>
        /// <param name="startRowId"></param>
        /// <param name="startColId"></param>
        /// <param name="startRowId"></param>
        /// <param name="startColId"></param>       
        public System.Data.DataTable ExcelToDataTable(int headingsRowId, object startLoc, object endLoc,
            ref BackgroundWorker bgWorker)
        {
            System.Data.DataTable returnTable = new DataTable();

            for (int ihy = 1; ihy <= TotalNoOfColumns; ihy++)
            {
                returnTable.Columns.AddRange(new DataColumn[] { new DataColumn(ReadFromCellString(headingsRowId, ihy), typeof(string)) });
            }

            try
            {
                Object[,] data = ReadFromCells(startLoc,endLoc);
                
                for (int loopRowCount = 1; loopRowCount <= TotalNoOfRows; loopRowCount++)
                {
                   

                    DataRow texRow = returnTable.NewRow();
                    for (int loopColumnCount = 1; loopColumnCount <= data.GetLength(1); loopColumnCount++)
                    {
                        texRow[loopColumnCount - 1] = data.GetValue(loopRowCount, loopColumnCount).ToString();
                    }
                    returnTable.Rows.Add(texRow);
                }

                return returnTable;
            }
            catch
            {
                return returnTable;
            }
        }



        /// <summary>
        /// Fn() is used to convert the integer to alphabet, to be used as a column index.  
        /// </summary>
        /// <param name="colId"></param>
        /// <returns>string</returns>
        public string ConvertInteger2Alphabet(int colId)
        {
            string s_colValue = null;

            try
            {
                // This logic would only work for only for Single Character Alphabets
                //if (colId >= 1 && colId <= 26)
                //{
                //    s_colValue = ((char)((colId + 65) - 1)).ToString();
                //}

                // This logic would only work for only Double Character Alphabets 
                //if (colId >= 27 && colId <= 256)
                //{
                //    s_colValue = ((char)(((colId - 1) / 26) + 64)).ToString() + (char)(((colId - 1) % 26) + 65);
                //}

                // This logic would only work for Single, Double, Tripple, Quadruple...... Character Alphabets
                if (colId >= 1)
                {
                    do
                    {
                        s_colValue = ((char)(65 + ((colId - 1) % 26))).ToString() + s_colValue;
                        colId = (colId - ((colId - 1) % 26)) / 26;
                    } while (colId > 0);
                }
            }
            catch 
            {
                
            }

            return s_colValue;
        }

    }
}

