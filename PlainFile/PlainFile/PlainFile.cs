using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlainFile
{
	private static string _rootLocation;
	public static string RootLocation
	{
		get
		{
			return _rootLocation;
		}
		set
		{
			_rootLocation = value;
		}
	}

	public static bool Write(object obj, string key)
	{
		return Write(obj, key, string.Empty);
	}

	public static bool Write(object obj, string key, string collection)
	{
		if (!Directory.Exists(RootLocation))
		{
			return false;
		}

		string path = Path.Combine(RootLocation, collection);
		if (Directory.Exists(RootLocation) && !Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}

		string result = string.Empty;
		Type type = obj.GetType();
		if (type.IsValueType)
		{
			result = obj.ToString();
		}
		else if (type == typeof(string))
		{
			result = obj as string;
		}
		else
		{
			result = JsonConvert.SerializeObject(obj);
		}

		try
		{
			File.WriteAllText(Path.Combine(path, string.Format("{0}.txt", key)), result);
			return true;
		}
		catch
		{
			return false;
		}
	}

	public static string Get(string key)
	{
		return Get(key, string.Empty);
	}

	public static string Get(string key, string collection)
	{
		string path = Path.Combine(RootLocation, collection, string.Format("{0}.txt", key));
		if (!File.Exists(path))
		{
			return string.Empty;
		}

		try
		{
			return File.ReadAllText(path);
		}
		catch
		{
			return string.Empty;
		}
	}

	public static int GetInt(string key)
	{
		return GetInt(key, string.Empty);
	}

	public static int GetInt(string key, string collection)
	{
		string result = Get(key, collection);
		int outcome = 0;
		int.TryParse(result, out outcome);
		return outcome;
	}

	public static long GetLong(string key)
	{
		return GetLong(key, string.Empty);
	}

	public static long GetLong(string key, string collection)
	{
		string result = Get(key, collection);
		long outcome = 0;
		long.TryParse(result, out outcome);
		return outcome;
	}

	public static double GetDouble(string key)
	{
		return GetDouble(key, string.Empty);
	}

	public static double GetDouble(string key, string collection)
	{
		string result = Get(key, collection);
		double outcome = 0;
		double.TryParse(result, out outcome);
		return outcome;
	}

	public static float GetFloat(string key)
	{
		return GetFloat(key, string.Empty);
	}

	public static float GetFloat(string key, string collection)
	{
		string result = Get(key, collection);
		float outcome = 0;
		float.TryParse(result, out outcome);
		return outcome;
	}

	public static T Get<T>(string key)
	{
		return Get<T>(key, string.Empty);
	}

	public static T Get<T>(string key, string collection)
	{
		string result = Get(key, collection);

		Type type = typeof(T);
		if (type.IsValueType || type == typeof(string))
		{
			return default(T);
		}
		else
		{
			return JsonConvert.DeserializeObject<T>(result);
		}
	}
}