   M S B A M L   `   `   `  ����C  ?KHEV2019, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:KHEV2019KHEV2019  5          KHEV2019.DoOrder   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:KHEV2019     5        N� Title$  DoOrder��5	      .��5      � 
�������.+�5   
   �� -   5         ��Name$ UserText��5      $y�Hidden=�6&   $
��Left=�6:   $��	41,10,0,0q�6U   $
��Wrap=�6h   $	��Top=�6|   �� 5   
   -   5        $��Cancel��5      $
��Left=�6"   $��715,420,0,0q�6=   $	��Top=�6R   $��75��6j   $��14(�6�   $��Microsoft Tai Le)�6�   $
��Bold"�6�   �� 5   
   -   5        $��OK��5      $
��Left=�6   $��612,420,0,0q�69   $	��Top=�6N   $��75��6f   $��14(�6�   $��Microsoft Tai Le)�6�   $
��Bold"�6�   �� 5   
   -   5         ��Name$ ComboAddress��5      $
��Left=�6)   $��
216,98,0,0q�6D   $	��Top=�6X   $	��269��6p   $��14(�6|   $��Microsoft Tai Le)�6�   $
��Bold"�6�   �� 5   
   -   5        $ 
ComboDeliv��5      $
��Left=�6'   $��216,153,0,0q�6B   $	��Top=�6W   $	��269��6o   $��24��6{   $��14(�6�   $��Microsoft Tai Le)�6�   $
��Bold"�6�   �� 5   
   -   5        $ ComboPay��5      $
��Left=�6%   $��216,205,0,0q�6@   $	��Top=�6U   $	��269��6m   $��26��6y   $��14(�6�   $��Microsoft Tai Le)�6�   $
��Bold"�6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ DeliveryDate��5      $
��Left=�6+   $��216,255,0,0q�6F   $	��Top=�6[    J� RenderTransformOrigin$ 0.5,0.5&�6s   $	��150��6�   $��29��6�   $��14(�6�   $��Microsoft Tai Le)�6�   $
��Bold"�6�   z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ 0.164Q�6&   T� 5      5      5      �� 5   
   $��Address��6   $
��Left=�6"   $��
41,100,0,0q�6=   $	��Top=�6Q   $	��150��6i   $��14(�6u   $��Microsoft Tai Le)�6�   $
��Bold"�6�   �� 5   
   $��Delivery method��6   $
��Left=�6*   $��
41,155,0,0q�6E   $	��Top=�6Y   $	��150��6q   $��28��6}   $��14(�6�   $��Microsoft Tai Le)�6�   $
��Bold"�6�   �� 5   
   -   5         ��Name$ 	cardLabel��5      $y�Hidden=�6#   $��Card Number��67   $
��Left=�6M   $��
41,305,0,0q�6h   $	��Top=�6|   $	��150��6�   $��Microsoft Tai Le)�6�   $��14(�6�   $
��Bold"�6�   �� 5   
   $��Payment method��6   $
��Left=�6)   $��
41,205,0,0q�6D   $	��Top=�6X   $	��150��6p   $��14(�6|   $��Microsoft Tai Le)�6�   $
��Bold"�6�   �� 5    
   $��Delivery Date��6   $
��Left=�6(   $��
41,258,0,0q�6C   $	��Top=�6W   $	��150��6o   $��14(�6{   $��Microsoft Tai Le)�6�   $
��Bold"�6�   �� 5!   
   $.��(Please provide the required information.��6   $
��Left=�6C   $��	41,10,0,0q�6^   $	��Top=�6q   $��83��6�   $	��701��6�   $��36(�6�   $
��Bold"�6�   $��Microsoft Tai Le)�6�   �� 5"   
   -	   5        $ TCostd��5"      $y�Hidden=�6$   $
��Left=�68   $��
687,98,0,0q�6S   $
��Wrap=�6g   $	��Top=�6{     5#   
   -
   5        $ 	DateOrder��5#   )   $y�Hidden=�6   $
��Left=�6<   $��612,177,0,0q�6W   $	��Top=�6l   �� 5$   
   -   5        $ 	Order_num��5$      $y�Hidden=�6'   $
��Left=�6;   $��612,218,0,0q�6V   $
��Wrap=�6k   $	��Top=�6�   �� 5%   
   -   5        $ 	ComboCard��5%      $y�Hidden=�6&   $
��Left=�6:   $��216,305,0,0q�6U   $	��Top=�6j   $	��269��6�   $��26��6�   $��14(�6�   $
��Bold"�6�   $��Microsoft Tai Le)�6�   �� 5&   
   -   5        $ 
StatusText��5&      $
��Left=�6(   $��693,153,0,0q�6C   $
��Wrap=�6X   $��Active��6l   $y�Hidden=�6z   $	��Top=�6�   �� 5'   
   -   5         ��Name$ Add_Card��5'      $y�Hidden=�6#   $	��Add��67   $
��Left=�6E   $��529,305,0,0q�6`   $	��Top=�6u   $��93��6�   $��26��6�   $
��Bold"�6�   $��14(�6�   $��Microsoft Tai Le)�6�   5)      5*      