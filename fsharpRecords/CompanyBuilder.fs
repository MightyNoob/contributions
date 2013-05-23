module CompanyBuilder
open CompanyModel
open System.Collections.Generic

let buildCompany = 
    let departments = new List<Department>()
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
        Employees= Array.zeroCreate 2;        
        SubUnits= Array.zeroCreate 0;
        }
    researchDept.Employees.[0] <- erikEmployee
    researchDept.Employees.[1] <- ralfEmployee
    departments.Add(researchDept)

    let rayManager = {
        Salary=234567M;
        Person= {Name="Ray";Address="Redmond"};
        } 
    let erikEmployee = {
        Salary=12345M;
        Person={Name="Erik";Address="Utrecht"};
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
        Employees= Array.zeroCreate 1;        
        SubUnits= Array.zeroCreate 0;
        }
    dev11Dept.Employees.[0]<-joeEmployee

    let dev1Dept = {
        Name="Development Department 1";
        Manager= klausManager;
        Employees= Array.zeroCreate 0;        
        SubUnits= Array.zeroCreate 1;
        }
    let devDept = {
        Name="Development Department";
        Manager= rayManager;
        Employees= Array.zeroCreate 0;        
        SubUnits= Array.zeroCreate 1;
    }
    devDept.SubUnits.[0]<-dev1Dept
    dev1Dept.SubUnits.[0]<-dev11Dept
    departments.Add(devDept)

    let company = {
        Name="meganalysis";
        Departments=departments.ToArray()
    }
    company
  

