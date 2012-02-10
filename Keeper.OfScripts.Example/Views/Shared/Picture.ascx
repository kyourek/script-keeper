<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Keeper.OfScripts.Example.Models.PictureModel>" %>

<%     
    // Register jQuery (fancybox requires it) from Google's CDN using the remote ScriptKeeper
    Html.ScriptKeeper().Remote.Register("//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js");
    
    // We can use other jQuery plugins by registering them with the local ScriptKeeper.
    Html.ScriptKeeper().Local.Register("~/Scripts/fancybox/jquery.easing-1.3.pack.js");
    Html.ScriptKeeper().Local.Register("~/Scripts/fancybox/jquery.mousewheel-3.0.4.pack.js");
            
    // And, of course, we need the FancyBox script
    Html.ScriptKeeper().Local.Register("~/Scripts/fancybox/jquery.fancybox-1.3.4.js");
    
    // Finally, load the image in the fancybox using the inline ScriptKeeper.
    Html.ScriptKeeper().Inline.Register("$('#" + Model.Id + "').fancybox();");
%>

<a id="<%= Model.Id %>" href="<%= Url.Content(Model.Source) %>">
    <img alt="<%= Model.Title %>" src="<%= Url.Content(Model.Thumb) %>" />
</a>
