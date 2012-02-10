using System;
using System.Linq;

namespace Keeper.OfScripts
{
	public interface IStyleGroup : IPageResourceGroup { }
	
	internal class LocalStyleGroup : LocalResourceGroup<LinkedStyle>, IStyleGroup
	{
		protected override LinkedStyle CreateResource(string source)
		{
			return new LinkedStyle(source);
		}
	}
	
	internal class EmbeddedStyleGroup : PageResourceGroup<EmbeddedStyle>, IStyleGroup
	{
		public override bool HasRegistered(string resource)
		{
			return this.FirstOrDefault(s => s.Source == resource) != null;
		}
		
		public override void Register(params string[] resources)
		{
			if (resources == null) throw new ArgumentNullException("resources");
			if (resources.Length < 1) throw new ArgumentException("At least one resource is required to register.");
			
			foreach (var style in resources)
				if (!HasRegistered(style))
					Add(new EmbeddedStyle(style));
		}
		
		public override string Render()
		{
			var str = "<style type=\"text/css\">" + Environment.NewLine;
			foreach (var style in this)
				str += style.Source + Environment.NewLine;
			str += "</style>";
			return str;
		}
	}
}
