   M S B A M L   `   `   `  ����R  NDepartments_and_Workers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Departments_and_WorkersDepartments_and_Workers  5   	   (    "Departments_and_Workers.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Departments_and_Workers     5   	     � Title$H  BБаза данных департаментов и отделов��5   	   $	��450��65   $	��800��6B   .��5	      � .+�5
   
   �� -   5         ��Name$ 
HeadingLbl��5
      $P��JБаза данных сотрудников и департаментов��6$   $��Center=�6V   $	��Top=�6s   $��151,33,150,0q�6�   $��57��6�   $	��491��6�   $��24(�6�   �� 5   
   -   5         ��Name$ DepartmentsCB��5      $
��Left=�6*   $	��Top=�6E   $	��155��6]   $��
43,141,0,0q�6i   ��5      �� 5      .R�5      �� ��6    �� *Name+ �� Mode$ TwoWay=�5      5      �� 5   
   -   5        $ DepartmentsLbl��5      $��Департамент��6(   $
��Left=�6>   $	��Top=�6Y   $��	43,94,0,0q�6q   $��22(�6�   �� 5   
   -   5        $ WorkerssLbl��5      $��Сотрудники��6%   $
��Left=�6:   $	��Top=�6U   $��
355,95,0,0q�6m   $��22(�6�   �� 5   
   -   5         ��Name$ EmployeesLV��5      $
��Left=�6(   $	��141��6C   $	��Top=�6P   $	��392��6h   $��355,147,0,0q�6t    �� View 5      �� 5      .P�5      �� $��Имя��6%    �� Width$	 120��5   %    �� DisplayMemberBinding 5   %   �� *Name+�� 5      $��Возраст��6%   $ 50��66    5   %   �� *Age+�� 5      $��Зарплата��6%   $ 60��67    5   %   �� *Salary+�� 5       $��Должность��6%   $	 155��68    5!   %   �� *
Position+5"      5$      �� 5%   
   -   5         ��Name$ AddEmployeeBtn��5%      $+��%Добавить сотрудника��6)   $��355,317,307,70q�6G   6_   �� 5&   
   -   5        $ DeleteEmployeeBtn��5&      $)��#Удалить сотрудника��6,   $��540,317,122,70q�6I   6a   �� 5'   
   -   5        $ AddDepartmentBtn��5'      $-��'Добавить департамент��6+   $��43,234,585,153q�6J   6b   �� 5(   
   -	   5        $ EditDepartmentBtn��5(      $7��1Редактировать департамент��6,   $��43,184,585,203q�6P   6h   5)      5*      