   M S B A M L   `   `   `  ����N  JCustomChromeLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullV>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibraryCustomChromeLibrary  5   	    Als   Als.UserLookupWindow2   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Mcc>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibrary     5   	     � Title$  User Lookup��5	       � 
ResizeMode$ NoResize=�6    � WindowStartupLocation$ CenterScreen=�60   .��5   
   �� $��Stretch=�6   $��10q�61   $��Stretch=�6>      OutGroupBox#��[  6Z   ��5      �� 5      $��	User List��6   
�������6-   .��5   
   �� $
��Auto=�6   $��Stretch=�6:   .��5      ]� -   5        $��stackPanel1��5   &   $
��Auto��6   $
��Auto��69   ��. .+�5      �� -   5        $��	groupBox1��5   �   $	��Top=�6   $��Searching Parameter��60     InnerGroupBox#��[ 6M   $��Stretch=�6w   $��5q�6�   .��5      � .+�5      �� -   5        $��	txtUserId��5   ]   $��23��6"   $
��Left=�6.   $��
109,14,0,0q�6I   $	��Top=�6n   $	��161��6�   �� 5      -   5        $��txtUserName��5   _   $��23��6"   $
��Left=�6.   $��109,43,466,0q�6I   $	��Top=�6r   $	��161��6�   �� 5      -   5        $��txtNoExtention��5   ]   $��23��6"   $
��Left=�6.   $��
109,73,0,0q�6I   $	��Top=�6s   $	��161��6�   �� 5      -   5        $��	lblUserId��5   ^   $��23��6$   $
��Left=�60   $��	17,14,0,0q�6K   $��User ID/PSID��6o   $	��Top=�6�   �� 5      -   5        $��lblNoExtention��5   ^   $��23��6$   $
��Left=�60   $��	17,73,0,0q�6K   $��No Extention��6t   $	��Top=�6�   �� 5      -   5        $��lblUserName��5   ^   $��23��6$   $
��Left=�60   $��	17,43,0,0q�6K   $
��Name��6q   $	��Top=�6}   �� 5      -	   5        $��	btnSearch��5   {   $��Search��6!   $��23��62   $	��100��6>   $��Right=�6J   $��0,128,245,6q�6f   $��Center=�6�   6�   �� 5      -
   5        $��btnReset��5   z   $��Reset��6!   $��23��61   $	��100��6=   $��Right=�6I   $��0,128,135,6q�6e   $��Center=�6�   6�   �� 5      -   5        $��btnClose��5   y   $��Close��6!   $��23��61   $	��100��6=   $��Right=�6I   $��
0,128,25,6q�6e   $��Center=�6�   6�   5      5      �� 5      -   5        $��	groupBox2��5   �   $	��Top=�6   $��Search Result��60   #��[ 6G   $��Stretch=�6q   $��5q�6�   $��Stretch=�6�   .��5       � .+�5!      &   System.Windows.Controls.DataGrid  -   5        $��dgUser��5#         AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�5"   5   $��
10,10,7,10q�6h   	   RowHeaderWidth$	 0Q�5#   )   
   SelectionMode$
 Single=�6<      SelectionUnit$ FullRow=�6S   $   HorizontalScrollBarVisibility$
 Auto=�5$      "   VerticalScrollBarVisibility$
 Auto=�6@   $
��1000��6c   $
��Left=�6t   $��Stretch=�6�   $	��450��6�   ��5"   P   ��    Columns 5%      0  *System.Windows.Controls.DataGridTextColumn  5&   "   -   5        RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��5&   i      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6J   ��  �� Path$ RowNo�6�     5'   "   -   5        UserIdColumn��$ User ID/PSID��5'   l    . $	 100 6�    6K   �� $ User_ID�6�     5(   "   -   5        UserNameColumn��$
 Name��5(   p    . $	 150 6�    6M   �� $ 	User_Name�6�     5)   "   -   5        ExtNoColumn��$ No Extention��5)   o    . $	 100 6�    6J   �� $ User_No_Ext�6�   4  .System.Windows.Controls.DataGridTemplateColumn  5*   "   $ Select��69   $ 50 6I      CellTemplate 5+   &   �� 5,   *   .R�5-   .   �� -   5          
LinkButton#��[ 5-   5   $��Center=�6n   .��6�   Select6�   5.   +   50   #   54      56      57      58      59      5:      5<      