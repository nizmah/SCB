   M S B A M L   `   `   `  ����R  NMicrosoft.Windows.Shell, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullbFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.ShellMicrosoft.Windows.Shell  5   	   N JCustomChromeLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullV>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibraryCustomChromeLibrary 5   	   > :Als, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:AlsAls 5         Als.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   XshellFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.Shell     5   	   Nccl>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibrary     5   	   #localclr-namespace:Als     5      -   5          � Title$  
MainWindow��5   	   $	��650��6   $	��900��6*    � WindowStartupLocation$ CenterScreen=�66    � WindowStyle$
 None=�6[    � 
ResizeMode$ 	CanResize=�5   .   $	��650��6E   $	��900��6U    � AllowsTransparency .  � Icon$ /Als;component/scbIco.ico��6�    � WindowState$ Normal=�6�   *   $Microsoft.Windows.Shell.WindowChrome   WindowChrome 5
        5   
      ResizeBorderThickness$ 6q�5      	   CaptionHeight$	 43Q�5      
   CornerRadius$
 1��5         GlassFrameThickness$ 1q�5      5      .��5      � .+�5   
   �� 
���   �$	��0.5q�6%    �� CornerRadius$ 1,1,0,0��6;   $	��Top=�6~   ��6R   ��  �� Path$ CaptionButtonMargin���5      �� 5       �� EndPoint$ 1,0.5&�6&    �� 
StartPoint$ 0,0.5&�67   .��5      �  � Color$ 	#FF10495C��6#    � Offset$ 0Q�65   � 5      $ 	#FF1B8EC1��6#   $ 1Q�65   5      ._�5      � $	��111��6   .+�5      �� $��36(�6   &   System.Windows.Media.TextOptions   TextRenderingMode$ Aliased=�6*      TextFormattingMode$ Ideal=�5       $��Browallia New)�6C   $��Center=�5!      $��20,10q�69   ��5"      �� 5#      $ 0.5,1&�6.   $ 0.5,0&�6?   .��5$      � $ 	#D3FFFFFF��6+   $ 0Q�6=   � 5%      $ 	#F1FFFFFF��6+   $ 1Q�6=   � 5&      $ 	#ACD8CFCF��6+   $ 0.256Q�6=   � 5'      $ 	#BFEBE7E7��6+   $
 0.25Q�6=   5(      Airport Lounge System5)   C   o� 5*      -   5        $��mainMenu��5*   G   $��Stretch=�6   $��0,73,0,0q�65   $��Stretch=�6W   .@�5+      m� $��Admin��6   $��5,0,0,0q�6.   .@�5-      m� -   5        $
��Role��5-   #   61   m� 50      -   5        $
��User��50   #   61   m� 51      -   5        $��Delete Audit Trail��51   #   6?   m� 52      -   5        $��E_xit��52   #   62   53      m� 55      $��Master��6   .@�56      m� -   5        $��	Card Type��56   #   66   m� 57      -   5        $
��Card��57   #   61   m� 58      -	   5        $
��Bank��58   #   61   m� 59      -
   5        $��Vendor��59   #   63   m� 5:      -   5        $��Merchant��5:   #   65   m� 5;      -   5        $��Merchant Price List��5;   #   6@   m� 5<      -   5        $��
GL Account��5<   #   67   5=      m� 5>      $��Transaction��6   .@�5?      m� -   5        $��Daily Transaction��5?   #   6>   m� 5@      -   5        $ ��Approval Daily Transaction��5@   #   6G   m� 5A      -   5        $��Monthly Transaction��5A   #   6@   5C      m� 5D      $��Report��6   .@�5E      m� -   5        $��Monthly Report��5E   #   6;   m� 5F      -   5        $#��Comparison Transaction Report��5F   #   6J   m� 5G      -   5        $��Memo Payment��5G   #   69   m� 5H      -   5        $��Audit Trail��5H   #   68   5I      5J      5K      5M      (  "CustomChromeLibrary.CaptionButtons  5N   
   ���	#FF2CDDE66    � 5O   
   -   5        $��	mainFrame��5O   $   $��
0,112,0,40q�6   6$   �� 5P   
   
�������$��1q�6&   $��42��6:   $��Bottom=�6F   ��5Q      �� 5R      $ 1,0.5&�6&   $ 0,0.5&�67   .��5S      � $ 	#FF10495C��6#   $ 0Q�65   � 5T      $ 	#FF1B8EC1��6#   $ 1Q�65   5U      ._�5W      � $	��825��6   .+�5X      �� -   5        $��
textBlock1��5X   (   $��25��6   $��Copyright Code ID©2015��6:   $��302,0,291,0q�6X   $��Browallia New)�6n   $��24(�6�   �� 5Y      $��23��6   $��Right=�6(   $��	0,11,20,0q�6D   $	��Top=�6X   
�������$
��Bold"�6�   $��13(�6�   $��	Condensed&�6�   5Z      5[      5\      �� 5]   
   -   5        $��
textBlock2��5]   P   $��23��6   $��Right=�6    $��
0,56,130,0q�6<   $��Current User:��6b   $	��Top=�6w   
�������$��75��6�   �� 5^   
   -   5        $��
textBlock3��5^   f   
�������$��23��6+   $��Right=�67   $��	0,56,20,0q�6S   $	��Top=�6x   $
��Bold"�5_       �� TextAlignment$ Right=�6�   ��6&   �� $ 	FirstName� �� TargetNullValue$ x�� �� Mode$ OneWay=� �� NotifyOnTargetUpdated . 6�   5`      5a      