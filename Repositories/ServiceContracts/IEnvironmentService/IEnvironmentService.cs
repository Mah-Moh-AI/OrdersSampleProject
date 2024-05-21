using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.ServiceContracts.IEnvironmentService
{
	public interface IEnvironmentService
	{
		string ContentRootPath { get; }
	}
}
