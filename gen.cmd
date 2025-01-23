@echo off
pushd
@setlocal
set ERROR_CODE=0

REM From Alec Mev https://superuser.com/questions/35698/how-to-supress-terminate-batch-job-y-n-confirmation/715798#715798
IF [%JUSTTERMINATE%] == [OKAY] (
    SET JUSTTERMINATE=
    src\GambitSharp.Generator\bin\Release\net8.0\SharpGambit.Generator.exe %*
) ELSE (
    SET JUSTTERMINATE=OKAY
    CALL %0 %* <NUL
)

:end
@endlocal
popd
exit /B %ERROR_CODE%