   M S B A M L   `   `   `  ����   Als    Als.RolePage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       N� Title$
  Role��.���� $��	Role List��$��Stretch=�$��10q�   OutGroupBox#��[  
�������.���� $
��Auto=�$��Stretch=�.��]� -   $��stackPanel1��$
��Auto��$
��Auto����. .+��� $	��Top=�$��Searching Parameter��$��Stretch=�$��5q�  InnerGroupBox#��[ 
���   �.��� .+��� -   $��txtRoleName��$��23��$
��Left=�$��109,14,466,0q�$	��Top=�$	��161���� -   $��lblRoleName��$��23��$
��Left=�$��	17,14,0,0q�$��	Role Name��$	��Top=��� -   $��	btnSearch��$��Search��$��23��$	��100��$��Right=�$��
0,80,140,6q�$��Center=��� -   $��btnReset��$��Reset��$��23��$	��100��$��Right=�$��	0,80,25,6q�$��Center=��� $	��Top=�$��Stretch=�$��Search Result��$��Stretch=�$��5q�#��[ 
���   �.��� .+�&   System.Windows.Controls.DataGrid  -   $��dgRole��   AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�$��
10,45,7,10q�   RowHeaderWidth$ 0Q�   SelectionMode$ Single=�	   SelectionUnit$	 FullRow=�$
   HorizontalScrollBarVisibility$

 Auto=�"   VerticalScrollBarVisibility$
 Auto=�$	��800��$
��1000��$
��Left=�$��Stretch=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  -   RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��   
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ RowNo�  -   RoleNameColumn��$ 	Role Name�� . $	 100  �� $ 	Role_Name�  -	   RoleDescriptionColumn��$ Description�� . $	 200  �� $ Role_Description�4  .System.Windows.Controls.DataGridTemplateColumn  $ Member��$ 75    CellTemplate �� .R��� -
     
LinkButton#��[ $��Center=�.��Member  $ Role Access��$ 75  �� .R��� -   #��[ $��Center=�.��Role Access  $ Update��$ 75  �� .R��� -   #��[ $��Center=�.��Update  $
 View��$ 75  �� .R��� -   #��[ $��Center=�.��View  $ Delete��$ 75  �� .R��� -   #��[ $��Center=�.��Delete�� -   $��	btnCreate��$��Create��$��23��$
��Left=�$��10,10,0,167q�$	��Top=�$	��100��