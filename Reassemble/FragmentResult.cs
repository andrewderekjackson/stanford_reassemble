using System.Diagnostics;

namespace Reassemble {

    [DebuggerDisplay("Overlap: {Overlap}, String1: {String1}, String2: {String2}")]
    public class FragmentResult {

        public string String1 { get; set; }

        public string String2 { get; set; }

        public int Overlap { get; set; }

        public string MergedString { get; set; }

    }
}