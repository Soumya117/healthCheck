using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

public enum MeasurementType
{
    [EnumMember(Value = "Temperature")]
    TEMP,
    [EnumMember(Value = "HeartRate")]
    HR,
    [EnumMember(Value = "RespitoryRate")]
    RR,
}
