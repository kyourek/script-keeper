using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;

namespace Keeper.OfScripts
{
	/// <summary>
	/// A group of scripts that is responsible for tracking which static scripts
	/// have been registered.
	/// </summary>
	public interface IScriptGroup : IPageResourceGroup { }
			
	internal class LocalScriptGroup : LocalResourceGroup<LinkedScript>, IScriptGroup
	{		
        protected override LinkedScript CreateResource(string source)
        {
            return new LinkedScript(source);
        }
	}
	
    internal class RemoteScriptGroup : RemoteResourceGroup<LinkedScript>, IScriptGroup
    {
        protected override LinkedScript CreateResource(string source)
        {
            return new LinkedScript(source);   
        }
    }
    	
	internal class InlineScriptGroup : PageResourceGroup<InlineScript>, IScriptGroup
	{
		public override bool HasRegistered(string resource)
		{
			return this.FirstOrDefault(s => s.Source == resource) != null;	
		}
		
		public override void Register(params string[] resources)
		{
			if (resources == null) throw new ArgumentNullException("resources");
			if (resources.Length < 1) throw new ArgumentException("At least one resource is required to register.");
			
			foreach (var script in resources)
				if (!HasRegistered(script))
					Add(new InlineScript(script));
		}
		
		public override string Render()
		{
			Func<IEnumerable<Script>, string> aggregate = coll =>
			{
				var str = string.Empty;
				foreach (var s in coll)
					str += string.IsNullOrEmpty(str) ? s.Source : Environment.NewLine + s.Source;	
				return str;
			};
			var rendered = "<script type=\"text/javascript\">" + Environment.NewLine;
			rendered += "// <![CDATA[" + Environment.NewLine;
			rendered += aggregate(this.Cast<Script>()) + Environment.NewLine;
			rendered += "// ]]>" + Environment.NewLine;
			rendered += "</script>";
			return rendered;
		}
	}
}
