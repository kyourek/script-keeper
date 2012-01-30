using System;
using System.Web.Routing;

namespace Keeper.OfScripts
{
	public class ScriptKeeper
	{
		private readonly IScriptGroup _Local;
		private readonly IScriptGroup _Remote;
		private readonly IScriptGroup _Inline;
		private readonly IScriptHelper _Helper;

		public IScriptGroup Local { get { return _Local; } }
		public IScriptGroup Remote { get { return _Remote; } }
		public IScriptGroup Inline { get { return _Inline; } }
		public IScriptHelper Helper { get { return _Helper; } }
		
		public ScriptKeeper(IScriptHelper scriptHelper)
		{
			if (scriptHelper == null) throw new ArgumentNullException("scriptHelper");
			
			_Helper = scriptHelper;

			_Local = new LocalScriptGroup { Name = "Local", Helper = _Helper.Local };
			_Remote = new RemoteScriptGroup { Name = "Remote" };
			_Inline = new InlineScriptGroup { Name = "Inline" };
		}
		
		public string Render()
		{
			var str = Remote.Render();
			str += Environment.NewLine + Local.Render();
			str += Environment.NewLine + Inline.Render();
			
			return str;
		}
	}
}
