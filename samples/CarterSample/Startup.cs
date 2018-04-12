namespace CarterSample
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Carter;
    using CarterSample.Features.Actors;
    using HtmlNegotiator;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IActorProvider, ActorProvider>();

            //var mapper = new DefaultTypeViewMapper(new Dictionary<Type, string> { { typeof(Actor), "test.html" } });

            //var mapper = new FixedMapper();

            services.AddSingleton<IViewLocator>(new DefaultViewLocator(new Dictionary<Type, string> { { typeof(Actor), "testpoo.html" } }));

            services.AddCarter();
        }

        public void Configure(IApplicationBuilder app, IConfiguration config)
        {
            var appconfig = new AppConfiguration();
            config.Bind(appconfig);

            app.UseExceptionHandler("/errorhandler");

            app.UseCarter(this.GetOptions());
        }

        private CarterOptions GetOptions()
        {
            return new CarterOptions(ctx => this.GetBeforeHook(ctx), ctx => this.GetAfterHook(ctx));
        }

        private Task<bool> GetBeforeHook(HttpContext ctx)
        {
            ctx.Request.Headers["HOWDY"] = "FOLKS";
            return Task.FromResult(true);
        }

        private Task GetAfterHook(HttpContext ctx)
        {
            Console.WriteLine("We hit a route!");
            return Task.CompletedTask;
        }
    }
}
