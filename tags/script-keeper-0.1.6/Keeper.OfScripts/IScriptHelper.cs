using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;

namespace Keeper.OfScripts
{
	/// <summary>
	/// Helper for local script groups that can resolve paths
	/// to local files on the server.
	/// </summary>
	public interface ILocalScriptHelper
	{
		/// <summary>
		/// Resolves the absolute path to <paramref name="contentPath"/>.
		/// </summary>
		/// <returns>
		/// The absolute path of <paramref name="contentPath"/>.
		/// </returns>
		/// <param name='contentPath'>
		/// The content path to resolve. This path can begin with a '~'
		/// to denote a path relative to the web root.
		/// </param>
		string ServerPath(string contentPath);
		
		/// <summary>
		/// Resolves the path to <paramref name="contentPath"/> relative to the web root.
		/// </summary>
		/// <returns>
		/// The resolved path of <paramref name="contentPath"/> relative to the web root.
		/// </returns>
		/// <param name='contentPath'>
		/// The content path to resolve. This path can begin with a '~'
		/// to denote a path relative to the web root.
		/// </param>
		string UrlContent(string contentPath);
	}
	
	/// <summary>
	/// Helper object for resolving paths, etc., for the <c>ScriptKeeper</c>.
	/// </summary>
	public interface IScriptHelper
	{
		ILocalScriptHelper Local { get; }	
	}
	
	class RequestContextLocalHelper : ILocalScriptHelper
	{
		private readonly UrlHelper _UrlHelper;
		private readonly RequestContext _RequestContext;
		
		public RequestContext RequestContext { get { return _RequestContext; } }
		
		public RequestContextLocalHelper(RequestContext requestContext)
		{
			if (requestContext == null) throw new ArgumentNullException("requestContext");
			
			_RequestContext = requestContext;
			_UrlHelper = new UrlHelper(_RequestContext);
		}
		
		public string UrlContent(string contentPath)
		{
			return _UrlHelper.Content(contentPath);	
		}
		
		public string ServerPath(string contentPath)
		{
			return RequestContext.HttpContext.Request.MapPath(contentPath);
		}
	}
	
	class RequestContextHelper : IScriptHelper
	{
		private readonly RequestContext _RequestContext;
		private readonly ILocalScriptHelper _Local;
		
		public RequestContext RequestContext { get { return _RequestContext; } }
		public ILocalScriptHelper Local { get { return _Local; } }
		
		public RequestContextHelper(RequestContext requestContext)
		{
			if (requestContext == null) throw new ArgumentNullException("requestContext");
			
			_RequestContext = requestContext;
			_Local = new RequestContextLocalHelper(_RequestContext);
		}
	}
}
