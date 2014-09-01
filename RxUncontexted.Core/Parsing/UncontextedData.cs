using System;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace RxUncontexted.Parsing
{

    [Serializable]
    public class UncontextedData
    {
        public int A { get; set; }
        public float B { get; set; }
        public int C { get; set; }
        public int D { get; set; }
        public E E { get; set; }

        // ReSharper disable once MethodTooLong
        public override String ToString()
        {
            return String.Format("A: {0,-5} B: {1,-5} C: {2,-5} D: {3,-5} E.F: {4,-5} E.G: {5,-5} ", A, B, C, D, E.F, E.G);
        }

    }
}