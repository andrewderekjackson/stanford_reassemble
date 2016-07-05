using System;
using System.Collections.Generic;
using System.Linq;

namespace Reassemble {

    public class ReassembleImpl {

        /// <summary>
        /// Calculates the offset/merged result of the two strings and returns the results.
        /// </summary>
        public FragmentResult CalculateOverlap(string string1, string string2) {

            if (string1 == null || string2 == null) {
                throw new ArgumentNullException("string1 or string2 cannot be null.");
            }

            var result1 = CalculateOverlapCore(string1, string2);
            var result2 = CalculateOverlapCore(string2, string1);

            if (result1.Overlap == 0 && result2.Overlap == 0) {
                // just merge the strings
                result1.MergedString = result1.String1 + result1.String2;
                return result1;
            }


            if (result1.Overlap >= result2.Overlap) {
                return result1;
            } else {
                return result2;
            }
        }

        internal FragmentResult CalculateOverlapCore(string string1, string string2) {

            // first check to see if string2 is fully contained in string1
            if (string1.Contains(string2)) {
                return new FragmentResult() {
                    String1 = string1,
                    String2 = string2,
                    Overlap = string2.Length,
                    MergedString = string1
                };
            }

            // search for best match reducing from the right

            //  eg:
            //    a b c d 
            //          c d e f
            //    a b c d 
            //          d e f

            for (int i = string2.Length; i > 0; i--) {
                string overlap = string2.Substring(0, i);
                string rest = string2.Substring(i);
                if (string1.EndsWith(overlap, StringComparison.InvariantCulture)) {
                    return new FragmentResult() {
                        String1 = string1,
                        String2 = string2,
                        Overlap = overlap.Length,
                        MergedString = string1 + rest
                    };
                }
            }


            // search for best match reducing from the left

            //  eg:
            //          c d e f 
            //    a b c d      
            //          c d e f 
            //      a b c      

            for (int i = 0; i < string2.Length; i++) {
                string overlap = string2.Substring(i);
                string rest = string2.Substring(0, i);
                if (string1.StartsWith(overlap, StringComparison.InvariantCulture)) {
                    return new FragmentResult() {
                        String1 = string1,
                        String2 = string2,
                        Overlap = overlap.Length,
                        MergedString = rest + string1
                    };
                }
            }
            
            // no overlap
            return new FragmentResult() {
                String1 = string1,
                String2 = string2,
                Overlap = 0
            };

        }
        

        public string Reassemble(string[] fragments) {

            if (fragments == null || fragments.Length == 0) {
                return string.Empty;
            }
            
            List<string> input = new List<string>(fragments);
            
            while (input.Count > 1) {
                Reduce(input);
            }

            return input[0];
        }

        private void Reduce(List<string> input){

            List<FragmentResult> results = new List<FragmentResult>();

            foreach (var f1 in input) {
                foreach (var f2 in input) {
                    if (f1 != f2) {
                        var overlap = CalculateOverlap(f1, f2);
                        results.Add(overlap);
                    }
                }
            }

            var paring = results.GroupBy(v => v.Overlap).Select(v => v.FirstOrDefault()).FirstOrDefault();
            if (paring != null) {
                input.Remove(paring.String1);
                input.Remove(paring.String2);
                input.Add(paring.MergedString);
            }

        }
    }
}

