/*using BHS.Domain.SeedWork;

namespace BHS.Domain.Entities;

public class StatusList : Enumeration, IAggregateRoot
{
    //Fortune
    public static readonly StatusList FortuneBetterLuckNextTime =
        new(89454, "Chúc may mắn lần sau", "Better Luck Next Time", "Fortune");

    public static readonly StatusList FortuneTurns = new(89455, "Lượt quay", "Turns", "Fortune");
    public static readonly StatusList FortunePoints = new(89456, "Điểm", "Points", "Fortune");

    public static readonly StatusList FortuneGift = new(89457, "Quà tặng", "Gift", "Fortune");

    //PointOfUser
    public static readonly StatusList PointOfUserQr = new(2089, "Quét Qr", "Qr Scan", "PointOfUser");
    public static readonly StatusList PointOfUserOrder = new(2090, "Đặt hàng", "Order", "PointOfUser");
    public static readonly StatusList PointOfUserExchangeGift = new(2092, "Đổi quà", "Exchange Gift", "PointOfUser");

    public static readonly StatusList PointOfUserRotationLuck =
        new(2093, "Vòng quay may mắn", "Rotation Luck", "PointOfUser");

    public static readonly StatusList PointOfUserExpired = new(2094, "Hết hạn", "Expired", "PointOfUser");
    public static readonly StatusList PointOfUserDeducted = new(2095, "Điểm bị trừ", "Points Deducted", "PointOfUser");

    public static readonly StatusList PointOfUserAdded = new(2096, "Điểm được cộng", "Points Added", "PointOfUser");

    //ProductForUser
    public static readonly StatusList ProductViewed = new(48653, "Sản phẩm đã xem", "Product Viewed", "ProductForUser");

    public static readonly StatusList ProductSuggestion =
        new(48654, "Sản phẩm gợi ý", "Product Suggestion", "ProductForUser");

    //LoyaltyProgram
    public static readonly StatusList LoyaltyProgramPurchase = new(12345, "Tích điểm bằng hình thức mua hàng",
            "Accumulate points by shopping", "LoyaltyProgram");

    public static readonly StatusList LoyaltyProgramQrCode =
        new(12346, "Tích điểm bằng QrCode", "Accumulate points by QrCode", "LoyaltyProgram");

    public static readonly StatusList LoyaltyProgramGiftExchange =
        new(12347, "Đổi quà", "Gift Exchange", "LoyaltyProgram");

    //GiftExchange
    public static readonly StatusList ProductGift = new(1337, "Đổi sản phẩm", "Product", "GiftExchange");
    public static readonly StatusList RotationLuckGift = new(1338, "Đổi lượt quay", "Rotation Luck", "GiftExchange");

    public static readonly StatusList VoucherGift = new(1339, "Đổi mã giảm giá", "Voucher", "GiftExchange");

    //Notify
    public static readonly StatusList NotifyPointsLoyalty = new(6610, "Ưu đãi điểm", "Loyalty Points", "Notify");
    public static readonly StatusList NotifyLoyalty = new(6611, "Ưu đãi", "Loyalty", "Notify");
    public static readonly StatusList NotifyOther = new(6612, "Khác", "Other", "Notify");
    public static readonly StatusList NotifySystem = new(6613, "Hệ thống", "System", "Notify");

    public static readonly StatusList Draft = new(110, "Chưa sử dụng", "Draft", "GeneralStatus");
    public static readonly StatusList Active = new(100, "Đang sử dụng", "Active", "GeneralStatus");
    public static readonly StatusList InActive = new(120, "Ngưng sử dụng", "InActive", "GeneralStatus");


    public StatusList(int id, string name, string nameEn, string statusType) : base(id, name)
    {
        StatusType = statusType;
        NameEn = nameEn;
    }

    public string StatusType { get; set; }
    public string NameEn { get; set; }
}*/

