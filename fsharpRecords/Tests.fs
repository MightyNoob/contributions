module Tests

open NUnit.Framework 
open CompanyModel
open CompanyBuilder


type [<TestFixture>] OperationsTests =
 class
    new() = {} 
    [<Test>]  
    member this.TotalTest() =
     let c = buildCompany
     Assert.AreEqual(123456 + 12345 + 1234 + 234567 + 23456 + 2345 + 2344, totalComp(c))

    [<Test>]  
    member this.CutTest() =
     let c = buildCompany
     let total = totalComp(c)
     compCut(c)
     let totalAfterCut = totalComp(c)   
     Assert.AreEqual(total/2M, totalAfterCut)
     
  end