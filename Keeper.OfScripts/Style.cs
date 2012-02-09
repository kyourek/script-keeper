using System;

namespace Keeper.OfScripts
{
	internal abstract class Style : PageResource
	{
		public Style(string source) : base(source) { }
	}
	
	internal class LinkedStyle : Style
	{
		public LinkedStyle(string source) : base(source) { }
		
		public override string Render()
		{
			return "<link rel='stylesheet' type='text/css' href='" + Source + "' />";
		}
	}
	
	internal class HeadStyle : Style
	{
		public HeadStyle(string source) : base(source) { }
		
		public override string Render()
		{
			var s = "<style type='text/css'>";
			s += Source;
			s += "</style>";			
			return s;
		}
	}
}

