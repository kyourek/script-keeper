using System;

namespace Keeper.OfScripts
{
	/// <summary>
	/// Exception that is thrown when an attempt is made to add a script
	/// that has already been added to a script group.
	/// </summary>
	public class ScriptAlreadyAddedException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Keeper.OfScripts.ScriptAlreadyAddedException"/> class.
		/// </summary>
		public ScriptAlreadyAddedException() : base() { }
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Keeper.OfScripts.ScriptAlreadyAddedException"/> class.
		/// </summary>
		/// <param name='message'>
		/// The exception message.
		/// </param>
		public ScriptAlreadyAddedException(string message) : base(message) { }
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Keeper.OfScripts.ScriptAlreadyAddedException"/> class.
		/// </summary>
		/// <param name='message'>
		/// The exception message.
		/// </param>
		/// <param name='innerException'>
		/// The inner exception.
		/// </param>
		public ScriptAlreadyAddedException(string message, Exception innerException) : base(message, innerException) { }
	}
	
	/// <summary>
	/// Exception that is thrown when an attempt is made to register a local script
	/// that does not exist on the server.
	/// </summary>
	public class ScriptNotFoundException : System.IO.FileNotFoundException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Keeper.OfScripts.ScriptNotFoundException"/> class.
		/// </summary>
		public ScriptNotFoundException() : base() { }
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Keeper.OfScripts.ScriptNotFoundException"/> class.
		/// </summary>
		/// <param name='message'>
		/// The exception message.
		/// </param>
		public ScriptNotFoundException(string message) : base(message) { }
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Keeper.OfScripts.ScriptNotFoundException"/> class.
		/// </summary>
		/// <param name='message'>
		/// The exception message.
		/// </param>
		/// <param name='innerException'>
		/// The inner exception.
		/// </param>
		public ScriptNotFoundException(string message, Exception innerException) : base(message, innerException) { }		
	}
}
