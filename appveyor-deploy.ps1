$ErrorActionPreference = "Stop"

echo "appveyor_build_version $env:appveyor_build_version"
echo "appveyor_pull_request_head_repo_branch $env:appveyor_pull_request_head_repo_branch"
echo "appveyor_repo_branch $env:appveyor_repo_branch"

if($env:appveyor_pull_request_head_repo_branch){
    echo "it is a pull request"
} else {
    echo "it is not a pull request"
}


if($env:appveyor_repo_branch -eq "master"){
    echo "master branch"
} else {
    echo "not master branch"
}

if(-not $env:appveyor_pull_request_head_repo_branch -and $env:appveyor_repo_branch -eq "master"){
    echo "what I'm looking for"
} else {
    echo "not what I'm looking for"
}