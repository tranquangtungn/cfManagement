   M S B A M L   `   `   `  ����?  ;CNPM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CNPMCNPM  5   	       
CNPM.Login   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v19.1, Version=19.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:CNPM     5   	     � Title$  Đăng nhập��5	   	   $	��450��6   $	��800��6(    � WindowStartupLocation$ CenterScreen=�64    � 
ResizeMode$ NoResize=�6Y    � WindowStyle$ ThreeDBorderWindow=�6o   .��5
      � ��5   
   �� 5       �� ImageSource$" Resoures/maxresdefault-1.png��6    j� Stretch$ UniformToFill=�6D   .+�5   
   �� $��Đăng nhập��5      $
��Left=�5      $��	74,57,0,0q�5      $	��Top=�5      $��Arial)�5      $��24(�5      $
��Bold"�5       J� RenderTransformOrigin$ 0.246,0.763&�5      �� 5   
   -   5         ��Name$ txtUser��5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tên đăng nhập��5      	   
Foreground
	 � � �.  (MaterialDesignThemes.Wpf.TextFieldAssist
   UnderlineBrush

 � � � %  !MaterialDesignFloatingHintTextBox#��[  5      $��61,95,491,259q�6H   $��Arial)�6_   $��22(�6r   '  !MaterialDesignThemes.Wpf.PackIcon  5   
   �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ UserOutline=�5      $��35��5      $��35��5       $��
11,115,0,0q�5!      �� 5"   
   -   5        $ txtPass��5#      $ Mật khẩu��5$      
	 � � �

 � � �$��61,167,491,187q�5'      #��[  5(      $��Arial)�5)      $y�	Collapsed=�5*      $��22(�5+      G� 5,   
   -   5         G�Name$ passBox��5-      $ Mật khẩu��5.      
	 � � �

 � � � ) %MaterialDesignFloatingHintPasswordBox#��[ 51      $��61,167,491,187q�52      $��Arial)�53      $��22(�54      �� 55   
   -   5         ��Name$ checkBox_ShowPass��56        MaterialDesignCheckBox#��[ 57      $��134,235,474,122q�58      $��Hiện mật khẩu��59      $��Arial)�5:      $��22(�5;        5<   
   $ 
KeyOutline=�5=      $��30��5>      $��30��5?      $��
10,185,0,0q�5@      �� 5A   
   -   5         ��Name$ btnLogin��5B       # MaterialDesignRaisedLightButton#��[ 5C      $
��Auto��5D      +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 7��5E      
����ح� � ToolTip$8 2MaterialDesignRaisedLightButton with Round Corners��5G      $��61,304,575,63q�5H      .��5I      �� $��Đăng nhập��5J      $��Arial)�5K      $��22(�5L      $	��108��5M      5N      �� 5O   
   -   5         ��Name$ lblForgotPassWord��5P      $��Quên mật khẩu��5Q      $
��Left=�5R      $��
88,363,0,0q�5S      $	��Top=�5T      $��Arial)�5U      $��12(�5V      $ 0.246,0.763&�5W      $��Italic$�61   ��6D   Z� 5X      5Y      