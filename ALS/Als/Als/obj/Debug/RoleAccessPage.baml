   M S B A M L   `   `   `  ����  Als    Als.RoleAccessPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Role Access��5      .��5   
   �� $��Stretch=�6   $��10q�61   $��Stretch=�6>      OutGroupBox#��[  6Z   ��5      �� 5      $��Role Access List��6   
�������64   .��5   
   �� $
��Auto=�6   $��Stretch=�6:   .��5      ]� -   5        $��stackPanel1��5   &   $
��Auto��6   $
��Auto��69   ��. .+�5      �� -   5        $��	groupBox1��5   Y   $	��Top=�6   $��Stretch=�60   $��5q�6N   $��0q�6j   .��5      � .+�5      �� -   5        $��lblRoleNameValue��5   _   $��23��6$   $
��Left=�60   $��
109,14,0,0q�6K   $	��Top=�6w   $	��161��6�   �� 5      -   5        $��lblRoleName��5   ^   $��23��6$   $
��Left=�60   $��	17,14,0,0q�6K   $��	Role Name��6q   $	��Top=�6�   5      5      �� 5      -   5        $��	groupBox2��5   x   $	��Top=�6   $��List of Module Access��60   $��Stretch=�6O   $��5q�6m     InnerGroupBox#��[ 6�   .��5      � .+�5      &   System.Windows.Controls.DataGrid  -   5        $��dgRoleAccess��5         AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�5   5   $��
10,10,7,10q�6h      RowHeaderWidth$ 0Q�5   /      SelectionMode$ Single=�6B   	   SelectionUnit$	 FullRow=�6Y   $
   HorizontalScrollBarVisibility$

 Auto=�5      "   VerticalScrollBarVisibility$
 Auto=�6@   $
��1000��6c   $
��1000��6s   $
��Left=�6�   $��Stretch=�6�   ��5   P   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5    "   -   5        RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��5    i      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6J   ��  �� Path$ RowNo�6�     5!   "   -   5        ModuleNameColumn��$ Module Name��5!   t    . $	 200 6�    6O   �� $ Module_Name�6�   4  .System.Windows.Controls.DataGridTemplateColumn  5#   "   $ 75 69      HeaderTemplate 5$   &   �� 5%   *   .R�5&   .   �� $��25��64   $��Center=�6@   .��6]   Create6e   5'   +      CellTemplate 5)   &   �� 5*   *   .R�5+   .   �� $��	chkCreate��6T   $��Center=�67    d� 	IsChecked 6e   �� $ Create_Flag� �� NotifyOnTargetUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�5,   +   5.   #     5/   "   $ 75 69    50   &   �� 51   *   .R�52   .   �� $��25��64   $��Center=�6@   .��6]   Update6e   53   +    55   &   �� 56   *   .R�57   .   �� $��	chkUpdate��6T   $��Center=�67    6e   �� $ Update_Flag� . $ PropertyChanged=�58   +   5:   #     5;   "   $ 75 69    5<   &   �� 5=   *   .R�5>   .   �� $��25��64   $��Center=�6@   .��6]   View6c   5?   +    5A   &   �� 5B   *   .R�5C   .   �� $��chkView��6T   $��Center=�67    6c   �� $ 	View_Flag� . $ PropertyChanged=�5D   +   5F   #     5G   "   $ 75 69    5H   &   �� 5I   *   .R�5J   .   �� $��25��64   $��Center=�6@   .��6]   Delete6e   5K   +    5M   &   �� 5N   *   .R�5O   .   �� $��	chkDelete��6T   $��Center=�67    6e   �� $ Delete_Flag� . $ PropertyChanged=�5P   +   5R   #     5S   "   $ 75 69    5T   &   �� 5U   *   .R�5V   .   �� $��25��64   $��Center=�6@   .��6]   Upload6e   5W   +    5Y   &   �� 5Z   *   .R�5[   .   �� $��	chkUpload��6T   $��Center=�67    6e   �� $ Upload_Flag� . $ PropertyChanged=�5\   +   5^   #   5b      5c      5d      �� 5e      -	   5        $��gbDetail��5e   c   $	��Top=�6   $��Stretch=�60   $��5q�6N   $�� ��6Y   $��0q�6s   #��[ 6�   .��5f      � .+�5g      �� -
   5        $��lblCreatedBy��5g   ^   $��23��6$   $
��Left=�60   $��	10,10,0,0q�6K   $��
Created By��6r   $	��Top=�6�   �� 5h      -   5        $��lblCreatedDate��5h   ^   $��23��6$   $
��Left=�60   $��	10,40,0,0q�6K   $��Created Date��6t   $	��Top=�6�   �� 5i      -   5        $��lblUpdatedBy��5i   ^   $��23��6$   $
��Left=�60   $��	10,70,0,0q�6K   $��
Updated By��6r   $	��Top=�6�   �� 5j      -   5        $��lblUpdatedDate��5j   _   $��23��6$   $
��Left=�60   $��
10,100,0,0q�6K   $��Updated Date��6u   $	��Top=�6�   �� 5k      -   5        $��lblCreatedByValue��5k   _   $��23��6$   $
��Left=�60   $��
103,10,0,0q�6K   $	��Top=�6x   �� 5l      -   5        $��lblCreatedDateValue��5l   _   $��23��6$   $
��Left=�60   $��
103,40,0,0q�6K   $	��Top=�6z   �� 5m      -   5        $��lblUpdatedByValue��5m   _   $��23��6$   $
��Left=�60   $��
103,70,0,0q�6K   $	��Top=�6x   �� 5n      -   5        $��lblUpdatedDateValue��5n   `   $��23��6$   $
��Left=�60   $��103,100,0,0q�6K   $	��Top=�6{   5o      5p      � 5q      $	��Top=�6   $��Stretch=�60   $��Stretch=�6N   .+�5r      �� -   5        $��btnSave��5r   y   $��_Save��6!   $��23��61   $	��100��6=   $��Right=�6I   $��
0,10,135,0q�6e   $��Center=�6�   6�   �� 5s      -   5        $��	btnCancel��5s   z   $��_Cancel��6!   $��23��63   $	��100��6?   $��Right=�6K   $��	0,10,20,0q�6g   $��Center=�6�   6�   �� 5t      -   5        $��lblID��5t   _   $��23��6$   $
��Left=�60   $��
306,10,0,0q�6K   $	��Top=�6l   $y�Hidden=�6�   5u      5w      5x      5y      5|      