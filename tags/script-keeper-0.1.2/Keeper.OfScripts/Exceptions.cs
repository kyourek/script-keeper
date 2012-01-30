using System;

namespace Keeper.OfScripts
{
	public class ScriptAlreadyAddedException : Exception
	{
		public ScriptAlreadyAddedException() : base() { }
		public ScriptAlreadyAddedException(string message) : base(message) { }
		public ScriptAlreadyAddedException(string message, Exception innerException) : base(message, innerException) { }
	}
	
	public class ScriptNotFoundException : System.IO.FileNotFoundException
	{
		public ScriptNotFoundException() : base() { }
		public ScriptNotFoundException(string message) : base(message) { }
		public ScriptNotFoundException(string message, Exception innerException) : base(message, innerException) { }		
	}
}
