   M S B A M L   `   `   `  ���� N  JCustomChromeLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullV>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibraryCustomChromeLibrary   Als   Als.VendorLookupWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Mcc>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibrary       � Title$  Vendor Lookup�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.���� $��Stretch=�$��10q�$��Stretch=�   OutGroupBox#��[  ���� $��Vendor List��
�������.���� $
��Auto=�$��Stretch=�.��]� -   $��stackPanel1��$
��Auto��$
��Auto����. .+��� -   $��	groupBox1��$	��Top=�$��Searching Parameter��  InnerGroupBox#��[ $��Stretch=�$��5q�.��� .+��� -   $��txtVendorName��$��23��$
��Left=�$��
109,14,0,0q�$	��Top=�$	��161���� -   $��txtVendorAccNo��$��23��$
��Left=�$��109,43,466,0q�$	��Top=�$	��161���� -   $��txtBankName��$��23��$
��Left=�$��
109,73,0,0q�$	��Top=�$	��161���� -   $��txtBankBranch��$��23��$
��Left=�$��109,103,0,0q�$	��Top=�$	��161���� -   $��lblVendorName��$��23��$
��Left=�$��	17,14,0,0q�$��Vendor Name��$	��Top=��� -   $��lblVendorAccNo��$��23��$
��Left=�$��	17,43,0,0q�$��Vendor Acc No��$	��Top=��� -	   $��lblBankName��$��23��$
��Left=�$��	17,73,0,0q�$��	Bank Name��$	��Top=��� -
   $��lblBankBranch��$��23��$
��Left=�$��
17,103,0,0q�$��Bank Branch��$	��Top=��� -   $��	btnSearch��$��Search��$��23��$	��100��$��Right=�$��0,128,245,6q�$��Center=��� -   $��btnReset��$��Reset��$��23��$	��100��$��Right=�$��0,128,135,6q�$��Center=��� -   $��btnClose��$��Close��$��23��$	��100��$��Right=�$��
0,128,25,6q�$��Center=��� -   $��	groupBox2��$	��Top=�$��Search Result��#��[ $��Stretch=�$��5q�$��Stretch=�.��� .+�&   System.Windows.Controls.DataGrid  -   $��dgVendor��   AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�$��
10,10,7,14q�	   RowHeaderWidth$	 0Q�
   SelectionMode$
 Single=�   SelectionUnit$ FullRow=�$   HorizontalScrollBarVisibility$
 Auto=�"   VerticalScrollBarVisibility$
 Auto=�$
��Left=�$��Stretch=�$
��1000��$	��700������    Columns 0  *System.Windows.Controls.DataGridTextColumn  -   RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��   
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ RowNo�  -   VendorNameColumn��$ Vendor Name�� . $	 125  �� $ Vendor_Name�  -   BankNameColumn��$
 Bank�� . $ 75  �� $ 	Bank_Name�  -   BankBranchColumn��$ Bank Branch�� . $	 100  �� $ Bank_Branch�  -   VendorDescriptionColumn��$ Vendor Description�� . $	 150  �� $ Vendor_Description�  -   VendorAccNoColumn��$ Vendor Account No�� . $	 120  �� $ Vendor_Account_No�4  .System.Windows.Controls.DataGridTemplateColumn  $ Select��$ 50    CellTemplate �� .R��� -     
LinkButton#��[ $��Center=�.��Select