using System.Collections.Generic;
using System.Linq;

namespace LFTC.Lexic
{
    public class TokenSplitOptions
    {
        /// <summary>
        /// List of atom strings. Two atoms next to each other must be separated after split even if there
        /// is no whitespace between them (e.g. "+-" --> "+", "-")
        /// </summary>
        public string[] Atoms { get; }

        public TokenSplitOptions(IEnumerable<string> atoms)
        {
            Atoms = atoms.ToArray();
        }
        public TokenSplitOptions() : this(new List<string>()) { }
    }
}
