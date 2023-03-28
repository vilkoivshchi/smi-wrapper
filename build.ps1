dotnet publish -r linux-x64 /p:ShowLinkerSizeComparison=true,IncludeNativeLibrariesForSelfExtract=true,Configuration=Debug,Platform="Any CPU",PublishReadyToRun=false,PublishTrimmed=True,TrimMode=CopyUsed --self-contained true

$archiveName = Split-path . -Leaf
$archiveName += Get-Date -Format "yyyyMMdd-HHmm"
$archiveName += ".zip"

if ($LastExitCode -eq 0)
{
    rm -Recurse ".\smi-wrapper\bin\Debug\net6.0\linux-x64\*.*"
    &"C:\Program Files\7-Zip\7z.exe" a $archiveName -r ".\smi-wrapper\bin\Debug\net6.0\linux-x64"
    rm -Recurse .\smi-wrapper\bin\
}
pause