using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Client
{
    public class RetryInterceptor : Interceptor
    {
        public int MaxRetries { get; set; } = 5;
        public int RetryDelay { get; set; } = 20;
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation
        )
        {
            var retryCount = 0;
            async Task<TResponse> RetryCallback(Task<TResponse> responseTask)
            {
                var response = responseTask;
                if (!response.IsFaulted)
                {
                    return response.Result;
                }

                retryCount++;

                if (retryCount == MaxRetries)
                {
                    return response.Result;
                }
                await Task.Delay(RetryDelay);
                var result = continuation(request, context).ResponseAsync.ContinueWith(RetryCallback).Unwrap();
                return result.Result;
            }

            var responseContinuation = continuation(request, context);
            var responseAsync = responseContinuation.ResponseAsync.ContinueWith(RetryCallback);

            return new AsyncUnaryCall<TResponse>(
                responseAsync.Result,
                responseContinuation.ResponseHeadersAsync,
                responseContinuation.GetStatus,
                responseContinuation.GetTrailers,
                responseContinuation.Dispose
                );
        }
    }
}