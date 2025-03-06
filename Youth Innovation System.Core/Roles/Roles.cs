using System.Runtime.Serialization;

namespace Youth_Innovation_System.Core.Roles
{
    public enum UserRoles
    {
        [EnumMember(Value = "Admin")]
        Admin,
        [EnumMember(Value = "User")]
        User
    }
}
