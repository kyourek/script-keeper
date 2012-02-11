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
			return "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + Source + "\" />";
		}
	}
	
	internal class EmbeddedStyle : Style
	{
		public EmbeddedStyle(string source) : base(source) { }
		
		public override string Render()
		{
			var s = "<style type=\"text/css\">" + Environment.NewLine;
			s += Source + Environment.NewLine;
			s += "</style>";			
			return s;
		}
	}
}
