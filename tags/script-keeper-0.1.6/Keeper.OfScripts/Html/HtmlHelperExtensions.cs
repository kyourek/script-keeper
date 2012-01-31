using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Keeper.OfScripts.Html
{
	/// <summary>
	/// HtmlHelper extensions for the <c>ScriptKeeper</c> class.
	/// </summary>
	public static class HtmlHelperExtensions
	{
		private static readonly string Key = typeof(ScriptKeeper).AssemblyQualifiedName;

		/// <summary>
		/// Returns an instance of <c>ScriptKeeper</c> that can be used to register
		/// script files in the current context.
		/// </summary>
		/// <returns>
		/// The instance of <c>ScriptKeeper</c>.
		/// </returns>
		/// <param name='html'>
		/// The instance of <c>HtmlHelper</c> that will return a <c>ScriptKeeper</c>.
		/// </param>
		/// <exception cref='ArgumentNullException'>
		/// Thrown if <paramref name="html"/> is <see langword="null" /> .
		/// </exception>
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
