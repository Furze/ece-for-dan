using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MoE.ECE.Web.Bootstrap
{
    /// <summary>
    /// This class will look for readonly attributes on our models and not display them in SwaggerUi
    /// </summary>
    public class ReadOnlySchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Properties == null)
            {
                return;
            }

            foreach (var schemaProperty in schema.Properties)
            {
                var property = context.Type.GetProperty(schemaProperty.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property != null && property.GetCustomAttributes(typeof(ReadOnlyAttribute), false).SingleOrDefault() is ReadOnlyAttribute attr && attr.IsReadOnly)
                {
                    // https://github.com/swagger-api/swagger-ui/issues/3445#issuecomment-339649576
                    if (schemaProperty.Value.Reference != null)
                    {
                        schemaProperty.Value.AllOf = new List<OpenApiSchema>
                        {
                            new OpenApiSchema
                            {
                                Reference = schemaProperty.Value.Reference
                            }
                        };
                        schemaProperty.Value.Reference = null;
                    }

                    schemaProperty.Value.ReadOnly = true;
                }
            }
        }
    }
}