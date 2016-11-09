using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Armyknife.Extendability
{
	public interface ArmyknifeExtension
	{
		string Key { get; }
		string Name { get; }
		string Execute(string args);
		string Help();
	}
}
