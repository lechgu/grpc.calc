FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /app

RUN wget -q -O /etc/apk/keys/sgerrand.rsa.pub https://alpine-pkgs.sgerrand.com/sgerrand.rsa.pub \
    && wget https://github.com/sgerrand/alpine-pkg-glibc/releases/download/2.34-r0/glibc-2.34-r0.apk \
    && apk add glibc-2.34-r0.apk


COPY Service/ ./Service
COPY calculator.proto .
RUN dotnet publish Service -c release -r linux-musl-x64 -p:PublishSingleFile=true -p:PublishReadyToRun=true -p:PublishTrimmed=true 

FROM mcr.microsoft.com/dotnet/runtime-deps:5.0-alpine
COPY --from=build /app/Service/bin/release/net5.0/linux-musl-x64/publish/Service /usr/local/bin/calculator

ENV PORT=80
ENV Logging__LogLevel__Microsoft=Information

CMD [ "calculator" ]
