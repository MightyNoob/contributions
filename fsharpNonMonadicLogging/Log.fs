module Log

open Data
open System.Collections.Generic


    type LogEntry = {
        name: Person;
        oldSalary: decimal;
        mutable newSalary: decimal;
    }

    type Log = {mutable LogList : List<LogEntry>}
    
    //Compute the Salary deltas from a given Log
    let log2deltas = List.sortBy (fun entry-> entry.newSalary - entry.oldSalary) >> List.map (fun entry-> entry.newSalary - entry.oldSalary)

    //Compute the mean value of salary deltas from a given log
    let log2mean  =  
            let mean (l:decimal list) = decimal (List.sum l) / (decimal) (List.length l)
            log2deltas >> mean   

    //Compute the median of deltas from a given Log
    let log2median = 
            let median (l:decimal list) = l.[(List.length l)/2]
            log2deltas >> median

   