   M S B A M L   `   `   `  ����T  PСистема учета, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Warehouse	Warehouse  5   	   !    Warehouse.ProductListWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Warehouse     5   	   -   5          � Title$  Товары��5   	   $	��500��6   $	��800��6%    � WindowStartupLocation$ CenterScreen=�6H    � 
ResizeMode$ NoResize=�6n   .��5	      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ productGrid��5         AutoGenerateColumns .  $��10,89,10,50q�6D      
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$  Идентификатор��6%      
Visibility$ 	Collapsed=�6Z   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6<   �� *	ProductID+6Z     5      $ Наименование��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6T   	 6;   �� *Name+6T     5      -   5        dataGridTextColumn1��$ Количество��5   B   $
 * 6s   	 6V   �� *
Quantity+6s     5      $ Цена��6%   $
 * 6M   	 63   �� *Price+6M     5      -   5        dataGridTextColumn��$  Производитель��5   A   $
 * 6y   	 6X   �� *Manufacturer+6y     5      $ Артикул��6%   $
 * 6R   	 66   �� *	Article+6R   5      �� 5   
    �� Orientation$ 
Horizontal=�6   $
��Left=�6.   $��Bottom=�6I   $��10q�6d   .+�5      �� -   5         ��Name$ AddProductButton��5      $!��Добавить товар��6/   $	��150��6H   $��0,0,10,0q�6T   ���#F4F2F3���#043D3D$��0q�6�    � Cursor$
 Hand��6�   �� 5      -   5        $ EditProductButton��5      $-��'Редактировать данные��60   $	��150��6O   $��0,0,10,0q�6[   $��30��6�   ���#F4F2F3���#043D3D$��0q�6�   $
 Hand��6�   �� 5      -   5        $ DeleteProductButton��5      $��Удалить товар��62   $	��150��6J   $��0,0,10,0q�6V   ���#F4F2F3���#043D3D$��0q�6�   $
 Hand��6�   5      �� 5   
   -   5        $��TextBoxSearchProduct��5      $
��Left=�6.   $��
192,17,0,0q�6I    �� TextWrapping$
 Wrap=�6]   $	��Top=�6q   $	��252��6�   $��16(�6�   $ IBeam��6�   6�   �� 5   
   -	   5        $��ComboBoxChoose��5      $
��Left=�6\   $��
164,54,0,0q�6w   $	��Top=�6�   $	��301��6�   $��14(�6�   $
 Hand��6�   �� 5!   
   $;��5Введите наименование товара:��6   $
��Left=�67   $��	10,14,0,0q�6R   $	��Top=�6e   �� 5"   
   $2��,Выберите производителя:��6   $
��Left=�62   $��	10,53,0,0q�6M   $	��Top=�6`   �� 5#   
   $
��Left=�6   $��
443,17,0,0q�6,   $	��Top=�6@   $��23��6X   $��23��6c   .��5$      �� $��Images/find.png��6   $ IBeam��6-   6>   5%      �� 5'   
   -
   5        $
 Exel��5'      $��742,413,10,10q�6   $��0q�66   
������ .��5(      �� $��Images/excel.png��6   $��30��6.   $��30��69   $
 Hand��6E   5)      �� 5*   
   -   5        $ ResetSearchButton��5*      $!��Сбросить поиск��6,   $
��Left=�6E   $��25��6`   $��
470,54,0,0q�6l   $	��Top=�6�   $	��120��6�   6�   5+      5,      