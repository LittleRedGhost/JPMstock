   M S B A M L   `   `   `  ����  wpfStock    wpfStock.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  JPMorgan Stocks��5   	   $	��320��6!   $	��640��6.   $��14(�6:    � 
ResizeMode$ NoResize=�6H   .��5      � .+�5   
   �� $��Center=�6    �� Orientation$ Vertical=�62   .+�5      �� $��Center=�6   $ 
Horizontal=�66   .+�5      �� -   5         ��Name$ listStockType��5      $	��120��61   $	��100��6>   $��10q�6J   5	      �� 5
      $��Center=�6   $��Center=�6:   .+�5      �� -   5         ��Name$ 
labelPrice��5      $��
Price (£)��60   $��0q�6D   �� 5      -   5         ��Name$ 	textPrice��5       �� TextWrapping$
 Wrap=�61   $
��0.00��6E   $��0q�6Q   $��5q�6\   5      �� 5      $��Center=�6   $��Center=�6:   .+�5      �� -   5         ��Name$ buttonDividendYield��5      $��Dividend Yield��6:   $��Center=�6S   $��Center=�6p   $��5q�6�   $��5q�6�   6�   �� 5      -   5        $ labelDividendYield��5      $��£-.--��68   $��20q�6H   $��20(�6T   $
��Bold"�6b   5      �� 5      $��Center=�6   $��Center=�6:   .+�5      �� -   5        $ buttonPERatio��5      $��	P/E Ratio��64   $��5q�6H   $��5q�6S   6_   �� 5      -   5        $ labelPERatio��5      $��£-.--��62   $��20q�6B   $��20(�6N   $
��Bold"�6\   5      5      �� 5      $��Center=�6   $ 
Horizontal=�66   .+�5      �� -   5        $ 	buttonBuy��5      $	��Buy��6,   $��5q�6:   $��5q�6E   $��Center=�6i   $��Center=�6�   �� 5      -	   5        $ 
listTrades��5      $	��440��6.   $	��100��6:    �� SelectionMode$ Multiple=�6G   $��Courier New)�6`   $��11(�6y   $
��Bold"�6�   �� 5      $��Center=�6   $��Center=�6:   .+�5      �� -
   5        $ 	labelVWSP��5      $
��VWSP��6/   $��Center=�6>   �� 5      -   5        $ labelVWSPresult��5      $��£-.--��65   $��20(�6E   $
��Bold"�6S   $
��20,5q�6e   $��Center=�6s   �� 5      -   5        $ 
labelCount��5      $	��...��60   $��Center=�6>   5      5       �� 5"      -   5        $ labelGeoMean��5"      $��Geometric Mean��6*   $��16(�6C   $
��Bold"�6Q   $��Center=�6c   $��Center=�6�   $��0,5,0,0q�6�   5#      5$      5%      