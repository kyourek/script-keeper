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
		
	internal abstract class ScriptGroup<TScript> : IScriptGroup, IEnumerable<TScript>
		where TScript : Script
	{
		private readonly List<TScript> _List = new List<TScript>();	
				
		private string _Name;
		
		public int Count { get { return _List.Count; } }
		
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		
		public abstract bool HasRegistered(string script);		
		public abstract void Register(params string[] scripts);
		
		public virtual string Render()
		{
			var str = string.Empty;

			foreach (var script in _List)
			{
				str += string.IsNullOrEmpty(str) ? script.Render() : Environment.NewLine + script.Render();
			}
			
			return str;
		}
		
		public void Add(TScript script)
		{
			if (_List.Contains(script)) throw new ScriptAlreadyAddedException("The script has already been added.");
			_List.Add(script);	
		}
		
		public void Register(params TScript[] scripts)
		{
			foreach (var script in scripts)
			{
				if (!HasRegistered(script))
				{
					Add(script);	
				}
			}
		}
		
		public bool HasRegistered(TScript script)
		{
			return _List.Contains(script);	
		}		
		
		public IEnumerator<TScript> GetEnumerator()
		{
			return _List.GetEnumerator();
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _List.GetEnumerator();
		}
	}
	
	internal class LocalScriptGroup : ScriptGroup<LinkedScript>
	{		
		private ILocalScriptHelper _Helper;		
		
		public ILocalScriptHelper Helper 
		{ 
			get { return _Helper; } 
			set { _Helper = value; }
		}		
		
		public override bool HasRegistered(string script)
		{
			if (Helper == null) throw new InvalidOperationException("Cannot resolve a URL with a null KeeperHelper.");
			
			var source = Helper.UrlContent(script);
			return this.FirstOrDefault(s => s.Source == source) != null;
		}		
		
		public override void Register(params string[] scripts)
		{
			if (scripts == null) throw new ArgumentNullException("scripts");
			if (scripts.Length < 1) throw new ArgumentException("At least one script is required to register.");
			
			if (Helper == null) throw new InvalidOperationException("Cannot resolve a URL with a null KeeperHelper.");
			
			foreach (var script in scripts)
			{
				if (!HasRegistered(script)) 
				{
					var path = Helper.UrlContent(script);
					var file = Helper.ServerPath(script);
					
					if (!File.Exists(file)) throw new ScriptNotFoundException("The script '" + path + "' does not exist.");
					
					Add(new LinkedScript(path));
				}
			}
		}
	}
	
	internal class RemoteScriptGroup : ScriptGroup<LinkedScript>
	{
		public override bool HasRegistered(string script)
		{
			return this.FirstOrDefault(s => s.Source == script) != null;
		}
		
		public override void Register(params string[] scripts)
		{
			if (scripts == null) throw new ArgumentNullException("scripts");
			if (scripts.Length < 1) throw new ArgumentException("At least one script is required to register.");
			
			foreach (var script in scripts)
			{
				if (!HasRegistered(script))
				{
					Add(new LinkedScript(script));					
				}
			}
		}
	}
	
	internal class InlineScriptGroup : ScriptGroup<InlineScript>
	{
		public override bool HasRegistered(string script)
		{
			return this.FirstOrDefault(s => s.Source == script) != null;	
		}
		
		public override void Register(params string[] scripts)
		{
			if (scripts == null) throw new ArgumentNullException("scripts");
			if (scripts.Length < 1) throw new ArgumentException("At least one script is required to register.");
			
			foreach (var script in scripts)
			{
				if (!HasRegistered(script))
				{
					Add(new InlineScript(script));
				}
			}
		}
		
		public override string Render()
		{
			Func<IEnumerable<Script>, string> aggregate = coll =>
			{
				var str = string.Empty;
				
				foreach (var s in coll)
				{
					str += string.IsNullOrEmpty(str) ? s.Source : Environment.NewLine + s.Source;	
				}
				
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
