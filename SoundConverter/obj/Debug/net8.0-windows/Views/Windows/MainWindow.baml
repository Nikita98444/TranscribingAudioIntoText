   M S B A M L   `   `   `  ����I  ESoundConverter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:SoundConverterSoundConverter  5   	   E'clr-namespace:SoundConverter.ViewModelsSoundConverter.ViewModels  5   	   E'clr-namespace:SoundConverter.ConvertersSoundConverter.Converters  5
   	       SoundConverter.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:SoundConverter     5   	   6vm'clr-namespace:SoundConverter.ViewModels     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5   	   8
converters'clr-namespace:SoundConverter.Converters  5
   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � DataContext 5      3   -SoundConverter.ViewModels.MainWindowViewModel  5   
   '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      GlassFrameThickness$ 0 32 0 0q�6      NonClientFrameEdges$ Left,Bottom,Right=�66      UseAeroCaptionButtons$
 True��6^   ��5      �� 5   
    �� 
TargetType" �6   .R�5      ��  �� Padding$ 0 26 0 0q�6   ._�5      �  �Name$ RootGrid��6   .+�5      �� ���#1F2933$	��Top=�5      $��26��6-   $��0,-26,144,0q�69   ._�5      �� 	 �� Orientation$	 
Horizontal=�6%   .+�5      �� $��Convert��6(   $��20(�67   ���#1F2933���#E0E0E0$
��Bold"�5   =   $��Center=�6O   $��30��6j   
 �� TextAlignment$
 Center=�6v   $	��156��5   (   $��0,0,0,0q�64    J� Effect 5    "   3  -System.Windows.Media.Effects.DropShadowEffect  5!   &      Color$ #E0E0E0��67      	Direction$ 0Q�6G      ShadowDepth$ 0Q�6U      
BlurRadius$ 14Q�6e   5#      5'      5(      �� 5*      5+      5,       �� Triggers 5-      P� 5.       P� Property �@�WindowState P� Value$ 	Maximized=�61   . �5/      ��  �� 
TargetName$ RootGrid��6    �� Property � �� �� Value$ 6q�6E   50      52       � 	Resources 55      8   2SoundConverter.Converters.MultiBoolToBoolConverter   MultiBoolToBoolConverter   BooleanToVisibilityConverterD   >SoundConverter.Converters.InvertedBooleanToVisibilityConverter ( $InvertedBooleanToVisibilityConverter��%W   &	        &	      &	 &       56   
   6.   �� 57   
   6'     58   
   6:   59      .��5;      � ���#303C48 � RowDefinitions 5<   
   �� 5=      $
��373* �6   �� 5>      $��51 �6   .+�5A   
   d� -   5         d�Name$ ModelToggleButton��5A      $��Модель PND v1.0��5B      $	��150��5C      $��30��5D      $	��Top=�5E      $
��Left=�5F      $��10,5,0,0q�5G       d� 	IsChecked 5H      �� *IsModelSelected+ �� Mode$ TwoWay=�{�5I      �� *IsModelEnable+��5J      �� 5K       �� 
TargetType" d�6   .�5L      ��  � �� 5M      �� 5N      " d�6.   .R�5O   "   ��  ��Name$ Border��6)   #��z��5P   !   #��z��5Q   !   '  !System.Windows.VisualStateManager   VisualStateGroups 5R   &   %  System.Windows.VisualStateGroup  5S   *   MouseStates��   States. 5T   .      System.Windows.VisualState  	MouseOver��   
Storyboard. 5U   2   �� .��5V   6   ��   �� 
TargetName$  Border��6E   ! �� TargetProperty$1! +(Border.Background).(SolidColorBrush.Color)�5W   =   	" �� To" 	LightBlue5X   =   # g� Duration$# 0:0:0.3D�5Y   =   5Z   3   5[   /     5\   .   
Normal��. 5]   2   �� .��5^   6   �� $  Border��6E   $1! +(Border.Background).(SolidColorBrush.Color)�5_   =   $# 0:0:0.3D�5a   =   " 5`   =   �� $ �� RelativeSource$ �� *TemplatedParent+% �� Path$(% "Background.(SolidColorBrush.Color)�5a   =   5b   3   5c   /   5d   +   ._�5f   &   �� $��Center=�67   $��Center=�5g   /   5h   #   5i      5k      & �� Triggers& 5l      P� 5m       �@d�	IsChecked$
 TrueY�67   . �5n      ��  � ��
 ����6;   5o      5q      5s      d� 5u   
   -   5        $ ModelToggleButton2��5u      ${�False��63   
�������$��Модель PND v2.0��5v      $	��150��5w      $��30��5x      $	��Top=�5y      $
��Left=�5z      $��	170,5,0,0q�5{       5|      �� *IsModelSelected2+$ TwoWay=���5}      �� 5~      " d�6   .�5      ��  � �� 5�      �� 5�      " d�6.   .R�5�   "   �� $ Border��6)   #��z��5�      #��z��5�       5�   &     5�   *   MouseStates��. 5�   .     	MouseOver��. 5�   2   �� .��5�   6   �� $  Border��6E   $1! +(Border.Background).(SolidColorBrush.Color)�5�   6   " 	LightBlue5�   6   $# 0:0:0.3D�5�   6   5�   3   5�   /     5�   .   
Normal��. 5�   2   �� .��5�   6   �� $  Border��6E   $1! +(Border.Background).(SolidColorBrush.Color)�5�   6   $# 0:0:0.3D�5�   6   " 5�   6   �� $ �� *TemplatedParent+$(% "Background.(SolidColorBrush.Color)�5�   6   5�   3   5�   /   5�   +   ._�5�   &   �� $��Center=�67   $��Center=�5�   (   5�   #   5�      5�      & 5�      P� 5�       �@d�	IsChecked$
 TrueY�67   . �5�      ��  � ��
 ����6;   5�      5�      5�      �� 5�   
   $��Right=�6   $	��Top=�61   $	��300��6I   $��0,5,10,0q�6U   $��30��6g   .+�5�      �� $F��@Открыть папку с текстовыми файлами��5�      $��30��6>   ���#8FBC8F���#2F4F4F��6t   �� *OpenOutFilePatchCommand+6�   5�      �� 5�   
   -   5        $ DropZone��5�      $ 40q�6#   ' �� CornerRadius$' 20��60   $��3q�5�   2   $��145,63,145,25q�5�   	   ( J� RenderTransformOrigin$( 0.5,0.5&�6D   ��5�   	   �� *BorderBrushColor+) J� 	AllowDrop) 5�       �� *IsDropAllowed+z�5�      �� 5�      * �� ScaleX$* 1Q�6!   + �� ScaleY$+ 1Q�6,   ._�5�      �� $��Center=�6   $��Center=�66   $	 Vertical=�6Q   .+�5�      �� $	��117��6   $	��150��6%   .+�5�      �� $	��120��6    $	��150��6,   ���#D3D3D3���#A9A9A9$��3��6Y   , �� RadiusX$, 10��6m   - �� RadiusY$- 10��6z   � 5�      . � Points#. �   �J] '����J]������#D3D3D3���#A9A9A9$��3��6Z   $
��Left=�6n   $��Center=�6�   $	��117��6�   �� 5�      	/ �� X1$/ 90��6   	0 �� Y1$0 0��6#   	1 �� X2$1 90��6*   	2 �� Y2$2 30��62   ���#A9A9A9$��3��6K   �� 5�      -   5        3 ��Name$3 	BlockIcon��5�      $��60��5�      $��60��6$   $
��Left=�5�      $	��Top=�5�      4 �� Left$4 28��5�      
5 �� Top$5 45��5�      y�5�      �� *IsDropAllowed+6 �� 	Converter#6 [ .+�5�      A� $��60��6"   $��60��6-   
���   �$��3��6H   �� 5�      $/ 15��6   $0 45��6'   $1 45��6/   $2 15��67   
���   �$��3��6N   5�      5�      �� 5�      $��20(�6=   ���#C0C0C0$��
20,20,20,0q�6`   $��Center=�6t   ��6   �� *DroppedFileName+6t   5�      *	  $Microsoft.Xaml.Behaviors.Interaction7 	  Triggers7 5�      +
  %Microsoft.Xaml.Behaviors.EventTrigger
  5�      8 
  	EventName$
8 Drop��6!   *  $Microsoft.Xaml.Behaviors.TriggerBase9   Actions.9 5�      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  :   Command: 6,   �� *HandleFileDropCommand+5�      ; � Triggers; 5�      :� 5�      < :� RoutedEvent$< UIElement.DragEnter��6   .[�5�      �� .��5�      �� .��5�      �� $2! ,(Border.BorderBrush).(SolidColorBrush.Color)�6-   " #FFD7006v   $# 0:0:0.3D�6�   W� 5�      $9! 3(UIElement.RenderTransform).(ScaleTransform.ScaleX)�6.   	= W� To= 1.056~   $# 0:0:0.3D�6�   W� 5�      $9! 3(UIElement.RenderTransform).(ScaleTransform.ScaleY)�6.   = 1.056~   $# 0:0:0.3D�6�   5�      5�      5�      :� 5�      $< UIElement.DragLeave��6   .[�5�      �� .��5�      �� .��5�      �� $2! ,(Border.BorderBrush).(SolidColorBrush.Color)�6-   " #C0C0C06v   $# 0:0:0.3D�6�   W� 5�      $9! 3(UIElement.RenderTransform).(ScaleTransform.ScaleX)�6.   = 16~   $# 0:0:0.3D�6�   W� 5�      $9! 3(UIElement.RenderTransform).(ScaleTransform.ScaleY)�6.   = 16~   $# 0:0:0.3D�6�   5�      5�      5�      :� 5�      $< UIElement.Drop��6   .[�5�      �� > �� BeginStoryboardName$> DropAnimation��6'   �� 5�      -   5        ? ��Name$? DropAnimation��5�   &   .��5�      �� .��5�      �� $2! ,(Border.BorderBrush).(SolidColorBrush.Color)�6-   " #32CD326v   $# 0:0:0.3D�6�   W� 5�      $9! 3(UIElement.RenderTransform).(ScaleTransform.ScaleX)�6.   = 16~   $# 0:0:0.3D�6�   W� 5�      $9! 3(UIElement.RenderTransform).(ScaleTransform.ScaleY)�6.   = 16~   $# 0:0:0.3D�6�   5�      5�      5�      5�      �� 5�   
   ���#1F2933$��50��6&   $��1��63   ._�5�      �� $	 
Horizontal=�6   $��Center=�62   $��Right=�6M   .+�5     �� $!��Получить текст��6   $��0,0,10,5q�62   ���#E0E0E0���	#FF455A64
���������6�   �� *HandleGetTextCommand+{�5     `� 5     @ `� 	Converter#@ [  6'   .1�5     �� $% IsModelSelected�6&   �� 5     $% IsAudioFile�6&   �� 5     $% IsButtonEnabled�6&   5     5	     �� 5     $��Отмена��6   $��0,0,10,5q�6+   ���#E0E0E0���	#FF455A64
�������${�False��5     5     5     �� 5  
   $��1��6   $��50��6   $��50��6*   $��10,0,0,0q�66   $
��Left=�6H   $��Center=�6c   .+�5     � y�6   �� *LoadVisible+.+�5     A� ���#E0E0E0$��4��6+   $��40��6?   $��40��6J   F� 5     ���	#FF536E7B$��4��6*   A �� StrokeDashArray$
A 5,10R�6>   $��40��6U   $��40��6`   %����     -1� Zb -1� -1 -1	$( 0.5,0.5&�5  %   z�5     �� 5     -   5        rotateTransform��B �� Angle$B 0Q�5  C   ; 5     :� 5     $< Loaded��6'   .[�5     �� .��5  "   �� C g� RepeatBehavior$C Forever��6-   .��5   &   W� $  rotateTransform��66   $! Angle�5!      D W� FromD 05"      = 3606)   $# 0:0:1D�62   5#  #   5$     5%     5'     5(     � 5+     y�6   �� *CheckMarkVisible+.+�5-     F� -   5        E F�Name$E SuccessCheckmark��5-     "���Ѐ�� @x}� -1������ 	
������$��5��5.     �� 51     -   5        F ��Name$F SuccessText��51     $��
Успех��61   
������$��18(�6V   $��Center=�52  	   $
��Left=�6$   $��50,0,0,0q�6?   53     � 55     y�6   �� *CrossMarkVisible+.+�57     F� -	   5        $E 
ErrorCross��57     +���Ѐ�� ��� �������� р�� �	
���  ��$��5��58     �� 5;     -
   5        $F 	ErrorText��5;     $��Ошибка��6/   
���  ��$��18(�6N   $��Center=�5<     $
��Left=�6+   $��50,0,0,0q�6F   5=     5@     5C     5D     