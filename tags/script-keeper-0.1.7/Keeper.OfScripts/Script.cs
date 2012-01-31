using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Keeper.OfScripts
{
	internal abstract class Script : IEquatable<Script>
	{
		private string _Source;
		
		public string Source { get { return _Source; } }
		
		public Script(string source)
		{
			if (source == null) throw new ArgumentNullException("source");
			_Source = source;
		}
		
		public abstract string Render();
		
		public override string ToString()
		{
			return Source;
		}
		
		public override bool Equals(object obj)
		{
			var other = obj as Script;
			return Equals(other);			
		}
		
		public override int GetHashCode()
		{
			return Source.GetHashCode();
		}
		
		public bool Equals(Script other)
		{
			if (other == null) return false;
			if (!other.Source.Equals(Source)) return false;
			if (other.GetType() != GetType()) return false;
			
			return true;
		}		
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
