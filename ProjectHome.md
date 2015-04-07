**ScriptKeeper** keeps track of scripts.

Install with **NuGet**!
```
PM> Install-Package ScriptKeeper 
```

It's an **HtmlHelper** extension method designed to allow a script to be registered multiple times during an MVC action. That script will be rendered only once in the final page.

It's most useful in partial views that require a JavaScript file that hasn't been linked in the parent view.

Imagine a partial view that declares a `<div>` for a model. That view requires some JavaScript file to add content to the `<div>`. But, the parent of that partial view doesn't know that the JavaScript file needs to be included. With **ScriptKeeper**, the solution is simple:

```
<% Html.ScriptKeeper().Local.Register("~/script/required/by/partialview.js"); %>

<div id="<%= Model.Id %>"></div>
```

And, in the parent view:

```
<% foreach (var item in Model) { %>
    <% Html.RenderPartial("PartialView", item); %>
<% } %>

<%= Html.ScriptKeeper().Render() %>
```

Regardless of how many `item` are in `Model`, the `partialview.js` script will only appear once on the page:

```
<script type="text/javascript" src="/script/required/by/partialview.js"></script>
```

Aliases are available in the Web.config file:

```
<% Html.ScriptKeeper().Register("jQuery"); %>
```

**And there's more...**

**ScriptKeeper** can also consolidate inline scripts in partial views. Imagine the previous `<div>` needs a JavaScript function call to get its content loaded. Something like this:

```
<script type="text/javascript">
    loadContent('<%= Model.Id %>');
</script>
```

Each call to `loadContent` can be registered in the partial view:

```
<% Html.ScriptKeeper().Local.Register("~/script/required/by/partialview.js"); %>
<% Html.ScriptKeeper().Inline.Register("loadContent('" + Model.Id + "');"); %>

<div id="<%= Model.Id %>"></div>
```

and rendered at the bottom of the page:
```
<%= Html.ScriptKeeper().Render() %>
```

which produces:

```
<div id="1"></div>
<div id="2"></div>
<div id="3"></div>
<!-- etc. -->
<script type="text/javascript" src="/script/required/by/partialview.js"></script>
<script type="text/javascript">
// <![CDATA[
    loadContent('1');
    loadContent('2');
    loadContent('3');
    // etc.
// ]]>
</script>
```

To use **ScriptKeeper**, include the namespace `Keeper.OfScripts.Html` in `Web.config`:

```
<namespaces>
    <!-- Other included namespaces will appear here. -->

    <!-- Include the Keeper.OfScripts.Html namespace to use ScriptKeeper. -->
    <add namespace="Keeper.OfScripts.Html" />
</namespaces>
```