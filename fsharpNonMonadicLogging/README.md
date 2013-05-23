== Headline ==

A non-monadic implementation of the Logging feature in [[Language:FSharp|F#]] for the [[Technology:.NET]] Framework

== Characteristics ==

This first step in logging salary changes is implemented through a straightforward family of functions in a way that salary changes are passed into and out of functions through tuples.
This code is quite voluminous and can be improved and shortened by using monads.

== Illustration ==

Salary changes are kept through the following log data:
<syntaxhighlight lang="fsharp">
    type LogEntry = {
        name: Person;
        oldSalary: decimal;
        mutable newSalary: decimal;
    }

    type Log = {mutable LogList : List<LogEntry>}
</syntaxhighlight>
A log resulting of a salary cut from the sample company:
<syntaxhighlight lang="fsharp">
[LogEntry {name = "Craig", oldSalary = 123456.0, newSalary = 61728.0},
 LogEntry {name = "Erik", oldSalary = 12345.0, newSalary = 6172.5},
 LogEntry {name = "Ralf", oldSalary = 1234.0, newSalary = 617.0},
 LogEntry {name = "Ray", oldSalary = 234567.0, newSalary = 117283.5},
 LogEntry {name = "Klaus", oldSalary = 23456.0, newSalary = 11728.0},
 LogEntry {name = "Karl", oldSalary = 2345.0, newSalary = 1172.5},
 LogEntry {name = "Joe", oldSalary = 2344.0, newSalary = 1172.0}]
</syntaxhighlight>
The median can be computed for a given log as follows:
<syntaxhighlight lang="fsharp">
let log2deltas = 
		List.sortBy (fun entry-> entry.newSalary - entry.oldSalary) >> List.map (fun entry-> entry.newSalary - entry.oldSalary)

let log2median = 
        let median (l:decimal list) = l.[(List.length l)/2]
        log2deltas >> median
</syntaxhighlight>
The sample log resolves to the following median:
<syntaxhighlight lang="fsharp">
-6172.5
</syntaxhighlight>

[[Feature:Salary cut]] is implemented in a logging-enabling way as follows:
<syntaxhighlight lang="fsharp">
</syntaxhighlight>
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
<syntaxhighlight lang="fsharp">

== Relationships ==

See [[Contribution:fsharp]] for a implementation of[[101feature:Cut]] without logging.

== Architecture ==

The contribution consists of five modules: "Data.fs" which contains the definition of the used classes; 
"Sample.fs" which creates a sample instance of a company; 
"Log.fs" which contains the log datastructure as well as the functions log2deltas, log2mean and log2median.
"Cut.fs" which contains the cut functions (with logs) for employees, departments and a company.
"Main.fs" which basically creates a sample company through "buildCompany" of "Sample.fs" and executes cut printing the results as well as the log;


== Metadata ==

* [[developedBy::Contributor:Marcus Opdenberg]]
* [[implements::Feature:Salary total]]
* [[implements::Feature:Salary cut]]
* [[implements::Feature:Company]]
* [[uses::Language:FSharp|F#]]
* [[uses::Technology:fcs.exe]]
* [[uses::Technology:.NET]]
* [[uses::Technology:Visual Studio]]