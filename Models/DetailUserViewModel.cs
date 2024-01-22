namespace ZaverecnyProjektForman2.Models
{
    public class DetailUserViewModel
    {
        public string UserId { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public DateTime? RegistrationDate { get; set; } = null;
        public string Role { get; set; } = "";
    }
}
