namespace EquipmentService.Core.ResultTypes;

public sealed record Error(int ErrorCode, string Description)
{
    public static readonly Error None = new(500, string.Empty);
    public static readonly Error NullValue = new(404, "Null value");
}

