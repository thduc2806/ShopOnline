namespace Admin_site.Helper
{
    public class DependencyInjectionHelper
    {
        internal static IServiceProvider _serviceProvider;
        public static void Init(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.CreateScope().ServiceProvider;
        }
        public static T GetService<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
