using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNotepad.Helpers
{
	public static class ServiceLocator
	{
		public static IServiceLocator Current
		{
			get
			{
				if (!ServiceLocator.IsLocationProviderSet)
				{
					throw new InvalidOperationException(" ServiceLocationProvider must be set.");
				}
				return ServiceLocator._currentProvider();
			}
		}

		public static void SetLocatorProvider(ServiceLocatorProvider newProvider)
		{
			ServiceLocator._currentProvider = newProvider;
		}

		public static bool IsLocationProviderSet
		{
			get
			{
				return ServiceLocator._currentProvider != null;
			}
		}

		private static ServiceLocatorProvider _currentProvider;
	}

	public delegate IServiceLocator ServiceLocatorProvider();
}
