cd "F:\Programs\Workspaces\Visual Studio\Projects\SodaDungeon2Tool\SodaDungeon2Tool\"
set /p Build=<latestReleaseVersion.txt
echo "F:\Programs\Workspaces\Visual Studio\Projects\SodaDungeon2Tool\SodaDungeon2Tool\"
echo %Build%
if "%Build%" == "0.0.3.0" (
    echo WRONG assembly version!
	exit 1
)
echo 0.0.3.0>latestReleaseVersion.txt"
