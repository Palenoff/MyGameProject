   M S B A M L   `   `   `  ����R  NDepartments_and_Workers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Departments_and_WorkersDepartments_and_Workers  5   	   ,    &Departments_and_Workers.EmployeeWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Departments_and_Workers     5   	   -   5          � Title$O  IРедактирование информации о сотруднике��5   	   $	��450��68   $��524.107��6E   .��5
      � -   5         �Name$ DepartmentsCB��5
      .+�5   
   �� -   5         ��Name$ SaveBtn��5      $��Сохранить��6"   $
��Left=�66   $	��Top=�6Q   $��76��6i   $��111,325,0,0q�6t   6�   �� 5   
   -   5        $ 	CancelBtn��5      $��Отмена��6$   $
��Left=�65   $	��Top=�6P   $��76��6h   $��337,325,0,0q�6s   6�   �� 5   
   -   5         ��Name$ NameFNameTB��5      $
��Left=�6'   $��20��6B    �� TextWrapping$
 Wrap=�6N   $	��Top=�6b   $	��198��6z   $��
220,99,0,0q�6�   ��6�   �� *Name+ �� UpdateSourceTrigger$ PropertyChanged=��� 5   
   -   5         ��Name$ NameFNameLbl��5      $��Имя Отчество��6&   $
��Left=�6=   $	��Top=�6X   $��	92,95,0,0q�6p   �� 5   
   -   5        $ AgeTB��5      $
��Left=�6!   $��20��6<   $
 Wrap=�6H   $	��Top=�6\   $	��198��6t   $��220,139,0,0q�6�   ��6�   �� *Age+$ PropertyChanged=��� 5   
   -   5        $ AgeLbl��5      $��Возраст��6    $
��Left=�62   $	��Top=�6M   $��
92,135,0,0q�6e   �� 5   
   -	   5        $ SalaryTB��5      $
��Left=�6$   $��20��6?   $
 Wrap=�6K   $	��Top=�6_   $	��198��6w   $��220,176,0,0q�6�   ��6�   �� *Salary+$ PropertyChanged=��� 5   
   -
   5        $ 	SalaryLbl��5      $��Зарплата��6#   $
��Left=�66   $	��Top=�6Q   $��
92,172,0,0q�6i   �� 5   
   -   5        $ 
PositionTB��5      $
��Left=�6&   $��20��6A   $
 Wrap=�6M   $	��Top=�6a   $	��198��6y   $��220,216,0,0q�6�   ��6�   �� *
Position+$ PropertyChanged=��� 5   
   -   5        $ PositionLbl��5      $��Должность��6%   $
��Left=�69   $	��Top=�6T   $��
92,212,0,0q�6l   �� 5   
   -   5         ��Name$ DepartmentsCB_Employees��5      $
��Left=�64   $	��Top=�6O   $	��155��6g   $��220,263,0,0q�6s   ��6�   �� *Departments+�� 5   
   -   5        $ DepartmentLbl��5      $��Департамент��6'   $
��Left=�6=   $	��Top=�6X   $��
92,263,0,0q�6p   5      5      