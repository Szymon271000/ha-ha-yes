namespace Api.Data.Model.Authentication
{
    internal class User
    {
        public int UserId { get; set; }
#pragma warning disable CS8618
        public Credentials Credentials { get; set; }
#pragma warning restore CS8618
    }
}
