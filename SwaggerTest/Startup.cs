using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace SwaggerTest
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
            services.AddCors(t=>
                t.AddPolicy("anypolicy",
                    s=>s.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin())
                );

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    //设置json 序列化字符串首字母不以小写形式返回
                    .AddJsonOptions(options =>
                        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());

            //注册Swagger生成器，定义一个和多个Swagger 文档
            services.AddSwaggerGen(w =>
            {
                w.SwaggerDoc("testv1", new Info()
                {
                    Title = "test title",
                    Version = "version1",
                    Description = "swagger test description",
                    TermsOfService = "服务条款",
                    //作者
                    Contact = new Contact()
                    {
                        Name = "fang",
                        Url = "http://api.fang.com",
                        Email = "wjf@fang.com"
                    },
                    //许可证
                    License = new License()
                    {
                        Name = "许可证名称",
                        Url = "http://license.fang.com"
                    }
                });

                 // 设置SWAGER JSON和UI的注释路径。
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                w.IncludeXmlComments(xmlPath);
            });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseCors(s=>
                    s.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    );

            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();

            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/testv1/swagger.json", "api v1");
                //默认在http://localhost:<port>/swagger 查看api文档
                //要在应用的根 (http://localhost:<port>/) 处提供 Swagger UI，请将 RoutePrefix 属性设置为空字符串
                s.RoutePrefix = string.Empty;               
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
