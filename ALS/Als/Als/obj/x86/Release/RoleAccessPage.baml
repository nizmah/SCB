   M S B A M L   `   `   `  ����   Als    Als.RoleAccessPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       N� Title$  Role Access��.���� $��Stretch=�$��10q�$��Stretch=�   OutGroupBox#��[  ���� $��Role Access List��
�������.���� $
��Auto=�$��Stretch=�.��]� -   $��stackPanel1��$
��Auto��$
��Auto����. .+��� -   $��	groupBox1��$	��Top=�$��Stretch=�$��5q�$��0q�.��� .+��� -   $��lblRoleNameValue��$��23��$
��Left=�$��
109,14,0,0q�$	��Top=�$	��161���� -   $��lblRoleName��$��23��$
��Left=�$��	17,14,0,0q�$��	Role Name��$	��Top=��� -   $��	groupBox2��$	��Top=�$��List of Module Access��$��Stretch=�$��5q�  InnerGroupBox#��[ .��� .+�&   System.Windows.Controls.DataGrid  -   $��dgRoleAccess��   AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�$��
10,10,7,10q�   RowHeaderWidth$ 0Q�   SelectionMode$ Single=�	   SelectionUnit$	 FullRow=�$
   HorizontalScrollBarVisibility$

 Auto=�"   VerticalScrollBarVisibility$
 Auto=�$
��1000��$
��1000��$
��Left=�$��Stretch=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  -   RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��   
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ RowNo�  -   ModuleNameColumn��$ Module Name�� . $	 200  �� $ Module_Name�4  .System.Windows.Controls.DataGridTemplateColumn  $ 75    HeaderTemplate �� .R��� $��25��$��Center=�.��Create   CellTemplate �� .R��� $��	chkCreate��$��Center=� d� 	IsChecked �� $ Create_Flag� �� NotifyOnTargetUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�  $ 75  �� .R��� $��25��$��Center=�.��Update �� .R��� $��	chkUpdate��$��Center=� �� $ Update_Flag� . $ PropertyChanged=�  $ 75  �� .R��� $��25��$��Center=�.��View �� .R��� $��chkView��$��Center=� �� $ 	View_Flag� . $ PropertyChanged=�  $ 75  �� .R��� $��25��$��Center=�.��Delete �� .R��� $��	chkDelete��$��Center=� �� $ Delete_Flag� . $ PropertyChanged=�  $ 75  �� .R��� $��25��$��Center=�.��Upload �� .R��� $��	chkUpload��$��Center=� �� $ Upload_Flag� . $ PropertyChanged=��� -	   $��gbDetail��$	��Top=�$��Stretch=�$��5q�$�� ��$��0q�#��[ .��� .+��� -
   $��lblCreatedBy��$��23��$
��Left=�$��	10,10,0,0q�$��
Created By��$	��Top=��� -   $��lblCreatedDate��$��23��$
��Left=�$��	10,40,0,0q�$��Created Date��$	��Top=��� -   $��lblUpdatedBy��$��23��$
��Left=�$��	10,70,0,0q�$��
Updated By��$	��Top=��� -   $��lblUpdatedDate��$��23��$
��Left=�$��
10,100,0,0q�$��Updated Date��$	��Top=��� -   $��lblCreatedByValue��$��23��$
��Left=�$��
103,10,0,0q�$	��Top=��� -   $��lblCreatedDateValue��$��23��$
��Left=�$��
103,40,0,0q�$	��Top=��� -   $��lblUpdatedByValue��$��23��$
��Left=�$��
103,70,0,0q�$	��Top=��� -   $��lblUpdatedDateValue��$��23��$
��Left=�$��103,100,0,0q�$	��Top=�� $	��Top=�$��Stretch=�$��Stretch=�.+��� -   $��btnSave��$
��Save��$��23��$	��100��$��Right=�$��
0,10,135,0q�$��Center=��� -   $��	btnCancel��$��Cancel��$��23��$	��100��$��Right=�$��	0,10,20,0q�$��Center=��� -   $��lblID��$��23��$
��Left=�$��
306,10,0,0q�$	��Top=�$y�Hidden=�