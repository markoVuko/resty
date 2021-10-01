using Application;
using Application.Commands;
using Application.Queries;
using Implementation.Commands;
using Implementation.Queries;
using Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            //services.AddTransient<ICreateRoleCommand, EfCreateRoleCommand>();
            services.AddTransient<ICreateRoleCommand, EfCreateRoleCommand>();
            services.AddTransient<IEditRoleCommand, EfEditRoleCommand>();
            services.AddTransient<IDeleteRoleCommand, EfDeleteRoleCommand>();
            services.AddTransient<IGetRoleQuery, EfGetRoleQuery>();
            services.AddTransient<IGetRolesQuery, EfGetRolesQuery>();

            services.AddTransient<ICreateOrderCommand, EfCreateOrderCommand>();
            services.AddTransient<IEditOrderCommand, EfEditOrderCommand>();
            services.AddTransient<IDeleteOrderCommand, EfDeleteOrderCommand>();
            services.AddTransient<IGetOrderQuery, EfGetOrderQuery>();
            services.AddTransient<IGetOrdersQuery, EfGetOrdersQuery>();

            services.AddTransient<ICreateItemCommand, EfCreateItemCommand>();
            services.AddTransient<IEditItemCommand, EfEditItemCommand>();
            services.AddTransient<IDeleteItemCommand, EfDeleteItemCommand>();
            services.AddTransient<IGetItemQuery, EfGetItemQuery>();
            services.AddTransient<IGetItemsQuery, EfGetItemsQuery>();

            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<IEditUserCommand, EfEditUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IGetUserQuery, EfGetUserQuery>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();

            services.AddTransient<ICreateUserRecordCommand, EfCreateUserRecordCommand>();
            services.AddTransient<IEditUserRecordCommand, EfEditUserRecordCommand>();
            services.AddTransient<IDeleteUserRecordCommand, EfDeleteUserRecordCommand>();
            services.AddTransient<IGetUserRecordQuery, EfGetUserRecordQuery>();
            services.AddTransient<IGetUserRecordsQuery, EfGetUserRecordsQuery>();

            services.AddTransient<IGetLogQuery, EfGetLogQuery>();
            services.AddTransient<IGetLogsQuery, EfGetLogsQuery>();

            services.AddTransient<ICreateScheduleCommand, EfCreateScheduleCommand>();
            //services.AddTransient<IEditSetScheduleCommand, EfEditSetScheduleCommand>();
            services.AddTransient<IEditSetScheduleCommand, EfEditUserScheduleCommand>();
            services.AddTransient<IDeleteScheduleCommand, EfDeleteScheduleCommand>();
            services.AddTransient<IGetScheduleQuery, EfGetScheduleQuery>();
            services.AddTransient<IGetSchedulesQuery, EfGetSchedulesQuery>();

            services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
            services.AddTransient<IEditCategoryCommand, EfEditCategoryCommand>();
            services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();
            services.AddTransient<IGetCategoryQuery, EfGetCategoryQuery>();
            services.AddTransient<IGetCategoriesQuery, EfGetCategoriesQuery>();

            services.AddTransient<ICreateSupplierCommand, EfCreateSupplierCommand>();
            services.AddTransient<IEditSupplierCommand, EfEditSupplierCommand>();
            services.AddTransient<IDeleteSupplierCommand, EfDeleteSupplierCommand>();
            services.AddTransient<IGetSupplierQuery, EfGetSupplierQuery>();
            services.AddTransient<IGetSuppliersQuery, EfGetSuppliersQuery>();

            services.AddTransient<ICreateRecordTypeCommand, EfCreateRecordTypeCommand>();
            services.AddTransient<IEditRecordTypeCommand, EfEditRecordTypeCommand>();
            services.AddTransient<IDeleteRecordTypeCommand, EfDeleteRecordTypeCommand>();
            services.AddTransient<IGetRecordTypeQuery, EfGetRecordTypeQuery>();
            services.AddTransient<IGetRecordTypesQuery, EfGetRecordTypesQuery>();


            services.AddTransient<ICreateWorkTypeCommand, EfCreateWorkTypeCommand>();
            services.AddTransient<IEditWorkTypeComand, EfEditWorkTypeCommand>();
            services.AddTransient<IDeleteWorkTypeCommand, EfDeleteWorkTypeCommand>();
            services.AddTransient<IGetWorkTypeQuery, EfGetWorkTypeQuery>();
            services.AddTransient<IGetWorkTypesQuery, EfGetWorkTypesQuery>();
        }

        public static void AddValidators(this IServiceCollection services)
        {
            //services.AddTransient<CreateRoleValidator>();
            services.AddTransient<CreateWorkTypeValidator>();
            services.AddTransient<CreateSupplierValidator>();
            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<CreateRecordTypeValidator>();
            services.AddTransient<CreateOrderValidator>();
            services.AddTransient<CreateUserRecordValidator>();
            services.AddTransient<CreateRoleValidator>();
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<CreateItemValidator>();
            services.AddTransient<CreateScheduleValidator>();
            services.AddTransient<EditItemValidator>();
            services.AddTransient<EditUserValidator>();
            services.AddTransient<EditSupplierValidator>();
            services.AddTransient<EditRoleValidator>();
            services.AddTransient<EditSetScheduleValidator>();
            services.AddTransient<EditCategoryValidator>();
            services.AddTransient<EditRecordTypeValidator>();
            services.AddTransient<EditWorkTypeValidator>();
        }

        public static void AddJwtActor(this IServiceCollection services)
        {
            services.AddTransient((Func<IServiceProvider, IAppActor>)(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();

                var user = accessor.HttpContext.User;

                if (user.FindFirst("ActorData") == null)
                {
                    //throw new InvalidOperationException("Actor data doesnt exist in token.");
                    return new GuestActor();
                }

                var actorString = user.FindFirst("ActorData").Value;

                var actor = JsonConvert.DeserializeObject<Core.JwtActor>(actorString);

                return actor;

            }));
        }

        public static void AddJwtAuth(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "asp_api",
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMyVerySecretKey")),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void AddSwaggerExt(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Zavrsni Rad 204/17", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                    });
            });
        }
    }
}
