using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Web.Json
{
    public class JsonFormatter : MediaTypeFormatter
    {
        private Encoding _encoding;
        private JsonSerializerSettings _settings;

        public JsonFormatter(JsonSerializerSettings settings)
        {
            _settings = settings;
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            _encoding = new UTF8Encoding(false, true);
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContentHeaders contentHeaders, IFormatterLogger formatterLogger)
        {
            JsonSerializer serializer = JsonSerializer.Create(_settings);

            return Task.Factory.StartNew(() =>
            {
                using (StreamReader streamReader = new StreamReader(stream, _encoding))
                {
                    using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
                    {
                        return serializer.Deserialize(jsonTextReader, type);
                    }
                }
            }); 
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContentHeaders contentHeaders, TransportContext transportContext)
        {
            JsonSerializer serializer = JsonSerializer.Create(_settings);

            return Task.Factory.StartNew(() =>
            {
                using (StreamWriter streamWriter = new StreamWriter(stream, _encoding))
                {
                    using (JsonTextWriter jsonTextWriter = new JsonTextWriter(streamWriter))
                    {
                        serializer.Serialize(jsonTextWriter, value);
                    }
                }
            }); 
        }
    }
}
