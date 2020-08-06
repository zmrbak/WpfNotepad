using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WpfNotepad.Helpers
{
	[Serializable]
	public class ActivationException : Exception
	{
		public ActivationException()
		{
		}

		public ActivationException(string message) : base(message)
		{
		}

		public ActivationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ActivationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
