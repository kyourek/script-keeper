<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"> 
    <title>ScriptKeeper Example</title>
    <link rel="stylesheet" type="text/css" href="<%= Url.Content("~/Scripts/fancybox/jquery.fancybox-1.3.4.css") %>" />
</head>
<body>
    <div>
    <%
        // We'll render a few pictures that use the fancybox jQuery plugin here.
        // Notice that none of the scripts required to use fancybox are linked on this page.
        // They are taken care of in the partial view.
        var pic1 = new PictureModel { Id = "pic1", Title = "Picture1", Source = "~/Content/Images/dscf1907.jpg", Thumb = "~/Content/Images/dscf1907_thumb.jpg" };
        var pic2 = new PictureModel { Id = "pic2", Title = "Picture2", Source = "~/Content/Images/dscf1919.jpg", Thumb = "~/Content/Images/dscf1919_thumb.jpg" };
        var pic3 = new PictureModel { Id = "pic3", Title = "Picture3", Source = "~/Content/Images/dscf1921.jpg", Thumb = "~/Content/Images/dscf1921_thumb.jpg" };

        foreach (var pic in new[] { pic1, pic2, pic3 })
        {
            Html.RenderPartial("Picture", pic);
        }
    %>
    
    </div>
    
    <%=
        // Render the ScriptKeeper's output just before the closing body tag.
        Html.ScriptKeeper().Render()
    %>
</body>
</html>
