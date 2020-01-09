using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSnake.Model
{
    class RectField : Figure
    {
        private Double rectWidth;
        private Double rectHeight;

        public Double RectWidth
        {
            get { return rectWidth; }
            set
            {
                rectWidth = value;
            }
        }
        public Double RectHeight
        {
            get { return rectHeight; }
            set
            {
                rectHeight = value;
            }
        }
    }
}
