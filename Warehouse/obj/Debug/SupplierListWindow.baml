   M S B A M L   `   `   `  ����T  PСистема учета, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Warehouse	Warehouse  5   	   "    Warehouse.SupplierListWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Warehouse     5   	   -   5          � Title$  Поставщики��5   	   $	��500��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�6L    � 
ResizeMode$ NoResize=�6r   .��5
      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ supplierGrid��5         AutoGenerateColumns .  $��10,55,10,50q�6E      
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$  Идентификатор��6%      
Visibility$ 	Collapsed=�6[   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6<   �� *
SupplierID+6[     5      $ Имя��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6K   	 62   �� *Name+6K     5      $ Телефон��6%   $
 * 6P   	 66   �� *Phone+6P   5      �� 5   
    �� Orientation$ 
Horizontal=�6   $
��Left=�6.   $��Bottom=�6I   $��10q�6d   .+�5      �� -   5         ��Name$ AddSupplierButton��5      $+��%Добавить поставщика��60   $	��150��6N   $��0,0,10,0q�6Z   ���#F4F2F3���#043D3D$��0q�6�    � Cursor$
 Hand��6�   �� 5      -   5        $ EditSupplierButton��5      $-��'Редактировать данные��61   $	��150��6P   $��0,0,10,0q�6\   $��30��6�   ���#F4F2F3���#043D3D$��0q�6�   $
 Hand��6�   �� 5      -   5        $ DeleteSupplierButton��5      $)��#Удалить поставщика��63   $	��150��6P   $��0,0,10,0q�6\   ���#F4F2F3���#043D3D$��0q�6�   $
 Hand��6�   5      �� 5   
   -   5         ��Name$ TextBoxSearchSupplier��5      $
��Left=�6a   $��
221,13,0,0q�6|    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��252��6�   $��16(�6�   $ IBeam��6�   �� 5   
   $C��=Введите наименование поставщика:��6   $
��Left=�6;   $��	10,10,0,0q�6V   $	��Top=�6i   �� 5   
   $
��Left=�6   $��
473,13,0,0q�6,   $	��Top=�6@   $��23��6X   $��23��6c   $
 Hand��6o   .��5      �� $��/Images/find.png��6   $ IBeam��6.   5      �� 5   
   -   5        $
 Exel��5      $��738,414,10,10q�6   $��0q�66   
������ .��5      �� $��Images/excel.png��6   $��30��6.   $��30��69   $
 Hand��6E   5      5       5!      