   M S B A M L   `   `   `  ????C  ?ScadaGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:ScadaGUIScadaGUI  5   	   -    'ScadaGUI.ChangeInputDigitalOutputWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:ScadaGUI     5   	     ? Title$$  ChangeInputDigitalOutputWindow??5   	   $	??225??60   $	??400??6=    ? DataContext 5	   	   ??  ?? RelativeSource ?? *Self+.??5
      ?  ? ColumnDefinitions 5   
   ?? 5      $??* ?6   ?? 5      $??* ?6    ? RowDefinitions 5   
   ?? 5      $??* ?6   ?? 5      $??* ?6   .+?5   
   ?? $??Initial??6   $??Center=?5      $	??180??5      $??40??6   $??40q?6(   $??20(?64   $??0??5      $??0??6    ?? 5   
   -   5        $??initialValue??5   V   $??1??5      $??0??6"   $	??180??5      $??30??6    ?? SelectedItem 5      ??  ?? Path$ tempOutput.InitialValue? ?? Mode$ TwoWay=?5      ?? 5   
   -   5        $??
initialVal??5      $??Required field!??6&   $??1??5      $??0??6"   
???  ??$y?Hidden=?5   #    J? IsHitTestVisible .  5      ?? 5   
   -   5        $??Confirm??5      $	??150??5      $??24??6   $??0??5       $??1??6!   
???????5"      ?? 5$   
   -   5        $??Close??5$      $	??150??5%      $??24??6   $??1??5&      $??1??6!   
???????5(      5*      5+      