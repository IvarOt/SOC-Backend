using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.logic.Interfaces.Api
{
	public interface ITokenProvider
	{
		Task<string> GetAccessTokenAsync();
		Task<DateTime> GetExpirationAsync();
	}
}
