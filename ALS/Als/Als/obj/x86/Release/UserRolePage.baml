   M S B A M L   `   `   `  ����   Als    Als.UserRolePage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       N� Title$  	User Role��.���� $��Stretch=�$��10q�$��Stretch=�   OutGroupBox#��[  ���� $��User Role List��
�������.���� $
��Auto=�$��Stretch=�.��]� -   $��stackPanel1��$
��Auto��$
��Auto����. .+��� -   $��lblID��$��23��$
��Left=�$��0q�$	��Top=�$y�Hidden=��� -   $��	groupBox1��$	��Top=�  InnerGroupBox#��[ $��Stretch=�$��5q�$�� ��$��0q�.��� .+��� -   $��lblRoleNameValue��$��23��$
��Left=�$��
109,14,0,0q�$	��Top=�$	��161���� -   $��lblRoleName��$��23��$
��Left=�$��	17,14,0,0q�$��	Role Name��$	��Top=��� -   $��	groupBox2��$	��Top=�$��List of Member��#��[ $��Stretch=�$��5q�.��� .+�&   System.Windows.Controls.DataGrid  -   $��
dgUserRole��   AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�$��
10,45,7,10q�   RowHeaderWidth$ 0Q�   SelectionMode$ Single=�	   SelectionUnit$	 FullRow=�$
   HorizontalScrollBarVisibility$

 Auto=�"   VerticalScrollBarVisibility$
 Auto=�$
��Left=�$	��Top=�$	��110��$	��600������    Columns 0  *System.Windows.Controls.DataGridTextColumn  -   RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��   
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ RowNo�  -	   UserIdColumn��$ User ID�� . $	 100  �� $ User_ID�  -
   UserNameColumn��$ 	User Name�� . $	 150  �� $ 	User_Name�  -   ExtNoColumn��$ No Extention�� . $	 100  �� $ User_No_Ext�4  .System.Windows.Controls.DataGridTemplateColumn  $ Delete��$ 75    CellTemplate �� .R��� -     
LinkButton#��[ $��Center=�.��Delete�� -   $��btnAdd��$	��Add��$��23��$
��Left=�$��10,10,0,167q�$	��Top=�$	��100���� -   $��gbDetail��$	��Top=�$��Stretch=�$��5q�$�� ��#��[ $��0q�.��� $	��120��.+��� -   $��lblCreatedBy��$��23��$
��Left=�$��	10,10,0,0q�$��
Created By��$	��Top=��� -   $��lblCreatedDate��$��23��$
��Left=�$��	10,40,0,0q�$��Created Date��$	��Top=��� -   $��lblUpdatedBy��$��23��$
��Left=�$��	10,70,0,0q�$��
Updated By��$	��Top=��� -   $��lblUpdatedDate��$��23��$
��Left=�$��
10,100,0,0q�$��Updated Date��$	��Top=��� -   $��lblCreatedByValue��$��23��$
��Left=�$��
103,10,0,0q�$	��Top=��� -   $��lblCreatedDateValue��$��23��$
��Left=�$��
103,40,0,0q�$	��Top=��� -   $��lblUpdatedByValue��$��23��$
��Left=�$��
103,70,0,0q�$	��Top=��� -   $��lblUpdatedDateValue��$��23��$
��Left=�$��103,100,0,0q�$	��Top=��� $	��Top=� �� Orientation$ 
Horizontal=�$��Right=�.+��� -   $��btnSave��$
��Save��$��23��$	��100��$��	0,10,10,0q�$��Center=��� -   $��	btnCancel��$��Cancel��$��23��$	��100��$��	0,10,20,0q�$��Center=�