# for running the tests locally
$ErrorActionPreference = "Stop"

# build tests
Set-Location $psscriptroot\test
dotnet fable yarn-test

# run tests
Set-Location $psscriptroot
mocha .\test\bin\test.js