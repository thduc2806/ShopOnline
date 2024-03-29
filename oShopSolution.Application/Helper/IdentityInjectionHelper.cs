﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Helper
{
    public class IdentityInjectionHelper
    {
        private static string _baseUrl = string.Empty;
        public static void SetBaseUrl(string baseUrl) => _baseUrl = baseUrl;
        public static string GetBaseUrl() => _baseUrl;
        private static IServiceCollection _serviceCollection;

        public static void Init(ref IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public static T GetService<T>() where T : class
            => (_serviceCollection != null) ? _serviceCollection.BuildServiceProvider().GetRequiredService<T>() : null;
    }
}
