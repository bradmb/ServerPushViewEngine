# Server Push View Engine

## Introduction
I'm a big fan of HTTP/2. We run NGINX in front of our services with HTTP/2 enabled, and
I've been waiting for them to announce support for server push when using NGINX as a proxy.

With the release of NGINX 1.13.9, that day has come. So to celebrate, I created a wrapper for the
ASP.NET Razor view engine so you can dynamically add the HTTP/2 preload headers to your project without
having to write custom header code for every page that you want to have javascript, images, or css files preloaded.

## Installation
Simplty add this project to your solution and reference it in your main project.
Then in your Glboal.asax.cs file, add this to the bottom of your Application_Start():

```
ViewEngines.Engines.Clear();
ViewEngines.Engines.Add(new PushViewEngine());
```

## Enabling On Specific Files
If you don't want all of your script, image, and style tags to automatically be included in the HTTP/2 preload headers, you can disable automatic inclusion by initializing the PushViewEngine() like so:

```
ViewEngines.Engines.Clear();
ViewEngines.Engines.Add(new PushViewEngine(requirePrefix: true));
```

Then, in your tags that you want included in the HTTP/2 preload, simply append this tag:
```
data-http2-push="true"
```

So, on a script tag, this would look like:

```
<script src="myscript.js" data-http2-push="true" type="application/javascript"></script>
```

## NuGet Package Coming Soon
This will be eventually added into NuGet as a package you can install
