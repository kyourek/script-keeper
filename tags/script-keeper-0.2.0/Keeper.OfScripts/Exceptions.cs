using System;

namespace Keeper.OfScripts
{
	/// <summary>
	/// Exception that is thrown when an attempt is made to add a resource
	/// that has already been added to a resource group.
	/// </summary>
	public class ResourceAlreadyAddedException : Exception
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="Keeper.OfScripts.ResourceAlreadyAddedException"/> class.
        /// </summary>
		public ResourceAlreadyAddedException() : base() { }
		
        /// <summary>
        /// Initializes a new instance of the <see cref="Keeper.OfScripts.ResourceAlreadyAddedException"/> class.
        /// </summary>
        /// <param name='message'>
        /// The exception message.
        /// </param>
		public ResourceAlreadyAddedException(string message) : base(message) { }
		
        /// <summary>
        /// Initializes a new instance of the <see cref="Keeper.OfScripts.ResourceAlreadyAddedException"/> class.
        /// </summary>
        /// <param name='message'>
        /// The exception message.
        /// </param>
        /// <param name='innerException'>
        /// The inner exception.
        /// </param>
		public ResourceAlreadyAddedException(string message, Exception innerException) : base(message, innerException) { }
	}
	
	/// <summary>
	/// Exception that is thrown when an attempt is made to register a local resource
	/// that does not exist on the server.
	/// </summary>
	public class ResourceNotFoundException : System.IO.FileNotFoundException
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="Keeper.OfScripts.ResourceNotFoundException"/> class.
        /// </summary>
		public ResourceNotFoundException() : base() { }
		
        /// <summary>
        /// Initializes a new instance of the <see cref="Keeper.OfScripts.ResourceNotFoundException"/> class.
        /// </summary>
        /// <param name='message'>
        /// The exception message.
        /// </param>
		public ResourceNotFoundException(string message) : base(message) { }
		
        /// <summary>
        /// Initializes a new instance of the <see cref="Keeper.OfScripts.ResourceNotFoundException"/> class.
        /// </summary>
        /// <param name='message'>
        /// The exception message.
        /// </param>
        /// <param name='innerException'>
        /// The inner exception.
        /// </param>
		public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException) { }		
	}
}
