module Sample

open Data
open System.Collections.Generic

let buildCompany = 
    let mutable departments = new List<Department>()
    let craigManager = {
        Salary=123456M;
        Person= {Name="Craig";Address="Redmond"};
        }
    let erikEmployee = {
        Salary=12345M;
        Person={Name="Erik";Address="Utrecht"};
        }
    let ralfEmployee = {
        Salary=1234M;
        Person={Name="Ralf";Address="Koblenz"};
        }
    let researchDept = {
        Name="Research Department";
        Manager= craigManager;
        Employees= new List<Employee>();        
        SubUnits= new List<Department>();
        }
    researchDept.Employees.Add(erikEmployee) 
    researchDept.Employees.Add(ralfEmployee)
    departments.Add(researchDept)

    let rayManager = {
        Salary=234567M;
        Person= {Name="Ray";Address="Redmond"};
        } 
    let klausManager = {
        Salary=23456M;
        Person={Name="Klaus";Address="Boston"}
    }
    let karlManager = {
        Salary=2345M;
        Person={Name="Karl";Address="Riga"}
    }
    let joeEmployee = {
        Salary=2344M;
        Person={Name="Joe";Address="Wifi City"};
        }
    let dev11Dept = {
        Name="Development Department 1.1";
        Manager= karlManager;
        Employees= new List<Employee>();        
        SubUnits= new List<Department>();
        }
    dev11Dept.Employees.Add (joeEmployee)

    let dev1Dept = {
        Name="Development Department 1";
        Manager= klausManager;
        Employees= new List<Employee>();        
        SubUnits= new List<Department>();
        }
    let devDept = {
        Name="Development Department";
        Manager= rayManager;
        Employees= new List<Employee>();        
        SubUnits= new List<Department>();
    }
    devDept.SubUnits.Add(dev1Dept)
    dev1Dept.SubUnits.Add(dev11Dept)
    departments.Add(devDept)

    let company = {
        Name="meganalysis";
        Departments=departments
    }
    company