namespace DoNETCoreAPI.Web.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; }=string.Empty;
        public bool Status { get; set; }
    }
}
