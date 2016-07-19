module Reassemble

let reduce_left (s:string) = s.Substring(1,s.Length-1)
let reduce_right (s:string) = s.Substring(0, s.Length-1)

let merge (s1:string) (s2:string) (index:int) =
    s1 + s2

let rec find_overlap_core (s1:string) (s2:string) (reduce: (string) -> (string)) = 
    
    match s1 with
        | _ when s1 = "" || s2 = "" -> 0
        | _ when s1.Contains(s2) -> s2.Length
        | _ -> find_overlap_core s1 (reduce s2) reduce
               

let rec find_overlap (s1:string) (s2:string) =
    max (find_overlap_core s1 s2 reduce_left) (find_overlap_core s1 s2 reduce_right)
    

 