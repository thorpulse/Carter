namespace CarterSample.Features.Actors
{
    using Carter.Response;
    public class Actor : INegotiatedModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Age { get; set; }

        public string ViewName { get; set; } = "test.html";
    }
}