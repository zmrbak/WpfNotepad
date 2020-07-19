using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfNotepad.Interactivity
{
	public interface IAttachedObject
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		DependencyObject AssociatedObject { get; }

		// Token: 0x06000002 RID: 2
		void Attach(DependencyObject dependencyObject);

		// Token: 0x06000003 RID: 3
		void Detach();
	}
}
