using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Reassemble {

    public class ReassembleImpl {

        /// <summary>
        /// Calculates the offset of the two strings and returns the results.
        /// </summary>
        public int CalculateOverlap(string string1, string string2) {

            if (string1 == null || string2 == null) {
                throw new ArgumentNullException("string1 or string2 cannot be null.");
            }

            int maxOverlap = -1;

            string s1 = string1.Length >= string2.Length ? string1 : string2;
            string s2 = string1.Length >= string2.Length ? string2 : string1;

            // pad the strings so we can merge using positive indexes
            s1 = string1.PadLeft(s1.Length + s2.Length - 1);
            s1 = s1.PadRight(s1.Length + s2.Length - 1);

            for (int s1Index = 0; s1Index < s1.Length - s2.Length; s1Index++) {

                bool matching = false;
                int overlap = 0;

                for (int index = 0; index < s2.Length; index++) {

                    char s1char = s1[s1Index + index];
                    char s2char = s2[index];

                    if (s1char == s2char) {
                        matching = true;
                        overlap++;
                    } else {
                        if (matching) {
                            break;
                        }
                    }
                }

                if (overlap > maxOverlap) {
                    maxOverlap = overlap;
                }

            }

            return maxOverlap;


            //// search for best match reducing from the right
            //for (int i = string2.Length; i > 0; i--) {
            //    string needle = string2.Substring(0, i);
            //    int index = string1.IndexOf(needle, StringComparison.Ordinal);

            //    if (index > -1) {
            //        // matched
            //        maxOverlap = needle.Length;
            //        break;
            //    }
            //}

            //// search for best match reducing from the left
            //for (int i = 0; i < string2.Length; i++) {
            //    string needle = string2.Substring(i);
            //    int index = string2.IndexOf(needle, StringComparison.Ordinal);

            //    if (index > -1) {
            //        if (maxOverlap < needle.Length) {
            //            maxOverlap = needle.Length;
            //            break;
            //        }
            //    }
            //}

            //return maxOverlap;
        }

        public string Merge(FragmentResult result) {
            return Merge(result.String1, result.String2);
        }

        public string Merge(string string1, string string2) {

            if (string1 == null || string2 == null) {
                throw new ArgumentNullException("string1 or string2 cannot be null.");
            }

            string origs1 = string1.Length >= string2.Length ? string1 : string2;
            string s2 = string1.Length >= string2.Length ? string2 : string1;

            // pad the strings so we can merge using positive indexes
            string s1 = string1.PadLeft(origs1.Length + s2.Length - 1);
            s1 = s1.PadRight(s1.Length + s2.Length - 1);

            int maxIndex = -1;
            int maxOverlap = 0;

            for (int s1Index = 0; s1Index < s1.Length - s2.Length; s1Index++) {
                
                bool matching = false;
                int overlap = 0;
                int matchingIndex = -1;

                for (int index = 0; index < s2.Length; index++) {

                    char s1char = s1[s1Index + index];
                    char s2char = s2[index];

                    if (s1char == s2char) {
                        if (!matching) {
                            matchingIndex = s1Index;
                            // start
                        }
                        matching = true;
                        overlap++;
                    } else {
                        if (matching) {
                            // end
                            break;
                        }
                    }
                }

                if (overlap > maxOverlap) {
                    maxOverlap = overlap;
                    maxIndex = matchingIndex;
                }
            }
            
            // apply the changes
            StringBuilder sb = new StringBuilder(s1);
            for (int i = 0; i < s2.Length; i++) {
                sb[maxIndex + i] = s2[i];
            }

            string result = sb.ToString().Trim();

            return result;
        }
    }
}

