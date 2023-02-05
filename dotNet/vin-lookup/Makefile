.ONESHELL:
.SHELLFLAGS := -eu -o pipefail -c
.PHONY:

.DEFAULT_GOAL := build


LOCAL_PORT = 10101
COMMIT_SHA = $(shell git rev-parse --short HEAD)
VERSION = 0.0.1

all : clean restore build publish

clean:
	dotnet clean

restore:
	dotnet restore

build: 
	dotnet build

publish:
	dotnet publish -c Release -r linux-x64
	warp-packer --arch linux-x64 --input_dir bin/Release/netcoreapp2.1/linux-x64/publish --exec made --output made

run:
	dotnet run

publish-app:
	pushd ./src/Socotra.VinLookup
	socotra-app version minor
	socotra-app publish docker vin-lookup-example:latest
	popd

dr-build-marketplace:
	pushd ./src/Socotra.VinLookup
	docker build . --tag  vin-lookup-example:latest --build-arg VERSION=$(VERSION) --build-arg COMMITSHA=$(COMMIT_SHA) --build-arg arch=amd64
	popd

dr-build-locally:
	pushd src/Socotra.VinLookup
	docker build . --tag  vin-lookup-example:latest --build-arg VERSION=$(VERSION) --build-arg COMMITSHA=$(COMMIT_SHA) --build-arg arch=arm64
	popd

dr-start-local: dr-build-locally dr-stop
	(docker rm vin-lookup-example || echo "Nothing to cleanup")
	docker run --rm -d --name vin-lookup-example --publish 127.0.0.1:$(LOCAL_PORT):$(LOCAL_PORT) vin-lookup-example:latest
	docker ps

dr-start-marketplace: dr-build-marketplace dr-stop
	(docker rm vin-lookup-example || echo "Nothing to cleanup")
	docker run --rm -d --name vin-lookup-example --publish 127.0.0.1:$(LOCAL_PORT):$(LOCAL_PORT) vin-lookup-example:latest
	docker ps

dr-stop:
	(docker stop vin-lookup-example || echo "Nothing to stop")
	docker ps

dr-logs:
	docker logs vin-lookup-example