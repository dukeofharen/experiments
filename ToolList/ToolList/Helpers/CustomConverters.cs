using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolList.Entities;
using ToolList.Models;

public static class CustomConverters
{
	public static ToolModel ConvertTool(this Tool tool)
	{
		ToolModel model = Mapper.Map<ToolModel>(tool);
		model.Category = tool.Category.Name;
		model.License = Enum.GetName(typeof(License), tool.License);
		model.Type = Enum.GetName(typeof(ToolType), tool.Type);
		model.OSs = tool.OperatingSystems.Select(o => o.Name).ToArray();
		return model;
	}

	public static ToolModel[] ConvertTools(this Tool[] tools)
	{
		ToolModel[] models = new ToolModel[tools.Length];
		int i = 0;
		foreach (Tool tool in tools)
		{
			models[i] = tool.ConvertTool();
			i++;
		}
		return models;
	}
}
