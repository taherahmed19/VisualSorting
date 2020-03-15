using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSorting
{
    class Sorting
    {
        int formHeight;
        int forwWidth;
        Random rnd;
        public List<SortLine> sortValues;

        public Sorting(int formHeight, int forwWidth)
        {
            this.formHeight = formHeight;
            this.forwWidth = forwWidth;
            this.sortValues = new List<SortLine>();
            this.rnd = new Random();
            this.GenerateRandomValues();
        }

        void GenerateRandomValues()
        {
            int calcWidth = 0;
            int x = 0;
            while (calcWidth < (Form1.Width + 10))
            {
                int randomSort = this.rnd.Next(10, this.formHeight);
                if (!(this.sortValues.Where(item => item.length == randomSort).Count() > 0))
                {
                    this.sortValues.Add(new SortLine(calcWidth, randomSort));
                    calcWidth += Form1.PenWidth;
                    x += 10;
                }
            }
        }

        public void BubbleSort(int index)
        {
            int temp = -1;
            if (this.sortValues[index].length > this.sortValues[index + 1].length)
            {
                temp = this.sortValues[index].length;
                this.sortValues[index].length = this.sortValues[index + 1].length;
                this.sortValues[index + 1].length = temp;
                this.sortValues[index].swaped = true;
                this.sortValues[index + 1].swaped = true;
            }
        }
    }
}
