   M S B A M L   `   `   `  ���� I  ERemotionServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:RemotionServerRemotionServer  E'clr-namespace:RemotionServer.PropertiesRemotionServer.Properties      RemotionServer.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:RemotionServer     <cultural'clr-namespace:RemotionServer.Properties       � WindowStyle$
  None=� � 
ResizeMode$ NoResize=� � AllowsTransparency .   � Title$ RemotionServer��$��278.992��$��529.758�� � 	Resources    NewRetroWaveColor  UserSettingsCollection S� 
ObjectType!   RemotionServer.SettingsData S� 
MethodName  buttonStyle �� 
TargetType �� Property	 �� Value
 �� 
TargetType �� CornerRadius �� Triggers :� RoutedEvent g� Duration �� TargetProperty	 W� To P� Property P� Value  helpButtonStyle  CloseConnectionButtonStyle��%  &	        &	      &	 4     &	 �    &	 1    ��#1C9100��S� "  $ get_name_flag_dict���� " ��.���  � ��	 �� "
 ��.R��� #��z��#���   $��1q�$ 0��._��� $��Center=�$��Center=���  � ��	 �	#FF1B1B1B��  � ��#	 �   ��  �@�Cursor$
	 Hand����  � ��$
	 14px(� :� $ 
MouseEnter��[��� .���� .��W� $ 0:0:0.1D�$ FontSize�$ 15X�:� $ 
MouseLeave��[��� .���� .��W� $ 0:0:0.1D�$ FontSize�$ 14X�P�  �@J�IsMouseOver . . ���  � ��	 �	#FF232524�� " ��.���  � ��	 �� "
 ��.R��� #��z�����	#FFA200FF$��3q�$ 20��._��� $��Center=�$��Center=���  � ��$	 16(���  � ��	 �	#FF1B1B1B��  � ��	 �	#FFA200FF��  �@�Cursor$
	 Hand����  � ��$	 ?����  � ��$	 Heavy"���  � ��$	 33����  � ��$	 33�� :� $ 
MouseEnter��[��� .���� .��W� $ 0:0:0.1D�$ FontSize�$ 15X�:� $ 
MouseLeave��[��� .���� .��W� $ 0:0:0.1D�$ FontSize�$ 14X�P�  �@J�IsMouseOver . . ���  � ��	 �	#FF232524�� " ��.���  � ��	 �� "
 ��.R��� #��z��
���  ��$��1q�._��� $��Center=�$��Center=���  � ��
	 �  ����  � ��	 �	#FF1B1B1B��  �@�Cursor$
	 Hand�� :� $ 
MouseEnter��[��� .���� .��W� $ 0:0:0.1D�$ FontSize�$ 13X�:� $ 
MouseLeave��[��� .���� .��W� $ 0:0:0.1D�$ FontSize�$ 12X�P�  �@J�IsMouseOver . . ���  � ��	 �	#FF232524.���� $��1q�
���   �._�� -   ���	#FF1B1B1B$��0,0,0,-1q�.+��� -    ��Name$ background_jpg��$��Resources/background.png��$
��Fill=��� -    ��Name$ RemotionServerLabel��$��Remotion server��$
��Left=�$	��Top=�$��16(�$
��Bold"�$��10,0,0,0q��� -    ��Name$ MinimizeButton��#��[ $	��___��$
��Left=�$��	194,4,0,0q�$	��Top=�$��40��$��21���� -   $ CloseApplicationButton��#��[ $��X��$
��Left=�$��	239,4,0,0q�$	��Top=�$��40��$��21���� -    ��Name$# AvailableIpAddressesTextBlock��#��[  $
��Left=�$��	10,44,0,0q�$
��Wrap=�)   #RemotionServer.Properties.Resources   AvailableIpLabel#��Z $	��Top=�$��14(��� -    ��Name$ 	IpListBox��$
��Left=�$	��116��$��	10,68,0,0q�$	��Top=�$	��240����Z� ���� .R���  �� Orientation$ 
Horizontal=�.+��� ���� d� -    d�Name$% OpenCloseConnectionToggleButton��   OpenSocketButton#��Z $��
10,189,0,0q�$��40��$	��Top=�$
��Left=�$	��240���� -	   $ UpdateButton��#��[    UpdateButton#��Z $
��Left=�$��
165,41,0,0q�$	��Top=�$��85��$��22��d� -
   $ ExpanderButton��   SettingsTabHeader#��Z $
��Left=�$��188,134,-59,0q�$	��Top=�$	��160��$��28�� J� RenderTransformOrigin$ 0.5,0.5&�z�V� .~��� �� ��  �� Angle$	 270Q�T� �� -   $ Shortcut1TextBlock��$
��Left=�#��[  $��	304,7,0,0q�$
��Wrap=�    FastButton1GroupBox#��Z  $	��Top=�$��14(�$	��213��$��24���� -   ! ��Name$! 	comboBox1��" �� SelectedValuePath$" Value��# �� DisplayMemberPath$	# Key��$ �� 
IsEditable$ .  $
��Left=�$��
304,33,0,0q�$	��Top=�$	��213������ % �� Source#% [ �� -   $ Shortcut2TextBlock��$
��Left=�#��[  $��
304,68,0,0q�$
��Wrap=�&   FastButton2GroupBox#��Z& $	��Top=�$��14(�$	��213��$��24���� -   $! 	comboBox2��$" Value��$	# Key��$ .  $
��Left=�$��
304,92,0,0q�$	��Top=�$	��213������ #% [ �� -   $ ScrollSpeedLabel��$��14(�#��[  '   ScrollSpeedLabel#��Z' $
��Left=�$��304,127,0,0q�$
��Wrap=�$	��Top=�$	��203���� -   ( ��Name$( ScrollSensSlider��) �� IsMoveToPointEnabled) . $
��Left=�$��304,151,0,0q�$	��Top=�$	��164��* � Minimum$* 15Q�+ � Maximum$	+ 120Q��� -   $ TouchpadSensLabel��$��14(�$y�	Collapsed=�#��[  $
��Left=�$��304,172,0,0q�$
��Wrap=�,   TouchpadSpeedLabel#��Z, $	��Top=�$	��203���� -   $( TouchpadSensSlider��$y�	Collapsed=�$
��Left=�$��304,196,0,0q�$	��Top=�$	��164��$* 1Q��� -   $ ScrollingSensValueLabel��$
��Left=�$��473,146,0,0q�$	��Top=�$��34��$��14(��� -   $ TouchpadSensValueLabel��$y�	Collapsed=�$
��Left=�$��473,189,0,0q�$	��Top=�$��14(��� -   $ DebugMessageTextBlock��#��[  $
��Wrap=�$
��Left=�$��	5,234,0,0q�$	��Top=�$��36��$	��274������ " �� :� $ 	TextInput��.[��� .���� $ Opacity�$ 00:00:01D�.��W� - W� From$- 0X�$ 1X�$ 00:00:01D�