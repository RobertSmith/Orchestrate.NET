Orchestrate.NET
===============

.NET Library for Orchestrate

[![Build status](https://ci.appveyor.com/api/projects/status/c40ejfyfohxjblet)](https://ci.appveyor.com/project/RobertSmith/orchestrate-net)

Installation
------------
PM> Install-Package Orchestrate.NET

Running Tests
-------------
Orchestrate.NET runs against the actual Orchestrate API. Go to Orchestrate.io and set up an account. Create a new application and use the API key provided. Also make sure your application is set to use the US datacenter, the EU datacenter is not yet supported for testing.

Docs
----
Please refer to the docs at orchestrate.io for more details.

Creating a Client
-----------------
var orchestrate = new Orchestrate.Net.Orchestrate(<api key>);

Creating a collection
---------------------
Creating a collection requires an object to be placed in the collection. No empty collections allowed.

var collection = orchestrate.CreateCollection(collectionName, key, item);

or you can pass in a json string:

var collection = orchestrate.CreateCollection(collectionName, key, json);

Get
---
You can retrieve items by key with the Get method:

Result result = orchestrate.Get(collectionName, key);

var item = result.Value;

Put
---
You can add items to your collection via the Put method:

Result result = orchestrate.Put(collectionName, key, item);

or you can pass in a json string:

var collection = orchestrate.Put(collectionName, key, json);

Patch
---
You can do a lot of things with your key/value collection via the Patch method:

Result result = orchestrate.Patch(collectionName, key, item);

The Patch method is very extensive and you should take a look at the Orchestrate api reference first.

Search
------
Searching a collection is as simple as defining a Lucene query string. (http://lucene.apache.org/core/4_3_0/queryparser/org/apache/lucene/queryparser/classic/package-summary.html#Overview)

SearchResult result = orchestrate.Search(collectionName, query, limit, offset);

Lists, Refs, Events and Graphs
-----------------------
Are all supported, see the docs on the Orchestrate.io website for details.

## Contributing

#### Building the source

For running tests follow either of the approaches
  - update the `app.config` file in the path `Orchestrate.Net.Tests/` and replace **YOUR-API-KEY-GOES-HERE** with your api key (remember to unset this value before you commit/push your code to repository).

  - set the environment variable `OrchestrateApiKey` value to match your api key (if you set this after you have opened Visual Studio, please restart Visual Studio, otherwise the environment variable will not be available).

  If both the above settings are set, then the value set in environment variable will be used.
