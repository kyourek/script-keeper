using System;
using System.IO;
using System.Web.Routing;

namespace Keeper.OfScripts
{
	internal class StyleKeeper
	{
		private readonly IStyleGroup _Local;
		private readonly IStyleGroup _Embedded;
		private readonly IStyleHelper _Helper;

		public IStyleGroup Local { get { return _Local; } }
		public IStyleGroup Embedded { get { return _Embedded; } }
		public IStyleHelper Helper { get { return _Helper; } }

		public StyleKeeper(IStyleHelper styleHelper)
		{
			if (styleHelper == null) throw new ArgumentNullException("styleHelper");
			_Helper = styleHelper;
			_Local = new LocalStyleGroup { Name = "Local", Helper = _Helper.Local };
			_Embedded = new EmbeddedStyleGroup { Name = "Embedded" };
		}

		public string Render()
		{
			var str = Embedded.Render();
			str += Environment.NewLine + Local.Render();
            
			return str;
		}
	}
}
