   M S B A M L   `   `   `  ���� N  JCustomChromeLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullV>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibraryCustomChromeLibrary   Als   Als.UserLookupWindow2   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Mcc>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibrary       � Title$  User Lookup�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.���� $��Stretch=�$��10q�$��Stretch=�   OutGroupBox#��[  ���� $��	User List��
�������.���� $
��Auto=�$��Stretch=�.��]� -   $��stackPanel1��$
��Auto��$
��Auto����. .+��� -   $��	groupBox1��$	��Top=�$��Searching Parameter��  InnerGroupBox#��[ $��Stretch=�$��5q�.��� .+��� -   $��	txtUserId��$��23��$
��Left=�$��
109,14,0,0q�$	��Top=�$	��161���� -   $��txtUserName��$��23��$
��Left=�$��109,43,466,0q�$	��Top=�$	��161���� -   $��txtNoExtention��$��23��$
��Left=�$��
109,73,0,0q�$	��Top=�$	��161���� -   $��	lblUserId��$��23��$
��Left=�$��	17,14,0,0q�$��User ID/PSID��$	��Top=��� -   $��lblNoExtention��$��23��$
��Left=�$��	17,73,0,0q�$��No Extention��$	��Top=��� -   $��lblUserName��$��23��$
��Left=�$��	17,43,0,0q�$
��Name��$	��Top=��� -	   $��	btnSearch��$��Search��$��23��$	��100��$��Right=�$��0,128,245,6q�$��Center=��� -
   $��btnReset��$��Reset��$��23��$	��100��$��Right=�$��0,128,135,6q�$��Center=��� -   $��btnClose��$��Close��$��23��$	��100��$��Right=�$��
0,128,25,6q�$��Center=��� -   $��	groupBox2��$	��Top=�$��Search Result��#��[ $��Stretch=�$��5q�$��Stretch=�.��� .+�&   System.Windows.Controls.DataGrid  -   $��dgUser��   AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�$��
10,10,7,10q�	   RowHeaderWidth$	 0Q�
   SelectionMode$
 Single=�   SelectionUnit$ FullRow=�$   HorizontalScrollBarVisibility$
 Auto=�"   VerticalScrollBarVisibility$
 Auto=�$
��1000��$
��Left=�$��Stretch=�$	��450������    Columns 0  *System.Windows.Controls.DataGridTextColumn  -   RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��   
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ RowNo�  -   UserIdColumn��$ User ID/PSID�� . $	 100  �� $ User_ID�  -   UserNameColumn��$
 Name�� . $	 150  �� $ 	User_Name�  -   ExtNoColumn��$ No Extention�� . $	 100  �� $ User_No_Ext�4  .System.Windows.Controls.DataGridTemplateColumn  $ Select��$ 50    CellTemplate �� .R��� -     
LinkButton#��[ $��Center=�.��Select