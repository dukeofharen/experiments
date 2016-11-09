using SiteOnWheels.App.Data.Enums;
using SiteOnWheels.App.Data.Exceptions;
using SiteOnWheels.App.Data.Interfaces;
using SiteOnWheels.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SiteOnWheels.App.Data
{
	public class Executor
	{
		private SiteOnWheelsWriter _writer;

		public Executor(SiteOnWheelsWriter writer)
		{
			this._writer = writer;
		}

		public void Execute(string location, string outputLocation, string siteObjectLocation = "")
		{
			this._writer.Write(string.Format("Running SiteOnWheels, version {0}", GetVersion()));
			DataObject.Location = location;
			DataObject.OutputLocation = outputLocation;
			if (!string.IsNullOrEmpty(siteObjectLocation))
			{
				DataObject.SiteObjectLocation = siteObjectLocation;
			}

			if (!Directory.Exists(string.Format("{0}{1}{2}", DataObject.TemplatesLocation, Path.DirectorySeparatorChar, DataObject.SiteObject.Template)))
			{
				throw new SOWNotFoundException(string.Format("template '{0}'", DataObject.SiteObject.Template));
			}

			string content = string.Empty;

			List<SiteOnWheelsExtension> extensions = GetExtensions();

			foreach (SiteOnWheelsExtension extension in extensions)
			{
				extension.OnInit();
			}

			this._writer.Write("Loading posts...");
			Item[] items = DataObject.GetPostsAndPages().OrderByDescending(i => i.AddedOn).ToArray();

			//Delete all old files from the output folder
			DirectoryInfo outputDirData = new DirectoryInfo(DataObject.OutputLocation);

			this._writer.Write("Removing files from output location.");
			foreach (FileInfo file in outputDirData.GetFiles())
			{
				file.Delete();
			}
			foreach (DirectoryInfo dir in outputDirData.GetDirectories())
			{
				dir.Delete(true);
			}

			// Parse files and write them to the output folder
			foreach (Item item in items)
			{
				this._writer.Write("Processing post '{0}'", item.Title);
				DataObject.SiteTitle = string.Format("{0} - {1}", DataObject.SiteObject.SiteName, item.Title);
				item.ParseItemTemplate();
				item.RenderedContents = item.RenderedContents.ParseTemplate();
				item.RenderedContents = item.RenderedContents.ParseMenu();
				item.ParseVariables();
				item.RenderedContents = item.RenderedContents.ParseGlobalVariables();
				content = item.RenderedContents;
				foreach (SiteOnWheelsExtension extension in extensions)
				{
					content = extension.BeforeFileWrite(content, item.IsPost ? FileType.Post : FileType.Page);
				}
				File.WriteAllText(string.Format(@"{0}{1}{2}", DataObject.OutputLocation, Path.DirectorySeparatorChar, item.NewFileName), content);
			}

			//Create index.html / posts pages
			foreach (SiteOnWheelsExtension extension in extensions)
			{
				extension.BeforePostsWrite(items);
			}
			this._writer.Write("Creating item index file(s)");
			DataObject.SiteTitle = DataObject.SiteObject.FullSiteTitle;
			Item[] posts = items.Where(i => i.IsPost).ToArray();
			int itemsPerPage = DataObject.SiteObject.ItemsPerPage;
			int numOfPages = itemsPerPage == 0 ? 1 : (int)Math.Ceiling((double)posts.Length / (double)itemsPerPage);
			for (int i = 0; i < numOfPages; i++)
			{
				StringBuilder postsBuilder = new StringBuilder();
				Item[] postsSelection;
				if (numOfPages == 1)
				{
					postsSelection = posts;
				}
				else
				{
					postsSelection = posts.Skip(i * itemsPerPage).Take(itemsPerPage).ToArray();
				}

				foreach (Item post in postsSelection)
				{
					post.RenderedContents = DataObject.FrontpagePostFrame.ParseGlobalVariables();
					post.ParseVariables();
					postsBuilder.Append(post.RenderedContents);
				}
				string indexHtml = postsBuilder.ToString();
				indexHtml = DataObject.FrontpageFrame.Replace("[content]", indexHtml);
				indexHtml = indexHtml.ParseTemplate();
				indexHtml = indexHtml.ParseMenu();
				indexHtml = indexHtml.ParseGlobalVariables();
				indexHtml = indexHtml.ParsePaging(numOfPages, i);

				string filename = "index.html";
				if (i != 0)
				{
					filename = string.Format("page-{0}.html", i + 1);
				}

				if (!string.IsNullOrEmpty(DataObject.SiteObject.BlogIndexFilename))
				{
					filename = DataObject.SiteObject.BlogIndexFilename;
				}
				foreach (SiteOnWheelsExtension extension in extensions)
				{
					indexHtml = extension.BeforeFileWrite(indexHtml, FileType.PostsIndexPage);
				}
				File.WriteAllText(string.Format(@"{0}{1}{2}", DataObject.OutputLocation, Path.DirectorySeparatorChar, filename), indexHtml);
			}

			//Create tag pages
			List<string> tags = new List<string>();
			this._writer.Write("Parsing tag pages");
			foreach (Item item in items)
			{
				if (item.Tags != null)
				{
					foreach (string tag in item.Tags)
					{
						if (!tags.Contains(tag))
						{
							tags.Add(tag);
						}
					}
				}
			}

			foreach (string tag in tags)
			{
				string filename = tag.GetTagFilename();

				this._writer.Write("Parsing tag page '{0}'", tag);
				StringBuilder builder = new StringBuilder();
				Item[] postsSelection = items.Where(i => i.Tags.Contains(tag) && i.IsPost).ToArray();

				foreach (Item post in postsSelection)
				{
					post.RenderedContents = DataObject.FrontpagePostFrame.ParseGlobalVariables();
					post.ParseVariables();
					builder.Append(post.RenderedContents);
				}

				DataObject.SiteTitle = string.Format("Tag '{0}'", tag);

				string tagHtml = builder.ToString();
				tagHtml = DataObject.TagPage.Replace("[content]", tagHtml);
				tagHtml = tagHtml.ParseTemplate();
				tagHtml = tagHtml.ParseMenu();
				tagHtml = tagHtml.ParseGlobalVariables();
				tagHtml = tagHtml.Replace("[tag:name]", tag);

				foreach (SiteOnWheelsExtension extension in extensions)
				{
					tagHtml = extension.BeforeFileWrite(tagHtml, FileType.TagPage);
				}

				File.WriteAllText(string.Format(@"{0}{1}{2}", DataObject.OutputLocation, Path.DirectorySeparatorChar, filename), tagHtml);
			}

			DataObject.CopyAssets();

			foreach (SiteOnWheelsExtension extension in extensions)
			{
				extension.AfterComplete(items);
			}
		}

		private List<SiteOnWheelsExtension> GetExtensions()
		{
			List<SiteOnWheelsExtension> extensions = new List<SiteOnWheelsExtension>();

			string codeBase = Assembly.GetExecutingAssembly().CodeBase;
			UriBuilder uri = new UriBuilder(codeBase);
			string path = Uri.UnescapeDataString(uri.Path);
			string assemblyPath = Path.GetDirectoryName(path);
			DirectoryInfo info = new DirectoryInfo(assemblyPath);
			FileInfo[] files = info.GetFiles("*.dll");
			foreach (FileInfo file in files)
			{
				if ((file.Name.StartsWith("ext.") || file.Name.StartsWith("Ext.")) && file.Extension == ".dll")
				{
					Assembly.LoadFrom(file.FullName);
				}
			}

			Type interfaceType = typeof(SiteOnWheelsExtension);
			Type[] types = AppDomain.CurrentDomain.GetAssemblies()
						.SelectMany(s => s.GetTypes())
						.Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass)
						.ToArray();

			foreach (Type type in types)
			{
				SiteOnWheelsExtension extension = (SiteOnWheelsExtension)Activator.CreateInstance(type);
				extension.Writer = this._writer;
				this._writer.Write(string.Format("Loading extension '{0}'", extension.GetExtensionName()));
				extensions.Add(extension);
			}

			return extensions;
		}

		private string GetVersion()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
			return fvi.FileVersion;
		}
	}
}
