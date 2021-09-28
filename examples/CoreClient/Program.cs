using System;
using Grid;
using Grpc.Net.Client;

namespace Client2
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress(args[0]);
            var client = new Calculator.CalculatorClient(channel);
            var reply = await client.CalculateAsync(
                new CalculateRequest
                {
                    X = 25,
                    Y = 17,
                    Op = "+"
                }
            );
            Console.WriteLine(reply.Result);
        }
    }
}
