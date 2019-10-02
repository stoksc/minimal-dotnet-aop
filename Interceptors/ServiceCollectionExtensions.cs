using System;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace attr.Interceptors
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddScopedInterceptedBy<TService, TImplementation, TInterceptor>(
            this IServiceCollection services, 
            Func<IServiceProvider, object[]> getConstructorArguments
        )
            where TService : class
            where TImplementation : class, TService
            where TInterceptor : IInterceptor
        {
            services.AddScoped<TService, TImplementation>(x => 
                (TImplementation) new ProxyGenerator().CreateClassProxy(
                    typeof(TImplementation),
                    getConstructorArguments(x),
                    x.GetService<TInterceptor>()
                ));
            return services;
        }
    }
}