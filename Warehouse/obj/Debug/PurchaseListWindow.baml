   M S B A M L   `   `   `  ����T  PСистема учета, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Warehouse	Warehouse  5   	   "    Warehouse.PurchaseListWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Warehouse     5   	   -   5          � Title$  Покупки��5   	   $	��500��6   $	��800��6&    � WindowStartupLocation$ CenterScreen=�6I    � 
ResizeMode$ NoResize=�6o   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ purchaseGrid��5
         AutoGenerateColumns .  $��10,80,10,50q�6E      
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$/ )Идентификатор покупки��6%      
Visibility$ 	Collapsed=�6c   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6D   �� *
PurchaseID+6c     5      $ Продукт��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6V   	 66   �� *ProductName+6V     5      $ Пользователь��6%   $
 * 6X   	 6;   �� *
UserName+6X     5      $ Количество��6%   $
 * 6V   	 69   �� *
Quantity+6V     5      $ 
Сумма��6%   $
 * 6O   	 64   �� *Amount+6O     5      $ Дата покупки��6%   $
 * 6\   	 6;   �� *PurchaseDate+6\   5      �� 5   
    �� Orientation$ 
Horizontal=�6   $
��Left=�6.   $��Bottom=�6I   $��10q�6d   .+�5      �� -   5         ��Name$ AddPurchaseButton��5      $%��Добавить покупку��60   $	��150��6K   $��0,0,10,0q�6W   ���#F4F2F3���#043D3D$��0q�6�    � Cursor$
 Hand��6�   �� 5      -   5        $ EditPurchaseButton��5      $/��)Редактировать покупку��61   $	��150��6Q   $��0,0,10,0q�6]   $��30��6�   ���#F4F2F3���#043D3D$��0q�6�   $
 Hand��6�   �� 5      -   5        $ DeletePurchaseButton��5      $#��Удалить покупку��63   $	��150��6M   $��0,0,10,0q�6Y   ���#F4F2F3���#043D3D$��0q�6�   $
 Hand��6�   5      �� 5   
   -   5         ��Name$ ComboBoxChoose��5      $
��Left=�6+   $��
164,11,0,0q�6F   $	��Top=�6Z   $	��301��6r   $��14(�6~   $
 Hand��6�   6�   �� 5   
   $0��*Выберите пользователя:��6   $
��Left=�61   $��	10,10,0,0q�6L   $	��Top=�6_   �� 5   
   -   5         ��Name$ TextBoxSearchProduct��5      $
��Left=�6_   $��
204,43,0,0q�6z    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��261��6�   $��16(�6�   $ IBeam��6�   �� 5   
   $?��9Введите наименование продукта:��6   $
��Left=�69   $��9,40,0,0q�6T   $	��Top=�6f   �� 5   
   $
��Left=�6   $��
442,43,0,0q�6,   $	��Top=�6@   $��23��6X   $��23��6c   .��5      �� $��/Images/find.png��6   $ IBeam��6.   5      �� 5!   
   -   5        $
 Exel��5!      $��740,416,10,10q�6   $��0q�66   
������ .��5"      �� $��Images/excel.png��6   $��30��6.   $��30��69   $
 Hand��6E   5#      �� 5$   
   -	   5        $ ResetSearchButton��5$      $!��Сбросить поиск��6,   $
��Left=�6E   $��25��6`   $��
470,11,0,0q�6l   $	��Top=�6�   $	��120��6�   6�   5%      5&      