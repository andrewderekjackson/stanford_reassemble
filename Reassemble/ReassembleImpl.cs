using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reassemble {

    public class ReassembleImpl {

        /// <summary>
        /// Calculates the offset of the two strings and returns the results.
        /// </summary>
        public FragmentResult CalculateOffset(string string1, string string2) {

            int maxOverlapCount = 0;
            int maxOverlapIndex = 0;

            // make sure the longest string is first
            string s1 = string1.Length >= string2.Length ? string1 : string2;
            string s2 = string1.Length >= string2.Length ? string2 : string1;

            bool found = false;

            for (int i = s2.Length; i > 0 ; i--) {
                string needle = s2.Substring(0, i);
                int index = s1.IndexOf(needle, StringComparison.Ordinal);

                if (index > -1) {
                    // matched
                    maxOverlapIndex = index;
                    maxOverlapCount = needle.Length;
                    break;
                }
            }

            for (int i = 0; i < s2.Length; i++) {
                string needle = s2.Substring(i);
                int index = s1.IndexOf(needle, StringComparison.Ordinal);

                if (index > -1) {

                    if (maxOverlapCount < needle.Length) {
                        maxOverlapIndex = index;
                        maxOverlapCount = needle.Length;
                        break;
                    }
                }
            }

            return new FragmentResult() {
                String1 = string1,
                String2 = string2,
                AlignmentIndex = maxOverlapIndex,
                Overlap = maxOverlapCount
            };

        }
    }
}

