   M S B A M L   `   `   `  ����?  ;CNPM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CNPMCNPM  5          CNPM.ucTimeKeepManagement   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v19.1, Version=19.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      localclr-namespace:CNPM  5      
�������.��5
      � .+�5   
   �� $#��Thông tin mẫu chấm công��6   $
��Left=�64   $	��222��6O   $��
176,21,0,0q�6\   $	��Top=�6p   $	��890��6�   $��16(�6�   ��5      �� 5      6   .��5      � .+�5      �� $��Tên nhân viên:��6   $
��Left=�61   $��
130,10,0,0q�6L   $	��Top=�6`   $��16(�6x   �� 5      -   5          ��Name$  cboTenNhanVien��5      $��241,9,130,143q�5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$  ��5       �� 
IsEditable . $��16(�5      $��42��6#   �� 5      $��Ngày làm:��6   $
��Left=�6,   $��
130,56,0,0q�6G   $	��Top=�6[   $��16(�6s    J� RenderTransformOrigin$ 0.593,-0.042&�6�   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ 
dtpNgayLam��5      $
��Left=�5      $��
241,56,0,0q�5      $	��Top=�5      $��35��5      $	��507��6!   $��16(�5      �� 5      $��Số ca làm:��6   $
��Left=�6-   $��
130,97,0,0q�6H   $	��Top=�6\   $��16(�6t   �� 5       -   5        $  
cboSocalam��5!      $��241,96,130,56q�5"      $ 1��5#      $��16(�5$      $��42��6#   .@�5%      �� .��6#   15'      �� 5(      .��6#   25*      �� 5+      .��6#   35-      �� 5.      .��6#   450      51      �� 52      $��	Ghi chú:��6   $
��Left=�6+   $��130,153,0,0q�6F   $	��Top=�6[   $��16(�6s   $ 0.541,-0.095&�6�   �� 53      -   5         ��Name$ txtNote��53      $
��Left=�6+   $��41��6F   $��241,143,0,0q�6R    �� TextWrapping$
 Wrap=�6g   $	��Top=�6{   $	��507��6�   $��16(�6�   54      55      �� 56   
   -   5         ��Name$ 	btnInsert��57       #  MaterialDesignRaisedLightButton#��[  58       � ToolTip$4 .Resource name: MaterialDesignRaisedLightButton��59      $��18(�5:      ���	#FF6DA7F0$��345,266,775,354q�5<      $
��Auto��5=      $
��Bold"�5>      .��6'   Thêm5@      �� 5A   
   -   5        $ 	btnDelete��5B      #��[  5C      $4 .Resource name: MaterialDesignRaisedLightButton��5D      $��18(�5E      ���	#FF6DA7F0$��461,266,659,354q�5G      $
��Auto��5H      $
��Bold"�5I      .��6'   Xóa5K      �� 5L   
   -   5        $ btnSave��5M      #��[  5N      $4 .Resource name: MaterialDesignRaisedLightButton��5O      $��18(�5P      ���	#FF6DA7F0$��575,266,545,354q�5R      $
��Auto��5S      $
��Bold"�5T      .��6'   Lưu5V      �� 5W   
   -   5        $ 	btnCancel��5X      #��[  5Y      $4 .Resource name: MaterialDesignRaisedLightButton��5Z      $��18(�5[      ���	#FF6DA7F0$��689,266,431,354q�5]      $
��Auto��5^      $
��Bold"�5_      .��6'   Hủy5a      �� 5b   
   -	   5        $ 
btnReaload��5c      #��[  5d      $4 .Resource name: MaterialDesignRaisedLightButton��5e      $��18(�6N   ���	#FF6DA7F0$��803,266,317,354q�6s   $
��Auto��6�   $
��Bold"�6�   .��6�   Reload5g      '  !MaterialDesignThemes.Wpf.PackIcon  5h   
   �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]	   Kind$	 FileSearchOutline=�6"   $��851,325,0,0q�6;   $��28��6P   $��27��6\   �� 5i   
   -
   5        $ FindNhanVien��5j       % !MaterialDesignFloatingHintTextBox#��[ 5k      $ Tìm kiếm��5l      $��896,0,10,307q�5n      $��Bottom=�5o      $��16(�5p      ��5m      �� *	Content+5p      &   System.Windows.Controls.DataGrid  5q   
   -   5        
  Name$
 dgvChamCong��5r      $��1q�5s      $��Stretch=�5t      $��Stretch=�5u         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5v      $	��292��5w      $��
10,358,0,0q�5x      $	��Top=�5y      $
��1200��5z         
IsReadOnly . 5{      5|      5}      