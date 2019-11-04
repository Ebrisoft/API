namespace API.Unauthorized.Request
{
    public class SignIn
    {
        //  Properties
        //  ==========

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}