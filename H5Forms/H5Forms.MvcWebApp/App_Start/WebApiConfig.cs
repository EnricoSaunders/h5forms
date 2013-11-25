using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace H5Forms.MvcWebApp
{
    public static class WebApiConfig
    {
        public static JsonSerializerSettings JsonSerializerSettings { get; private set; }

        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            // Set camelCase JSON serialization as default.
            JsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
#if DEBUG
                    Formatting = Formatting.Indented,
#endif
                };

            var index = config.Formatters.IndexOf(config.Formatters.JsonFormatter);
            config.Formatters[index] = new JsonMediaTypeFormatter
            {
                SerializerSettings = JsonSerializerSettings
            };
        }
    }
}
