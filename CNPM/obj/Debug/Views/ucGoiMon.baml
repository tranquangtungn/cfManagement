   M S B A M L   `   `   `  ����?  ;CNPM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CNPMCNPM  5          CNPM.ucGoiMon   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v19.1, Version=19.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      localclr-namespace:CNPM  5      c _DevExpress.Xpf.Charts.v19.1, Version=19.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aTCustomGhttp://schemas.devexpress.com/winfx/2008/xaml/charts/rangecontrolclient  5	      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  6e   -   5          >�Name$  	ucGoiMon1��5	   �   .��5      � 
�������.+�5   
   �� $��Gọi món
��6   $��16(�6.   $
��Left=�6<   $��	10,10,0,0q�6W   $	��Top=�6j   $	��206��6�   $	��600��6�   ��5      �� 5      .��5      � $��	0,-18,0,0q�6   .+�5      �� -   5         ��Name$ 	tbxTenMon��5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 	Tên món��5       %  !MaterialDesignFloatingHintTextBox#��[  5      $��258,-11,26,120q�6P   �� 5      $��Số lượng:��6   $
��Left=�6,   $��
258,0,0,71q�6G   $��Bottom=�6[   �� 5      -   5         ��Name$ btnThem��5      ���	#FF6DA7F0 # MaterialDesignRaisedLightButton#��[ 5       � ToolTip$4 .Resource name: MaterialDesignRaisedLightButton��5      $��356,113,26,10q�6N   $��_Thêm��6e   $
��Auto��6u   6�   �� 5      -   5         ��Name$ imgMonAn��5      $
��Left=�6*   $��	68,10,0,0q�6F   $	��Top=�6Y   $	��145��6q   $	��146��6~   �� 5      -   5        $ btnTru��5      ���	#FF6DA7F0#��[ 5      $4 .Resource name: MaterialDesignRaisedLightButton��5      $��356,59,159,69q�6N   $��-��6e   $
��Auto��6q   6   �� 5       -   5        $ btnCong��5       ���	#FF6DA7F0#��[ 5"      $4 .Resource name: MaterialDesignRaisedLightButton��5#      $��496,59,26,69q�6N   $��+��6d   $
��Auto��6p   6~   �� 5$      -   5        $ 
tbxSoLuong��5$      $
��Left=�6.   $��55��6I   $��
437,48,0,0q�6U    �� TextWrapping$
 Wrap=�6i   $��1��6}   $	��Top=�6�   $��54��6�   5%      5'      �� 5(   
   $��
Gọi món��6   $��16(�6$   $
��Left=�62   $��
10,221,0,0q�6M   $	��Top=�6a   $	��429��6y   $	��600��6�   ��5)      �� 5*      .��5,      � .+�5-      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ Search=�6*    J� RenderTransformOrigin$ 1.75,1.5&�68   $��30��6Y   $��30��6e   $��	94,20,0,0q�6p   �� 5.      -   5        $ 
txtTimKiem��5/      $ Tìm kiếm��50      #��[  51      $��142,0,138,338q�6P   $ 0.5,0.5&�6g   &   System.Windows.Controls.DataGrid  52      -	   5        	  Name$	 
dgvDSMonAn��53      
   CanUserAddRows
 .     CanUserDeleteRows .     CanUserResizeRows .     
IsReadOnly . $��Stretch=�56      $��Stretch=�57         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 58      $	��314��59      $��	10,64,0,0q�6"   $	��Top=�5:      $	��556��5;      5<      5=      5>      �� 5?   
   -
   5         ��Name$ grBxThanhToan��5?      $��Hóa đơn thanh toán��6*   $��16(�6F   $
��Left=�6T   $��
620,10,0,0q�6o   $	��Top=�6�   $	��640��6�   $	��590��6�   ��5@      �� 5A      .��5C      �  � ColumnDefinitions 5D      �� 5E      �� 5F      $	��26* �6'   .+�5H      �� -   5        $ btnXoaMonAn��5H      ���	#FF6DA7F0#��[ 5J      $4 .Resource name: MaterialDesignRaisedLightButton��5K      $��73,10,296,543q�6N   $��Xóa món ăn��6e   $
��Auto��6z   $��1��6�   6�   �� 5L      -   5        $ btnXoaHoaDon��5L      ���	#FF6DA7F0#��[ 5N      $4 .Resource name: MaterialDesignRaisedLightButton��5O      $��328,10,42,543q�6N   $��Xóa hóa đơn��6e   $
��Auto��6{   $��1��6�   6�     5P      -   5        $	 	dgvHoaDon��5Q      
 .   .   .   . $��Stretch=�5T      $��Stretch=�5U      $ * 5V      $	��401��5W      $��	10,74,0,0q�5X      $	��Top=�5Y      $	��546��5Z      $��2��5[         Columns 5\      0  *System.Windows.Controls.DataGridTextColumn  5]      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Tên món��6-      
IsReadOnly .    Width$	 300 6q   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   ��  �� Path$ tenMon�6     5^      $ Số lượng��6-    . $	 100 6t    6?   �� $ soLuong�6�     5_      $ Thành tiền��6-    . $	 150 6x    6A   �� $ 	thanhTien�6�   5a      �� 5b      -   5        $ 
txtGiamGia��5b      $ Giảm giá��5c      #��[  5d      $��143,462,266,75q�6P   $��1��6h   $��0��6x   �� 5e      -   5        $ txtThanhToan��5e      $ Tổng cộng��5f      #��[  5g      $��327,462,42,75q�6P   $��1��6g   $ 0.588,0.056&�6w   �� 5h      -   5        $ btnThanhToan��5h      ���	#FF6DA7F0#��[ 5j      $4 .Resource name: MaterialDesignRaisedLightButton��5k      $��327,543,42,10q�6N   $��Thanh toán��6e   $
��Auto��6z   $��1��6�   6�   5l      5m      5r      5s      