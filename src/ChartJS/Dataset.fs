module ChartJS.Dataset

open ChartJS.Chart


let empty f = JsonData.initDict |> f

let addToDict label dict element (f: JsonData -> 'a) = JsonData.AddKV label element dict |> f

let data dict d f = JsonData.AddKV "data" d dict |> f

let datasetType dict t f = JsonData.AddKV "type" (JsonData.OfString t) dict |> f

let label dict l f = JsonData.AddKV "label" (JsonData.OfString l) dict |> f

let fillColour col f = (addToDict "fillColor") col f

let strokeColour col f = (addToDict "strokeColor") col f

let pointColour col f = (addToDict "pointColor") col f

let pointStrokeColour col f = (addToDict "pointStrokeColour") col f

let pointHighlightFill col f = (addToDict "pointHighlightFill") col f

let pointHightlightStroke col f = addToDict "pointHightlightStroke" col f

let endConf dict = dict