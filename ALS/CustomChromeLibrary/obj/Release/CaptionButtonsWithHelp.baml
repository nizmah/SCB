   M S B A M L   `   `   `  ���� N  JCustomChromeLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:CustomChromeLibraryCustomChromeLibrary  0    *CustomChromeLibrary.CaptionButtonsWithHelp   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1ccl!clr-namespace:CustomChromeLibrary     .����   �� Orientation$  
Horizontal=�$��Right=�$	��Top=�����  �� Path$ CaptionButtonMargin� � 	Resources    GlassButtonCloseStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius	 J� RenderTransformOrigin
 
� Center 
� GradientOrigin � Color � Offset � RowDefinitions �� Triggers P� Property P� Value �� 
TargetName �� ScaleX �� ScaleY  GlassButtonMinimazeStyle �� 
StartPoint �� EndPoint��%�  &	        &	 *    �� " ��.���  � ��
 �������  �@�Cursor$
 Hand����  � �� �� " ��.R��� $ ButtonBorder��$ 0,0,5,0��$��1q�
���   �$	 0.5,0.5&���
� $
 0.5,0.5&�$ 0.5,0.5&�.��� $	 Red��$	 0.5Q�� $ 	#FFB50000��$ 1Q�._��  �� $��1* ��� $��1* �.+��� $��0��$ 0,0,5,0������ .��� $ 	#08FFFFFF��$ 0Q�� $ 	#88FFFFFF��$ 1Q��� $��content��$��2��$��Center=�$��Center=� P�  � �� . . ��� $ ButtonBorder�� � z� V� .~��� $
 0.99Q�$
 0.99Q��� " ��.���  � ��
 �������  �@�Cursor$
 Hand����  � �� �� " ��.R��� $ ButtonBorder��$ 0��$��1,1,0,1q�
���   �$	 0.5,0.5&���
� $
 0.5,0.5&�$ 0.5,0.5&�.��� $ 	#FFAAAAC7��$	 0.1Q�� $ 	#FF2A2A71��$ 1Q�._��  �� $
��0.1* ��� $
��0.4* ��� $
��0.5* �.+��� $��1��$ 1������ $ 0.5,0&�$ 0.5,1&�.��� $ 	#88FFFFFF��$ 0Q�� $ Transparent��$ 1Q��� $��content��$��3��$��Center=�$��Center=� P�  � �� . . ��� $ ButtonBorder�� � z� V� .~��� $
 0.99Q�$
 0.99Q�.+�$   CustomChromeLibrary.HelpButton  #��[ (   "CustomChromeLibrary.MinimizeButton  (   "CustomChromeLibrary.MinimizeButton   Click$ MinimizeButton_Click��'   !CustomChromeLibrary.CaptionButton   CornerRadius$ 0��#��[ (   "CustomChromeLibrary.MaximizeButton  (   "CustomChromeLibrary.MaximizeButton   Click$ MaximizeButton_Click��#��[ %   CustomChromeLibrary.CloseButton  %   CustomChromeLibrary.CloseButton   Click$ CloseButton_Click��#��[  