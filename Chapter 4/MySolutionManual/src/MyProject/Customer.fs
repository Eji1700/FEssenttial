module MyApplication.Customer // Namespace.Module
open System

type Customer = {
    Name : string
}

module Domain =
    let create (name:string) =
        { Name = name }
    
module Db =
    open System.IO

    let save (customer:Customer) =
    // Imagine this talks to a database
        ()