   M S B A M L   `   `   `  ����   Als    Als.CardTypePage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       N� Title$  	Card Type��.���� $��Stretch=�$��10q�$��Stretch=�   OutGroupBox#��[  ���� $��Card Type List��
�������.���� $
��Auto=�$��Stretch=�.��]� -   $��stackPanel1��$
��Auto��$
��Auto����. .+��� -   $��	groupBox1��$	��Top=�$��Searching Parameter��  InnerGroupBox#��[ $��Stretch=�$��5q�.��� .+��� -   $��txtCardTypeName��$��23��$
��Left=�$��109,14,466,0q�$	��Top=�$	��161���� -   $��lblCardTypeName��$��23��$
��Left=�$��	17,14,0,0q�$��Card Type Name��$	��Top=��� -   $��	btnSearch��$��Search��$��23��$	��100��$��Right=�$��0,128,140,6q�$��Center=��� -   $��btnReset��$��Reset��$��23��$	��100��$��Right=�$��
0,128,25,6q�$��Center=��� -   $��	groupBox2��$	��Top=�$��Search Result��#��[ $��Stretch=�$��Stretch=�$��5q�.��� .+�&   System.Windows.Controls.DataGrid  -   $��
dgCardType��   AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�$��
10,45,7,10q�   RowHeaderWidth$ 0Q�   SelectionMode$ Single=�	   SelectionUnit$	 FullRow=�$
   HorizontalScrollBarVisibility$

 Auto=�"   VerticalScrollBarVisibility$
 Auto=�$
��1000��$
��1000��$
��Left=�$��Stretch=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  -	   RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��   
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ RowNo�  -
   CardTypeNameColumn��$ Card Type Name�� . $	 100  �� $ Card_Type_Name�  -   CardTypeDescriptionColumn��$ Description�� . $	 200  �� $ Card_Type_Description�4  .System.Windows.Controls.DataGridTemplateColumn  $ Update��$ 75    CellTemplate �� .R��� -     
LinkButton#��[ $��Center=�.��Update  $
 View��$ 75  �� .R��� -   #��[ $��Center=�.��View  $ Delete��$ 75  �� .R��� -   #��[ $��Center=�.��Delete�� -   $��	btnCreate��$��Create��$��23��$
��Left=�$��10,10,0,167q�$	��Top=�$	��100��