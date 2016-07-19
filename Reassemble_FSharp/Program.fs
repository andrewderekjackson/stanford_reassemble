
open System;

open Reassemble

[<EntryPoint>]
let main argv = 
    
    let step1 = find_overlap "ell that en" "hat end"
    if step1 <> 6 then
        failwith "Step 1: Expected 6"

    let step1_text = merge "ell that en" "hat end"



    let step2 = find_overlap "ell that end" "t ends well"
    if step2 <> 5 then
        failwith "Step 2: Expected 5"
    
    printfn "%i" step2
        
    
    System.Console.ReadLine() |> ignore
    0




