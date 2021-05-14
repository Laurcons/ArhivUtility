using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArhivUtility {
	public static class Extension {
        public static IEnumerable<(int begin, int end)> Ranges(this IEnumerable<int> nums) {
            var e = nums.GetEnumerator();
            if (e.MoveNext()) {
                var begin = e.Current;
                var end = begin + 1;
                while (e.MoveNext()) {
                    if (e.Current != end) {
                        yield return (begin, end);
                        begin = end = e.Current;
                    }
                    end++;
                }
                yield return (begin, end);
            }
        }
    }
}
