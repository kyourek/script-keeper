<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Keeper.OfScripts.Example.Models.VideoModel>>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>ScriptKeeper Example</title>
</head>
<body>
	<div>
		<!--
			This is an example .NET MVC page using the 
			Keeper.OfScripts.ScriptKeeper javascript registrar.
			
			Don't forget to include the namespace Keeper.OfScripts.Html
			in your Web.config so the ScriptKeeper extension method is
			available to the view's HtmlHelper instance.
			
			Example:
			
			<namespaces>
				(Other namespaces will be included here.)
			
				<add namespace="Keeper.OfScripts.Html" />		
			</namespaces>				
		-->
		<h2 style="color:gray;font-family:sans-serif;">ScriptKeeper Example</h2>
	</div>
		
	<div>
		<%
			/*
			 * We're going to render the "Video" partial view
			 * multiple times (once for each video in the model).
			 * The required scripts for installing the video player
			 * are included in the partial view, so we don't have
			 * to worry about them here (the partial views are
			 * autonomous).
			 */
		%>
			
		<% foreach (var video in Model) { %>
			<div style="margin-top:15px;">
				<% Html.RenderPartial("Video", video); %>
			</div>
		<% } %>
	</div>
	
	<% 
		/*
		 * We'll register the jQuery library using Google's CDN with our Remote script group.
		*/
		Html.ScriptKeeper().Remote.Register("//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"); 
	%>	
	
	<!-- 
		Render the script output of the keeper here (immediately before
		the closed body tag (</body>)). 
		The default behavior is to render scripts in the following order:
		- Remote
		- Local
		- Inline
		
		Notice that the rendered output will only contain one reference
		to the flowplayer javascript file even though we registered it
		multiple times (once for each partial view rendered).
		
		Also, each video-player installation line is included in a single
		inline script tag.
	-->
	<%= Html.ScriptKeeper().Render() %>
</body>

