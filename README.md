# Provide an API that query stratis.guru

Provide a cache logic for the stratis.guru API

## Deploy on Ubuntu

```bash
apt update && apt upgrade -y
apt install git curl docker
git clone https://github.com/ToniGalmes/StratisExplorerMobile.git
cd StratisExplorerMobile
docker build . -t stratis-explorer-mobile
docker run stratis-explorer-mobile -d -p 80:80
```
