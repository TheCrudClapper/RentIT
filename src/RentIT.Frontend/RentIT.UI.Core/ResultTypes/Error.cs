namespace RentIT.UI.Core.ResultTypes;

public sealed record Error(string Code, string Description)
{
    public static Error Create(string code, string description)
        => new Error(code, description);

    public static readonly Error None = new("Unexpected", string.Empty);
    public static readonly Error NullValue = new("Null value", "Given value is null");
}

