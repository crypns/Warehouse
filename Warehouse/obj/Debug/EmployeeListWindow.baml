   M S B A M L   `   `   `  ����T  PСистема учета, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Warehouse	Warehouse  5   	   "    Warehouse.EmployeeListWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Warehouse     5   	   -   5          � Title$  Сотрудники��5   	   $	��500��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�6L    � 
ResizeMode$ NoResize=�6q   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ userGrid��5
         AutoGenerateColumns .  $��10,78,10,50q�6A      
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$  Идентификатор��6%      
Visibility$ 	Collapsed=�6W   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6<   �� *UserID+6W     5      $ 
Логин��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6N   	 64   �� *Login+6N     5      $ Пароль��6%   $ 	Collapsed=�6R   	 65   �� *
Password+6R     5      $ Имя��6%   $
 * 6K   	 62   �� *Name+6K     5      $ Телефон��6%   $
 * 6P   	 66   �� *Phone+6P     5      $# Уровень доступа��6%   $
 * 6^   	 6>   �� *AccessLevel+6^   5      �� 5   
    �� Orientation$ 
Horizontal=�6   $
��Left=�6.   $��Bottom=�6I   $��10q�6d   .+�5      �� -   5         ��Name$ AddUserButton��5      $+��%Добавить сотрудника��6,   $	��150��6J   $��0,0,10,0q�6V   ���#F4F2F3���#043D3D$��0q�6�    � Cursor$
 Hand��6�   �� 5      -   5        $ EditUserButton��5      $-��'Редактировать данные��6-   $	��150��6L   $��0,0,10,0q�6X   $��30��6�   ���#F4F2F3���#043D3D$��0q�6�   $
 Hand��6�   �� 5      -   5        $ DeleteUserButton��5      $)��#Удалить сотрудника��6/   $	��150��6L   $��0,0,10,0q�6X   ���#F4F2F3���#043D3D$��0q�6�   $
 Hand��6�   �� 5      -   5        $
 Exel��5      $��30��6#   $��30��6.   $��	250,0,0,0q�6:   $��0q�6M   
������ .��5      �� $��Images/excel.png��6   $��30��62   $��30��6=   $
 Hand��6I   5      5      �� 5    
   $1��+Введите имя сотрудника:��6   $
��Left=�62   $��10,7,0,0q�6M   $	��Top=�6_   �� 5!   
   -   5        $��UserSearchComboBox��5!      $
��Left=�6-   $��
170,41,0,0q�6H   $	��Top=�6\   $	��261��6t   $��14(�6�   $
 Hand��6�   5"      �� 5#   
   $5��/Выберите уровень доступа:��6   $
��Left=�64   $��	10,40,0,0q�6O   $	��Top=�6b   �� 5$   
   -   5         ��Name$ TextBoxSearchUser��5$      $
��Left=�6-   $��
170,13,0,0q�6H    �� TextWrapping$
 Wrap=�6\   $	��Top=�6p   $	��261��6�   $��16(�6�   $ IBeam��6�   6�   �� 5%   
   $
��Left=�6   $��
408,13,0,0q�6,   $	��Top=�6@   $��23��6X   $��23��6c   .��5&      �� $��/Images/find.png��6   $ IBeam��6.   5'      �� 5(   
   -	   5        $ ResetSearchButton��5(      $!��Сбросить поиск��6,   $
��Left=�6E   $��25��6`   $��
436,41,0,0q�6l   $	��Top=�6�   $	��120��6�   6�   5)      5*      