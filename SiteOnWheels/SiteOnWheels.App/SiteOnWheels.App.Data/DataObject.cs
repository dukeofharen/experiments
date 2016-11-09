using SiteOnWheels.App.Data.Exceptions;
using SiteOnWheels.App.Data.Helpers;
using SiteOnWheels.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SiteOnWheels.App.Data
{
	public class DataObject
	{
		#region Fields
		private static SiteObject _siteObject;
		private static string _siteObjectLocation;
		private static Dictionary<string, string> _frames = new Dictionary<string, string>();
		public static List<string> Exclude = new List<string>();
		#endregion

		#region Properties
		public static string Location { get; set; }
		public static string OutputLocation { get; set; }

		public static string PostsLocation
		{
			get
			{
				return string.Format("{0}{1}posts", Location, Path.DirectorySeparatorChar);
			}
		}

		public static string PagesLocation
		{
			get
			{
				return string.Format("{0}{1}pages", Location, Path.DirectorySeparatorChar);
			}
		}

		public static string TemplatesLocation
		{
			get
			{
				return string.Format("{0}{1}templates", Location, Path.DirectorySeparatorChar);
			}
		}

		public static string AssetsLocation
		{
			get
			{
				return string.Format("{0}{1}assets", Location, Path.DirectorySeparatorChar);
			}
		}

		public static string RootFolder
		{
			get
			{
				return string.Format("{0}{1}root", Location, Path.DirectorySeparatorChar);
			}
		}

		public static string Frame
		{
			get
			{
				return LoadFrame("frame.tpl");
			}
		}

		public static string PostFrame
		{
			get
			{
				return LoadFrame("post.tpl");
			}
		}

		public static string PageFrame
		{
			get
			{
				return LoadFrame("page.tpl");
			}
		}

		public static string FrontpageFrame
		{
			get
			{
				return LoadFrame("frontpage.tpl");
			}
		}

		public static string FrontpagePostFrame
		{
			get
			{
				return LoadFrame("frontpage_post.tpl");
			}
		}

		public static string TagPage
		{
			get
			{
				return LoadFrame("tag_page.tpl");
			}
		}

		public static string MenuItemFrame
		{
			get
			{
				return LoadFrame("menu_item.tpl");
			}
		}

		public static string PagingFrame
		{
			get
			{
				return LoadFrame("paging_frame.tpl");
			}
		}

		public static string PagingItem
		{
			get
			{
				return LoadFrame("paging_item.tpl");
			}
		}

		public static string HeaderFrame
		{
			get
			{
				return LoadFrame("header.tpl", Location);
			}
			set
			{
				string content = HeaderFrame;
				content = value;
				_frames["header.tpl"] = content;
			}
		}

		public static string FooterFrame
		{
			get
			{
				return LoadFrame("footer.tpl", Location);
			}
			set
			{
				string content = FooterFrame;
				content = value;
				_frames["footer.tpl"] = content;
			}
		}

		public static SiteObject SiteObject
		{
			get
			{
				if (_siteObject != null)
				{
					return _siteObject;
				}
				else
				{
					string location = string.Format("{0}{1}{2}", Location, Path.DirectorySeparatorChar, SiteObjectLocation);
					if (!File.Exists(location))
					{
						throw new SOWNotFoundException(SiteObjectLocation);
					}
					string fileData = File.ReadAllText(location);
					using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(fileData)))
					{
						DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
						settings.UseSimpleDictionaryFormat = true;
						DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SiteObject), settings);
						stream.Position = 0;
						_siteObject = (SiteObject)ser.ReadObject(stream);
						return _siteObject;
					}
				}
			}
		}

		public static string SiteObjectLocation
		{
			get
			{
				if (string.IsNullOrEmpty(_siteObjectLocation))
				{
					return "site.json";
				}
				return _siteObjectLocation;
			}
			set
			{
				_siteObjectLocation = string.Format("site_{0}.json", value);
			}
		}

		public static string SiteTitle { get; set; }
		#endregion

		#region Methods
		public static Item[] GetPostsAndPages()
		{
			List<string> filePaths = new List<string>();
			filePaths.AddRange(Directory.GetFiles(PostsLocation, "*.md"));
			string[] directoryPaths = Directory.GetDirectories(PostsLocation);
			for (int i = 0; i < directoryPaths.Length; i++)
			{
				filePaths.AddRange(Directory.GetFiles(directoryPaths[i], "*.md"));
			}
			Item[] posts = new Item[filePaths.Count];
			for (int i = 0; i < posts.Length; i++)
			{
				Item post = new Item();
				post.FilePath = filePaths[i];
				string[] filePathParts = post.FilePath.Split(Path.DirectorySeparatorChar);
				post.NewFileName = string.Format("{0}.html", filePathParts[filePathParts.Length-1].Split('.')[0]);
				string fileContents = File.ReadAllText(filePaths[i]);
				string[] fileLines = fileContents.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
				foreach (string line in fileLines)
				{
					string value = string.Empty;
					if (line.StartsWith("Title="))
					{
						value = line.Replace("Title=", string.Empty);
						post.Title = value.StripNewline();
					}
					else if (line.StartsWith("Type="))
					{
						value = line.Replace("Type=", string.Empty).StripNewline().ToLower();
						post.IsPost = value == "post";
					}
					else if (line.StartsWith("Author="))
					{
						value = line.Replace("Author=", string.Empty);
						post.Author = value.StripNewline();
					}
					else if (line.StartsWith("AddedOn="))
					{
						value = line.Replace("AddedOn=", string.Empty);
						DateTime addedOn = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
						post.AddedOn = addedOn;
					}
					else if (line.StartsWith("ChangedOn="))
					{
						value = line.Replace("ChangedOn=", string.Empty);
						DateTime changedOn = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
						post.ChangedOn = changedOn;
					}
					else if (line.StartsWith("PreviewImage="))
					{
						value = line.Replace("PreviewImage=", string.Empty);
						post.PreviewImage = value.StripNewline();
					}
					else if (line.StartsWith("Tags="))
					{
						value = line.Replace("Tags=", string.Empty).StripNewline();
						string[] parts = value.Split('|');
						post.Tags = parts;
					}
					else
					{
						break;
					}
				}
				string[] contentParts = fileContents.Split(new string[] { "==========" + Environment.NewLine }, StringSplitOptions.None);
				if (contentParts.Length != 2)
				{
					throw new SOWArgumentException(string.Format("The file '{0}' doesn't contain content.", filePaths[i]));
				}
				post.Contents = contentParts[1].ParseGlobalVariables();
				post.RenderedContents = CommonMark.CommonMarkConverter.Convert(post.Contents);
				post.OriginalRenderedContents = post.RenderedContents;
				posts[i] = post;
			}
			return posts;
		}

		public static void CopyAssets()
		{
			string templateDir = string.Format("{0}{1}{2}", TemplatesLocation, Path.DirectorySeparatorChar, SiteObject.Template);
			string[] files = Directory.GetFiles(templateDir);
			string[] parts = new string[0];
			string lastPart = string.Empty;
			
			//Template assets
			foreach (string file in files)
			{
				parts = file.Split(Path.DirectorySeparatorChar);
				lastPart = parts[parts.Length - 1];
				if (!Exclude.Contains(lastPart))
				{
					File.Copy(file, string.Format("{0}{1}{2}", OutputLocation, Path.DirectorySeparatorChar, lastPart));
				}
			}

			string[] directories = Directory.GetDirectories(templateDir);
			foreach (string dir in directories)
			{
				parts = dir.Split(Path.DirectorySeparatorChar);
				lastPart = parts[parts.Length - 1];
				FileHelpers.DirectoryCopy(dir, string.Format("{0}{1}{2}", OutputLocation, Path.DirectorySeparatorChar, lastPart), true);
			}

			//Other assets
			Directory.CreateDirectory(string.Format("{0}{1}assets", OutputLocation, Path.DirectorySeparatorChar));
			files = Directory.GetFiles(AssetsLocation);
			foreach (string file in files)
			{
				parts = file.Split(Path.DirectorySeparatorChar);
				lastPart = parts[parts.Length - 1];
				if (!Exclude.Contains(lastPart))
				{
					File.Copy(file, string.Format("{0}{1}assets{2}{3}", OutputLocation, Path.DirectorySeparatorChar, Path.DirectorySeparatorChar, lastPart));
				}
			}

			directories = Directory.GetDirectories(AssetsLocation);
			foreach (string dir in directories)
			{
				parts = dir.Split(Path.DirectorySeparatorChar);
				lastPart = parts[parts.Length - 1];
				FileHelpers.DirectoryCopy(dir, string.Format("{0}{1}assets{1}{2}", OutputLocation, Path.DirectorySeparatorChar, lastPart), true);
			}

			//Copy files and folders from root folder to output root
			files = Directory.GetFiles(RootFolder);
			foreach (string file in files)
			{
				parts = file.Split(Path.DirectorySeparatorChar);
				lastPart = parts[parts.Length - 1];
				if (!Exclude.Contains(lastPart))
				{
					File.Copy(file, string.Format("{0}{1}{2}", OutputLocation, Path.DirectorySeparatorChar, lastPart));
				}
			}

			directories = Directory.GetDirectories(RootFolder);
			foreach (string dir in directories)
			{
				parts = dir.Split(Path.DirectorySeparatorChar);
				lastPart = parts[parts.Length - 1];
				FileHelpers.DirectoryCopy(dir, string.Format("{0}{1}{2}", OutputLocation, Path.DirectorySeparatorChar, lastPart), true);
			}
		}

		private static string LoadFrame(string filename, string rootFolder = "")
		{
			if (!_frames.ContainsKey(filename))
			{
				if (string.IsNullOrEmpty(rootFolder))
				{
					rootFolder = string.Format("{0}{1}{2}", TemplatesLocation, Path.DirectorySeparatorChar, SiteObject.Template);
				}
				string path = string.Format("{0}{1}{2}", rootFolder, Path.DirectorySeparatorChar, filename);
				if (!File.Exists(path))
				{
					throw new SOWNotFoundException(path);
				}
				string frame = File.ReadAllText(path);
				_frames.Add(filename, frame);
				Exclude.Add(filename);
				return frame;
			}
			else
			{
				return _frames[filename];
			}
		}
		#endregion
	}
}
