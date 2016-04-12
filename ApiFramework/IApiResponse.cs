namespace ApiFramework
{
    public interface IApiResponse
    {
        string ContentType { get; set; }
        object Content { get; set; }
    }
}