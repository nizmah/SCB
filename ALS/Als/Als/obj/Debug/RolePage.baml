   M S B A M L   `   `   `  ����  Als    Als.RolePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$
  Role��5      .��5   
   �� $��	Role List��6   $��Stretch=�6&   $��10q�6D      OutGroupBox#��[  6P   
�������.��5   
   �� $
��Auto=�6   $��Stretch=�6:   .��5      ]� -   5        $��stackPanel1��5   &   $
��Auto��6   $
��Auto��69   ��. .+�5      �� $	��Top=�6   $��Searching Parameter��60   $��Stretch=�6M   $��5q�6k     InnerGroupBox#��[ 6v   
���   �.��5      � .+�5      �� -   5        $��txtRoleName��5   _   $��23��6"   $
��Left=�6.   $��109,14,466,0q�6I   $	��Top=�6r   $	��161��6�   �� 5      -   5        $��lblRoleName��5   ^   $��23��6$   $
��Left=�60   $��	17,14,0,0q�6K   $��	Role Name��6q   $	��Top=�6�   �� 5      -   5        $��	btnSearch��5   {   $��_Search��6!   $��23��63   $	��100��6?   $��Right=�6K   $��
0,80,140,6q�6g   $��Center=�6�   6�   �� 5      -   5        $��btnReset��5   y   $��_Reset��6!   $��23��62   $	��100��6>   $��Right=�6J   $��	0,80,25,6q�6f   $��Center=�6�   6�   5      5      �� 5      $	��Top=�6   $��Stretch=�60   $��Search Result��6L   $��Stretch=�6c   $��5q�6�   #��[ 6�   
���   �.��5      � .+�5      &   System.Windows.Controls.DataGrid  -   5        $��dgRole��5         AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�5   5   $��
10,45,7,10q�6h      RowHeaderWidth$ 0Q�5   )      SelectionMode$ Single=�6<   	   SelectionUnit$	 FullRow=�6S   $
   HorizontalScrollBarVisibility$

 Auto=�5      "   VerticalScrollBarVisibility$
 Auto=�6@   $	��800��6c   $
��1000��6r   $
��Left=�6�   $��Stretch=�6�   ��5   P   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5   "   -   5        RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��5   i      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6J   ��  �� Path$ RowNo�6�     5    "   -   5        RoleNameColumn��$ 	Role Name��5    p    . $	 100 6�    6M   �� $ 	Role_Name�6�     5!   "   -	   5        RoleDescriptionColumn��$ Description��5!   ~    . $	 200 6�    6T   �� $ Role_Description�6�   4  .System.Windows.Controls.DataGridTemplateColumn  5"   "   $ Member��69   $ 75 6I      CellTemplate 5#   &   �� 5$   *   .R�5%   .   �� -
   5          
LinkButton#��[ 5%   5   $��Center=�6n   .��6�   Member6�   5&   +   5(   #     5)   "   $ Role Access��69   $ 75 6N    5*   &   �� 5+   *   .R�5,   .   �� -   5        #��[ 5,   5   $��Center=�6r   .��6�   Role Access6�   5-   +   5/   #     50   "   $ Update��69   $ 75 6I    51   &   �� 52   *   .R�53   .   �� -   5        #��[ 53   5   $��Center=�6n   .��6�   Update6�   54   +   56   #     58   "   $
 View��69   $ 75 6G    59   &   �� 5:   *   .R�5;   .   �� -   5        #��[ 5;   5   $��Center=�6l   .��6�   View6�   5<   +   5>   #     5?   "   $ Delete��69   $ 75 6I    5@   &   �� 5A   *   .R�5B   .   �� -   5        #��[ 5B   5   $��Center=�6n   .��6�   Delete6�   5C   +   5E   #   5I      �� 5J      -   5        $��	btnCreate��5J   o   $��_Create��6!   $��23��63   $
��Left=�6?   $��10,10,0,167q�6Z   $	��Top=�6�   $	��100��6�   6�   5K      5L      5M      5N      5O      5Q      