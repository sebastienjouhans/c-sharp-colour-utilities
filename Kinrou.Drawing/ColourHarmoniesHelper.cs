using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinrou.Drawing
{
    public class ColourHarmoniesHelper
    {
        public ColourHarmoniesHelper()
        { }


        /// <summary>
        /// http://www.easyrgb.com/index.php?X=WEEL
        /// Split complements
        /// This color scheme combines the two colors on either side of a color’s complement.
        /// H° +/- 150°
        /// </summary>
        /// <param name="colour"></param>
        /// <returns></returns>
        public static List<Colour> getSplitComplementaries(Colour colour)
        {
            return getTwoComplementaryColours(colour, 150);
        }

        /// <summary>
        /// http://www.easyrgb.com/index.php?X=WEEL
        /// Triadic
        /// This is the typical configuration of three colors that are equally spaced from each other on the color wheel.
        /// H° +/- 120°
        /// </summary>
        /// <param name="colour"></param>
        /// <returns></returns>
        public static List<Colour> getTriadicComplementaries(Colour colour)
        {
            return getTwoComplementaryColours(colour, 120);
        }

        /// <summary>
        /// http://www.easyrgb.com/index.php?X=WEEL
        /// Analogous
        /// Uses the colors of the same color temperature near each other on the wheel.
        /// H° +/- 120°
        /// </summary>
        /// <param name="colour"></param>
        /// <returns></returns>
        public static List<Colour> getAnalogousComplementaries(Colour colour)
        {
            return getTwoComplementaryColours(colour, 30);
        }


        /// <summary>
        /// http://www.easyrgb.com/index.php?X=WEEL
        /// Complement
        /// This is the color opposite on the color wheel.
        /// H° + 180°
        /// </summary>
        /// <param name="colour"></param>
        /// <returns></returns>
        public static Colour getComplement(Colour colour, double angle)
        {
            HSL hsl = colour.getHSL();

            double hVal = hsl.H + angle;
            hVal = hVal >= 360 ? hVal - 360 : hVal;
            HSL positiveHSL = new HSL() { H = hVal, S = hsl.S, L = hsl.L };

            return Colour.HslToRgb(positiveHSL);
        }


        /// <summary>
        /// http://www.easyrgb.com/index.php?X=WEEL
        /// Monochromatic
        /// Colors from the same family on the wheel. This will include lighter, darker and differently saturated versions of the color.
        /// </summary>
        /// <param name="colour"></param>
        /// <returns></returns>
        public static Colour getMonochromatic(Colour colour, double brightness)
        {
            HSL hsl = colour.getHSL();

            double lVal = hsl.L + brightness;
            lVal = (lVal > 1 ? 1 : (lVal < 0 ? 0 : lVal));
            HSL newHSL = new HSL() { H = hsl.H, S = hsl.S, L = lVal };

            return Colour.HslToRgb(newHSL);
        }


        public static List<Colour> getTwoComplementaryColours(Colour colour, double angle)
        {
            HSL hsl = colour.getHSL();

            double hVal = hsl.H + angle;
            hVal = hVal >= 360 ? hVal - 360 : hVal;
            HSL positiveHSL = new HSL() { H = hVal, S = hsl.S, L = hsl.L };

            double hVal1 = hsl.H - angle;
            hVal1 = hVal1 <= 0 ? hVal1 + 360 : hVal1;
            HSL negativeHSL = new HSL() { H = hVal1, S = hsl.S, L = hsl.L };

            Colour c0 = Colour.HslToRgb(positiveHSL);
            Colour c1 = Colour.HslToRgb(negativeHSL);
            return new List<Colour>() { c0, c1 };
        }
    }
}
