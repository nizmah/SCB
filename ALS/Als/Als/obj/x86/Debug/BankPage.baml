   M S B A M L   `   `   `  ����  Als    Als.BankPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$
  Bank��5      .��5
      � .+�5   
   �� $��Stretch=�6   $��10q�61   $��Stretch=�6>      OutGroupBox#��[  6Z   ��5      �� 5      $��	Bank List��6   
�������6-   .��5      �� $
��Auto=�6   $��Stretch=�6>   .��5      ]� -   5        $��stackPanel1��5   *   $
��Auto��6   $
��Auto��6=   ��. .+�5      �� -   5        $��	groupBox1��5   z   $	��Top=�6   $��Searching Parameter��64   $��Stretch=�6Q   $��5q�6o     InnerGroupBox#��[ 6�   .��5      � .+�5      �� -   5        $��txtBankName��5   a   $��23��6&   $
��Left=�62   $��
109,14,0,0q�6M   $	��Top=�6t   $	��161��6�   �� 5      -   5        $��txtBankBranch��5   a   $��23��6&   $
��Left=�62   $��
109,43,0,0q�6M   $	��Top=�6v   $	��161��6�   �� 5      -   5        $��txtCityofBranch��5   a   $��23��6&   $
��Left=�62   $��
109,73,0,0q�6M   $	��Top=�6x   $	��161��6�   �� 5      -   5        $��lblBankName��5   b   $��23��6(   $
��Left=�64   $��	17,14,0,0q�6O   $��	Bank Name��6u   $	��Top=�6�   �� 5      -   5        $��lblBankBranch��5   b   $��23��6(   $
��Left=�64   $��	17,43,0,0q�6O   $��Bank Branch��6w   $	��Top=�6�   �� 5      -   5        $��lblCityofBranch��5   b   $��23��6(   $
��Left=�64   $��	17,73,0,0q�6O   $��City of Branch��6y   $	��Top=�6�   �� 5      -	   5        $��	btnSearch��5      $��Search��6%   $��23��66   $	��100��6B   $��Right=�6N   $��0,128,140,6q�6j   $��Center=�6�   6�   �� 5      -
   5        $��btnReset��5   }   $��Reset��6%   $��23��65   $	��100��6A   $��Right=�6M   $��
0,128,25,6q�6i   $��Center=�6�   6�   5      5      �� 5      -   5        $��	groupBox2��5   t   $	��Top=�6   $��Search Result��64   $��Stretch=�6K   $��5q�6i   $��Stretch=�6�   #��[ 6�   .��5      � .+�5       &   System.Windows.Controls.DataGrid  -   5        $��dgBank��5"         AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�5!   5   $��
10,45,7,10q�6i      RowHeaderWidth$ 0Q�5"   )      SelectionMode$ Single=�6<   	   SelectionUnit$	 FullRow=�6S   $
   HorizontalScrollBarVisibility$

 Auto=�5#      $
��1000��6@   $
��1000��6Q   "   VerticalScrollBarVisibility$
 Auto=�6a   $
��Left=�6�   $��Stretch=�6�   ��5!   P   ��    Columns 5$   "   0  *System.Windows.Controls.DataGridTextColumn  5%   &   -   5        RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��5%   m      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6N   ��  �� Path$ RowNo�6�     5&   &   -   5        BankNameColumn��$ 	Bank Name��5&   t    . $	 125 6�    6Q   �� $ 	Bank_Name�6�     5'   &   -   5        BankBranchColumn��$ Branch��5'   x    . $	 125 6�    6S   �� $ Bank_Branch�6�     5(   &   -   5        BankCityColumn��$
 City��5(   t    . $	 125 6�    6Q   �� $ 	Bank_City�6�     5)   &   -   5        BankDescriptionColumn��$ Bank Description��5)   �    . $	 200 6�    6X   �� $ Bank_Description�6�   4  .System.Windows.Controls.DataGridTemplateColumn  5*   &   $ Update��6=   $ 50 6M      CellTemplate 5+   *   �� 5,   .   .R�5-   2   �� -   5          
LinkButton#��[ 5-   9   $��Center=�6r   .��6�   Update6�   5.   /   50   '     51   &   $
 View��6=   $ 50 6K    52   *   �� 53   .   .R�54   2   �� -   5        #��[ 54   9   $��Center=�6p   .��6�   View6�   55   /   57   '     58   &   $ Delete��6=   $ 50 6M    59   *   �� 5:   .   .R�5;   2   �� -   5        #��[ 5;   9   $��Center=�6r   .��6�   Delete6�   5<   /   5>   '   5B      �� 5C      -   5        $��	btnCreate��5C   r   $��Create��6%   $��23��66   $
��Left=�6B   $��10,10,0,167q�6]   $	��Top=�6�   $	��100��6�   6�   5D      5E      5F      5G      5H      5I      5J      