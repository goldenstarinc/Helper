   M S B A M L   `   `   `  ����N  JGoodDeeds_Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:GoodDeeds_InterfaceGoodDeeds_Interface  5      #    GoodDeeds_Interface.PostsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:GoodDeeds_Interface     5        N� Title$  	PostsPage��5	      .��5      � $��1��6   $��
8,10,10,10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� -   5        $��PostsListView��5      $��1��5      $��10q�5      $
��Auto=�5      ��5      �� 5      .R�5      A�  A� Columns$ 1��6"   5      ��5      �� 5      .R�5      �� ���#D3D3D3$��1q�63   $��5q�6G    �� CornerRadius$ 5��6R    �� Padding$ 10q�6c   ._�5       �� .+�5!      �� $��	Тема:��6(   $
��Bold"�65   $��14(�6G   ���#2F4F4F6U   �� 5"      $��16(�6?   $
��Bold"�6M   ���#2F4F4F$��0,0,0,5q�6t   ��6(   �� *Title+6t   �� 5$      $"��Основная часть:��6(   $
��Bold"�6?   $��14(�6Q   ���#2F4F4F6_   �� 5%      $
��Wrap=�6A   $��0,0,0,5q�6U   $��14(�6f   ��6(   �� *	Content+6f   �� 5'      $#��Местоположение:��6(   $
��Bold"�6?   $��14(�6Q   ���#2F4F4F6_   �� 5(      $��Italic$�6B   ���#708090$��0,0,0,5q�6j   ��6(   �� *
Location+6j   �� 5*      $$��Дата публикации:��6(   $
��Bold"�6@   $��14(�6R   ���#2F4F4F6`   �� 5+      ���#808080$��12(�6Y   $��0,0,0,10q�6g   ��6(   �� *
DatePosted+6g   �� 5-      -   5        $��Откликнуться��5-   %   ���#4CAF50
�������$��10q�50   %   
������  � Cursor$
 Hand��52   %   53   %   54      55      56      59      5:      5;      