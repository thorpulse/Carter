namespace CarterSample.Features.Home
{
    using System;
    using System.Threading.Tasks;
    using Carter;
    using Microsoft.AspNetCore.Http;

    public class HomeModule : CarterModule
    {
        public HomeModule()
        {
            this.Get("/", async (req, res, routeData) =>
            {
                res.StatusCode = 409;
                await res.WriteAsync("There's no place like 127.0.0.1");
            });

            this.Get<ListById>("/products", async context => await context.Response.WriteAsync("pets"));

            this.After = (ctx) =>
            {
                Console.WriteLine("Catch you later!");
                return Task.CompletedTask;
            };
        }
    }

    public class ListById : RouteMetaData
    {
        public override Type Request { get; }

        public override (int code, string description, Type Response)[] Responses { get; } = { (200, "Check out my phat resource", typeof(Person)), (401, "Oh no no no!", null) };
    }

    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
