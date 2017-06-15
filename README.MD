Acando.AspNet.NodeServices
===========

This is a fork of [Microsoft.AspNetCore.NodeServices](https://github.com/aspnet/JavaScriptServices/tree/dev/src/Microsoft.AspNetCore.NodeServices) with the intention of making if work for non-Core AspNet.

Requirements
-------------------------
Install node.js - REBOOT

Installation
-------------------------
Install from nuget.

`Install-Package Acando.AspNet.NodeServices`

Configuration
-------------------------
There are a few appsettings you can add in your web app (but everything should work without them):

* `NodeDefaultInvocationTimeoutMilliseconds` - Overrides the default invocation time; defaults to `60 * 1000`.
  * `<add key="NodeDefaultInvocationTimeoutMilliseconds" value="20000" />`   
* `node_env`- Sets the node_env (production/development); defaults to `development`
  * `<add key="node_env" value="production" />`   
* `BABEL_CACHE_PATH`- Sets the cache path for babel; defaults to `projectPath + "../babel.json"`  
  * Useful when deploying to azure as web app; set as example below
  * `<add key="BABEL_CACHE_PATH" value="d:\Local\Temp\babel.json" />`   
* `WEBSITE_NODE_DEFAULT_VERSION` - Sets the default node version to use; defaults to `8.1.1`    
  * `<add key="WEBSITE_NODE_DEFAULT_VERSION" value="8.1.1" />`   
* `NodePath` - Sets the path to node.exe; defaults to "node" which assumes it's in the path.
  * `<add key="NodePath" value="c:\Program Files\nodejs\node" />`   
