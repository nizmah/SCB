   M S B A M L   `   `   `  ����  Als    Als.UserRolePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  	User Role��5      .��5   
   �� $��Stretch=�6   $��10q�61   $��Stretch=�6>      OutGroupBox#��[  6Z   ��5      �� 5      $��User Role List��6   
�������62   .��5   
   �� $
��Auto=�6   $��Stretch=�6:   .��5      ]� -   5        $��stackPanel1��5   &   $
��Auto��6   $
��Auto��69   ��. .+�5      �� -   5        $��lblID��5   >   $��23��6   $
��Left=�6   $��0q�63   $	��Top=�6K   $y�Hidden=�6c   �� 5      -   5        $��	groupBox1��5   �   $	��Top=�6     InnerGroupBox#��[ 60   $��Stretch=�6Z   $��5q�6x   $�� ��6�   $��0q�6�   .��5      � .+�5      �� -   5        $��lblRoleNameValue��5   _   $��23��6$   $
��Left=�60   $��
109,14,0,0q�6K   $	��Top=�6w   $	��161��6�   �� 5      -   5        $��lblRoleName��5   ^   $��23��6$   $
��Left=�60   $��	17,14,0,0q�6K   $��	Role Name��6q   $	��Top=�6�   5      5      �� 5      -   5        $��	groupBox2��5   �   $	��Top=�6   $��List of Member��60   #��[ 6H   $��Stretch=�6r   $��5q�6�   .��5      � .+�5      &   System.Windows.Controls.DataGrid  -   5        $��
dgUserRole��5         AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�5   5   $��
10,45,7,10q�6h      RowHeaderWidth$ 0Q�5   -      SelectionMode$ Single=�6@   	   SelectionUnit$	 FullRow=�6W   $
   HorizontalScrollBarVisibility$

 Auto=�5      "   VerticalScrollBarVisibility$
 Auto=�6@   $
��Left=�6c   $	��Top=�6~   $	��110��6�   $	��600��6�   ��5   P   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5   "   -   5        RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��5   i      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6J   ��  �� Path$ RowNo�6�     5    "   -	   5        UserIdColumn��$ User ID��5    l    . $	 100 6�    6K   �� $ User_ID�6�     5!   "   -
   5        UserNameColumn��$ 	User Name��5!   p    . $	 150 6�    6M   �� $ 	User_Name�6�     5"   "   -   5        ExtNoColumn��$ No Extention��5"   o    . $	 100 6�    6J   �� $ User_No_Ext�6�   4  .System.Windows.Controls.DataGridTemplateColumn  5$   "   $ Delete��69   $ 75 6I      CellTemplate 5%   &   �� 5&   *   .R�5'   .   �� -   5          
LinkButton#��[ 5'   5   $��Center=�6Y   .��6�   Delete6�   5(   +   5*   #   5.      �� 5/      -   5        $��btnAdd��5/   k   $	��Add��6!   $��23��6/   $
��Left=�6;   $��10,10,0,167q�6V   $	��Top=�6y   $	��100��6�   6�   50      51      �� 52      -   5        $��gbDetail��52   c   $	��Top=�6   $��Stretch=�60   $��5q�6N   $�� ��6Y   #��[ 6s   $��0q�6�   .��53      � $	��120��6   .+�54      �� -   5        $��lblCreatedBy��54   ^   $��23��6$   $
��Left=�60   $��	10,10,0,0q�6K   $��
Created By��6r   $	��Top=�6�   �� 55      -   5        $��lblCreatedDate��55   ^   $��23��6$   $
��Left=�60   $��	10,40,0,0q�6K   $��Created Date��6t   $	��Top=�6�   �� 56      -   5        $��lblUpdatedBy��56   ^   $��23��6$   $
��Left=�60   $��	10,70,0,0q�6K   $��
Updated By��6r   $	��Top=�6�   �� 57      -   5        $��lblUpdatedDate��57   _   $��23��6$   $
��Left=�60   $��
10,100,0,0q�6K   $��Updated Date��6u   $	��Top=�6�   �� 58      -   5        $��lblCreatedByValue��58   _   $��23��6$   $
��Left=�60   $��
103,10,0,0q�6K   $	��Top=�6x   �� 59      -   5        $��lblCreatedDateValue��59   _   $��23��6$   $
��Left=�60   $��
103,40,0,0q�6K   $	��Top=�6z   �� 5:      -   5        $��lblUpdatedByValue��5:   _   $��23��6$   $
��Left=�60   $��
103,70,0,0q�6K   $	��Top=�6x   �� 5;      -   5        $��lblUpdatedDateValue��5;   `   $��23��6$   $
��Left=�60   $��103,100,0,0q�6K   $	��Top=�6{   5<      5=      �� 5>      $	��Top=�6    �� Orientation$ 
Horizontal=�62   $��Right=�6K   .+�5?      �� -   5        $��btnSave��5?   W   $
��Save��6   $��23��6,   $	��100��68   $��	0,10,10,0q�6D   $��Center=�6f   6�   �� 5@      -   5        $��	btnCancel��5@   Y   $��Cancel��6   $��23��6.   $	��100��6:   $��	0,10,20,0q�6F   $��Center=�6j   6�   5A      5D      5E      5F      5H      