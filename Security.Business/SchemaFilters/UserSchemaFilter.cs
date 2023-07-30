using Microsoft.OpenApi.Models;
using Security.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Security.Business
{
    public class UserSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Excludes id field from Swagger schema for User
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(User))
            {
                var excludedProperties = new[] { "id" };
                foreach (var property in schema.Properties.ToList())
                {
                    if (excludedProperties.Contains(property.Key))
                    {
                        schema.Properties.Remove(property.Key);
                    }
                }
            }
        }
    }
}
