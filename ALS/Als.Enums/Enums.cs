using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Als
{
    public enum ObjectPermissionAccess
    {
        Guest = 1, User = 2, Manager = 3, Administrator = 4, Owner = 5, SystemAdmin = 6
    }
    public enum DatabaseTablesMain
    {
        SalesOrderHeader = 1, Customer = 2, Employee = 3, Product = 4
    }
    public enum DatabaseTablesSales
    {
        Employee, Address, Customer, Product,
        ProductCategory, ProductInventory, PurchaseOrderDetail,
        PurchaseOrderheader, SalesOrderDetail, SalesOrderHeader, Vendor
    }
    public enum DatabaseTablesDbo
    {
        Hashes, TransactionHistory, ProtectedDocument
    }
    
}
