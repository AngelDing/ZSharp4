using System.Web.Http;
using WebActivatorEx;
using Swashbuckle.Application;
using System.Linq;
using System;
using System.Reflection;
using System.IO;

[assembly: PreApplicationStartMethod(typeof(ZSharp.Demo.WebApi.SwaggerConfig), "Register")]

namespace ZSharp.Demo.WebApi
{
    /// <summary>
    /// Swaggerœ‡πÿ≈‰÷√
    /// </summary>
    public class SwaggerConfig
    {
        private static string GetBaseDirectory()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            return baseDirectory;
        }

        private static string GetXmlDocPathByAssembly(Assembly assembly)
        {
            return Path.Combine(GetBaseDirectory(), "bin", assembly.GetName().Name + ".XML");
        }

        /// <summary>
        /// Swagger≈‰÷√◊¢≤·
        /// </summary>
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "ZSharp.Demo.WebApi");
                        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                        c.IncludeXmlComments(GetXmlDocPathByAssembly(Assembly.GetExecutingAssembly()));
                    })
                .EnableSwaggerUi(c => { });
        }
    }
}
