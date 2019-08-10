$tags = git tag --points-at HEAD
$tags = $tags -split " "
foreach ($tag in $tags) {
    if ($tag -match "^v(.+)") {
        $version = $Matches.1
        write-host "Extracted version $version from tag $tag"
        write-host '##vso[task.setvariable variable=BasiliskVersion]$version' # This is how you publish variables downstream in Azure Pipelines
        exit 0
    }
}

write-host "No tags matching pattern ^v.+ on HEAD!"
exit 1
