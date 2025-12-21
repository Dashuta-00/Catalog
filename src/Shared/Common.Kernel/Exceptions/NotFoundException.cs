namespace Common.Kernel.Exceptions;

public class NotFoundException : Exception
{
    public string Value { get; } = default!;
    public object? Key { get; }

    public NotFoundException(string value, object? key)
    : base($"Объект '{value}' с ключом '{key}' не найден.")
    {
        Value = value;
        Key = key;
    }
}