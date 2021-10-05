# IntuiFlow.Calculator

This is typical gRPC service and its clients implementation.

- `calculator.proto`: proto file
- `Service`: service implementation with Grpc.AspNetCore
- `Dockerfile`: for hosting the service inside Docker container
- `k8s`: kubernetes templates for the deployment, service, ingress and cert-manager
- `examples/CoreClient`: client example for .net core
- `examples/WinConsole`: .net framework client
- `examples/WebApp`: Azure Web App calling the service
- `examples/python`: python client example
