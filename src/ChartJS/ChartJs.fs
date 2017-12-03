module ChartJS.Chart
open System.Net.NetworkInformation

type Colour = {r: int; g: int; b: int; a: float}
    with static member ToString (c: Colour) = 
            sprintf "rgba(%i, %i, %i, %f)" (c.r) (c.g) (c.b) (c.a)

type JsonData =
    | CInt of int64
    | CFloat of double
    | CBool of bool
    | CColour of Colour
    | CLabel of string
    | CIPoint of int64 * int64
    | CFPoint of double * double
    | CSeq of JsonData seq
    | CDict of Map<string, JsonData>
    with
        static member ToString (jd: JsonData) =
            match jd with
                | CInt n -> string n
                | CBool b ->
                    match b with
                        | true -> "true"
                        | false -> "false"
                | CLabel s -> sprintf "'%s'" s
                | CIPoint (x, y) -> sprintf "{x: %i, y: %i}" x y
                | CFPoint (x, y) -> sprintf "{x: %f, y: %f}" x y
                | CSeq s ->
                    Seq.map (JsonData.ToString) s
                        |> String.concat ",\n"
                        |> sprintf "[%s]"
                | CDict d ->
                    Map.map (fun k v -> JsonData.ToString v) d
                        |> Map.toSeq
                        |> Seq.map (fun (k, v) -> sprintf "%s: %s" k v)
                        |> String.concat ",\n"
                        |> sprintf "[%s]"
                | CColour c -> Colour.ToString c
        static member initDict =
            CDict (Map.empty)

        static member initSeq =
            CSeq (Seq.empty)

        static member PrependElement (s: JsonData) (elt: JsonData) =
            match s with
                | CSeq l -> seq {yield elt; yield! l} |> CSeq
                | _ -> sprintf "type does not match CSeq: %A" s |> failwith

        static member AddAttribute (d: JsonData) (label: string) (elt: JsonData) =
            match d with
                | CDict dict -> Map.add label elt dict |> CDict
                | _ -> sprintf "type does not match CDict: %A" d |> failwith

        static member OfString (s: string) =
            s.Replace("'", "\'") |> CLabel
        static member OfIntSeq (s: int64 seq) =
            Seq.map CInt s |> CSeq

        static member OfDoubleSeq (s: double seq) =
            Seq.map CFloat s |> CSeq

        static member OfIntPairs (s: (int64 * int64) seq) =
            Seq.map CIPoint s |> CSeq

        static member OfDoublePairs (s: (double * double) seq) =
            Seq.map CFPoint s |> CSeq

        static member OfStringSeq (s: string seq) =
            Seq.map JsonData.OfString s |> CSeq
        static member AddElement (element: JsonData) (s: JsonData) =
            match s with
                | CSeq l -> seq {yield element; yield! l} |> CSeq
                | _ -> sprintf "Element's type does not match CSeq: %A" s |> failwith

        static member AddKV (label: string) (element: JsonData) (dict: JsonData)=
            match dict with
                | CDict d -> Map.add label element d |> CDict
                | _ -> sprintf "Element's type does not match CDict: %A" dict |> failwith


let buildDataset (data: JsonData) (label: string) (plotType: string) =
    let jlabel = CLabel label
    let jtype = CLabel plotType
    JsonData.initDict
        |> JsonData.AddKV "data" data
        |> JsonData.AddKV "type" jtype
        |> JsonData.AddKV "label" jlabel

let buildData (datasets: JsonData seq) (labels: string seq) =
    let jDatasets = CSeq datasets
    let jLabels = JsonData.OfStringSeq labels
    JsonData.initDict
        |> JsonData.AddKV "datasets" jDatasets
        |> JsonData.AddKV "labels" jLabels

let buildChart (data: JsonData) (plotType: string) =
    let jType = CLabel plotType
    JsonData.initDict
        |> JsonData.AddKV "type" jType
        |> JsonData.AddKV "data" data
