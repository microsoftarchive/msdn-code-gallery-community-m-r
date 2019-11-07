module Lexer

open System
open System.Collections.Generic
open System.Text.RegularExpressions
open StateMonad
    

let state = StateBuilder()
let lexerDefinitions = state

type LexDefinitions = 
  {regexes : string list;
   names : string list;
   nextlines : bool list;
   ignores : bool list; }
   

type Token = 
    { name : string;
      text: string; 
      pos :int;
      column: int;
      line: int }
   
let createLexDefs pb =  (runState pb) {regexes = []; names = []; nextlines = []; ignores = []} |> snd
 
let tokenize lexerBuilder (str:string) = 
    let patterns = createLexDefs lexerBuilder
    let combinedRegex =  Regex(List.fold (fun acc reg -> acc + "|" + reg) (List.head patterns.regexes) (List.tail patterns.regexes))
    let nextlineMap = List.zip patterns.names patterns.nextlines |> Map.ofList
    let ignoreMap = List.zip patterns.names patterns.ignores |> Map.ofList

    let tokenizeStep (pos,line,lineStart) = 
        if pos >= str.Length then
            None
        else
            let getMatchedGroupName (grps:GroupCollection) names = List.find (fun (name:string) -> grps.[name].Length > 0) names
            match combinedRegex.Match(str, pos) with
                | mt when mt.Success && pos = mt.Index  -> 
                    let groupName = getMatchedGroupName mt.Groups patterns.names
                    let column = mt.Index - lineStart
                    let nextPos = pos + mt.Length
                    let (nextLine, nextLineStart) = if nextlineMap.Item groupName then (line + 1, nextPos) else (line,lineStart)
                    let token = if ignoreMap.Item groupName 
                                then None 
                                else Some {
                                        name = groupName; 
                                        text = mt.Value; 
                                        pos = pos; 
                                        line = line; 
                                        column = column; }
                    Some(token, (nextPos, nextLine, nextLineStart))
                    
                | otherwise -> 
                    let textAroundError = str.Substring(pos, min (pos + 5) str.Length)
                    raise (ArgumentException (sprintf "Lexing error occured at line:%d and column:%d near the text:%s" line (pos - lineStart) textAroundError))

    Seq.unfold tokenizeStep (0, 0, 0) |> Seq.filter (fun x -> x.IsSome) |> Seq.map (fun x -> x.Value)
    

// Lexer definition functions
let buildDefinition name pattern nextLine ignore =
    state {
        let! x = state.Get()
        do! state.Put { regexes = x.regexes @  [sprintf @"(?<%s>%s)" name pattern];
                        names = x.names @ [name]; 
                        nextlines  = x.nextlines @ [nextLine];
                        ignores = x.ignores @ [ignore]}
    }
    

let addDefinition name pattern = buildDefinition name pattern false false
let addIgnoreDefinition name pattern = buildDefinition name pattern false true
let addNextlineDefinition name pattern = buildDefinition name pattern true true    

