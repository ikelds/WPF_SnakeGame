   M S B A M L   `   `   `  ����C  ?WpfSnake, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:WpfSnakeWpfSnake  5   	   7 clr-namespace:WpfSnake.ViewModelWpfSnake.ViewModel  5   	   /clr-namespace:WpfSnake.ModelWpfSnake.Model  5   	       WpfSnake.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:WpfSnake     5   	   /vm clr-namespace:WpfSnake.ViewModel     5   	   .modelclr-namespace:WpfSnake.Model     5   	     � Title$  Snake��5
       � SizeToContent$ WidthAndHeight=�6   $	��500��64   $	��500��6A    � WindowStartupLocation$ CenterScreen=�5   	    J� InputBindings 5      �� 5   
   
 �� Key$ Up��63    �� Command 6   �� *	UpCommand+63   �� 5   
   $
 Down��65    6   �� *DownCommand+65   �� 5   
   $
 Left��65    6   �� *LeftCommand+65   �� 5   
   $ Right��66    6   �� *RightCommand+66   �� 5   
   $ F5��65    6   �� *TestСommand+65   .��5      �� .+�5   
   �� -   5         ��Name$ br_name��5      
����+��$��5q�6;   ._�5      �� -   5         ��Name$ ic��5      ��6'   �� *
elements+ � 	Resources 5      	 �� DataType   WpfSnake.Model.Figure��%�   (��       5   #   *, +)�� 6   "	  6#   .R�5      �� .+�5      �� ��6(   �� *Color+��5    "   �� *FigureWidth+��5!   "   �� *FigureHeight+y�5"   #   �� *
Visibility+5#      5$      5(      ��5*      �� 5+      .R�5,      �� 
 ��Name$
 Canvas_GameField��6!   }�. |�. ��5-      �� *CanvasWidth+��5.      �� *CanvasHeight+50      51      ��54      �� 55       �� 
TargetType" ��6   .�56      ��  �� Property �@��Top �� Value 67   �� *Top+�� 57       �@��Left 68   �� *Left+58      5:      5<      5>      5?      