   M S B A M L   `   `   `  ����  Als    Als.VendorPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Vendor��5      .��5   
   �� $��Stretch=�6   $��10q�61   $��Stretch=�6>      OutGroupBox#��[  6Z   ��5      �� 5      $��Vendor List��6   
�������6/   .��5   
   �� $
��Auto=�6   $��Stretch=�6:   .��5      ]� -   5        $��stackPanel1��5   &   $
��Auto��6   $
��Auto��69   ��. .+�5      �� -   5        $��	groupBox1��5   �   $	��Top=�6   $��Searching Parameter��60     InnerGroupBox#��[ 6M   $��Stretch=�6w   $��5q�6�   $	��Top=�6�   .��5      � .+�5      �� -   5        $��txtVendorName��5   ]   $��23��6"   $
��Left=�6.   $��
109,14,0,0q�6I   $	��Top=�6r   $	��161��6�   �� 5      -   5        $��txtVendorAccNo��5   _   $��23��6"   $
��Left=�6.   $��109,43,466,0q�6I   $	��Top=�6u   $	��161��6�   �� 5      -   5        $��txtBankName��5   ]   $��23��6"   $
��Left=�6.   $��
109,73,0,0q�6I   $	��Top=�6p   $	��161��6�   �� 5      -   5        $��txtBankBranch��5   ^   $��23��6"   $
��Left=�6.   $��109,103,0,0q�6I   $	��Top=�6s   $	��161��6�   �� 5      -   5        $��lblVendorName��5   ^   $��23��6$   $
��Left=�60   $��	10,14,0,0q�6K   $��Vendor Name��6s   $	��Top=�6�   �� 5      -   5        $��lblVendorAccNo��5   ^   $��23��6$   $
��Left=�60   $��	10,43,0,0q�6K   $��Vendor Acc No��6t   $	��Top=�6�   �� 5      -	   5        $��lblBankName��5   ^   $��23��6$   $
��Left=�60   $��	10,73,0,0q�6K   $��	Bank Name��6q   $	��Top=�6�   �� 5      -
   5        $��lblBankBranch��5   _   $��23��6$   $
��Left=�60   $��
10,103,0,0q�6K   $��Bank Branch��6t   $	��Top=�6�   �� 5      -   5        $��	btnSearch��5   |   $��_Search��6!   $��23��63   $	��100��6?   $��Right=�6K   $��0,128,140,6q�6g   $��Center=�6�   6�   �� 5      -   5        $��btnReset��5   z   $��_Reset��6!   $��23��62   $	��100��6>   $��Right=�6J   $��
0,128,25,6q�6f   $��Center=�6�   6�   5       5!      �� 5"      -   5        $��	groupBox2��5"   �   $	��Top=�6   $��Search Result��60   #��[ 6G   $��Stretch=�6q   $��5q�6�   $��Stretch=�6�   .��5#      � .+�5$      &   System.Windows.Controls.DataGrid  -   5        $��dgVendor��5&         AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�5%   5   $��
10,45,7,10q�6h      RowHeaderWidth$ 0Q�5&   +      SelectionMode$ Single=�6>   	   SelectionUnit$	 FullRow=�6U   "
   VerticalScrollBarVisibility$

 Auto=�5'      $   HorizontalScrollBarVisibility$
 Auto=�6>   $
��1000��6c   $
��1000��6s   $
��Left=�6�   $��Stretch=�6�   ��5%   P   ��    Columns 5(      0  *System.Windows.Controls.DataGridTextColumn  5)   "   -   5        RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��5)   i      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6J   ��  �� Path$ RowNo�6�     5*   "   -   5        VendorNameColumn��$ Vendor Name��5*   t    . $	 125 6�    6O   �� $ Vendor_Name�6�     5+   "   -   5        BankNameColumn��$
 Bank��5+   p    . $ 75 6�    6M   �� $ 	Bank_Name�6�     5,   "   -   5        BankBranchColumn��$ Bank Branch��5,   t    . $	 100 6�    6O   �� $ Bank_Branch�6�     5-   "   -   5        VendorDescriptionColumn��$ Vendor Description��5-   �    . $	 150 6�    6V   �� $ Vendor_Description�6�     5.   "   -   5        VendorAccNoColumn��$ Vendor Account No��5.   {    . $	 120 6�    6P   �� $ Vendor_Account_No�6�   4  .System.Windows.Controls.DataGridTemplateColumn  5/   "   $ Update��69   $ 50 6I      CellTemplate 50   &   �� 51   *   .R�52   .   �� -   5          
LinkButton#��[ 52   5   $��Center=�6n   .��6�   Update6�   53   +   55   #     56   "   $
 View��69   $ 50 6G    57   &   �� 58   *   .R�59   .   �� -   5        #��[ 59   5   $��Center=�6l   .��6�   View6�   5:   +   5<   #     5=   "   $ Delete��69   $ 50 6I    5>   &   �� 5?   *   .R�5@   .   �� -   5        #��[ 5@   5   $��Center=�6n   .��6�   Delete6�   5A   +   5C   #   5G      �� 5H      -   5        $��	btnCreate��5H   o   $��_Create��6!   $��23��63   $
��Left=�6?   $��10,10,0,167q�6Z   $	��Top=�6�   $	��100��6�   6�   5I      5J      5K      5L      5M      5O      