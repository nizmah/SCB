   M S B A M L   `   `   `  ����  Als    Als.CardPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$
  Card��5      .��5   
   �� $��Stretch=�6   $��10q�61   $��Stretch=�6>      OutGroupBox#��[  6Z   ��5      �� 5      
�������$��	Card List��6/   .��5   
   �� $
��Auto=�6   $��Stretch=�6:   .��5      ]� -   5        $��stackPanel1��5   &   $
��Auto��6   $
��Auto��69   ��. .+�5      �� -   5        $��	groupBox1��5   v   $	��Top=�6   $��Searching Parameter��60   $��Stretch=�6M   $��5q�6k     InnerGroupBox#��[ 6�   .��5      � .+�5      �� -   5        $��txtBinNo��5   ]   $��23��6"   $
��Left=�6.   $��
109,14,0,0q�6I   $	��Top=�6m   $	��161��6�   �� 5      -   5        $��txtCardName��5   _   $��23��6"   $
��Left=�6.   $��109,43,466,0q�6I   $	��Top=�6r   $	��161��6�   �� 5      -   5        $��
cbCardType��5   ^   $��23��6#   $
��Left=�6/   $��
109,73,0,0q�6J   $	��Top=�6p   $	��161��6�    �� DisplayMemberPath$ Card_Type_Name��6�    �� SelectedValuePath$ ID��5      ��5   �   �� 5      �� 5      -   5        $��lblBinNi��5   ^   $��23��6$   $
��Left=�60   $��	17,14,0,0q�6K   $��BIN No��6n   $	��Top=�6|   �� 5      -   5        $��lblCardName��5   ^   $��23��6$   $
��Left=�60   $��	17,43,0,0q�6K   $��	Card Name��6q   $	��Top=�6�   �� 5      -   5        $��lblCardTypeName��5   ^   $��23��6$   $
��Left=�60   $��	17,73,0,0q�6K   $��Card Type Name��6u   $	��Top=�6�   �� 5      -	   5        $��	btnSearch��5   {   $��Search��6!   $��23��62   $	��100��6>   $��Right=�6J   $��0,128,140,6q�6f   $��Center=�6�   6�   �� 5      -
   5        $��btnReset��5   y   $��Reset��6!   $��23��61   $	��100��6=   $��Right=�6I   $��
0,128,25,6q�6e   $��Center=�6�   6�   5      5      �� 5       -   5        $��	groupBox2��5    �   $	��Top=�6   $��Search Result��60   #��[ 6G   $��Stretch=�6q   $��5q�6�   $��Stretch=�6�   .��5!      � .+�5"      &   System.Windows.Controls.DataGrid  -   5        $��dgCard��5$         AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�5#   5   $��
10,45,7,10q�6h   	   RowHeaderWidth$	 0Q�5$   )   
   SelectionMode$
 Single=�6<      SelectionUnit$ FullRow=�6S   $   HorizontalScrollBarVisibility$
 Auto=�5%      "   VerticalScrollBarVisibility$
 Auto=�6@   $
��1000��6c   $
��1000��6s   $
��Left=�6�   $��Stretch=�6�   ��5#   P   ��    Columns 5&      0  *System.Windows.Controls.DataGridTextColumn  5'   "   -   5        RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��5'   i      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6J   ��  �� Path$ RowNo�6�     5(   "   -   5        BinNoColumn��$ BIN No��5(   j    . $	 100 6�    6J   �� $ Bin_No�6�     5)   "   -   5        CardNameColumn��$ 	Card Name��5)   p    . $	 150 6�    6M   �� $ 	Card_Name�6�     5*   "   -   5        CardDescriptionColumn��$ Card Description��5*   ~    . $	 150 6�    6T   �� $ Card_Description�6�     5+   "   -   5        CardTypeNameColumn��$ Card Type Name��5+   y    . $	 100 6�    6Q   �� $ Card_Type_Name�6�     5,   "   -   5        DigitUniqueCardColumn��$ Digit Unique Card��5,       . $	 120 6�    6T   �� $ Digit_Unique_Card�6�   4  .System.Windows.Controls.DataGridTemplateColumn  5-   "   $ Update��69   $ 50 6I      CellTemplate 5.   &   �� 5/   *   .R�50   .   �� -   5          
LinkButton#��[ 50   5   $��Center=�6n   .��6�   Update6�   51   +   53   #     54   "   $
 View��69   $ 50 6G    55   &   �� 56   *   .R�57   .   �� -   5        #��[ 57   5   $��Center=�6l   .��6�   View6�   58   +   5:   #     5;   "   $ Delete��69   $ 50 6I    5<   &   �� 5=   *   .R�5>   .   �� -   5        #��[ 5>   5   $��Center=�6n   .��6�   Delete6�   5?   +   5A   #   5E      �� 5F      -   5        $��	btnCreate��5F   n   $��Create��6!   $��23��62   $
��Left=�6>   $��10,10,0,167q�6Y   $	��Top=�6   $	��100��6�   6�   5G      5H      5I      5J      5K      5N      