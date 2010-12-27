namespace mstum.utils
{
    /// <summary>
    /// A simple Even/Odd Class cycler
    /// </summary>
    /// <remarks>
    /// http://www.stum.de/2010/01/22/a-simple-evenodd-cycler-for-net/
    /// </remarks>
    public class EvenOddCycler
    {
        private readonly string _oddClassName;
        private readonly string _evenClassName;
        private int _numCycles;

        public EvenOddCycler() : this("even", "odd") { }

        public EvenOddCycler(string evenClassName, string oddClassName)
        {
            _evenClassName = evenClassName;
            _oddClassName = oddClassName;
            _numCycles = 0;
        }

        /// <summary>
        /// Toggle the class and return the class name
        /// </summary>
        /// <returns></returns>
        public string Cycle()
        {
            _numCycles++;
            return (_numCycles % 2 == 0) ? _evenClassName : _oddClassName;
        }

        /// <summary>
        /// Reset the Cycler
        /// </summary>
        /// <remarks>
        /// To reuse the Cycler on multiple tables, reset it so that Cycle returns the odd class for it's first iteration again
        /// </remarks>
        public void Reset()
        {
            _numCycles = 0;
        }
    }
}