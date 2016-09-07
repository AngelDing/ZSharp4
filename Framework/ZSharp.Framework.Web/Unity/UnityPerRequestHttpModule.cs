using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Microsoft.Practices.Unity
{
	public class UnityPerRequestHttpModule : IHttpModule
	{
		private static readonly object ModuleKey = new object();

		internal static object GetValue(object lifetimeManagerKey)
		{
			Dictionary<object, object> dictionary = GetDictionary(HttpContext.Current);
			if (dictionary != null)
			{
				object result = null;
				if (dictionary.TryGetValue(lifetimeManagerKey, out result))
				{
					return result;
				}
			}
			return null;
		}

		internal static void SetValue(object lifetimeManagerKey, object value)
		{
			Dictionary<object, object> dictionary = GetDictionary(HttpContext.Current);
			if (dictionary == null)
			{
				dictionary = new Dictionary<object, object>();
				HttpContext.Current.Items[ModuleKey] = dictionary;
			}
			dictionary[lifetimeManagerKey] = value;
		}

		public void Dispose()
		{
		}

		public void Init(HttpApplication context)
		{
			Guard.ArgumentNotNull(context, "context");
			context.EndRequest += new EventHandler(OnEndRequest);
		}

		private void OnEndRequest(object sender, EventArgs e)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			Dictionary<object, object> dictionary = GetDictionary(httpApplication.Context);
			if (dictionary != null)
			{
				foreach (IDisposable current in dictionary.Values.OfType<IDisposable>())
				{
					current.Dispose();
				}
			}
		}

		private static Dictionary<object, object> GetDictionary(HttpContext context)
		{
			if (context == null)
			{
				throw new InvalidOperationException(@"
                    The PerRequestLifetimeManager can only be used in the context of an HTTP request.
                    Possible causes for this error are using the lifetime manager on a non-ASP.NET application, 
                    or using it in a thread that is not associated with the appropriate synchronization context.");
			}
			return (Dictionary<object, object>)context.Items[ModuleKey];
		}
	}
}
