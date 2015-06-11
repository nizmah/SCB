using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Als
{
    public enum sTableCollection
    {
    Customer,
        SalesOrderHeader,
        Product,
        SalesOrderDetail,
        PurchaseOrderDetail,
        PurchaseOrderHeader
    
    }

    public class DataBaseVar
    {
        public string CreateDb =
        #region Create Database
 "USE Master\r\n" + "CREATE DATABASE [SamkarHardware] ON  PRIMARY " +
"( NAME = N'SamkarHardware', FILENAME = N'" +
Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) +
"\\Samkar Hardware\\Data\\SamkarHardware.mdf' , SIZE = 3072KB , FILEGROWTH = 1024KB )\r\n" +
 "LOG ON " +
"( NAME = N'SamkarHardware_log', FILENAME = N'" +
Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) +
"\\Samkar Hardware\\Data\\SamkarHardware_log.ldf' , SIZE = 1024KB , FILEGROWTH = 10%)" +
"GO\r\n" +
"EXEC dbo.sp_dbcmptlevel @dbname=N'SamkarHardware', @new_cmptlevel=90\r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET ANSI_NULL_DEFAULT OFF \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET ANSI_NULLS OFF \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET ANSI_PADDING OFF \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET ANSI_WARNINGS OFF \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET ARITHABORT OFF \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET AUTO_CLOSE OFF \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET AUTO_CREATE_STATISTICS ON \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET AUTO_SHRINK OFF \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET AUTO_UPDATE_STATISTICS ON \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET CURSOR_CLOSE_ON_COMMIT OFF \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET CURSOR_DEFAULT  GLOBAL \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET CONCAT_NULL_YIELDS_NULL OFF \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET NUMERIC_ROUNDABORT OFF \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET QUOTED_IDENTIFIER OFF \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET RECURSIVE_TRIGGERS OFF \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET AUTO_UPDATE_STATISTICS_ASYNC OFF \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET DATE_CORRELATION_OPTIMIZATION OFF \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET PARAMETERIZATION SIMPLE \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET  READ_WRITE \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET RECOVERY FULL \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET  MULTI_USER \r\n" +
"GO\r\n" +
"ALTER DATABASE [SamkarHardware] SET PAGE_VERIFY CHECKSUM  \r\n" +
"GO\r\n" +
"USE [SamkarHardware] \r\n" +
"GO\r\n" +
"IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = " +
"N'PRIMARY') ALTER DATABASE [SamkarHardware] MODIFY FILEGROUP [PRIMARY] DEFAULT\r\n";
        #endregion

        
    }
}
