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
	public interface ILocalScriptHelper : ILocalResourceHelper { }
	
    public interface ILocalStyleHelper : ILocalResourceHelper { }
    
	/// <summary>
	/// Helper object for resolving paths, etc., for the <c>ScriptKeeper</c>.
	/// </summary>
	public interface IScriptHelper
	{
		ILocalScriptHelper Local { get; }	
	}
	
    public interface IStyleHelper
    {
        ILocalStyleHelper Local { get; }   
    }
    
	class RequestContextLocalHelper : ILocalResourceHelper, ILocalScriptHelper, ILocalStyleHelper
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
	
	class RequestContextHelper : IScriptHelper, IStyleHelper
	{
		private readonly RequestContext _RequestContext;
		private readonly RequestContextLocalHelper _Local;
		
		public RequestContext RequestContext { get { return _RequestContext; } }
		public RequestContextLocalHelper Local { get { return _Local; } }
		
		public RequestContextHelper(RequestContext requestContext)
		{
			if (requestContext == null) throw new ArgumentNullException("requestContext");
			
			_RequestContext = requestContext;
			_Local = new RequestContextLocalHelper(_RequestContext);
		}
        
        ILocalScriptHelper IScriptHelper.Local
        {
            get { return Local; }   
        }
        
        ILocalStyleHelper IStyleHelper.Local
        {
            get { return Local; }   
        }
	}
}
