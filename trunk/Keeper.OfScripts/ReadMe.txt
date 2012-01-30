ScriptKeeper is an HtmlHelper extension method that tracks registered scripts.

//////////////////////////////////////////////////////////////////////////////
License:

Copyright (c) 2012 Ken Yourek

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//////////////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////////////
Description:

Its main purpose is to be used in .NET MVC partial views in which scripts are
required. Normally, partial views that require scripts would have to have
their scripts included in the parent view so as not to include the script
each time the partial view is rendered.

When a script is registered with one of the ScriptKeeper's script groups, the
ScriptKeeper remembers the source of the script and will only render it once
when the Render method is called. That means a partial view that registers a
script can be rendered multiple times on the same page, and the required script
will only be included once.

The ScriptKeeper can also be used to register short inline scripts. These scripts
will be rendered in their own script tag when the Render method is called.

See the example MVC app called Keeper.OfScripts.Example in this solution for
more information.
//////////////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////////////
Use:

Reference Keeper.OfScripts.dll in your MVC project. Then, add the proper namespace
to the 'namespaces' section in your project's Web.config so the HtmlHelper
ScriptKeeper extension method is available.

    <namespaces>
        .
        .
        .
        <add namespace="Keeper.OfScripts.Html" />
    </namespaces>

Register scripts that live on your server using the 'Local' script group:

    Html.ScriptKeeper().Local.Register("~/path/to/local/script.js");

Register short inline scripts using the 'Inline' script group:

    Html.ScriptKeeper().Inline.Register("var s = 'Hello World!';");
    
Register remote scripts, i.e. scripts from a CDN, using the 'Remote' script group:

    Html.ScriptKeeper().Remote.Register("//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js");
    
Render the output of your registered scripts using the Render method. This should
usually be done immediately before the closing body (</body>) tag.

	Html.ScriptKeeper().Render();
//////////////////////////////////////////////////////////////////////////////
