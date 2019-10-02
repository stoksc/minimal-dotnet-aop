using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using attr.Attributes;

namespace attr.Interceptors
{
    [Serializable]
    public class LogCallInterceptor : IInterceptor
    {
        private ILogger<LogCallInterceptor> _logger;

        public LogCallInterceptor(ILogger<LogCallInterceptor> logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            var attribute = (LogCallAttribute) invocation.Method.GetCustomAttributes(true).FirstOrDefault(a => a.GetType() == typeof(LogCallAttribute));

            if (attribute != null)
            {
                _logger.LogWarning($"{attribute.LogDecoration} calling {invocation.Method}");
            }

            invocation.Proceed();
        }
    }
}