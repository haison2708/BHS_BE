namespace BHS.API.CommandValidators;

public static class ErrorCode
{
    public const string IdNotExist = "0001";
    public const string NullOrEmpty = "0002";
    public const string LessThanValue = "0003";
    public const string IncorrectType = "0004";
    public const string IncorrectFormatDate = "0005";
    public const string IdExist = "0006";
    public const string Ended = "00067";
    public const string OutOfTurns = "0008";
    public const string IncorrectValue = "0009";
    public const string NotEnoughPoint = "00010";
    public const string OutOfGifts = "0011";
    public const string NotExistOrUsedOrExpired = "0012";
    public const string NotEqual = "0013";
}