using Microsoft.OpenApi.Models;
using WarehouseABPD.Services;


    public class Startup
    {
        public Startup ( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices ( IServiceCollection services )
        {
            services.AddControllers();
            services.AddSingleton<IWarehouseDbService, WarehouseDbService>();
            services.AddSingleton<IWarehouseDbService2, WarehouseDbService2>();
            services.AddSwaggerGen( c =>
             {
                 c.SwaggerDoc( "v1", new OpenApiInfo { Title = "WarehouseABPD", Version = "1.0" } );
             } );
        }

        public interface IWarehouseDbService
        {
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure ( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI( c => c.SwaggerEndpoint( "/swagger/1.0/swagger.json", "WarehouseABPD 1.0" ) );
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints( endpoints =>
             {
                 endpoints.MapControllers();
             } );
        }
}
