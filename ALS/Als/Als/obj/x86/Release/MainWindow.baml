   M S B A M L   `   `   `  ���� R  NMicrosoft.Windows.Shell, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullbFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.ShellMicrosoft.Windows.Shell  N JCustomChromeLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullV>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibraryCustomChromeLibrary > :Als, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:AlsAls    Als.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     XshellFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.Shell     Nccl>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibrary     #localclr-namespace:Als     -     � Title$  
MainWindow��$	��650��$	��900�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � 
ResizeMode$ 	CanResize=�$	��650��$	��900�� � AllowsTransparency .  � Icon$ /Als;component/scbIco.ico�� � WindowState$ Normal=�*   $Microsoft.Windows.Shell.WindowChrome   WindowChrome      ResizeBorderThickness$ 6q�	   CaptionHeight$	 43Q�
   CornerRadius$
 1��   GlassFrameThickness$ 1q�.��� .+��� 
���   �$	��0.5q� �� CornerRadius$ 1,1,0,0��$	��Top=�����  �� Path$ CaptionButtonMargin�����  �� EndPoint$ 1,0.5&� �� 
StartPoint$ 0,0.5&�.���  � Color$ 	#FF10495C�� � Offset$ 0Q�� $ 	#FF1B8EC1��$ 1Q�._�� $	��111��.+��� $��36(�&   System.Windows.Media.TextOptions   TextRenderingMode$ Aliased=�   TextFormattingMode$ Ideal=�$��Browallia New)�$��Center=�$��20,10q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#D3FFFFFF��$ 0Q�� $ 	#F1FFFFFF��$ 1Q�� $ 	#ACD8CFCF��$ 0.256Q�� $ 	#BFEBE7E7��$
 0.25Q�Airport Lounge Systemo� -   $��mainMenu��$��Stretch=�$��0,73,0,0q�$��Stretch=�.@�m� $��Admin��$��5,0,0,0q�.@�m� -   $
��Role��m� -   $
��User��m� -   $��Delete Audit Trail��m� -   $��E_xit��m� $��Master��.@�m� -   $��	Card Type��m� -   $
��Card��m� -	   $
��Bank��m� -
   $��Vendor��m� -   $��Merchant��m� -   $��Merchant Price List��m� -   $��
GL Account��m� $��Transaction��.@�m� -   $��Daily Transaction��m� -   $ ��Approval Daily Transaction��m� -   $��Monthly Transaction��m� $��Report��.@�m� -   $��Monthly Report��m� -   $#��Comparison Transaction Report��m� -   $��Memo Payment��m� -   $��Audit Trail��(  "CustomChromeLibrary.CaptionButtons  ���	#FF2CDDE6 � -   $��	mainFrame��$��
0,112,0,40q��� 
�������$��1q�$��42��$��Bottom=����� $ 1,0.5&�$ 0,0.5&�.��� $ 	#FF10495C��$ 0Q�� $ 	#FF1B8EC1��$ 1Q�._�� $	��825��.+��� -   $��
textBlock1��$��25��$��Copyright Code ID©2015��$��302,0,291,0q�$��Browallia New)�$��24(��� $��23��$��Right=�$��	0,11,20,0q�$	��Top=�
�������$
��Bold"�$��13(�$��	Condensed&��� -   $��
textBlock2��$��23��$��Right=�$��
0,56,130,0q�$��Current User:��$	��Top=�
�������$��75���� -   $��
textBlock3��
�������$��23��$��Right=�$��	0,56,20,0q�$	��Top=�$
��Bold"� �� TextAlignment$ Right=����� $ 	FirstName� �� TargetNullValue$ x�� �� Mode$ OneWay=� �� NotifyOnTargetUpdated . 