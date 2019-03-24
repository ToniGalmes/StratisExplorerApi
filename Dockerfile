FROM microsoft/dotnet:2.2-sdk

RUN git clone https://github.com/clintnetwork/StratisExplorerApi.git \
    && cd /StratisExplorerApi/ \
	&& dotnet build

WORKDIR /StratisExplorerApi/

EXPOSE 5000

CMD ["dotnet", "run"]