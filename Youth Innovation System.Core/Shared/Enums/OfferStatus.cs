using System.Runtime.Serialization;

namespace Youth_Innovation_System.Core.Shared.Enums
{
    public enum OfferStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Accepted")]
        Accepted,
        [EnumMember(Value = "Refused")]
        Refused
    }
}
