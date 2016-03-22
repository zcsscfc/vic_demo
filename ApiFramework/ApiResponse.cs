namespace ApiFramework
{
    public class ApiResponse : IApiResponse
    {
        public string ContentType { get; set; }
        public object Content { get; set; }
    }
}