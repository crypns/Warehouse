   M S B A M L   `   `   `  ����T  PСистема учета, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Warehouse	Warehouse  5   	       Warehouse.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Warehouse     5   	     � Title$  Авторизация��5   	   $	��650��6   $
��1000��6*    � 
ResizeMode$ NoResize=�67    � WindowStartupLocation$ CenterScreen=�6M    � WindowStyle$
 None=�5	   	   .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��350 �6   �� 5      $��* �6   .+�5   
   �� ���#008080 �� Padding$ 30q�6&   �� 5   
   $��Center=�6   .+�5      �� $
��Wrap=�6   $&�� Войдите в систему��6,    �� TextAlignment$ Center=�6E   $
��Bold"�5      $��42(�6*   ���#F4F2F3$	��238��6M   $	��302��6Z   $��	0 300 0 0q�6f    K� ZIndex$ 1��6y   � 5       5      �� 5      $	��89* �6'   �� 5      $	��86* �6'   .+�5      �� $��2��6   .+�5      A� $	��185��6   $	��185��6,   
 �� Top$
 -600��68   	 J� RenderTransformOrigin$	 1.97,-2.046&�6J   ���#6FC0C0
 �� Left$	
 235��6}   $��Center=�6�   $	��Top=�6�   z�5      V� 5      .~�5    "   �� �� 5!   "   �� 5"   "    �� Angle$ -0.355Q�62   T� 5#   "   5$      5&      � 5'       � Points �    -1��� ŧ��?�����#58A2A26A   � 5(      ' �    �� -1��� ���@���l�����#6FC0C0$
 40��6P   $
 -287��6a   $	 0.5,0.5&�6s   $	��391��6�   $	��225��6�   $
��Left=�6�   $	��Top=�6�   z�5)      V� 5*      .~�5+   "   �� �� 5,   "    �� AngleX$ 3.026Q�60   �� 5-   "   $ 3.605Q�62   T� 5.   "    T� X$ -10.314Q�65    T� Y$ -0.649Q�6A   5/      51      � 52      ' �    ����� ����� ���������#58A2A2$	
 141��6K   $	 -76��6]   $	 0.5,0.5&�6n   $	��310��6�   $	��118��6�   $
��Left=�6�   $	��Top=�6�   z�53      V� 54      .~�55   "   �� �� 56   "   �� 57   "   $ 39.483Q�62   T� 58   "   59      5;      � 5<      ' �    ����� ����� ��������#8DC4C4$	
 169��6K   $
 -290��6]   $	 0.5,0.5&�6o   $	��392��6�   $	��119��6�   $
��Left=�6�   $	��Top=�6�   z�5=      V� 5>      .~�5?   "   �� �� 5@   "   �� 5A   "   $ 39.483Q�62   T� 5B   "   5C      5E      � 5F      ' �    ����� ����� ��������#8DC4C4$	
 196��6K   $
 -278��6]   $	 0.5,0.5&�6o   $	��310��6�   $	��148��6�   $
��Left=�6�   $	��Top=�6�   z�5G      V� 5H      .~�5I   "   �� �� 5J   "   �� 5K   "   $ 20.714Q�62   T� 5L   "   5M      5O      A� 5P      $	��230��6   $	��237��6,   $
 -477��68   $	 1.97,-2.046&�6J   ���#043D3D$
 46��6}   $
��Left=�6�   $	��Top=�6�   z�5Q      V� 5R      .~�5S   "   �� �� 5T   "   �� 5U   "   $ -0.355Q�62   T� 5V   "   5W      5Y      A� 5Z      $	��180��6   $	��180��6,   $
 -457��68   $	 1.97,-2.046&�6J   ���#008080$
 74��6}   $
��Left=�6�   $	��Top=�6�   z�5[      V� 5\      .~�5]   "   �� �� 5^   "   �� 5_   "   $
 -0.8Q�62   T� 5`   "   5a      5c      � 5d      ' �    ����� ����� ��������#58A2A2$	
 105��6K   $
 -635��6]   $��Center=�6o   $	��Top=�6�   $	 0.5,0.5&�6�   $	��310��6�   $	��118��6�   z�5e      V� 5f      .~�5g   "   �� �� 5h   "   �� 5i   "   $ 39.483Q�62   T� 5j   "   5k      5m      � 5n      ' �    �� -1�,@�� Zb��� ���#043D3D$	
 -61��6I   $	 -45��6[   $	 0.5,0.5&�6l   $	��280��6�   $	��130��6�   $
��Left=�6�   $	��Top=�6�   z�5o      V� 5p      .~�5q   "   �� �� 5r   "   �� 5s   "   $ 39.483Q�62   T� 5t   "   5u      5w      5x      5y      5z      �� 5}   
   $��1��6   ���#f0f1f5._�5~      �� $��\Images\user.jpg��6   $	��180��6.   $	��180��6:   $��
0 -300 0 0q�6G   5      �� 5�   
   -   5        
������ $��0q�5�   *   $��20��6>   $��20��6I   $��1��6U   $��592,27,38,585q�6e   $��X��6�   $��Arial)�6�    � Cursor$
 Hand��6�   5�      �� 5�   
   -   5        
������ $��0q�5�   *   $��20��6>   $��20��6I   $��1��6U   $��548,27,82,585q�6e   $��-��6�   $��Arial)�6�   $
 Hand��6�   5�      �� 5�   
   $
��Wrap=�6   $+��%Введите свои данные:��6(   $ Center=�6D   $��SemiBold"�5�      $��30(�6.   ���#043D3D$��108,287,129,289q�6Q   $��1��6j   �� 5�   
   $��30(�6   ���#043D3D$��60,350,487,233q�63   $��1��6K   .��6[   Логин:5�      �� 5�   
   $��30(�6   ���#043D3D$��60,406,467,181q�63   $��1��6K   .��6[   Пароль:5�      �� 5�   
   -   5         ��Name$ Register��5�      $	��250��6:   $��50��6F   $��1��6R   $��340,544,60,40q�6b   ���#F4F2F3���#043D3D$��0q�6�   $
 Hand��6�   $��22(�6�   $		 0,0&�6�   $��Регистрация��6�   �� 5�   
   -   5         ��Name$ UsernameTextBox��5�      $	��400��6,   $��30��68   $��1��6D   $��168,366,82,260q�6T   $��18(�6l   6z   G� 5�   
   -   5         G�Name$ PasswordBox��5�      $	��380��6+   $��188,420,82,205q�67   $��1��6O   $��18(�6_   �� 5�   
   -   5        $ Enter��5�      $	��250��6:   $��50��6F   $��1��6R   $��52,544,348,40q�6b   ���#F4F2F3���#043D3D$��0q�6�   $
 Hand��6�   $��22(�6�   $		 0,0&�6�   $��Вход��6�   5�      5�      