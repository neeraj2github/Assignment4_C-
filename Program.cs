using System;
using System.Linq;
using System.Collections.Generic;
					
public class Program
{  
     IList<Employee> employeeList;
	 IList<Salary> salaryList;
	
	public Program(){
		employeeList = new List<Employee>() { 
          
			new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
			new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
			new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
			new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
			new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
			new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
			new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}		

		};
		
		   salaryList = new List<Salary>() {
          
			new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
            
		};
	}
	
	public static void Main()
	{		
		Program program = new Program();
		
		program.Task1();
		
		program.Task2();
		
		program.Task3();
	}
	
	public void Task1(){
		//Implementation
       var query = from e in employeeList
                join s in salaryList on e.EmployeeID equals s.EmployeeID
                group s by e.EmployeeFirstName + " " + e.EmployeeLastName into g
                select new {
                    Name = g.Key,
                    TotalSalary = g.Sum(x => x.Amount)
                } into result
                orderby result.TotalSalary
                select result;
    
    foreach (var item in query)
    {
        Console.WriteLine($"{item.Name} - {item.TotalSalary}");
    }
	}


	
	public void Task2(){
		 //Implementation
         var query = from e in employeeList
                orderby e.Age descending
                select e;

    Employee secondOldest = query.Skip(1).FirstOrDefault();

    var salaryQuery = from s in salaryList
                      where s.EmployeeID == secondOldest.EmployeeID && s.Type == SalaryType.Monthly
                      select s.Amount;

    Console.WriteLine($"Employee: {secondOldest.EmployeeFirstName} {secondOldest.EmployeeLastName}");
    Console.WriteLine($"Age: {secondOldest.Age}");
    Console.WriteLine($"Total Monthly Salary: {salaryQuery.Sum()}");
	}
	
	public void Task3(){
		 //Implementation
   var result = from employee in employeeList
                 where employee.Age > 30
                 join salary in salaryList on employee.EmployeeID equals salary.EmployeeID
                 group salary by salary.Type into salaryGroup
                 select new {
                     Type = salaryGroup.Key,
                     MeanSalary = salaryGroup.Average(salary => salary.Amount)
                 };
                 
    foreach(var item in result){
        Console.WriteLine("{0}, Mean Salary: {1}", item.Type, item.MeanSalary);
    }
	}
}

public enum SalaryType{
	Monthly,
	Performance,
	Bonus
}

public class Employee{
	public int EmployeeID { get; set; }
	public string EmployeeFirstName { get; set; }
	public string EmployeeLastName { get; set; }
	public int Age { get; set; }	
}

public class Salary{
	public int EmployeeID { get; set; }
	public int Amount { get; set; }
	public SalaryType Type { get; set; }
}