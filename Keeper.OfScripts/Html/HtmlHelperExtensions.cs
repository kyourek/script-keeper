using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Keeper.OfScripts.Html
{
	public static class HtmlHelperExtensions
	{
		private static readonly string Key = typeof(ScriptKeeper).AssemblyQualifiedName;
		
		public static ScriptKeeper ScriptKeeper(this HtmlHelper html)
		{
			if (html == null) throw new ArgumentNullException("html");
			
			var viewContext = html.ViewContext;
			var httpContext = viewContext.HttpContext;
			var scriptKeeper = httpContext.Items[Key] as ScriptKeeper;
			
			if (scriptKeeper == null)
			{
				var keeperHelper = new RequestContextHelper(viewContext.RequestContext);
				
				httpContext.Items[Key] = scriptKeeper = new ScriptKeeper(keeperHelper);
			}
			
			return scriptKeeper;
		}
	}
}
