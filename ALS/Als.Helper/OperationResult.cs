using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Als
{
    public class OperationResult
    {
        private string _errorMessage;
        private string _errorCode;
        private bool _succeeded;
        public OperationResult()
        {
            dErrorMessage = "0.0";
            dErrorCode = "0.0";
            _succeeded = true;
        }
        public string dErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }
        public bool Succeeded
        {
            get { return _succeeded; }
            set { _succeeded = value; }
        }
        public string dErrorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }
    }

    public class FileOperationResult : OperationResult
    {
        public FileOperationResult()
        {
        }
    }

    public class SqlQueryResult : OperationResult
    {
        private DataTable _resultDataTable;

        public SqlQueryResult()
        {
            dResultDataTable = null;
        }
        public DataTable dResultDataTable
        {
            get { return _resultDataTable; }
            set { _resultDataTable = value; }
        }
    }

    public class ScalarQueryResult : OperationResult
    {

        public ScalarQueryResult()
        {

        }
        public string sResult
        { get; set; }
    }

    public class ApplicationPaths
    {
        private static readonly string _backupDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
            "\\Samkar Hardware\\Backup\\";
        private static readonly string _dataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
            "\\Samkar Hardware\\Data\\";
        public static string BackupDirectory
        {
            get { return _backupDirectory; }
        }
        public static string DataDirectory
        {
            get { return _dataDirectory; }
        }
    }

    public class CustomOperationResult : SqlQueryResult
    {
        private Object customResult;
        public CustomOperationResult()
        {
            customResult = "";
        }
        public Object CustomResult
        {
            get { return customResult; }
            set { customResult = value; }
        }
    }
}
