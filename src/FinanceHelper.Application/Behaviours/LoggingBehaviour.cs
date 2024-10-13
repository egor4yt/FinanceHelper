using System.Diagnostics;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace FinanceHelper.Application.Behaviours;

public class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestName = request.GetType().Name;
        var requestGuid = Guid.NewGuid().ToString();
        var requestDataAsJson = JsonSerializer.Serialize(request);
        if (requestDataAsJson != "{}")LogContext.PushProperty("RequestData", requestDataAsJson);
        
        LogContext.PushProperty("RequestName", requestName);
        LogContext.PushProperty("RequestId", requestGuid);
        
        TResponse response;

        try
        {
            response = await next();
        }
        finally
        {
            stopwatch.Stop();
            LogContext.PushProperty("ElapsedMilliseconds", stopwatch.ElapsedMilliseconds);
        }

        return response;
    }
}