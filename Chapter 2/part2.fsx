type LogLevel =
    | Error
    | Warning
    | Info

let log (level:LogLevel) message =
    printfn $"[%A{level}]: %s{message}" 

let logError = log Error

let m1 = log Error "Curried function"
let m2 = logError "Partially Applied function"