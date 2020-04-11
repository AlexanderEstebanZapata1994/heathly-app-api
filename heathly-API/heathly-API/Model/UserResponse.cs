namespace heathly_API.Model
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public bool IsLoggedIn { get; set; }
        public ErrorType Error { get; set; }
    }
    public class ErrorType {
        public bool HasError {get; set;}
        public string ErrorMessage {get; set;}
    }
}