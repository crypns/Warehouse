   M S B A M L   `   `   `  ����T  PСистема учета, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Warehouse	Warehouse  5   	       Warehouse.OrderListWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Warehouse     5   	   -   5          � Title$  Заказы��5   	   $	��500��6   $	��800��6%    � WindowStartupLocation$ CenterScreen=�6H    � 
ResizeMode$ NoResize=�6n   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ 	orderGrid��5
         AutoGenerateColumns .  $��10,104,10,50q�6B      
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$- 'Идентификатор заказа��6%      
Visibility$ 	Collapsed=�6_   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6C   �� *	OrderID+6_     5      $ Поставщик��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6Y   	 68   �� *SupplierName+6Y     5      $ Пользователь��6%   $
 * 6X   	 6;   �� *
UserName+6X     5      $ Продукт��6%   $
 * 6V   	 66   �� *ProductName+6V     5      $ 
Сумма��6%   $
 * 6O   	 64   �� *Amount+6O     5      $ Дата заказа��6%   $
 * 6X   	 6:   �� *	OrderDate+6X   5      �� 5   
    �� Orientation$ 
Horizontal=�6   $
��Left=�6.   $��Bottom=�6I   $��10q�6d   .+�5      �� -   5         ��Name$ AddOrderButton��5      $!��Добавить заказ��6-   $	��150��6F   $��0,0,10,0q�6R   ���#F4F2F3���#043D3D$��0q�6�    � Cursor$
 Hand��6�   �� 5      -   5        $ EditOrderButton��5      $+��%Редактировать заказ��6.   $	��150��6L   $��0,0,10,0q�6X   $��30��6�   ���#F4F2F3���#043D3D$��0q�6�   $
 Hand��6�   �� 5      -   5        $ DeleteOrderButton��5      $��Удалить заказ��60   $	��150��6H   $��0,0,10,0q�6T   ���#F4F2F3���#043D3D$��0q�6�   $
 Hand��6�   5      �� 5   
   -   5         ��Name$ TextBoxSearchProduct��5      $
��Left=�60   $��
204,10,0,0q�6K    �� TextWrapping$
 Wrap=�6_   $	��Top=�6s   $	��261��6�   $��16(�6�   $ IBeam��6�   6�   �� 5   
   $?��9Введите наименование продукта:��6   $
��Left=�69   $��9,7,0,0q�6T   $	��Top=�6e   �� 5   
   $
��Left=�6   $��
442,10,0,0q�6,   $	��Top=�6@   $��23��6X   $��23��6c   .��5      �� $��/Images/find.png��6   $ IBeam��6.   5      �� 5   
   -   5         ��Name$ ComboBoxChoose_User��5      $
��Left=�60   $��
163,39,0,0q�6K   $	��Top=�6_   $	��301��6w   $��14(�6�   $
 Hand��6�   6�   �� 5    
   $0��*Выберите пользователя:��6   $
��Left=�61   $��9,38,0,0q�6L   $	��Top=�6^   �� 5!   
   -   5        $ ComboBoxChoose_Supplier��5!      $
��Left=�64   $��
164,70,0,0q�6O   $	��Top=�6c   $	��300��6{   $��14(�6�   $
 Hand��6�   6�   �� 5"   
   $,��&Выберите поставщика:��6   $
��Left=�6/   $��	10,69,0,0q�6J   $	��Top=�6]   �� 5$   
   -	   5        $
 Exel��5$      $��735,415,10,10q�6   $��0q�66   
������ .��5%      �� $��Images/excel.png��6   $��30��6.   $��30��69   $
 Hand��6E   5&      �� 5'   
   -
   5        $ ResetSearchButton��5'      $!��Сбросить поиск��6,   $
��Left=�6E   $��25��6`   $��
469,70,0,0q�6l   $	��Top=�6�   $	��120��6�   6�   5(      5)      