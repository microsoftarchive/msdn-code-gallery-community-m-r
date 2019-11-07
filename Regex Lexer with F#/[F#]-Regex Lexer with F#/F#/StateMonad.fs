module StateMonad

type State<'s,'a> = State of ('s -> ('a *'s))
let runState (State f) = f
type StateBuilder() = 
    member b.Return(x) = State (fun s -> (x,s))
    member b.Delay(f) = f() : State<'s,'a>
    member b.Zero() = State (fun s -> ((),s))
    member b.Bind(State p,rest) = State (fun s -> let v,s2 = p s in  (runState (rest v)) s2)
    member b.Get () = State (fun s -> (s,s))
    member b.Put s = State (fun _ -> ((),s))