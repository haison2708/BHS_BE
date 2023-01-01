using BHS.Domain.SeedWork;

namespace BHS.Domain.Enumerate;

public enum PointOfUserType
{
    [StringValue("aaaaaaaaaa")] Qr = 2089,
    [StringValue("aaaaaaaaaa")] Order = 2090,
    GiftExchange = 2092,
    RotationLuck = 2093,
    Expired = 2094,
    Deducted = 2095,
    Added = 2096
}