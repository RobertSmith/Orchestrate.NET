﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Orchestrate.Net
{
	public static class StringExtensions
	{
		private static readonly IDictionary<string, HttpMethod> HttpMethodsMap = new Dictionary<string, HttpMethod>
		{
			{"GET",  HttpMethod.Get},
			{"DELETE",  HttpMethod.Delete},
			{"HEAD",  HttpMethod.Head},
			{"OPTIONS",  HttpMethod.Options},
            {"PATCH", new HttpMethod("PATCH")},
			{"POST",  HttpMethod.Post},
			{"PUT",  HttpMethod.Put},
			{"TRACE",  HttpMethod.Trace}
		};

		//TODO: (CV) This extension is temporary (to use System.Net.Http). Once Orchestarte class is rafactored this should go away.
		internal static HttpMethod ToHttpMethod(this string source)
		{
			var needle = source.ToUpper();
			return HttpMethodsMap.ContainsKey(needle) ? HttpMethodsMap.FirstOrDefault(a => a.Key == needle).Value : null;
		}
	}
}