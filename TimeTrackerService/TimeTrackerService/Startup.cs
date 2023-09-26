using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Supabase;
using System.Text.Json.Serialization;
using TimeTrackerService.Db;
using TimeTrackerService.Models;
using TimeTrackerService.Repository;
using TimeTrackerService.Repository.Interfaces;
using TimeTrackerService.Services.Implementations;
using TimeTrackerService.Services.Interfaces;
using Task = TimeTrackerService.Models.Task;

namespace TimeTrackerService
{
    public class Startup
    {
        private readonly string _cors = "cors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddJsonOptions(options =>
                                     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TimeTrackerService", Version = "v1" });
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                //mc.AddProfile(new UddMappingProfile());
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: _cors, builder => {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();
            

            // Note the creation as a singleton.
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }); 
            services.AddScoped<ITasksService, TasksService>();
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddScoped(typeof(IRepository<Task>), typeof(TaskRepository));
            services.AddScoped(typeof(IRepository<Project>), typeof(ProjectsRepository));


            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HiringAgency v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors(_cors);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
