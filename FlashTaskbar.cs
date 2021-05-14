using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;

namespace ArhivUtility {
	public static class FlashTaskbarExt {
		[DllImport("user32.dll")]
		private static extern bool FlashWindow(IntPtr hwnd, bool bInvert);
		
		public static void FlashTaskbar(this Form form) {
			FlashWindow(form.Handle, true);
		}
	}
}
