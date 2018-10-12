FROM microsoft/aspnetcore:2.0
COPY publish/ /app/
WORKDIR /app
EXPOSE 80
ENTRYPOINT ["dotnet", "ChristmasMothers.Web.Application.dll"]
HEALTHCHECK CMD curl --fail http://localhost:80/api/health || exit 1