namespace Youth_Innovation_System.Shared.DTOs.Identity.Roles
{
    public class UpdateUserRoleDto
    {
        public string userId { get; set; }
        public List<string> Roles { get; set; }
    }
}
