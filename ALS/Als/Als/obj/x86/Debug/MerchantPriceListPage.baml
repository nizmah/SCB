   M S B A M L   `   `   `  ����  Als    Als.MerchantPriceListPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Merchant Price List��5      .��5   
   �� $��Stretch=�6   $��10q�61   $��Stretch=�6>      OutGroupBox#��[  6[   ��5      �� 5      $��Merchant Price List��6   
�������67   .��5   
   �� $
��Auto=�6   $��Stretch=�6:   .��5      ]� -   5        $��stackPanel1��5   &   $
��Auto��6   $
��Auto��69   ��. .+�5      �� $	��Top=�6   $��Searching Parameter��60   $��Stretch=�6M   $��5q�6k     InnerGroupBox#��[ 6v   
���   �.��5      � .+�5      �� -   5        $��
cbMerchant��5   Z   $��23��6   $
��Left=�6+   $��
150,10,0,0q�6F   $	��Top=�5      $	��161��67    �� DisplayMemberPath$ Merchant_Name��5       �� SelectedValuePath$ ID��5      ��5      �� 5      �� 5      -   5        $��
cbCardType��5   Z   $��23��6   $
��Left=�6+   $��
150,43,0,0q�6F   $	��Top=�5      $	��161��67   $ Card_Type_Name��5      $ ID��5      ��5      �� 5      �� 5       -   5        $��lblMerchantNameValue��5    [   $��23��6    $
��Left=�6,   $��
150,10,0,0q�6G   $	��Top=�6w   $y�Hidden=�6�   �� 5#      -   5        $��lblMerchantName��5#   Z   $��23��6    $
��Left=�6,   $��	17,10,0,0q�6G   $��Merchant Name��6q   $	��Top=�6�   �� 5$      -   5        $��lblCardType��5$   Z   $��23��6    $
��Left=�6,   $��	17,43,0,0q�6G   $��Card Type Name��6m   $	��Top=�6�   �� 5&      -   5        $��btnShow��5&   �   $��Show Price List��6   $��23��67   $	��160��6C   $
��Left=�6O   $��20,123,140,6q�6j   $	��Top=�6�   6�   �� 5'      -   5        $��lblMerchantID��5'   [   $��23��6    $
��Left=�6,   $��
310,10,0,0q�6G   $	��Top=�6p   $y�Hidden=�6�   �� 5(      -	   5        $��	btnCancel��5(   �   $��Cancel��6!   $��23��62   $
��Left=�6>   $y�Hidden=�6Y   $��200,123,45,5q�6m   $	��Top=�6�   $	��100��6�   6�   5)      5*      �� 5+      -
   5        $��	groupBox2��5+   �   $	��Top=�6   #��[ 60   $��List of Price��6Z   $��Stretch=�6q   $��10q�6�   $��Stretch=�6�   $y�Hidden=�6�   .��5,      � .+�5-      &   System.Windows.Controls.DataGrid  -   5        $��dgMerchantPriceList��5/         AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�5.   5   $��
10,45,7,10q�6h   	   RowHeaderWidth$	 0Q�5/   6   
   SelectionMode$
 Single=�6I      SelectionUnit$ FullRow=�6`   $   HorizontalScrollBarVisibility$
 Auto=�50      "   VerticalScrollBarVisibility$
 Auto=�6@   $
��1000��6c   $
��Left=�6t   $��Stretch=�6�   ��5.   P   ��    Columns 51      0  *System.Windows.Controls.DataGridTextColumn  52      -   5        RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��52   e      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6F   ��  �� Path$ RowNo�6�     53      -   5        GuestAmountColumn��$ Total Guest��53   r    . $	 125 6�    6L   �� $ Guest_Amount�6�     54      -   5        PriceAmountColumn��$ Price Amount��54   r    . $	 125 6�    6L   �� $ Price_Amount�6�     55      -   5        PointAmontColumn��$ Point Amount��55   q    . $	 125 6�    6K   �� $ Point_Amount�6�   4  .System.Windows.Controls.DataGridTemplateColumn  56      $ Update��65   $ 50 6E      CellTemplate 57   "   �� 58   &   .R�59   *   �� -   5          
LinkButton#��[ 59   1   $��Center=�6j   .��6�   Update6�   5:   '   5<        5=      $
 View��65   $ 50 6C    5>   "   �� 5?   &   .R�5@   *   �� -   5        #��[ 5@   1   $��Center=�6h   .��6�   View6�   5A   '   5C        5D      $ Delete��65   $ 50 6E    5E   "   �� 5F   &   .R�5G   *   �� -   5        #��[ 5G   1   $��Center=�6j   .��6�   Delete6�   5H   '   5J      5N      �� 5O      -   5        $��	btnCreate��5O   n   $��Create��6!   $��23��62   $
��Left=�6>   $��10,10,0,167q�6Y   $	��Top=�6   $	��100��6�   6�   5P      5Q      5R      5S      5T      5V      