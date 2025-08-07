namespace VLB;

public static class Version
{
	public const int Current = 20100;

	public static string CurrentAsString => GetVersionAsString(20100);

	private static string GetVersionAsString(int version)
	{
		int num = version / 10000;
		int num2 = (version - num * 10000) / 100;
		int num3 = (version - num * 10000 - num2 * 100) / 1;
		return $"{num}.{num2}.{num3}";
	}
}
