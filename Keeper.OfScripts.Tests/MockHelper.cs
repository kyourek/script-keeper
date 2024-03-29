using System;
using System.IO;
using System.Reflection;

namespace Keeper.OfScripts.Tests
{
	class MockLocalHelper : ILocalScriptHelper, ILocalStyleHelper
	{
		public string UrlContent(string contentPath)
		{
			return ServerPath(contentPath);
		}	
		
		public string ServerPath(string contentPath)
		{
			var path = contentPath;
			if (path.StartsWith("~"))
				path = path.Replace("~", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
			
			return path;
		}
	}
	
	class MockHelper : IScriptHelper, IStyleHelper
	{
		private readonly MockLocalHelper _Local = new MockLocalHelper();
		
		public ILocalScriptHelper Local { get { return _Local; } }		
		
		ILocalStyleHelper IStyleHelper.Local { get { return _Local; } }
	}	
}

