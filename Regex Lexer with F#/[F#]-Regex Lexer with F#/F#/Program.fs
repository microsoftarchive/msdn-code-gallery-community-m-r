open Lexer
let definitions = 
    lexerDefinitions {
        do! addNextlineDefinition "NEWLINE"     @"(\n\r)|\n|\r"
        do! addIgnoreDefinition   "WS"          @"\s"
        do! addDefinition         "LET"         "let"
        do! addDefinition         "ID"          "(?i)[a-z][a-z0-9]*"
        do! addDefinition         "FLOAT"       @"[0-9]+\.[0-9]+"
        do! addDefinition         "INT"         "[0-9]+"
        do! addDefinition         "OPERATOR"    @"[+*=!/&|<>\^\-]+"
    }


let lex input =
    try
        let y = Lexer.tokenize definitions input
        printfn "%A" y
    with e -> printf "%s" e.Message
    
lex "let a = 5"
