using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;

namespace Keeper.OfScripts
{
	public interface ILocalScriptHelper
	{
		string ServerPath(string contentPath);
		string UrlContent(string contentPath);
	}
	
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
