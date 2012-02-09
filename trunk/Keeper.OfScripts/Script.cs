using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Keeper.OfScripts
{
	internal abstract class Script : PageResource
	{
		public Script(string source) : base(source) { }
	}
	
	internal class LinkedScript : Script
	{
		public LinkedScript(string source) : base(source) { }
		
		public override string Render()
		{
			return "<script type=\"text/javascript\" src=\"" + Source + "\"></script>";		
		}
	}
	
	internal class InlineScript : Script
	{
		public InlineScript(string source) : base(source) { }
		
		public override string Render()
		{
			return "<script type=\"text/javascript\">" + Environment.NewLine + 
					"// <![CDATA[" + Environment.NewLine +
					Source + Environment.NewLine +
					"// ]]>" + Environment.NewLine +
					"</script>";
		}
	}
}
