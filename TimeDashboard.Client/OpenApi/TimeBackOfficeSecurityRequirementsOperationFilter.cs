﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using Umbraco.Extensions;

namespace TimeDashboard.Client.OpenApi;
internal class TimeBackOfficeSecurityRequirementsOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.MethodInfo.HasMapToApiAttribute("time") == false)
        {
            return;
        }

        if (!context.MethodInfo.GetCustomAttributes(true).Any(x => x is AllowAnonymousAttribute) &&
            !(context.MethodInfo.DeclaringType?.GetCustomAttributes(true).Any(x => x is AllowAnonymousAttribute) ?? false))
        {
            operation.Responses.Add(StatusCodes.Status401Unauthorized.ToString(), new OpenApiResponse
            {
                Description = "The resource is protected and requires an authentication token"
            });

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "time"
                            }
                        }, new string[] { }
                    }
                }
            };
        }


        // If method/controller has an explicit AuthorizeAttribute or the controller ctor injects IAuthorizationService, then we know Forbid result is possible.
        if (context.MethodInfo.GetCustomAttributes(false).Any(x => x is AuthorizeAttribute
            || context.MethodInfo.DeclaringType?.GetConstructors().Any(x => x.GetParameters().Any(x => x.ParameterType == typeof(IAuthorizationService))) is true))
        {
            operation.Responses.Add(StatusCodes.Status403Forbidden.ToString(), new OpenApiResponse
            {
                Description = "The authenticated user do not have access to this resource"
            });
        }
    }
}

