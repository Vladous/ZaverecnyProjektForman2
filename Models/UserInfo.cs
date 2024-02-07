using Humanizer;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZaverecnyProjektForman2.Models
{
    [NotMapped]
    public class UserInfo
    {
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
}
