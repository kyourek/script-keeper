<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Keeper.OfScripts.Example.Models.VideoModel>" %>

<a
	href="<%= Model.Source %>"
	style="display:block;width:425px;height:300px;"
	id="<%= Model.Id %>">
</a>

<%
	/*
	 * Register the flowplayer script. This script is necessary for the video to play
	 * in the flowplayer flash player. We can use the same syntax for path resolution
	 * as we'd use with a UrlHelper.
	*/
	Html.ScriptKeeper().Local.Register("~/Scripts/flowplayer/example/flowplayer-3.2.6.min.js");
	
	/*
	 * Register the Javascript line required to install each specific video in its player.
	 * The line looks like this: flowplayer('videoId', 'path/to/flowplayer-3.2.7.swf');
	 *
	 * These lines will be combined into one script tag when we call Html.ScriptKeeper().Render().
	 */
	Html.ScriptKeeper().Inline.Register("flowplayer('" + Model.Id + "', '" + Url.Content("~/Scripts/flowplayer/flowplayer-3.2.7.swf") + "');");
%>
