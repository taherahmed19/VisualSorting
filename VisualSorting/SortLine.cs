using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSorting
{
    class SortLine
    {
        public int x { get; set; }
        public int length { get; set; }
        public bool swaped { get; set; }

        public SortLine(int x, int length)
        {
            this.x = x;
            this.length = length;
            this.swaped = false;
        }
    }
}
