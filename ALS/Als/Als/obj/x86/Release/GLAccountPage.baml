   M S B A M L   `   `   `  ����   Als    Als.GLAccountPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       N� Title$  
GL Account��.���� $��Stretch=�$��10q�$��Stretch=�   OutGroupBox#��[  ���� $��GL Account List��
�������.���� $
��Auto=�$��Stretch=�.��]� -   $��stackPanel1��$
��Auto��$
��Auto����. .+��� -   $��	groupBox1��$	��Top=�$��Searching Parameter��  InnerGroupBox#��[ $��Stretch=�$��5q�.��� .+��� -   $��txtGLAccountNo��$��23��$
��Left=�$��
150,14,0,0q�$	��Top=�$	��161���� -   $��txtGLAccountName��$��23��$
��Left=�$��
150,43,0,0q�$	��Top=�$	��161���� -   $��lblGLAccountNo��$��23��$
��Left=�$��	17,14,0,0q�$��GL Account No��$	��Top=��� -   $��lblGLAccountName��$��23��$
��Left=�$��	17,43,0,0q�$��GL Account Name��$	��Top=��� -   $��	btnSearch��$��Search��$��23��$	��100��$��Right=�$��0,128,140,6q�$��Center=��� -   $��btnReset��$��Reset��$��23��$	��100��$��Right=�$��
0,128,25,6q�$��Center=��� -	   $��	groupBox2��$	��Top=�$��Search Result��#��[ $��Stretch=�$��5q�$��Stretch=�.��� .+�&   System.Windows.Controls.DataGrid  -
   $��dgGLAccount��   AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     HeadersVisibility$ Column=�$��
10,45,7,10q�   RowHeaderWidth$ 0Q�   SelectionMode$ Single=�	   SelectionUnit$	 FullRow=�$
   HorizontalScrollBarVisibility$

 Auto=�"   VerticalScrollBarVisibility$
 Auto=�$
��1000��$
��1000��$
��Left=�$��Stretch=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  -   RowNoColumn��,  &System.Windows.Controls.DataGridColumn   Header$ No��   
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ RowNo�  -   
GLNoColumn��$ GL No�� . $ 75  �� $ GL_No�  -   GLNameColumn��$ GL Name�� . $ 75  �� $ GL_Name�  -   TCColumn��$ TC�� . $ 50  �� $ TC�  -   AccEntryColumn��$ 	Acc Entry�� . $ 75  �� $ 	Acc_Entry�  -   GLProdColumn��$ GL Prod�� . $ 75  �� $ GL_Prod�  -   GLDeptColumn��$ GL Dept�� . $ 75  �� $ GL_Dept�  -   GLUnitColumn��$ GL Unit�� . $ 75  �� $ GL_Unit�  -   GLClassColumn��$ GL Class�� . $ 75  �� $ GL_Class�  -   GLDescriptionColumn��$ Remarks�� . $ 75  �� $ GL_Description�4  .System.Windows.Controls.DataGridTemplateColumn  $ Update��$ 50    CellTemplate �� .R��� -     
LinkButton#��[ $��Center=�.��Update  $
 View��$ 50  �� .R��� -   #��[ $��Center=�.��View  $ Delete��$ 50  �� .R��� -   #��[ $��Center=�.��Delete�� -   $��	btnCreate��$��Create��$��23��$
��Left=�$��10,10,0,167q�$	��Top=�$	��100��