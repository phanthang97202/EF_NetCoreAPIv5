using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyASP_.NET_WebAPI_5.Data;
using MyASP_.NET_WebAPI_5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MyASP_.NET_WebAPI_5
{
    public class Startup
    {
        public Startup(IConfiguration configuration) // nó lấy ra bản sao của IConfiguration nó sẽ được inject vào hàm khởi tạo Start up thông Dependence Injection
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; } // chúng ta có thể truy cập thuộc tính Configuration từ bất kì đâu trong lớp Start Up nhưng chi có thể lấy ra để ĐỌC

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(); // thêm controller vào container cho phép sử dụng container để xử lý các yêu cầu HTTP


            services.AddDbContext<MyDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("MyDB"));
            });

            //đăng kí dependence injection 
            services.AddScoped<ILoaiRepository, LoaiRepository>();

            //thêm dịch vụ Swagger vào container. Dịch vụ này được cấu hình để tạo tài liệu API sử dụng Swagger UI.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyASP_.NET_WebAPI_5", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // check lỗi trong quá trình phát triển code
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyASP_.NET_WebAPI_5 v1"));
            }

            app.UseHttpsRedirection(); //  chuyển hướng các yêu cầu HTTP sang HTTPS. 

            //Middleware này định tuyến yêu cầu đến các xử lý tương ứng. 
            //Nó xác định xem yêu cầu nào đi đến đâu dựa trên các định tuyến đã được cấu hình, 
            //    chẳng hạn như các attribute routing trong controllers.
           
            app.UseRouting();
            
            app.UseAuthorization();  // Middleware này được sử dụng để kích hoạt xác thực và ủy quyền trong ứng dụng

            //Middleware này định tuyến yêu cầu đến các controllers trong ứng dụng. 
            //Nó kết nối yêu cầu với các phương thức trong controllers tương ứng để xử lý yêu cầu và trả về phản hồi.
            //Phương thức MapControllers() xác định các endpoint cho controllers được sử dụng trong ứng dụng.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
