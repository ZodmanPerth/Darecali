$solutionFile = "C:\Carl\dev\Darecali\Darecali.sln"
$sourceRootFolder = "C:\Carl\dev\Darecali\Darecali"
$scriptFolder = "$sourceRootFolder.Deployment\Scripts\PowerShell"
$sourceTitle = "Darecali"
$nugetRepositoryFolder = "c:\nuget\"

$progressId = 22100;
$progressActivity = "Build and Deploy NuGet Local";
$progressTotalSteps = 3;
$progressStep = 100 / ($progressTotalSteps + 1);
$progressPercentage = 0;

Set-Location $sourceRootFolder

# Build Solution in release mode
Write-Progress -Id $progressId -Activity $progressActivity -Status "Building solution" -PercentComplete ($progressPercentage += $progressStep);
Import-Module $scriptFolder\Build-VisualStudioSolution.ps1
Build-VisualStudioSolution -SolutionFilePath $solutionFile -Configuration "release" -CleanFirst -Silent

# Package for nuGet
Write-Progress -Id $progressId -Activity $progressActivity -Status "Creating NuGet package" -PercentComplete ($progressPercentage += $progressStep);
$packageOutput = (nuget pack "$sourceTitle.csproj" -Prop Configuration=Release)

# Copy Compiled nuGet package to local repository
Write-Progress -Id $progressId -Activity $progressActivity -Status "Copying package to local NuGet repository" -PercentComplete ($progressPercentage += $progressStep);
$package = (Get-ChildItem *.nupkg | Sort-Object -Property ($_.LastWriteTime) -Descending).Name[0]
copy-item $package $nugetRepositoryFolder

Write-Progress -Id $progressId -Activity $progressActivity -Completed;