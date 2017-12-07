$ErrorActionPreference = "Stop"

$version = '0.6.0' # the version under development, update after a release
$versionSuffix = '-local.1' # manually incremented for local builds

function isVersionTag($tag){
    $v = New-Object Version
    [Version]::TryParse($tag, [ref]$v)
}

if ($env:appveyor){
    $versionSuffix = '-build.' + [int]::Parse($env:appveyor_build_number).ToString()
    if ($env:appveyor_repo_tag -eq 'true' -and (isVersionTag($env:appveyor_repo_tag_name))){
        $version = $env:appveyor_repo_tag_name
        $versionSuffix = ''
    }
    Update-AppveyorBuild -Version "$version$versionSuffix"
}

$v = "$version$versionSuffix"

yarn install --ignore-engines

Set-Location $psscriptroot\src
dotnet restore
dotnet fable yarn-build

Set-Location $psscriptroot\test
dotnet restore
dotnet fable yarn-test

Set-Location $psscriptroot
yarn version --new-version $v --no-git-tag-version

$js = "dist/ts2fable.js"
yarn global add babel-cli
babel --plugins shebang $js -o $js

# yarn pack is packing too many files
if ($env:appveyor){
    node build-update-package.js
}
npm pack
tar tf ts2fable-$v.tgz