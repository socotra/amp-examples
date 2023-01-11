SHELL := bash
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

dr-build:
	pushd src/Socotra.VinLookup && docker build . --tag  vin-lookup-example:latest --build-arg VERSION=$(VERSION) --build-arg COMMITSHA=$(COMMIT_SHA) && popd

dr-start: dr-build dr-stop
	(docker rm vin-lookup-example || echo "Nothing to cleanup")
	docker run --rm -d --name vin-lookup-example --publish 127.0.0.1:$(LOCAL_PORT):$(LOCAL_PORT) vin-lookup-example:latest
	docker ps

dr-stop:
	(docker stop vin-lookup-example || echo "Nothing to stop")
	docker ps

dr-logs:
	docker logs vin-lookup-example