#!/bin/bash
set -e

usage() { echo "Usage: ./build.sh [--help] [--zip-file <file>] [--version-file <file>]" 1>&2; exit 1; }

OPTIONS=$(getopt -o '' -l zip-file:,version-file:,help -- "$@")

eval set -- "${OPTIONS}"

while [ $# -gt 0 ]
do
  case $1 in
    --zip-file) ZIP_FILE=$2; shift;;
    --version-file) VERSION_FILE=$2; shift;;
    --help) usage;;
    (--) shift; break;;
    (*) break;;
  esac
  shift
done

VERSION=$(gitversion /showvariable MajorMinorPatch).${GO_PIPELINE_COUNTER:-"0-"$(gitversion /showvariable PreReleaseNumber)}
ASSEMBLY_VERSION=$(echo $VERSION | cut -d- -f1)

dotnet restore ./ChristmasMothers.sln --source https://artifactory.com/artifactory/api/nuget/nuget && \
  dotnet publish ./ChristmasMothers.Web.Application/ChristmasMothers.Web.Application.csproj -c Release -o ../publish -p:Version=$ASSEMBLY_VERSION

if [ "x$ZIP_FILE" != "x" ]; then
  zip -r $ZIP_FILE publish/ ChristmasMothers.Web.Application/Dockerfile docker-build.sh
fi

if [ "x$VERSION_FILE" != "x" ]; then
  echo $VERSION > $VERSION_FILE
fi

SQLSCRIPT=../../../ChristmasMothers-sql-server-scripts.zip
ORACLESCRIPT=../../../ChristmasMothers-oracle-scripts.zip

cd database-scripts/sql-server/integrated
zip -r $SQLSCRIPT *
cd ../../../
cd database-scripts/oracle/integrated
zip -r $ORACLESCRIPT *
cd ../../../

