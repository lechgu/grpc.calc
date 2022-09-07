using Grid;
using Grpc.Core;
  

namespace AzureApp
{
    public class CalculatorService: Calculator.CalculatorBase
    {
        public override Task<CalculateReply> Calculate(CalculateRequest request, ServerCallContext context)
        {
            long result = -1;
            switch (request.Op)
            {
                case "+":
                    result = request.X + request.Y;
                    break;
                case "-":
                    result = request.X - request.Y;
                    break;
                case "*":
                    result = request.X * request.Y;
                    break;
                case "/":
                    if (request.Y != 0)
                    {
                        result = request.X / request.Y;
                    }
                    break;
                default:
                    break;
            }

            var reply = new CalculateReply
            {
                Result = result
            };
            return Task.FromResult(reply);
        }
    }
}
