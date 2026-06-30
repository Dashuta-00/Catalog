namespace Common.Kernel.Extensions;

public static class StringExtensions
{
    extension(string source)
    {
        public string ToSnakeCase()
        {
            return string.IsNullOrEmpty(source) 
                ? source : 
                string.Concat(source.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString().ToLower() : x.ToString().ToLower()));
        }
    }
}