using ContactMe.Models;
using ContactMe.Webhooks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Oak.Events;
using Oak.Webhooks;
using Oak.Email;
using ContactMe.Email;
using Oak.ContactMe.Models;

namespace ContactMe
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ContactMe", Version = "v1" });
            });

            services.AddOakWebhooks();
            services.AddOakEventDispatcher((config) => 
            {
                config.Mode = EventDispatcherMode.Ambiguous;
            });

            services.Configure<HookOptions>(this.Configuration.GetSection("Hooks"));

            services.AddAsyncEvent<DiscordWebhook, ContactMeEvent>();
            services.AddWebhook<DiscordWebhook, DiscordWebhookRequest>();

            services.AddMailKitClient(
                this.Configuration.GetSection("Email"), 
                this.Configuration.GetSection("Smtp"));
            services.AddAsyncEvent<EmailEventHandler, ContactMeEvent>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactMe v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
