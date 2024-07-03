using System.Diagnostics;
using MediatR;
using Serilog;

namespace FinanceHelper.Application.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestName = request.GetType().Name;
        var requestGuid = Guid.NewGuid().ToString();
        Log.Information("Handling {@RequestName} {@RequestId} {@RequestData}", requestName, requestGuid, request);

        TResponse response;
        
        try
        {
            response = await next();
        }
        finally
        {
            stopwatch.Stop();
            Log.Information("Handled {@RequestName} {RequestId}, Execution time={@ElapsedMilliseconds} ms", typeof(TRequest).Name, requestGuid, stopwatch.ElapsedMilliseconds);
        }

        return response;
    }
}