module LexerFacts

open Xunit
open Lexer
open System.Linq

let simpleDefs = 
    state {
        do! addNextlineDefinition "NextLine"           "/"
        do! addIgnoreDefinition "IgnoredSymbol"        "=+"
        do! addDefinition "String"                     "[a-zA-Z]+"
        do! addDefinition "Number"                     "\d+"
        do! addDefinition "Name"                       "Matt"
    }


[<Fact>]
let Will_return_no_tokens_for_empty_string() =
  
    let tokens = Lexer.tokenize simpleDefs ""
    
    Assert.Equal(0, tokens.Count())


[<Fact>]
let Will_throw_exception_for_invalid_token() =
  
    let tokens = Lexer.tokenize simpleDefs "-"

    let ex = Assert.ThrowsDelegateWithReturn(fun () -> upcast tokens.Count()) |> Record.Exception

    Assert.NotNull(ex)
    Assert.True(ex :? System.ArgumentException)

[<Fact>]
let Will_ignore_symbols_defined_as_ignore_symbols() =
  
    let tokens = Lexer.tokenize simpleDefs "========="
    
    Assert.Equal(0, tokens.Count())


[<Fact>]
let Will_get_token_with_correct_position_and_type() =
  
    let tokens = Lexer.tokenize simpleDefs "1one=2=two"
    
    Assert.Equal("Number",tokens.ElementAt(2).name)
    Assert.Equal("2",tokens.ElementAt(2).text)
    Assert.Equal(5,tokens.ElementAt(2).pos)
    Assert.Equal(5,tokens.ElementAt(2).column)
    Assert.Equal(0,tokens.ElementAt(2).line)

[<Fact>]
let Will_tokenize_string_with_alernating_numbers_and_strings() =
  
    let tokens = Lexer.tokenize simpleDefs "1one2two"
    
    Assert.Equal("1",tokens.ElementAt(0).text)
    Assert.Equal("one",tokens.ElementAt(1).text)
    Assert.Equal("2",tokens.ElementAt(2).text)
    Assert.Equal("two",tokens.ElementAt(3).text)

[<Fact>]
let Will_increment_line_with_newline_symbol() =
  
    let tokens = Lexer.tokenize simpleDefs "1one/2two"
    
    Assert.Equal("Number",tokens.ElementAt(2).name)
    Assert.Equal("2",tokens.ElementAt(2).text)
    Assert.Equal(5,tokens.ElementAt(2).pos)
    Assert.Equal(0,tokens.ElementAt(2).column)
    Assert.Equal(1,tokens.ElementAt(2).line)

[<Fact>]
let Will_give_priority_to_lexer_definitions_defined_earlier() =
  
    let tokens = Lexer.tokenize simpleDefs "Matt"
    
    Assert.Equal("String",tokens.ElementAt(0).name)