module CompanyModel 
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
         mutable Employees : Employee[];
         mutable SubUnits : Department[];
        }

    type Company = {
         mutable Name : string
         mutable Departments : Department[]
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


    (*type Person() =
     class
        let mutable name = ""
        let mutable address = ""
        member this.Name with get() = name and set v = name <- v
        member this.Address with get() = address and set v = address <- v
    end

    type Employee(person:Person) =
     class
        let mutable salary = 0M
        member this.Salary with get() = salary and set v = salary <- v
        member this.Person with get() = person
    end

    type Department() =
     class
        let mutable name = ""
        let mutable manager= new Employee(new Person())
        let mutable employees = new List<Employee>()
        let mutable subUnits = new List<Department>()

        member this.Name with get() = name and set v = name <- v
        member this.Manager with get() = manager and set v = manager <- v
        member this.Employees with get() = employees and set v = employees <- v
        member this.SubUnits with get() = subUnits and set v = subUnits <- v
        
        member this.Cut =
            List.iter (fun (dept:Department) -> dept.Cut) (List.ofSeq subUnits)
            manager.Salary <- manager.Salary / 2M
            List.iter (fun (emp:Employee) -> emp.Salary <- emp.Salary / 2M) (List.ofSeq employees)
        
        member this.Total with get() =
         manager.Salary
          |> fun t -> List.fold (fun (acc) (elem:Department) -> acc + elem.Total) t (List.ofSeq subUnits)
          |> fun t -> List.fold (fun (acc) (elem:Employee) -> acc + elem.Salary) t (List.ofSeq employees)
   end

    type Company() = 
     class
       let mutable name = ""
       let mutable departments:List<Department> = new List<Department>()  
        
       member this.Name with get() = name and set v = name <- v
       member this.Departments with get() = departments and set v = departments <- v
       
       member this.TotalSalaries =
        List.fold (fun (acc) (elem:Department) -> acc + elem.Total) 0M (List.ofSeq departments)
        
      
       member this.CutSalaries = 
         List.iter (fun (dept:Department) -> dept.Cut) (List.ofSeq departments)
    end*)
