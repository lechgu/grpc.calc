import sys

import grpc
import calculator_pb2
import calculator_pb2_grpc

host = sys.argv[1]
port = sys.argv[2]
channel = grpc.secure_channel(f"{host}:{port}", grpc.ssl_channel_credentials())

req = calculator_pb2.CalculateRequest(x=17, y=25, op="+")
client = calculator_pb2_grpc.CalculatorStub(channel)
reply = client.Calculate(req)
print(reply.result)
