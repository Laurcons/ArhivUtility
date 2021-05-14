using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArhivUtility {
	public class Range<T> {
		public T Begin { get; private set; }
		public T End { get; private set; }
		public bool IsSingle => Begin.Equals(End);

		public Range(T begin, T end) {
			Begin = begin;
			End = end;
		}
		public Range(T val) {
			Begin = End = val;
		}
	}
}
