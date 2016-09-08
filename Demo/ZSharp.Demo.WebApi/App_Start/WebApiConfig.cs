using Newtonsoft.Json;
using System.Web.Http;
using ZSharp.Framework.Web.Api;

namespace ZSharp.Demo.WebApi
{
    /// <summary>
    /// Api配置初始化
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Api配置注册
        /// </summary>
        /// <param name="config">配置信息</param>
        public static void Register(HttpConfiguration config)
        {
             

            var xml = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
            xml.UseXmlSerializer = true;
            //// 解决json序列化时的循环引用问题(如果使用的是默認的Newtonsoft.JsonFormatter格式器)
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            json.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            json.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            ////配置http請求超時為3分鐘
            //FlurlHttp.GlobalSettings.DefaultTimeout = TimeSpan.FromSeconds(100);
            ////為前端啟動敏感字符替換
            //SecurityJsonConverter.EnableSecurityConvert = true;

            ////針對所有訪問啟用壓縮
            //config.MessageHandlers.Add(new EncodingDelegateHandler());

            ////允許跨域訪問
            //config.SetCorsPolicyProviderFactory(new CorsPolicyFactory());
            //config.EnableCors();

            //// There can be multiple exception loggers. (By default, no exception loggers are registered.)
            //config.Services.Add(typeof(IExceptionLogger), new CustomExceptionLogger());

            //// There must be exactly one exception handler. 
            //// To make this sample easier to run in a browser, replace the default exception handler with one that sends
            //// back text/plain content for all errors.
            //config.Services.Replace(typeof(IExceptionHandler), new GenericTextExceptionHandler());

            // Web API 配置和服务
            config.DependencyResolver = new UnitySharedDependencyResolver(UnityConfig.GetConfiguredContainer());

            ////性能計數器
            //PerfCounterAttribute.EnablePerfCounter = ConfigHelper.EnablePerfCounter;
            //PerfCounterAttribute.CollectorTypeEnum = CollectorType.CtripSZ;
            //var perfFilter = new PerfCounterAttribute() { Source = "GroupTour", SystemCode = "GroupTour.OfflineBooking.WebApi" };
            //config.Filters.Add(perfFilter);

            // Attribute routing
            config.MapHttpAttributeRoutes();

            // Convention-based routing
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //MongoInitHelper.InitMongoDBRepository();
            //CustomClassMap.InitMongoDBRepository();
        }
    }
}
