using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Book.Data.Helpers
{
	public class SerializeHelpers
	{
		public static string Serialize<T>(T objectToSerialize)
		{
			using (MemoryStream memStm = new MemoryStream())
			{
				DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
				settings.UseSimpleDictionaryFormat = true;
				var serializer = new DataContractJsonSerializer(typeof(T), settings);
				serializer.WriteObject(memStm, objectToSerialize);

				memStm.Seek(0, SeekOrigin.Begin);

				using (var streamReader = new StreamReader(memStm))
				{
					string result = streamReader.ReadToEnd();
					return result;
				}
			}
		}

		public static T Deserialize<T>(string jsonString)
		{
			DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
			settings.UseSimpleDictionaryFormat = true;
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T), settings);
			MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
			T obj = (T)serializer.ReadObject(ms);
			return obj;
		}
	}
}
