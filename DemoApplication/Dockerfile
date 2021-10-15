# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
EXPOSE 80

COPY publish/ ./
ENTRYPOINT ["dotnet", "DemoApplication.dll"]
