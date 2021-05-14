using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArhivUtility {
	public class DataFormatException : Exception {
		public string Action { get; set; }

		public DataFormatException() : base() { }
		public DataFormatException(string message) : base(message) { }
		public DataFormatException(string message, Exception inner) : base(message, inner) { }
		public DataFormatException(string message, string action) : base(message) {
			Action = action;
		}
		public DataFormatException(string message, string action, Exception inner) : base (message, inner) {
			Action = action;
		}
	}
}
