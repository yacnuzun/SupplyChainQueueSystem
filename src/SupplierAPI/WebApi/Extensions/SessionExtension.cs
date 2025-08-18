using Newtonsoft.Json;

namespace SupplierAPI.WebApi.Extensions
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection ContainerServiceCreate(IServiceCollection serviceCollection)
        {
            ServiceProvider = serviceCollection.BuildServiceProvider();
            return serviceCollection;
        }
    }
    public static class SessionExtension
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            string jsonValue = JsonConvert.SerializeObject(value);
            session.SetString(key, jsonValue);
        }
        public static T GetObject<T>(this ISession session, string key) where T : class, new()
        {
            T response = new T();

            string jsonValue = session.GetString(key);
            if (string.IsNullOrEmpty(jsonValue))
            {
                return response;
            }
            response = JsonConvert.DeserializeObject<T>(jsonValue);
            return response;
        }
    }
}
