﻿// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

module Progam
open Microsoft.FSharp.Text.Lexing

[<EntryPoint>]
let main argv =
    let parseFromFile (fileName:string) = 
        let fileStr = System.IO.File.ReadAllText fileName
        let lexbuf = LexBuffer<char>.FromString fileStr
        try
            Parser.start Lexer.token lexbuf
        with
          | _ -> printfn "Syntax error in %s on line %d, column %d" fileName (lexbuf.StartPos.Line+1) (lexbuf.StartPos.Column+1);
                 failwith "Syntax error"
    let fnames = List.map System.IO.Path.GetFullPath (List.ofArray argv)
    let asts = List.map parseFromFile fnames
    let typedAsts = TypeChecker.typecheckProgram asts fnames
    //let ast = parseFromFile @"C:\Users\caleb\Documents\juniper_programs\test.jun"
    printfn "%A" asts
    System.Console.ReadKey() |> ignore
    0 // return an integer exit code