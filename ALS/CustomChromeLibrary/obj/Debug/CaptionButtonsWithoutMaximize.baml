   M S B A M L   `   `   `  ����N  JCustomChromeLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:CustomChromeLibraryCustomChromeLibrary  5      R NMicrosoft.Windows.Shell, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullbFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.ShellMicrosoft.Windows.Shell 5      7    1CustomChromeLibrary.CaptionButtonsWithoutMaximize   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1ccl!clr-namespace:CustomChromeLibrary     5      XshellFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.Shell     5      .��5      ��   �� Orientation$  
Horizontal=�6   $��Right=�6*   $	��Top=�6F   ��6^   ��  �� Path$ CaptionButtonMargin� � 	Resources 5   
      GlassButtonCloseStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius	 J� RenderTransformOrigin
 
� Center 
� GradientOrigin � Color � Offset � RowDefinitions �� Triggers P� Property P� Value �� 
TargetName �� ScaleX �� ScaleY  GlassButtonMinimazeStyle �� 
StartPoint �� EndPoint��%�  &	        &	     �� 5      " ��62   .�5      ��  � ��
 �����6/   �� 5       �@�Cursor$
 Hand��6+   �� 5       � �� 5      �� 5      " ��6*   .R�5      �� $ ButtonBorder��6%   $ 0,0,5,0��6;   $��1q�6S   
���   �$	 0.5,0.5&�6{   ��5   "   
� 5   &   $
 0.5,0.5&�6:   $ 0.5,0.5&�6K   .��5   *   � $	 Red��67   $	 0.5Q�6C   � 5   *   $ 	#FFB50000��67   $ 1Q�6I   5   '   ._�5   "   �  5   &   �� 5   *   $��1* �68   �� 5   *   $��1* �68   .+�5    &   �� $��0��6-   $ 0,0,5,0��6:   ��5!   *   �� 5"   .   .��5%   2   � $ 	#08FFFFFF��6?   $ 0Q�6Q   � 5&   2   $ 	#88FFFFFF��6?   $ 1Q�6Q   5'   /   5)   '   �� 5*   &   $��content��6�   $��2��67   $��Center=�6H   $��Center=�6e   6�   5+   #   5,       5-      P� 5.   "    � �� . . �5/   &   �� $ ButtonBorder��6H    � z� 50   *   V� 51   .   .~�52   2   �� $
 0.99Q�6A   $
 0.99Q�6O   53   /   55   '   56   #   58      5:      5;      �� 5<      " ��65   .�5=      ��  � ��
 �����6/   �� 5?       �@�Cursor$
 Hand��6+   �� 5@       � �� 5A      �� 5B      " ��6*   .R�5C      �� $ ButtonBorder��6%   $ 0��6;   $��1,1,0,1q�6M   
���   �$	 0.5,0.5&�6{   ��5D   "   
� 5E   &   $
 0.5,0.5&�6:   $ 0.5,0.5&�6K   .��5F   *   � $ 	#FFAAAAC7��67   $	 0.1Q�6I   � 5G   *   $ 	#FF2A2A71��67   $ 1Q�6I   5H   '   ._�5J   "   �  5K   &   �� 5L   *   $
��0.1* �68   �� 5M   *   $
��0.4* �68   �� 5N   *   $
��0.5* �68   .+�5P   &   �� $��1��6-   $ 1��6:   ��5Q   *   �� 5R   .   $ 0.5,0&�6B   $ 0.5,1&�6U   .��5S   2   � $ 	#88FFFFFF��6?   $ 0Q�6Q   � 5T   2   $ Transparent��6?   $ 1Q�6S   5V   /   5X   '   �� 5Y   &   $��content��6�   $��3��67   $��Center=�6H   $��Center=�6e   6�   5Z   #   5[       5\      P� 5]   "    � �� . . �5^   &   �� $ ButtonBorder��6H    � z� 5_   *   V� 5`   .   .~�5a   2   �� $
 0.99Q�6A   $
 0.99Q�6O   5b   /   5d   '   5e   #   5g      5i      5j      5k      .+�5l   
   (   "CustomChromeLibrary.MinimizeButton  '   !CustomChromeLibrary.CaptionButton   CornerRadius$ 0��6   #��[ 6.   5n      %   CustomChromeLibrary.CloseButton  5o   
   #��[  6   5p      5q      