   M S B A M L   `   `   `  ����R  NMicrosoft.Windows.Shell, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullbFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.ShellMicrosoft.Windows.Shell  5   	   N JCustomChromeLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullV>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibraryCustomChromeLibrary 5   	    Als   Als.LoginWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   XshellFclr-namespace:Microsoft.Windows.Shell;assembly=Microsoft.Windows.Shell     5   	   Nccl>clr-namespace:CustomChromeLibrary;assembly=CustomChromeLibrary     5   	     � Title$  Login��5   	   $	��269��6   $	��459��6$    � 
ResizeMode$ NoResize=�60    � ShowInTaskbar .   � Topmost .  � WindowStartupLocation$ CenterScreen=�6k    � AllowsTransparency .   J� Opacity$ 1Q�6�   ��5      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5	      � 	 � Color$	 	#9B2E78A4��6   
 � Offset$
 0Q�6-   � 5
      $	 	#B16A8FFA��6   $
 0.328Q�6-   � 5      $	 	#AD588CFF��6   $
 0.254Q�6-   � 5      $	 	#970F60F9��6   $
 0.984Q�6-   � 5      $	 	#AB8CAAED��6   $
 0.639Q�6-   5      *   $Microsoft.Windows.Shell.WindowChrome   WindowChrome 5        5   
      ResizeBorderThickness$ 6q�5         CaptionHeight$ 43Q�5         CornerRadius$ 20��5         GlassFrameThickness$ -1q�5      5      .��5      � $	��208��6   $	��407��6   .+�5   
   �� -   5        $	��img��5      $	��168��6   $��20,10,0,158q�6'   $
��Left=�6<   �� 5   
   -   5        $��
textBlock1��5      $��38��5      $��Right=�6    $��	53,12,0,0q�6<   $	��Top=�5   &   $��24(�6>   $��Normal&�6L   $��	ExtraBold"�5      ��5      �� 6$   $ 0.5,1&�68   $ 0.5,0&�6I   .��6]   � $	 White��6j   $
 0.651Q�6x   � 6�   $	 White��6�   $
 1Q�6�   � 6�   $	 	#A8404040��6�   $
 0.587Q�6�   � 6�   $	 	#F8C8C8DE��6�   $
 0.579Q�6  � 6  $	 	#ABFFFFFF��6"  $
 0Q�64  6C  $��Airport Lounge System��5       J� Effect 5    
   3  -System.Windows.Media.Effects.DropShadowEffect  5!         
BlurRadius$ 2Q�6      Color$ Black��6.      ShadowDepth$ 2Q�6<   5#      �� 5$   
   -   5        $��txtUsr��5$   M   $��23��6   $
��Left=�6   $��
124,74,0,0q�69   $	��Top=�6[   $	��200��6s   ���	#60FFFFFF �� MaxLines$ 20��6�   $��1��6�   $��Center=�6�    �� 	MaxLength$ 50��6�   �� 5%   
   -   5        $��btnLogin��5%   �   $��Login��6   $��30��6!   $��Right=�6-   $��	0,0,114,5q�6I      ButtonLogin#��[  6\   $��Bottom=�6�   $	��100��6�   $��3��6�    �� 	IsDefault . 5'      �� 5(   
   -   5        $��btnExit��5(   Y   $
��Exit��6   $��30��6    $��Right=�6,   $��0,0,8,5q�6H   $��Bottom=�6h   $	��100��6�   $��4��6�    �� IsCancel . 5*      �� 5+   
   -   5        $��label1��5+   _   $��
User Name:��6   $��28��6%   $
��Left=�61   $��	47,72,0,0q�6L   $	��Top=�6m   �� 5,   
   -   5        $��label2��5,   _   $��	Password:��6   $��28��6$   $
��Left=�60   $��
55,108,0,0q�6K   $	��Top=�6m   G� 51   
   -   5        $��txtPwd��51   R   $��23��6   $
��Left=�6"   $��124,108,0,0q�6=   $	��Top=�6`   $	��200��6x   ���	#61FFFFFF$��2��6�   $��Center=�6�    G� 	MaxLength$ 50��6�   �� 52   
   -	   5        $��
textBlock3��52   M   $��20��6   $
��Left=�6    $��5,0,0,-2q�6;   $��Copyright Code Id 2015©��6_   $��Bottom=�6~   
�������$��10(�6�   53      54      