   M S B A M L   `   `   `  ����N  JCustomChromeLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullV>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibraryCustomChromeLibrary  5   	    Als   Als.MerchantLookupWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Mcc>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibrary     5   	     � Title$  Merchant Lookup��5	       � 
ResizeMode$ NoResize=�6    � WindowStartupLocation$ CenterScreen=�64   .��5      �� $��Stretch=�6   $��10q�65   $��Stretch=�6B      OutGroupBox#��[  6^   ��5      �� 5      $��Merchant List��6    
�������65   .��5   
   �� $
��Auto=�6   $��Stretch=�6:   .��5      ]� -   5        $��stackPanel1��5   &   $
��Auto��6   $
��Auto��69   ��. .+�5      �� -   5        $��	groupBox1��5   �   $	��Top=�6   $��Searching Parameter��60     InnerGroupBox#��[ 6M   $��Stretch=�6w   $��5q�6�   .��5      � .+�5      �� -   5        $��txtMerchantCode��5   ]   $��23��6"   $
��Left=�6.   $��
109,14,0,0q�6I   $	��Top=�6t   $	��161��6�   �� 5      -   5        $��txtMerchantName��5   ]   $��23��6"   $
��Left=�6.   $��
109,43,0,0q�6I   $	��Top=�6t   $	��161��6�   �� 5      -   5        $��txtVendorName��5   ]   $��23��6"   $
��Left=�6.   $��
109,73,0,0q�6I   $	��Top=�6r   $	��161��6�   �� 5      -   5        $��lblMerchantCode��5   ^   $��23��6$   $
��Left=�60   $��	17,14,0,0q�6K   $��Merchant Code��6u   $	��Top=�6�   �� 5      -   5        $��lblMerchantName��5   ^   $��23��6$   $
��Left=�60   $��	17,43,0,0q�6K   $��Merchant Name��6u   $	��Top=�6�   �� 5      -   5        $��lblVendorName��5   ^   $��23��6$   $
��Left=�60   $��	17,73,0,0q�6K   $��Vendor Name��6s   $	��Top=�6�   �� 5      -	   5        $��	btnSearch��5   {   $��Search��6!   $��23��62   $	��100��6>   $��Right=�6J   $��0,128,245,6q�6f   $��Center=�6�   6�   �� 5      -
   5        $��btnReset��5   z   $��Reset��6!   $��23��61   $	��100��6=   $��Right=�6I   $��0,128,135,6q�6e   $��Center=�6�   6�   �� 5      -   5        $��btnClose��5   y   $��Close��6!   $��23��61   $	��100��6=   $��Right=�6I   $��
0,128,25,6q�6e   $��Center=�6�   6�   5      5      �� 5       -   5        $��	groupBox2��5    �   $	��Top=�6   $��Search Result��60   #��[ 6G   $��Stretch=�6q   $��5q�6�   $��Stretch=�6�   .��5!      � .+�5"      &   System.Windows.Controls.DataGrid  -   5        $��
dgMerchant��5$         AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�5#   5   $��
10,10,7,10q�6h   	   RowHeaderWidth$	 0Q�5$   -   
   SelectionMode$
 Single=�6@      SelectionUnit$ FullRow=�6W   $   HorizontalScrollBarVisibility$
 Auto=�5%      "   VerticalScrollBarVisibility$
 Auto=�6@   $	��600��6c   $
��1000��6r   $
��Left=�6�   $��Stretch=�6�   ��5#   P   ��    Columns 5&      0  *System.Windows.Controls.DataGridTextColumn  5'   "   -   5        RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��5'   i      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6J   ��  �� Path$ RowNo�6�     5(   "   -   5        MerchantCodeColumn��$ Merchant Code��5(   x    . $	 100 6�    6Q   �� $ Merchant_Code�6�     5)   "   -   5        MerchantNameColumn��$ Merchant Name��5)   x    . $	 125 6�    6Q   �� $ Merchant_Name�6�     5*   "   -   5        VendorNameColumn��$ Vendor Name��5*   t    . $	 125 6�    6O   �� $ Vendor_Name�6�     5+   "   -   5        MerchantDescriptionColumn��$ Merchant Description��5+   �    . $	 150 6�    6X   �� $ Merchant_Description�6�   4  .System.Windows.Controls.DataGridTemplateColumn  5,   "   $ Select��69   $ 50 6I      CellTemplate 5-   &   �� 5.   *   .R�5/   .   �� -   5          
LinkButton#��[ 5/   5   $��Center=�6n   .��6�   Select6�   50   +   52   #   56      58      59      5:      5;      5<      5?      