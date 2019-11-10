# Runtime
FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /publish
COPY --from=build-env /publish .

ENTRYPOINT ["dotnet", "Y.Web.Host.dll"]
