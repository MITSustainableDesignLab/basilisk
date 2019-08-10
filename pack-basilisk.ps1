param (
    [string]$Version
)

if (!$Version) {
    $tags = git tag --points-at HEAD
    $tags = $tags -split " "
    foreach ($tag in $tags) {
        if ($tag -match "^v(.+)") {
            $Version = $Matches.1
            write-host "Using version=$Version from Git tag $tag"
            break
        }
    }
}
else {
    write-host "Using version=$Version because it was explicitly passed as an argument"
}

if (!$Version) {
    write-host "No version specified on the command line and no tags matching pattern ^v.+ on HEAD!"
    exit 1
}

dotnet pack --no-build -p:PackageVersion=$Version -p:FileVersion=$Version -o targets -c Release