   M S B A M L   `   `   `  ����N  JCustomChromeLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullV>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibraryCustomChromeLibrary  5   	    Als   Als.VendorLookupWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Mcc>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibrary     5   	     � Title$  Vendor Lookup��5	       � 
ResizeMode$ NoResize=�6    � WindowStartupLocation$ CenterScreen=�62   .��5   
   �� $��Stretch=�6   $��10q�61   $��Stretch=�6>      OutGroupBox#��[  6Z   ��5      �� 5      $��Vendor List��6   
�������6/   .��5   
   �� $
��Auto=�6   $��Stretch=�6:   .��5      ]� -   5        $��stackPanel1��5   &   $
��Auto��6   $
��Auto��69   ��. .+�5      �� -   5        $��	groupBox1��5   �   $	��Top=�6   $��Searching Parameter��60     InnerGroupBox#��[ 6M   $��Stretch=�6w   $��5q�6�   .��5      � .+�5      �� -   5        $��txtVendorName��5   ]   $��23��6"   $
��Left=�6.   $��
109,14,0,0q�6I   $	��Top=�6r   $	��161��6�   �� 5      -   5        $��txtVendorAccNo��5   _   $��23��6"   $
��Left=�6.   $��109,43,466,0q�6I   $	��Top=�6u   $	��161��6�   �� 5      -   5        $��txtBankName��5   ]   $��23��6"   $
��Left=�6.   $��
109,73,0,0q�6I   $	��Top=�6p   $	��161��6�   �� 5      -   5        $��txtBankBranch��5   ^   $��23��6"   $
��Left=�6.   $��109,103,0,0q�6I   $	��Top=�6s   $	��161��6�   �� 5      -   5        $��lblVendorName��5   ^   $��23��6$   $
��Left=�60   $��	17,14,0,0q�6K   $��Vendor Name��6s   $	��Top=�6�   �� 5      -   5        $��lblVendorAccNo��5   ^   $��23��6$   $
��Left=�60   $��	17,43,0,0q�6K   $��Vendor Acc No��6t   $	��Top=�6�   �� 5      -	   5        $��lblBankName��5   ^   $��23��6$   $
��Left=�60   $��	17,73,0,0q�6K   $��	Bank Name��6q   $	��Top=�6�   �� 5      -
   5        $��lblBankBranch��5   _   $��23��6$   $
��Left=�60   $��
17,103,0,0q�6K   $��Bank Branch��6t   $	��Top=�6�   �� 5      -   5        $��	btnSearch��5   |   $��_Search��6!   $��23��63   $	��100��6?   $��Right=�6K   $��0,128,245,6q�6g   $��Center=�6�   6�   �� 5      -   5        $��btnReset��5   {   $��_Reset��6!   $��23��62   $	��100��6>   $��Right=�6J   $��0,128,135,6q�6f   $��Center=�6�   6�   �� 5      -   5        $��btnClose��5   z   $��_Close��6!   $��23��62   $	��100��6>   $��Right=�6J   $��
0,128,25,6q�6f   $��Center=�6�   6�   5       5!      �� 5"      -   5        $��	groupBox2��5"   �   $	��Top=�6   $��Search Result��60   #��[ 6G   $��Stretch=�6q   $��5q�6�   $��Stretch=�6�   .��5#      � .+�5$      &   System.Windows.Controls.DataGrid  -   5        $��dgVendor��5&         AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�5%   5   $��
10,10,7,14q�6h   	   RowHeaderWidth$	 0Q�5&   +   
   SelectionMode$
 Single=�6>      SelectionUnit$ FullRow=�6U   $   HorizontalScrollBarVisibility$
 Auto=�5'      "   VerticalScrollBarVisibility$
 Auto=�6@   $
��Left=�6c   $��Stretch=�6~   $
��1000��6�   $	��700��6�   ��5%   P   ��    Columns 5(      0  *System.Windows.Controls.DataGridTextColumn  5)   "   -   5        RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��5)   i      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6J   ��  �� Path$ RowNo�6�     5*   "   -   5        VendorNameColumn��$ Vendor Name��5*   t    . $	 125 6�    6O   �� $ Vendor_Name�6�     5+   "   -   5        BankNameColumn��$
 Bank��5+   p    . $ 75 6�    6M   �� $ 	Bank_Name�6�     5,   "   -   5        BankBranchColumn��$ Bank Branch��5,   t    . $	 100 6�    6O   �� $ Bank_Branch�6�     5-   "   -   5        VendorDescriptionColumn��$ Vendor Description��5-   �    . $	 150 6�    6V   �� $ Vendor_Description�6�     5.   "   -   5        VendorAccNoColumn��$ Vendor Account No��5.   {    . $	 120 6�    6P   �� $ Vendor_Account_No�6�   4  .System.Windows.Controls.DataGridTemplateColumn  5/   "   $ Select��69   $ 50 6I      CellTemplate 50   &   �� 51   *   .R�52   .   �� -   5          
LinkButton#��[ 52   5   $��Center=�6n   .��6�   Select6�   53   +   55   #   59      5;      5<      5=      5>      5?      5A      