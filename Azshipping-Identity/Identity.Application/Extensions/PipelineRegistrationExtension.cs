using Identity.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MrStyx.Application.Behaviors;

namespace Identity.Application.Extensions;

public static class PipelineRegistrationExtension
{
    public static IServiceCollection AddPipelines(this IServiceCollection services)
    {
        // Outermost: captures validation, transaction, and handler failures with full exception chain.
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(MediatorExceptionLoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        return services;
    }
}
