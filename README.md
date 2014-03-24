Orchestrate.NET
===============

.NET Library for Orchestrate


Installation
------------
PM> Install-Package Orchestrate.NET

Running Tests
-------------
Orchestrate.NET runs against the actual Orchestrate API. Go to Orchestrate.io and set up an account. Create a new application and use the API key provided.

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

Search
------
Searching a collection is as simple as defining a Lucene query string. (http://lucene.apache.org/core/4_3_0/queryparser/org/apache/lucene/queryparser/classic/package-summary.html#Overview)

SearchResult result = orchestrate.Search(collectionName, query, limit, offset);

Lists, Refs, Events and Graphs
-----------------------
Are all supported, see the docs on the Orchestrate.io website for details.

## Contributing

#### Building the source

For running tests, be sure to update the `app.config` file in the path `Orchestrate.Net.Tests/` and replace **YOUR-API-KEY-GOES-HERE** with your api key.
