using System;
using System.Linq;
using System.Web.Routing;

using Keeper.OfScripts.Configuration;

namespace Keeper.OfScripts
{
	/// <summary>
	/// Container to hold multiple <c>ScriptGroup</c> instances. Each group can be
	/// used to register particular scripts.
	/// </summary>
	public class ScriptKeeper
	{
		private readonly IScriptGroup _Local;
		private readonly IScriptGroup _Remote;
		private readonly IScriptGroup _Inline;
		private readonly IScriptHelper _Helper;
		
		/// <summary>
		/// Gets the <c>ScriptGroup</c> instance used to register
		/// scripts that reside on the local server.
		/// </summary>
		public IScriptGroup Local { get { return _Local; } }
		
		/// <summary>
		/// Gets the <c>ScriptGroup</c> instance used to register
		/// scripts that reside on remote servers, such as CDNs.
		/// </summary>
		public IScriptGroup Remote { get { return _Remote; } }
		
		/// <summary>
		/// Gets the <c>ScriptGroup</c> instance used to register
		/// inline scripts.
		/// </summary>
		/// <remarks>
		/// Scripts registered with this group should be short and unique.
		/// Use with caution (for now).
		/// </remarks>
		public IScriptGroup Inline { get { return _Inline; } }
		
		/// <summary>
		/// Gets the script helper object with which this <c>ScriptKeeper</c> was created.
		/// </summary>
		public IScriptHelper Helper { get { return _Helper; } }
		        
		/// <summary>
		/// Initializes a new instance of the <see cref="Keeper.OfScripts.ScriptKeeper"/> class.
		/// </summary>
		/// <param name='scriptHelper'>
		/// The script helper that is responsible for resolving paths, etc.
		/// </param>
		/// <exception cref='ArgumentNullException'>
		/// Thrown when <paramref name="scriptHelper"/> is <see langword="null" /> .
		/// </exception>
		public ScriptKeeper(IScriptHelper scriptHelper)
		{
			if (scriptHelper == null) throw new ArgumentNullException("scriptHelper");
			
			_Helper = scriptHelper;

			_Local = new LocalScriptGroup { Name = "Local", Helper = _Helper.Local };
			_Remote = new RemoteScriptGroup { Name = "Remote" };
			_Inline = new InlineScriptGroup { Name = "Inline" };
		}
		
		/// <summary>
		/// Registers the specified alias from the configuration file.
		/// </summary>
		/// <param name="alias">
		/// The alias whose source should be registered. The group with
		/// which the source is registered is determined by the residence
		/// value of the alias.
		/// </param>
		public void Register(string alias)
		{            
			var source = KeeperConfig.Instance.Section
                .Aliases
                .Cast<ResourceAliasElement>()
                .FirstOrDefault(a => a.Alias == alias);
            
            if (null == source) throw new ArgumentException("The alias '" + alias + "' does not exist in the configuration.");
            
            var residenceType = AliasSourceResidence.Remote;
            if (!string.IsNullOrEmpty(source.Residence))
                residenceType = (AliasSourceResidence)Enum.Parse(typeof(AliasSourceResidence), source.Residence, true);
            
            if (residenceType == AliasSourceResidence.Local)
                Local.Register(source.Source);
            else
                Remote.Register(source.Source);
		}
		
		/// <summary>
		/// Renders all instances of <c>ScriptGroup</c> in this container.
		/// The default order of rendering is: Remote, Local, Inline.
		/// </summary>
		public string Render()
		{
			var str = Remote.Render();
			str += Environment.NewLine + Local.Render();
			str += Environment.NewLine + Inline.Render();
			
			return str;
		}
	}
}
