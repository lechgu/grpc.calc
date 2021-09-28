using System;
using System.Net.Http;

using Grpc.Core;
using Grid;
using Grpc.Net.Client;

namespace WinConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var channel = new Channel("calculator.grid.demanddriventech.com", 443, ChannelCredentials.SecureSsl);
            var client = new Calculator.CalculatorClient(channel);
            var reply = client.Calculate(new CalculateRequest
            {
                X = 25,
                Y = 17,
                Op = "+"
            });
            Console.WriteLine(reply.Result);
        }
    }
}