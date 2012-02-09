using System;

namespace Keeper.OfScripts
{
	internal abstract class PageResource : IEquatable<PageResource>
	{
		private string _Source;
		
		public string Source { get { return _Source; } }
		
		public PageResource(string source)
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
			var other = obj as PageResource;
			return Equals(other);			
		}
		
		public override int GetHashCode()
		{
			return Source.GetHashCode();
		}
		
		public bool Equals(PageResource other)
		{
			if (other == null) return false;
			if (!other.Source.Equals(Source)) return false;
			if (other.GetType() != GetType()) return false;
			
			return true;
		}		
	}	
}
