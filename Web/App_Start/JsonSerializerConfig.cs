using Newtonsoft.Json;
using Notifications.Web.Json;
using SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Notifications.Web
{
    public static class JsonSerializerConfig
    {
        public static void RegisterConverters()
        {
            var settings = GetJsonSerializerSettings();

            GlobalConfiguration.Configuration.Formatters.Insert(0, new JsonFormatter(settings));

            //SignalR
            GlobalHost.DependencyResolver.Register(typeof(IJsonSerializer), () => new JsonNetSerializer(settings));
        }

        private static JsonSerializerSettings GetJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new MongoDBObjectIdJsonConverter());
            return settings;
        }
    }
}