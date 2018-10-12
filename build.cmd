dotnet restore ./ChristmasMothers.sln
dotnet publish ./ChristmasMothers.Web.Application/ChristmasMothers.Web.Application.csproj -c Release -o ../publish

docker build --no-cache --tag ChristmasMothers-core --build-arg source=publish/ --file ChristmasMothers.Web.Application/Dockerfile .
