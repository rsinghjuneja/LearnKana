using System.Collections.Generic;

namespace LearnKana.Droid.Utilities
{
    public class KanaImageDatabase
    {
        public int this[string key] => Database[key];

        public Dictionary<string, int> Database { get; } = new Dictionary<string, int>
        {
            ["a"] = 0,
            ["i"] = 0,
            ["u"] = 0,
            ["e"] = 0,
            ["o"] = 0,

            ["ka"] = 0,
            ["ki"] = Resource.Drawable.key_96,
            ["ku"] = 0,
            ["ke"] = 0,
            ["ko"] = 0,

            ["sa"] = 0,
            ["shi"] = 0,
            ["su"] = 0,
            ["se"] = 0,
            ["so"] = 0,

            ["ta"] = 0,
            ["chi"] = 0,
            ["tsu"] = 0,
            ["te"] = 0,
            ["to"] = Resource.Drawable.tomato_96,

            ["na"] = 0,
            ["ni"] = 0,
            ["nu"] = 0,
            ["ne"] = 0,
            ["no"] = 0,

            ["ha"] = 0,
            ["hi"] = 0,
            ["fu"] = 0,
            ["he"] = 0,
            ["ho"] = 0,

            ["ma"] = 0,
            ["mi"] = 0,
            ["mu"] = 0,
            ["me"] = 0,
            ["mo"] = 0,

            ["ya"] = 0,
            ["yu"] = 0,
            ["yo"] = 0,

            ["ra"] = 0,
            ["ri"] = 0,
            ["ru"] = 0,
            ["re"] = 0,
            ["ro"] = 0,

            ["wa"] = 0,
            ["wo"] = 0,

            ["n"] = 0,
        };
    }
}
