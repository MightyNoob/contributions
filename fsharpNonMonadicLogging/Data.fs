module Data

open System.Collections.Generic


    type Person = {
         mutable Name : string;
         mutable Address : string;
        }
    
    
    type Employee = {
         mutable Salary : decimal;
         Person : Person;
        }

    type Department = {
         mutable Name : string;
         mutable Manager : Employee;
         mutable Employees : List<Employee>;
         mutable SubUnits : List<Department>;
        }

    type Company = {
         mutable Name : string
         mutable Departments : List<Department>
        }


    let rec totalDept (dept:Department) =
        dept.Manager.Salary
        |> fun t -> List.fold (fun (acc) (elem:Department) -> acc + totalDept(elem)) t (List.ofSeq dept.SubUnits)
        |> fun t -> List.fold (fun (acc) (elem:Employee) -> acc + elem.Salary) t (List.ofSeq dept.Employees)


    let totalComp (comp:Company) = 
        List.fold (fun (acc) (elem:Department) -> acc + totalDept(elem)) 0M (List.ofSeq comp.Departments)


    let rec deptCut (dept:Department) = 
            List.iter (fun (dept:Department) -> deptCut dept) (List.ofSeq dept.SubUnits)
            dept.Manager.Salary <- dept.Manager.Salary / 2M
            List.iter (fun (emp:Employee) -> emp.Salary <- emp.Salary / 2M) (List.ofSeq dept.Employees)


    let compCut (comp:Company) =
            List.iter (fun (dept:Department) -> deptCut dept) (List.ofSeq comp.Departments)      
