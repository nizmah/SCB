   M S B A M L   `   `   `  ����   Als    Als.MerchantPriceListPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       N� Title$  Merchant Price List��.���� $��Stretch=�$��10q�$��Stretch=�   OutGroupBox#��[  ���� $��Merchant Price List��
�������.���� $
��Auto=�$��Stretch=�.��]� -   $��stackPanel1��$
��Auto��$
��Auto����. .+��� $	��Top=�$��Searching Parameter��$��Stretch=�$��5q�  InnerGroupBox#��[ 
���   �.��� .+��� -   $��
cbMerchant��$��23��$
��Left=�$��
150,10,0,0q�$	��Top=�$	��161�� �� DisplayMemberPath$ Merchant_Name�� �� SelectedValuePath$ ID������ �� -   $��
cbCardType��$��23��$
��Left=�$��
150,43,0,0q�$	��Top=�$	��161��$ Card_Type_Name��$ ID������ �� -   $��lblMerchantNameValue��$��23��$
��Left=�$��
150,10,0,0q�$	��Top=�$y�Hidden=��� -   $��lblMerchantName��$��23��$
��Left=�$��	17,10,0,0q�$��Merchant Name��$	��Top=��� -   $��lblCardType��$��23��$
��Left=�$��	17,43,0,0q�$��Card Type Name��$	��Top=��� -   $��btnShow��$��Show Price List��$��23��$	��160��$
��Left=�$��20,123,140,6q�$	��Top=��� -   $��lblMerchantID��$��23��$
��Left=�$��
310,10,0,0q�$	��Top=�$y�Hidden=��� -	   $��	btnCancel��$��Cancel��$��23��$
��Left=�$y�Hidden=�$��200,123,45,5q�$	��Top=�$	��100���� -
   $��	groupBox2��$	��Top=�#��[ $��List of Price��$��Stretch=�$��10q�$��Stretch=�$y�Hidden=�.��� .+�&   System.Windows.Controls.DataGrid  -   $��dgMerchantPriceList��   AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�$��
10,45,7,10q�	   RowHeaderWidth$	 0Q�
   SelectionMode$
 Single=�   SelectionUnit$ FullRow=�$   HorizontalScrollBarVisibility$
 Auto=�"   VerticalScrollBarVisibility$
 Auto=�$
��1000��$
��Left=�$��Stretch=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  -   RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��   
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ RowNo�  -   GuestAmountColumn��$ Total Guest�� . $	 125  �� $ Guest_Amount�  -   PriceAmountColumn��$ Price Amount�� . $	 125  �� $ Price_Amount�  -   PointAmontColumn��$ Point Amount�� . $	 125  �� $ Point_Amount�4  .System.Windows.Controls.DataGridTemplateColumn  $ Update��$ 50    CellTemplate �� .R��� -     
LinkButton#��[ $��Center=�.��Update  $
 View��$ 50  �� .R��� -   #��[ $��Center=�.��View  $ Delete��$ 50  �� .R��� -   #��[ $��Center=�.��Delete�� -   $��	btnCreate��$��Create��$��23��$
��Left=�$��10,10,0,167q�$	��Top=�$	��100��