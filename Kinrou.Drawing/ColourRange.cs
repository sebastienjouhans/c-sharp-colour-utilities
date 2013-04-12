using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
	 * references
	 * http://stackoverflow.com/questions/1678457/best-algorithm-for-matching-colours
    * http://www.easyrgb.com/index.php?X=MATH
	 * 
	 */

namespace Kinrou.Drawing
{
    public class ColourRange
    {
        private List<ColourRangeVo> _colourList;

        public ColourRange(List<ColourRangeVo> colourList)
        {
            _colourList = colourList;
            init();
        }


        private void init()
		{
            for (int i = 0; i < _colourList.Count; i++)
            {
                _colourList[i].hsl = _colourList[i].colour.getHSL();
            }
		}

        /// <summary>
        /// matches the colour passed as an argument with the closest one contained in the list
        /// </summary>
        /// <param name="colour"></param>
        /// <returns></returns>
        public ColourRangeVo getColorMatch( Colour colour )
		{
			ColourRangeVo col = new ColourRangeVo();

            HSL hsl = colour.getHSL();
            double r = colour.R;
            double g = colour.G;
			double b = colour.B;
            double h = hsl.H;
            double s = hsl.S;
            double l = hsl.L;
			double ndf = 0;
            double distance = 255;

            ColourRangeVo tempColour = null;
            
            for (int i = 0; i < _colourList.Count; i++)
			{
                if (colour.getIsColourMatch(_colourList[i].colour))
                {
                    col.colour = _colourList[i].colour;
	                col.match = true;
                    col.name = _colourList[i].name;
	                return col;
                }

                double hVal = 0.5 * Math.Pow((double)_colourList[i].hsl.H - (double)h, 2);
                double sVal = 0.5 * Math.Pow((double)(_colourList[i].hsl.S*100) - (double)(s*100), 2);
                double lVal = Math.Pow((double)(_colourList[i].hsl.L*100) - (double)(l*100), 2);

                ndf = hVal + sVal + lVal;
                ndf = Math.Sqrt(ndf);

                if (ndf < distance)
                {
                    distance = ndf;
                    tempColour = _colourList[i];
                }
            }

            if (tempColour==null)
            {
                col.colour = new Colour();
                col.match = false;
                col.name = "Invalid Color";
                return col;
            }
            else
            {
                col.colour = tempColour.colour;
                col.match = true;
                col.name = tempColour.name;
                return col;
            }

		}

    }


    public class ColourRangeVo
    {
        public bool match { get; set; }
        public string name { get; set; }
        public Colour colour { get; set; }
        public HSL hsl { get; set; }
    }


    


}

