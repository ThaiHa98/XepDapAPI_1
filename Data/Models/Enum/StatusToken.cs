using System.Runtime.Serialization;

namespace Data.Models.Enum
{
    public enum StatusToken
    {
        [EnumMember(Value = "Valid")]
        Valid,
        [EnumMember(Value = "Expired")]
        Expired
    }
}
