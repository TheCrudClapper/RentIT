namespace EquipmentService.Core.Domain.ResultTypes;

public enum ErrorType
{
    Validation,
    NotFound,
    Conflict,
    Unauthorized,
    Forbidden,
    Unexpected
}

public sealed record Error(ErrorType Type, string Code, string Description)
{
    public static Error Create(ErrorType error, string code, string description)
        => new Error(error, code, description);

    public static readonly Error None = new(ErrorType.Unexpected, "Unexpected", string.Empty);
    public static readonly Error NullValue = new(ErrorType.Unexpected, "Null value", "Given value is null");
}
