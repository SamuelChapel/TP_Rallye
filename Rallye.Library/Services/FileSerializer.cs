using System.Text.Json;

namespace Rallye.Library.Services;

public static class FileSerializer
{
	private static readonly string _path = AppDomain.CurrentDomain.BaseDirectory;

	public static void SerializeInFile<T>(IEnumerable<T> source, string fileName)
	{
		var serializedSource = source.Select(x => JsonSerializer.Serialize(x)).ToList();
		File.WriteAllLines(Path.Combine(_path, fileName), serializedSource);
	}

	public static IEnumerable<T> DeSerializeFromFile<T>(string fileName)
	{
		try
		{
			var lines = File.ReadAllLines(Path.Combine(_path, fileName));
			var deserializedType = lines.Select(x => JsonSerializer.Deserialize<T>(x)!);

			return deserializedType;
		}
		catch (Exception)
		{
			return Enumerable.Empty<T>();
		}
		
	}
}
