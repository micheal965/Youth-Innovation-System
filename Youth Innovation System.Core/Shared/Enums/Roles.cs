using System.Runtime.Serialization;

namespace Youth_Innovation_System.Core.Shared.Enums
{
    public enum UserRoles
    {
        [EnumMember(Value = "Admin")]
        Admin,
        [EnumMember(Value = "User")]
        User
    }
}
