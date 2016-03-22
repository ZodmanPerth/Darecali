function Build-VisualStudioSolution
{
    param
    (
        [parameter(Mandatory=$false)]
        [ValidateNotNullOrEmpty()]
        [String] $SolutionFilePath,

        [parameter(Mandatory=$false)]
        [ValidateNotNullOrEmpty()]
        [String] $Configuration = "Debug",

        [parameter(Mandatory=$false)]
        [ValidateNotNullOrEmpty()]
        [string] $Platform = "Any CPU",

	    [ValidateNotNullOrEmpty()]
        [string] $BuildLogFilePath = $env:TEMP + "\build.log",

        [parameter(Mandatory=$false)]
        [ValidateNotNullOrEmpty()]
        [string] $OutputPath = "",

        [parameter(Mandatory=$false)]
        [ValidateNotNullOrEmpty()]
        [Switch] $ShowBuildLog,

        [parameter(Mandatory=$false)]
        [ValidateNotNullOrEmpty()]
        [Switch] $Silent,

        [parameter(Mandatory=$false)]
        [ValidateNotNullOrEmpty()]
        [Switch] $CleanFirst
    )

    process
    {
        # Local Variables
        #TODO: get the latest version of the framework from this folder
        #TODO: 64 bit support
        $MsBuild = $env:systemroot + "\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe";

        # Local Variables
        $SlnFileParts = $SolutionFilePath.Split("\");
        $SlnFileName = $SlnFileParts[$SlnFileParts.Length - 1];
        $bOk = $true;

        try
        {
            # Clear first?
            if ($CleanFirst)
            {
                # Display Progress
                Write-Progress -Id 20275 -Activity $SlnFileName  -Status "Cleaning..." -PercentComplete 10;

                $BuildArgs = @{
			        FilePath = $MsBuild
			        ArgumentList = $SolutionFilePath, "/t:clean", ("/p:Configuration=" + $Configuration), ("/p:Platform=""" + $Platform + """"), "/v:minimal"
			        RedirectStandardOutput = $BuildLogFilePath
			        Wait = $true
			        WindowStyle = "Hidden"
                }

                # Start the build
                Start-Process @BuildArgs #| Out-String -stream -width 1024 > $DebugBuildLogFile

                # Display Progress
                Write-Progress -Id 20275 -Activity $SlnFileName -Status "Done cleaning." -PercentComplete 50;
            }

            # Display Progress
            Write-Progress -Id 20275 -Activity $SlnFileName  -Status "Building..." -PercentComplete 60;

            $myArgs = $SolutionFilePath, "/m /t:rebuild", ("/p:Configuration=" + $Configuration), ("/p:Platform=""" + $Platform + """"), "/v:normal", "/nr:false"
            if ($OutputPath -ne "")
            {
                $myArgs += "/p:OutputPath=$OutputPath"
            }

            # Prepare the Args for the actual build
            $BuildArgs = @{
                FilePath = $MsBuild
                ArgumentList = $myArgs
                RedirectStandardOutput = $BuildLogFilePath
                Wait = $true
                WindowStyle = "Hidden"
            }

            # Start the build
            Start-Process @BuildArgs #| Out-String -stream -width 1024 > $DebugBuildLogFile

            # Display Progress
            Write-Progress -Id 20275 -Activity $SlnFileName  -Status "Done building." -PercentComplete 100;
        }
        catch
        {
            $bOk = $false;
            Write-Error ("Unexpect error occured while building " + $SlnFileParts[$SlnFileParts.Length - 1] + ": " + $_.Message);
        }

        # All good so far?
        if($bOk)
        {
            #Show projects which where built in the solution
            #Select-String -Path $BuildLog -Pattern "Done building project" -SimpleMatch

            # Show if build succeeded or failed...
            $successes = Select-String -Path $BuildLogFilePath -Pattern "Build succeeded." -SimpleMatch
            $failures = Select-String -Path $BuildLogFilePath -Pattern "Build failed." -SimpleMatch

            if($failures -ne $null)
            {
                Write-Warning ($SlnFileName + ": A build failure occured. Please check the build log $BuildLogFilePath for details.");

                if ($Silent -ne $true)
                {
                    Start-Process -verb "Open" $BuildLogFilePath;
                    $logDisplayed = $true;
                }
            }

            # Show the build log (if required)
            if($ShowBuildLog -and ($logDisplayed -ne $true))
            {
                Start-Process -verb "Open" $BuildLogFilePath;
            }
        }

        Write-Progress -Id 20275 -Activity $SlnFileName -Completed
    }

    <#
        .SYNOPSIS
        Executes MSBuild against the specified Visual Studio solution file.

        .Description

        .PARAMETER SolutionFilePath
        The path to the Visual Studio solution file.

        .PARAMETER Configuration
        The project configuration to build within the solution file. Default is "Debug".

        .PARAMETER Platform
        The platform to build the solution file for.  Default is "Any CPU".

        .PARAMETER BuildLogFilePath
        The path to the output log. Defaults to a file in the temp folder.

        .PARAMETER OutputPath
        The override output path of MSBuild (if specified).

        .PARAMETER ShowOutputLog
        If set, this switch withh show the build log in the default viewer.

        .PARAMETER CleanFirst
        If set, this switch will cause the function to first run MsBuild as a "clean" operation, before executing the build.

        .EXAMPLE

        .LINK
        http://stackoverflow.com/questions/2560652/
        http://geekswithblogs.net/dwdii/archive/2011/05/27/part-2-automating-a-visual-studio-build-with-powershell.aspx

        .NOTES
        Name:   Build-VisualStudioSolution
        Author: Daniel Dittenhafer
        Modified By: Carl Scarlett
    #>
}