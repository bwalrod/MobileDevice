using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using MobileDevice.API.Models;
using MobileDevice.API.Data;
using MobileDevice.API.Helpers;
using MobileDevice.API.Data.Department;
using MobileDevice.API.Data.SimCard;
using MobileDevice.API.Data.AppUser;
using MobileDevice.API.Data.Assignee;
using MobileDevice.API.Data.Device;
using MobileDevice.API.Data.DeviceAttribute;
using MobileDevice.API.Data.DeviceAttributeType;
using MobileDevice.API.Data.DeviceDate;
using MobileDevice.API.Data.DeviceDateType;
using MobileDevice.API.Data.DeviceNote;
using MobileDevice.API.Data.DeviceStatus;
using MobileDevice.API.Data.Product;
using MobileDevice.API.Data.ProductCapacity;
using MobileDevice.API.Data.ProductManufacturer;
using MobileDevice.API.Data.ProductModel;
using MobileDevice.API.Data.ProductType;
using MobileDevice.API.Data.Assignment;

namespace MobileDevice.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddDbContext<DataContext>
            services.AddDbContext<DataContext>(e => e.EnableSensitiveDataLogging().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opt => {
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            services.AddCors();
            services.AddAutoMapper();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IAssigneeRepository, AssigneeRepository>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IAuthority, Authority>();
            services.AddScoped<IDeviceNoteRepository, DeviceNoteRepository>();
            services.AddScoped<IDeviceDateRepository, DeviceDateRepository>();
            services.AddScoped<IDeviceAttributeRepository, DeviceAttributeRepository>();
            services.AddScoped<IDeviceDateTypeRepository, DeviceDateTypeRepository>();
            services.AddScoped<IDeviceAttributeTypeRepository, DeviceAttributeTypeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<ISimCardRepository, SimCardRepository>();
            services.AddScoped<IDeviceStatusRepository, DeviceStatusRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductCapacityRepository, ProductCapacityRepository>();
            services.AddScoped<IProductManufacturerRepository, ProductManufacturerRepository>();
            services.AddScoped<IProductModelRepository, ProductModelRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder => {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });                
                // app.UseHsts();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
