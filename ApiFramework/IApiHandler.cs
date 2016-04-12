namespace ApiFramework
{
    public interface IApiHandler
    {
        void ProcessRequest(IApiRequest request, IApiResponse response);
    }
}