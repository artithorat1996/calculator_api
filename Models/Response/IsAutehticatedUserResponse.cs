namespace API.Models.Response
{
    public class IsAutehticatedUserResponse
    {
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }

    }
}
