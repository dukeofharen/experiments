using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ToolList.Entities;
using ToolList.Data;
using ToolList.Exceptions;
using ToolList.Models;

namespace ToolList.Business.Implementations
{
	public class ToolManager : IToolManager
	{
		public Task<Tool> GetTool(int id)
		{
			return Task.Run(() =>
			{
				using (var ctx = new TLEntities())
				{
					Tool tool = ctx.Tools
								.Where(t => t.Id == id && t.Activated)
								.Include(t => t.Category)
								.Include(t => t.OperatingSystems)
								.FirstOrDefault();
					if (tool == null)
					{
						throw new TLNotFoundException("tool");
					}
					return tool;
				}
			});
		}

		public Task<Tool[]> GetTools(int categoryId = 0, ToolType? type = null, int operatingSystem = 0, License? license = null)
		{
			return Task.Run(() =>
			{
				using (var ctx = new TLEntities())
				{
					var query = ctx.Tools
							.Where(t => t.Activated)
							.Include(t => t.Category)
							.Include(t => t.OperatingSystems);
					if (categoryId != 0)
					{
						query = query.Where(t => t.Category.Id == categoryId);
					}
					if (type.HasValue)
					{
						query = query.Where(t => t.Type == type.Value);
					}
					if (operatingSystem != 0)
					{
						query = query.Where(t => t.OperatingSystems.Where(o => o.Id == operatingSystem).Any());
					}
					if (license.HasValue)
					{
						query = query.Where(t => t.License == license.Value);
					}
					return query.ToArray();
				}
			});
		}


		public Task<bool> ActivateTool(string activationCode)
		{
			return Task.Run(() =>
			{
				using (var ctx = new TLEntities())
				{
					Tool tool = ctx.Tools
								.Where(t => !t.Activated && t.ActivationCode == activationCode)
								.FirstOrDefault();
					if (tool != null)
					{
						tool.Activated = true;
						ctx.SaveChanges();
						return true;
					}
					return false;
				}
			});
		}


		public Task<Tool> AddTool(ToolSubmitModel tool)
		{
			return Task.Run(() =>
			{
				using (var ctx = new TLEntities())
				{
					Tool toolEntity = new Tool();
					if (tool == null)
					{
						throw new ArgumentException("Model is empty");
					}
					if (string.IsNullOrEmpty(tool.Name))
					{
						throw new ArgumentException("Name is empty");
					}
					if (string.IsNullOrEmpty(tool.SiteUrl))
					{
						throw new ArgumentException("Site URL is empty");
					}
					if (string.IsNullOrEmpty(tool.Description))
					{
						throw new ArgumentException("Description is empty");
					}
					if (tool.Type == 0)
					{
						throw new ArgumentException("Type is empty");
					}
					if (tool.License == 0)
					{
						throw new ArgumentException("License is empty");
					}
					if (string.IsNullOrEmpty(tool.Creator))
					{
						throw new ArgumentException("Creator is empty");
					}
					if (string.IsNullOrEmpty(tool.CreatorSite))
					{
						throw new ArgumentException("Creator site is empty");
					}
					Category category = ctx.Categories.Where(c => c.Id == tool.Category).FirstOrDefault();
					if (category == null)
					{
						throw new ArgumentException("Category is empty");
					}

					toolEntity.Name = tool.Name;
					toolEntity.ImageUrl = tool.ImageUrl;
					toolEntity.License = (License)tool.License;
					toolEntity.SiteUrl = tool.SiteUrl;
					toolEntity.Type = (ToolType)tool.Type;
					toolEntity.Updated = DateTime.MinValue;
					toolEntity.Created = DateTime.Now;
					toolEntity.Activated = false;
					toolEntity.ActivationCode = Guid.NewGuid().ToString();
					toolEntity.Category = category;
					toolEntity.Creator = tool.Creator;
					toolEntity.CreatorSite = tool.CreatorSite;
					toolEntity.Description = tool.Description;
					toolEntity.DownloadUrl = tool.DownloadUrl;
					toolEntity.Version = tool.Version;

					List<Entities.OperatingSystem> oss = new List<Entities.OperatingSystem>();
					if (tool.OSs != null)
					{
						foreach (int osId in tool.OSs)
						{
							Entities.OperatingSystem os = ctx.OperatingSystems.Where(o => o.Id == osId).FirstOrDefault();
							if (os == null)
							{
								throw new ArgumentException("OS doesn't exist");
							}
							oss.Add(os);
						}
					}
					toolEntity.OperatingSystems = oss;

					ctx.Tools.Add(toolEntity);
					ctx.SaveChanges();
					return toolEntity;
				}
			});
		}
	}
}
