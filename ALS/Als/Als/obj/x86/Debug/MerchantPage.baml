   M S B A M L   `   `   `  ����  Als    Als.MerchantPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Merchant��5      .��5
      �� $��Stretch=�6   $��10q�65   $��Stretch=�6B      OutGroupBox#��[  6^   ��5      �� 5      $��Merchant List��6    
�������65   .��5      �� $
��Auto=�6   $��Stretch=�6>   .��5      ]� -   5        $��stackPanel1��5   &   $
��Auto��6   $
��Auto��69   ��. .+�5      �� -   5        $��	groupBox1��5   �   $	��Top=�6   $��Searching Parameter��60     InnerGroupBox#��[ 6M   $��Stretch=�6w   $��5q�6�   .��5      � .+�5      �� -   5        $��txtMerchantCode��5   ]   $��23��6"   $
��Left=�6.   $��
109,14,0,0q�6I   $	��Top=�6t   $	��161��6�   �� 5      -   5        $��txtMerchantName��5   ]   $��23��6"   $
��Left=�6.   $��
109,43,0,0q�6I   $	��Top=�6t   $	��161��6�   �� 5      -   5        $��txtVendorName��5   ]   $��23��6"   $
��Left=�6.   $��
109,73,0,0q�6I   $	��Top=�6r   $	��161��6�   �� 5      -   5        $��lblMerchantCode��5   ^   $��23��6$   $
��Left=�60   $��	10,14,0,0q�6K   $��Merchant Code��6u   $	��Top=�6�   �� 5      -   5        $��lblMerchantName��5   ^   $��23��6$   $
��Left=�60   $��	10,43,0,0q�6K   $��Merchant Name��6u   $	��Top=�6�   �� 5      -   5        $��lblVendorName��5   ^   $��23��6$   $
��Left=�60   $��	10,73,0,0q�6K   $��Vendor Name��6s   $	��Top=�6�   �� 5      -	   5        $��	btnSearch��5   {   $��Search��6!   $��23��62   $	��100��6>   $��Right=�6J   $��0,128,140,6q�6f   $��Center=�6�   6�   �� 5      -
   5        $��btnReset��5   y   $��Reset��6!   $��23��61   $	��100��6=   $��Right=�6I   $��
0,128,25,6q�6e   $��Center=�6�   6�   5      5      �� 5      -   5        $��	groupBox2��5   �   $	��Top=�6   $��Search Result��60   #��[ 6G   $��Stretch=�6q   $��Stretch=�6�   $��5q�6�   .��5      � .+�5      &   System.Windows.Controls.DataGrid  -   5        $��
dgMerchant��5!         AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�5    5   $��
10,45,7,10q�6h      RowHeaderWidth$ 0Q�5!   -      SelectionMode$ Single=�6@   	   SelectionUnit$	 FullRow=�6W   $
   HorizontalScrollBarVisibility$

 Auto=�5"      "   VerticalScrollBarVisibility$
 Auto=�6@   $
��1000��6c   $
��1000��6s   $
��Left=�6�   $��Stretch=�6�   ��5    P   ��    Columns 5#      0  *System.Windows.Controls.DataGridTextColumn  5$   "   -   5        RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��5$   i      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6J   ��  �� Path$ RowNo�6�     5%   "   -   5        MerchantCodeColumn��$ Merchant Code��5%   x    . $	 100 6�    6Q   �� $ Merchant_Code�6�     5&   "   -   5        MerchantNameColumn��$ Merchant Name��5&   x    . $	 125 6�    6Q   �� $ Merchant_Name�6�     5'   "   -   5        VendorNameColumn��$ Vendor Name��5'   t    . $	 125 6�    6O   �� $ Vendor_Name�6�     5(   "   -   5        MerchantDescriptionColumn��$ Merchant Description��5(   �    . $	 150 6�    6X   �� $ Merchant_Description�6�   4  .System.Windows.Controls.DataGridTemplateColumn  5)   "   $ Merchant Price List��69   $	 125 6V      CellTemplate 5*   &   �� 5+   *   .R�5,   .   �� -   5          
LinkButton#��[ 5,   5   $��Center=�6y   .��6�   Merchant Price List6�   5-   +   5/   #     50   "   $ Update��69   $ 50 6I    51   &   �� 52   *   .R�53   .   �� -   5        #��[ 53   5   $��Center=�6n   .��6�   Update6�   54   +   56   #     57   "   $
 View��69   $ 50 6G    58   &   �� 59   *   .R�5:   .   �� -   5        #��[ 5:   5   $��Center=�6l   .��6�   View6�   5;   +   5=   #     5>   "   $ Delete��69   $ 50 6I    5?   &   �� 5@   *   .R�5A   .   �� -   5        #��[ 5A   5   $��Center=�6n   .��6�   Delete6�   5B   +   5D   #   5H      �� 5I      -   5        $��	btnCreate��5I   n   $��Create��6!   $��23��62   $
��Left=�6>   $��10,10,0,167q�6Y   $	��Top=�6   $	��100��6�   6�   5J      5K      5L      5M      5N      5R      