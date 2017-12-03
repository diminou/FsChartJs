module ChartJS.Options

open ChartJS.Chart

type HoverMode = Nearest | Index
    with static member ToJsonData x =
            let y =
                match x with
                    | Nearest -> CLabel "nearest"
                    | Index -> CLabel "index"
            y

type Position = Top | Left | Bottom | Right
    with static member ToJsonData x =
            let y =
                match x with
                    | Top -> CLabel "top"
                    | Left -> CLabel "left"
                    | Bottom -> CLabel "bottom"
                    | Right -> CLabel "right"
            y

let newTitle = JsonData.initDict
let display tf dict = JsonData.AddKV "display" (CBool tf) dict
let position pos dict = JsonData.AddKV "position" (Position.ToJsonData pos) dict
let text t dict = JsonData.AddKV "text" (CLabel t) dict
let fontSize s dict = JsonData.AddKV "fontSize" (CInt s) dict

let newOptions = JsonData.initDict
let hover hm = JsonData.AddKV "hover" (HoverMode.ToJsonData hm)
let legend = JsonData.AddKV "legend"
