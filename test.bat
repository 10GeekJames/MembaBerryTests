@echo off

set filter=""
set env="qa"

if NOT "%1%" == "" (
    set env=%1%
    echo Env: %env%
)
if NOT "%2%" == "" (
    set filter="Category=%2%"
    echo Filter: %filter%
)

@echo on
dotnet build
dotnet test --filter %filter% -e env=%env%

