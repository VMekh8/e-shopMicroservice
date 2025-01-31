using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlock.BehaviorPipeline;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) :
    IPipelineBehavior<TRequest, TResponse>
    where
    TRequest : notnull, IRequest<TResponse>
    where
    TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("START: Handle Request={1} - Response={2} - RequestData={3}", 
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();
        
        var response = await next();

        timer.Stop();
        var timeElapsed = timer.Elapsed.Seconds;
        if (timeElapsed > 3)
        {
            logger.LogInformation("PERFORMANCE: Request={Request} took {TimeTaken}", 
                typeof(TRequest).Name, timeElapsed);
        }

        logger.LogInformation("END: Handled {Request} with {Response}",
            typeof(TRequest).Name, typeof(TResponse).Name);

        return response;
    }
}