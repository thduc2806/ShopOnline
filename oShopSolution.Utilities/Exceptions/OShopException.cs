using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Utilities.Exceptions
{
	public class OShopException : Exception
	{
		public OShopException()
		{
		}
		public OShopException(string message)
			: base(message)
		{
		}
		public OShopException(string message, Exception inner)
			:base(message,inner)
		{
		}
	}
}
