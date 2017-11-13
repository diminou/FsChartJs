module ChartJS.Tests

open ChartJS.Chart
open NUnit.Framework

[<Test>]
let ``hello returns 42`` () =
  let result =
    ChartJS.Chart.CInt 42L
      |> ChartJS.Chart.JsonData.ToString |> sprintf "%s"
  Assert.AreEqual("42",result)
