   M S B A M L   `   `   `  ����   Als    Als.CardPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       N� Title$
  Card��.���� $��Stretch=�$��10q�$��Stretch=�   OutGroupBox#��[  ���� 
�������$��	Card List��.���� $
��Auto=�$��Stretch=�.��]� -   $��stackPanel1��$
��Auto��$
��Auto����. .+��� -   $��	groupBox1��$	��Top=�$��Searching Parameter��$��Stretch=�$��5q�  InnerGroupBox#��[ .��� .+��� -   $��txtBinNo��$��23��$
��Left=�$��
109,14,0,0q�$	��Top=�$	��161���� -   $��txtCardName��$��23��$
��Left=�$��109,43,466,0q�$	��Top=�$	��161���� -   $��
cbCardType��$��23��$
��Left=�$��
109,73,0,0q�$	��Top=�$	��161�� �� DisplayMemberPath$ Card_Type_Name�� �� SelectedValuePath$ ID������ �� -   $��lblBinNi��$��23��$
��Left=�$��	17,14,0,0q�$��BIN No��$	��Top=��� -   $��lblCardName��$��23��$
��Left=�$��	17,43,0,0q�$��	Card Name��$	��Top=��� -   $��lblCardTypeName��$��23��$
��Left=�$��	17,73,0,0q�$��Card Type Name��$	��Top=��� -	   $��	btnSearch��$��Search��$��23��$	��100��$��Right=�$��0,128,140,6q�$��Center=��� -
   $��btnReset��$��Reset��$��23��$	��100��$��Right=�$��
0,128,25,6q�$��Center=��� -   $��	groupBox2��$	��Top=�$��Search Result��#��[ $��Stretch=�$��5q�$��Stretch=�.��� .+�&   System.Windows.Controls.DataGrid  -   $��dgCard��   AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�$��
10,45,7,10q�	   RowHeaderWidth$	 0Q�
   SelectionMode$
 Single=�   SelectionUnit$ FullRow=�$   HorizontalScrollBarVisibility$
 Auto=�"   VerticalScrollBarVisibility$
 Auto=�$
��1000��$
��1000��$
��Left=�$��Stretch=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  -   RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��   
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ RowNo�  -   BinNoColumn��$ BIN No�� . $	 100  �� $ Bin_No�  -   CardNameColumn��$ 	Card Name�� . $	 150  �� $ 	Card_Name�  -   CardDescriptionColumn��$ Card Description�� . $	 150  �� $ Card_Description�  -   CardTypeNameColumn��$ Card Type Name�� . $	 100  �� $ Card_Type_Name�  -   DigitUniqueCardColumn��$ Digit Unique Card�� . $	 120  �� $ Digit_Unique_Card�4  .System.Windows.Controls.DataGridTemplateColumn  $ Update��$ 50    CellTemplate �� .R��� -     
LinkButton#��[ $��Center=�.��Update  $
 View��$ 50  �� .R��� -   #��[ $��Center=�.��View  $ Delete��$ 50  �� .R��� -   #��[ $��Center=�.��Delete�� -   $��	btnCreate��$��Create��$��23��$
��Left=�$��10,10,0,167q�$	��Top=�$	��100��