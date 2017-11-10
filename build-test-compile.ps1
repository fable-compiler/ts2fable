$ErrorActionPreference = "Stop"

# generate the F# files
node dist\ts2fable.js splitter.config.js

Set-Location $psscriptroot\test-compile
dotnet restore
dotnet build

Set-Location $psscriptroot