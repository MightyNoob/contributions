module Cut

open Data
open Log
open System.Collections.Generic

   //Employee Salary cut with Logging
    let SalCutEmpLog (emp:Employee):Employee*LogEntry = 
            let templog = {name= emp.Person; oldSalary= emp.Salary; newSalary =emp.Salary}
            emp.Salary <- emp.Salary / 2M 
            templog.newSalary <-emp.Salary
            (emp,templog)


    //Dapartment Salary cut with Logging
    let rec SalCutDeptLog (dept:Department):Department*Log =
            let tempLogList = List<LogEntry>()

            //Manager
            let cache1:Employee*LogEntry = SalCutEmpLog dept.Manager
            tempLogList.Add(snd(cache1))
            dept.Manager.Salary <- fst(cache1).Salary
       
            //Employees
            List.iter (fun emp -> 
                    let cache:Employee*LogEntry = (SalCutEmpLog emp) 
                    emp.Salary <- (fst(cache).Salary) 
                    tempLogList.Add(snd(cache))) (List.ofSeq dept.Employees)

            //SubDepartments
            List.iter(fun sunit-> 
                    let cache2:Department*Log= (SalCutDeptLog sunit)
                    tempLogList.AddRange(List.ofSeq(snd(cache2).LogList))) (List.ofSeq dept.SubUnits)
            //wrap tempLog into Log container
            let tempLog = {LogList= tempLogList}
            //return Department*Log tupel 
            (dept,tempLog)


    //Company Salary cut with Logging
    let SalCutCompLog (comp:Company):Company*Log =
            let tempLogList = List<LogEntry>()

            List.iter (fun dept-> 
                    let cache:Department*Log = (SalCutDeptLog dept)
                    tempLogList.AddRange(List.ofSeq(snd(cache).LogList)))(List.ofSeq comp.Departments)

            let tempLog = {LogList= tempLogList} 
            (comp,tempLog)