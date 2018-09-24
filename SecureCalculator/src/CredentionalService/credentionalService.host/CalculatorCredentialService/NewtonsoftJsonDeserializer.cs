using System.IO;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;

namespace CalculatorCredentialService
{
    public class NewtonsoftJsonDeserializer : IDeserializer
    {
        #region IDeserializer members

        public T Deserialize<T>(IRestResponse response)
        {
            var content = response.Content;

            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return new JsonSerializer().Deserialize<T>(jsonTextReader);
                }
            }
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }

        #endregion
    }
}