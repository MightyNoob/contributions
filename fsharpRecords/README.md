== Headline ==

A simple implementation in [[Language:FSharp|F#]] for the [[Technology:.NET]] Framework

== Characteristics ==

The contribution demonstrates  basic style of [[functional programming]] in [[Language:FSharp|F#]]. 
Persons, Employees, Departments and Companies are represented via [[record]] types.  [[Pure function]]s are implemented 
in pipelining  style to realize totaling and cutting salaries.

== Illustration ==

The company model uses records to [[data composition]]:

Basic types for [[string]]s and [[float]]s are used to represent names, addresses and salaries. 
More advanced [[array]]s are used to represent a collection of the recently named [[record]]s (Employee, Department)

[[Feature:Salary total]] and [[Feature:Salary cut]] are implemented by families of functions on the company types. 
We only show the family for totaling salaries here which uses pipelining style.

<syntaxhighlight lang="fsharp">
-- Total all salaries in a company
-- totalComp:: Company -> Decimal
let totalComp (comp:Company) = 
    List.fold (fun (acc) (elem:Department) -> acc + totalDept(elem)) 0M (List.ofSeq comp.Departments)

	
-- Total all salaries in a department
-- totalDept:: Department -> Decimal
let rec totalDept (dept:Department) =
    dept.Manager.Salary
    |> fun t -> List.fold (fun (acc) (elem:Department) -> acc + totalDept(elem)) t (List.ofSeq dept.SubUnits)
    |> fun t -> List.fold (fun (acc) (elem:Employee) -> acc + elem.Salary) t (List.ofSeq dept.Employees)
</syntaxhighlight>


== Architecture ==

The contribution consit of four modules: "CompanyModels.fs" which contains the definition of the used classes (including the total / cut function); 
"CompanyBuilder.fs" which creates a sample instance of a company; 
"Program.fs" which basically creates a sample company through "buildCompany" of "CompanyBuilder.fs" and executes total and cut printing the results (simple testing);
"Test.fs" which contains a more professional NUnit Test scenario.

== Metadata ==

* [[uses::Language:FSharp|F#]]
* [[uses::Technology:fcs.exe]]
* [[uses::Technology:.NET]]
* [[uses::Technology:NUnit]]
* [[uses::Technology:Visual Studio]]
* [[implements::Feature:Salary total]]
* [[implements::Feature:Salary cut]]
* [[implements::Feature:Company]]
* [[developedBy::Contributor:Andrei Varanovich]]
* [[developedBy::Contributor:Marcus Opdenberg]]