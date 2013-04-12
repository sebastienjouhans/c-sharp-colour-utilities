using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinrou.Drawing
{
    public class Colour
    {
        public string Name { get; set; }
        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public HSL Hsl { get; set; }

        #region constructors
        public Colour() 
        { }

        public Colour(Color color)
        {
            setRGB(color.R, color.G, color.B);
        }


        public Colour(int red, int green, int blue)
        {
            setRGB(red, green, blue);
        }


        public Colour(double hue, double saturation, int brightness)
        {
            Colour colour = Colour.HslToRgb(new HSL { H = hue, S = saturation, L = brightness });
            A = colour.A;
            R = colour.R;
            G = colour.G;
            B = colour.B;
            Name = colour.Name;
            Hsl = colour.Hsl;
        }


        public Colour(string hex)
        {
            Colour colour = Colour.hexToColour(hex); ;
            A = colour.A;
            R = colour.R;
            G = colour.G;
            B = colour.B;
            Name = colour.Name;
            Hsl = colour.Hsl;
        }
        #endregion

        #region implicit operators
        public static implicit operator Colour(Color color)
        {
            Colour colour = new Colour();
            colour.A = color.A;
            colour.R = color.R;
            colour.G = color.G;
            colour.B = color.B;
            colour.Name = color.Name;
            colour.Hsl = new HSL() { H = color.GetHue(), S = color.GetSaturation(), L = color.GetBrightness() };
            return colour;
        }


        public static implicit operator Color(Colour colour)
        {
            return Color.FromArgb(colour.A, colour.R, colour.G, colour.B);
        }
        #endregion
        
        #region public methods
        public void setRGB(int red, int green, int blue)
        {
            Colour colour = Color.FromArgb(red, green, blue);
            A = colour.A;
            R = colour.R;
            G = colour.G;
            B = colour.B;
            Name = colour.Name;
            Hsl = colour.Hsl;
        }

        public HSL getHSL()
        {
            return Hsl;
        }


        public bool getIsColourMatch(Color coloutToMatch)
        {
            if (A == coloutToMatch.A &&
                R == coloutToMatch.R &&
                G == coloutToMatch.G &&
                B == coloutToMatch.B)
                return true;
            else
                return false;
        }


        public string getHex()
        {
            return R.ToString("X2") + G.ToString("X2") + B.ToString("X2");
        }


        public override string ToString()
        {
            return String.Format("R: {0:#0.##} G: {1:#0.##} B: {2:#0.##} // H: {3:#0.##} S: {4:#0.##} L: {4:#0.##}", R, G, B, Hsl.H, Hsl.S, Hsl.L);
        }

        public string ToRGBString()
        {
            return String.Format("R: {0:#0.##} G: {1:#0.##} B: {2:#0.##}", R, G, B);
        }

        public string ToHSLString()
        {
            return String.Format("H: {0:#0.##} S: {1:#0.##} L: {2:#0.##}", Hsl.H, Hsl.S, Hsl.L);
        }
        #endregion

        #region overload operator
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Colour p = obj as Colour;
            if ((System.Object)p == null)
            {
                return false;
            }

            return (R == p.R) && (G == p.G) && (B == p.B) && (A == p.A);
        }

        public bool Equals(Colour p)
        {
            if ((object)p == null)
            {
                return false;
            }

            return (R == p.R) && (G == p.G) && (B == p.B) && (A == p.A);
        }

        public override int GetHashCode()
        {
            return R ^ G ^ B;
        }


        public static bool operator ==(Colour a, Colour b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.A == b.A && a.R == b.R && a.G == b.G && a.B == b.B;

        }


        public static bool operator !=(Colour a, Colour b)
        {
            return !(a == b);
        }
        

        public static Colour hexToColour(string hex)
        {
            if (hex.Length != 6) throw new ArgumentException("the hexadecimal value must have a length of 6 characters");

            byte r = 255;
            byte g = 255;
            byte b = 255;
            int start = 0;

            r = byte.Parse(hex.Substring(start, 2), System.Globalization.NumberStyles.HexNumber);
            g = byte.Parse(hex.Substring(start + 2, 2), System.Globalization.NumberStyles.HexNumber);
            b = byte.Parse(hex.Substring(start + 4, 2), System.Globalization.NumberStyles.HexNumber);

            return Color.FromArgb(r, g, b);
        }
        #endregion

        #region static methods
        //http://www.bobpowell.net/rgbhsb.htm
        //h is stored from 0-360 the calcultion needs from 0-1 so we divide hsl.H by 360
        public static Colour HslToRgb(HSL hsl)
        {
            double r = 0, g = 0, b = 0;
            double h = hsl.H / 360;
            double s = hsl.S, l = hsl.L;

            double temp1, temp2;

            if (l == 0)
            {
                r = g = b = 0;
            }
            else
            {
                if (s == 0)
                {
                    r = g = b = l;
                }
                else
                {
                    temp2 = ((l <= 0.5) ? l * (1.0 + s) : l + s - (l * s));
                    temp1 = 2.0 * hsl.L - temp2;

                    double[] t3 = new double[] { h + 1.0 / 3.0, h, h - 1.0 / 3.0 };
                    double[] clr = new double[] { 0, 0, 0 };
                    for (int i = 0; i < 3; i++)
                    {
                        if (t3[i] < 0)
                            t3[i] += 1.0;

                        if (t3[i] > 1)
                            t3[i] -= 1.0;

                        if (6.0 * t3[i] < 1.0)
                            clr[i] = temp1 + (temp2 - temp1) * t3[i] * 6.0;
                        else if (2.0 * t3[i] < 1.0)
                            clr[i] = temp2;
                        else if (3.0 * t3[i] < 2.0)
                            clr[i] = (temp1 + (temp2 - temp1) * ((2.0 / 3.0) - t3[i]) * 6.0);
                        else
                            clr[i] = temp1;
                    }
                    r = clr[0];
                    g = clr[1];
                    b = clr[2];
                }
            }
            return Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b));
        }



        public static string getHex(Colour colour)
        {
            return colour.R.ToString("X2") + colour.G.ToString("X2") + colour.B.ToString("X2");
        }

        #endregion

    }


    public struct HSL
    {
        public double H { get; set; }
        public double S { get; set; }
        public double L { get; set; }
    }
}
