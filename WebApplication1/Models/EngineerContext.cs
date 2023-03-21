using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebApplication1.Models
{
    public class EngineerContext
    {
        private static IServiceProvider _serviceProvider;

        public static void Init(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static T GetService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
