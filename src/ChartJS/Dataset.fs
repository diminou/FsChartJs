module ChartJS.Dataset

open ChartJS.Chart


let emptyDatasets = JsonData.initSeq

let addDataset dataset = JsonData.AddElement dataset

let datasets = JsonData.AddKV "datasets"

let empty = JsonData.initDict

let addToDict label element = JsonData.AddKV label element

let data d  = JsonData.AddKV "data" d

let datasetType t = JsonData.AddKV "type" (JsonData.OfString t)

let label l = JsonData.AddKV "label" (JsonData.OfString l)

let fillColour col  = (addToDict "fillColor") col

let strokeColour col = (addToDict "strokeColor") col

let pointColour col = (addToDict "pointColor") col

let pointStrokeColour col = (addToDict "pointStrokeColour") col

let pointHighlightFill col = (addToDict "pointHighlightFill") col

let pointHightlightStroke col = addToDict "pointHightlightStroke" col

let endConf dict = dict