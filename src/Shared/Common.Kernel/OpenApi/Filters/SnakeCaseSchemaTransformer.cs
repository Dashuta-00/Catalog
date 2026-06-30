using Common.Kernel.Extensions;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace Common.Kernel.OpenApi.Filters;

public class SnakeCaseSchemaTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(
        OpenApiDocument document, 
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        foreach (var schema in document.Components?.Schemas?.Values!)
        {
            if (schema.Properties is null || schema.Properties.Count is 0)
            {
                continue;
            }
            
            var propertiesToRename = schema.Properties
                .Where(p => p.Key != p.Key.ToSnakeCase())
                .ToList();

            foreach (var property in propertiesToRename)
            {
                var snakeKey = property.Key.ToSnakeCase();
                if (schema.Properties.ContainsKey(snakeKey))
                {
                    continue;
                }
                
                schema.Properties[snakeKey] = property.Value;
                schema.Properties.Remove(snakeKey);
            }
        }
        
        return Task.CompletedTask;
    }
}