module Main

open Data
open Cut
open Sample
open Log
open System.Collections.Generic

let printList (param:decimal list) = 
    List.iter (fun entry -> printfn "Log entries: %M" entry )(List.ofSeq param)

let printLog (param:Log)=
    List.iter (fun logEntry -> printfn "Log entries: %s %M %M" logEntry.name.Name logEntry.oldSalary logEntry.newSalary) (List.ofSeq param.LogList)

let rec printDept (dept:Department) =
    printfn "Department looks like: %s %M" dept.Manager.Person.Name dept.Manager.Salary
    List.iter (fun employee -> printfn "Department looks like: %s %M" employee.Person.Name employee.Salary)(List.ofSeq dept.Employees)
    List.iter (fun sunit -> printDept sunit)(List.ofSeq dept.SubUnits)

let printComp (comp:Company)=
    List.iter (fun dept -> printDept dept)(List.ofSeq comp.Departments)
        
    

let c:Company = buildCompany
printfn "The initial Company looks like: \n"
printComp c
let cache = SalCutCompLog c
printfn "\nAfter the first Budget Cut it looks like the following:\n"
printLog(snd(cache))
printComp (fst(cache))
printfn "\nAfter the second cut we have:\n"
let cache2 = SalCutCompLog c
printLog(snd(cache2))
printComp (fst(cache2))
printfn"\nafter the Logfile is sorted:\n"
printList (log2deltas(List.ofSeq(snd(cache2).LogList)))
printfn "Mean is: %M"(log2mean(List.ofSeq(snd(cache2).LogList)))
printfn "Median is: %M"(log2median(List.ofSeq(snd(cache2).LogList)))