namespace Rallye.Library.Extensions;

public static class StringExtensions
{
	public static bool ContainNumbers(this string str) => str.Any(char.IsNumber);

	public static bool IsBetween4To6Digits(this string str) 
		=> str.Length > 3 && str.Length < 7 && str.All(char.IsDigit);
}
