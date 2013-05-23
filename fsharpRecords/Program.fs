open CompanyModel
open CompanyBuilder

let c = buildCompany

printfn "Company Name %A" c.Name
printfn "%M" (totalComp (c:Company))
compCut c
printfn "%M" (totalComp c)