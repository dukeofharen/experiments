using Book.Data.Interfaces;
using Book.Data.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Book.Data
{
	public class Executor
	{
		private List<IBookCommand> commands;
		private IBookManager _manager;

		public static string BookPath
		{
			get
			{
				string setting = ConfigurationManager.AppSettings["DefaultBookLocation"];
				if (!string.IsNullOrEmpty(setting))
				{
					return setting;
				}
				return Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "book");
			}
		}

		public Executor(IBookManager manager)
		{
			this._manager = manager;
			
			this.LoadPlugins();
			this.LoadCommands();
		}

		public void Execute(string command)
		{
			string[] parts = command.Split(new char[] { ' ' });
			IBookCommand cmd = this.commands.Where(c => c.CommandKey == parts[0]).FirstOrDefault();
			if (cmd == null)
			{
				Console.WriteLine(string.Format("The command '{0}' couldn't be found.", parts[0]));
			}
			else
			{
				cmd.Execute(parts.Skip(1).ToArray());
			}
		}

		private void LoadCommands()
		{
			this.commands = new List<IBookCommand>();

			Type interfaceType = typeof(IBookCommand);
			Type[] types = AppDomain.CurrentDomain.GetAssemblies()
						.SelectMany(s => s.GetTypes())
						.Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass)
						.ToArray();

			foreach (Type type in types)
			{
				IBookCommand command = (IBookCommand)Activator.CreateInstance(type);
				command.Manager = this._manager;
				this.commands.Add(command);
			}
		}

		private void LoadPlugins()
		{

		}

		private void InitializeSettings()
		{
			if (!Directory.Exists(BookPath))
			{
				Directory.CreateDirectory(BookPath);
			}
		}
	}
}
