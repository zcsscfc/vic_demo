namespace ApiFramework
{
    public class Response
    {
        public object Data { get; set; }

        public Meta Meta { get; set; }

        public Response Success(object obj = null)
        {
            var response = new Response
            {
                Meta = new Meta(true, "success"),
                Data = obj
            };
            return response;
        }

        public Response Failure(string message = "failure")
        {
            var response = new Response {Meta = new Meta(false, message)};
            return response;
        }
    }

    public class Meta
    {
        public Meta(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}