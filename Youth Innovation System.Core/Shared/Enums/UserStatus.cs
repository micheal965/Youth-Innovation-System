using System.Runtime.Serialization;

namespace Youth_Innovation_System.Core.Shared.Enums
{
    public enum UserStatus
    {
        [EnumMember(Value = "Online")]
        Online,
        [EnumMember(Value = "Offline")]
        Offline
    }
}
