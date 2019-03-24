# Provide an API that query stratis.guru

Provide a cache logic for the stratis.guru API

## Deploy on Ubuntu

```bash
apt update && apt upgrade -y
apt install git curl docker docker.io
git clone https://github.com/clintnetwork/StratisExplorerApi.git
cd StratisExplorerApi
docker build . -t stratis-explorer-api
docker run stratis-explorer-api -d -p 80:80
```
