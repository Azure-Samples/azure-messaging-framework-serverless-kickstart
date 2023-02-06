using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit.Sdk;

namespace Microsoft.TestFramework
{
    /// <summary>
    /// This class is an extended version of the InlineDataAttribute. It allows to load the data from a json file.
    /// </summary>
    public class ComplexLoadInlineDataAttribute : DataAttribute
    {
        private readonly string filePath = String.Empty; 

        public ComplexLoadInlineDataAttribute(string filePath)
        {
            this.filePath = filePath;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null)
            {
                throw new ArgumentNullException(nameof(testMethod));
            }

            var parameters = testMethod.GetParameters();

            var path = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            if (!File.Exists(path))
            {
                throw new ArgumentException($"There is no such file in: {path}");
            }

            var fileContent = File.ReadAllText(filePath);
            return GetData(fileContent, parameters);
        }

        private IEnumerable<object[]> GetData(string fileContent, ParameterInfo[] parameters)
        {
            var objectList = new List<object>();

            var jObject = JObject.Parse(fileContent);

            var jsonSerializerSettings = new JsonSerializerSettings
                                            {
                                                DateFormatString = "yyyy-MM-ddThh:mm:ss.fffZ"
                                            };
            
            foreach( ParameterInfo parameter in parameters )
            {
                var propertyValue = jObject.GetValue(parameter.Name!);
                var paramValue = JsonConvert.DeserializeObject(
                    JsonConvert.SerializeObject(propertyValue!, jsonSerializerSettings),
                    parameter.ParameterType
                    );
                objectList.Add( paramValue! );
            }
            
            yield return objectList.ToArray();
        }
    }
}
