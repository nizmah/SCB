   M S B A M L   `   `   `  ����N  JCustomChromeLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:CustomChromeLibraryCustomChromeLibrary  5      R NMicrosoft.Windows.Shell, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullbFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.ShellMicrosoft.Windows.Shell 5      (    "CustomChromeLibrary.CaptionButtons   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1ccl!clr-namespace:CustomChromeLibrary     5      XshellFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.Shell     5      .��5
      ��   �� Orientation$  
Horizontal=�6   $��Right=�6'   $	��Top=�6C   ��6[   ��  �� Path$ CaptionButtonMargin� � 	Resources 5   
      GlassButtonCloseStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius	 J� RenderTransformOrigin
 
� Center 
� GradientOrigin � Color � Offset � RowDefinitions �� Triggers P� Property P� Value �� 
TargetName �� ScaleX �� ScaleY  GlassButtonMinimazeStyle �� 
StartPoint �� EndPoint��%�  &	        &	     �� 5      " ��62   .�5      ��  � ��
 �����6/   �� 5       �@�Cursor$
 Hand��6+   �� 5       � �� 5      �� 5      " ��6*   .R�5      �� $ ButtonBorder��6%   $ 0,0,5,0��6;   $��1q�6S   
���   �$	 0.5,0.5&�6{   ��5   "   
� 5   &   $
 0.5,0.5&�6:   $ 0.5,0.5&�6K   .��5   *   � $	 Red��67   $	 0.5Q�6C   � 5   *   $ 	#FFB50000��67   $ 1Q�6I   5   '   ._�5   "   �  5   &   �� 5   *   $��1* �68   �� 5   *   $��1* �68   .+�5   &   �� $��0��6-   $ 0,0,5,0��6:   ��5    *   �� 5!   .   .��5$   2   � $ 	#08FFFFFF��6?   $ 0Q�6Q   � 5%   2   $ 	#88FFFFFF��6?   $ 1Q�6Q   5&   /   5(   '   �� 5)   &   $��content��6�   $��2��67   $��Center=�6H   $��Center=�6e   6�   5*   #   5+       5,      P� 5-   "    � �� . . �5.   &   �� $ ButtonBorder��6H    � z� 5/   *   V� 50   .   .~�51   2   �� $
 0.99Q�6A   $
 0.99Q�6O   52   /   54   '   55   #   57      59      5:      �� 5;      " ��65   .�5<      ��  � ��
 �����6/   �� 5>       �@�Cursor$
 Hand��6+   �� 5?       � �� 5@      �� 5A      " ��6*   .R�5B      �� $ ButtonBorder��6%   $ 0��6;   $��1,1,0,1q�6M   
���   �$	 0.5,0.5&�6{   ��5C   "   
� 5D   &   $
 0.5,0.5&�6:   $ 0.5,0.5&�6K   .��5E   *   � $ 	#FFAAAAC7��67   $	 0.1Q�6I   � 5F   *   $ 	#FF2A2A71��67   $ 1Q�6I   5G   '   ._�5I   "   �  5J   &   �� 5K   *   $
��0.1* �68   �� 5L   *   $
��0.4* �68   �� 5M   *   $
��0.5* �68   .+�5O   &   �� $��1��6-   $ 1��6:   ��5P   *   �� 5Q   .   $ 0.5,0&�6B   $ 0.5,1&�6U   .��5R   2   � $ 	#88FFFFFF��6?   $ 0Q�6Q   � 5S   2   $ Transparent��6?   $ 1Q�6S   5U   /   5W   '   �� 5X   &   $��content��6�   $��3��67   $��Center=�6H   $��Center=�6e   6�   5Y   #   5Z       5[      P� 5\   "    � �� . . �5]   &   �� $ ButtonBorder��6H    � z� 5^   *   V� 5_   .   .~�5`   2   �� $
 0.99Q�6A   $
 0.99Q�6O   5a   /   5c   '   5d   #   5f      5h      5i      5j      .+�5k   
   (   "CustomChromeLibrary.MinimizeButton  #��[ 6   (   "CustomChromeLibrary.MaximizeButton  5l   
   (   "CustomChromeLibrary.MaximizeButton   Click$ MaximizeButton_Click��6O   #��[ 6   6O   %   CustomChromeLibrary.CloseButton  5m   
   #��[  6   5n      5o      