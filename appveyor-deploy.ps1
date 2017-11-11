$ErrorActionPreference = "Stop"

# it only runs on successful builds that are not pull requests

if($env:appveyor_repo_name -eq "fable-compiler/ts2fable" -and $env:appveyor_repo_branch -eq "master"){
    "//registry.npmjs.org/:_authToken=$env:npmauthtoken`n" | out-file "$env:userprofile\.npmrc" -Encoding ASCII
    npm whoami
    $v = $env:appveyor_build_version
    yarn publish ts2fable-$v.tgz --new-version $v --tag next
}