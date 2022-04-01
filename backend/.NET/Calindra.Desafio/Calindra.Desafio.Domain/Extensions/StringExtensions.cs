namespace Calindra.Desafio.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string value)
        {
            return value == null || value.Trim() == string.Empty;
        }

        public static bool AnyChar(this string value)
        {
            return value != null && value?.Trim().Length > 0;
        }
    }
}
