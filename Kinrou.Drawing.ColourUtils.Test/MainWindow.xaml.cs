using Kinrou.Drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

namespace ColourUtilsTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ColourRange colourRange;

        public MainWindow()
        {
            InitializeComponent();

            List<ColourRangeVo> colList = MockData.getColourList();

            colourRange = new ColourRange(colList);

            /*
            ColourRangeVo c = null;

             // test rgb to hsl to rgb
            Colour col1 = Color.FromArgb(179, 101, 131);
            Colour col12 = Colour.HslToRgb(col1.getHSL());
            c = colourRange.getColorMatch(col1);
            Debug.WriteLine(c.colour.ToString());

             // test colour match in colour range
            Colour col2 = Color.FromArgb(151, 101, 179);
            c = colourRange.getColorMatch(col2);
            Debug.WriteLine(c.colour.ToString());

             // test colour match in colour range
            Colour col3 = Color.FromArgb(151, 101, 179);
            c = colourRange.getColorMatch(col3);
            Debug.WriteLine(c.colour.ToString());
            */



        }





        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Colour colour = Colour.hexToRgb(hexColourText.Text);
                ColourRangeVo colRangeVo = colourRange.getColorMatch(colour);
                hexColour.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(colour.R, colour.G, colour.B));
                colourRgbText.Content = string.Format("R={0}, G={1}, B={2}", colour.R, colour.G, colour.B);
                if (colRangeVo != null && colRangeVo.match)
                {
                    hexColourMatch.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(colRangeVo.colour.R, colRangeVo.colour.G, colRangeVo.colour.B));
                    colourNameMatch.Content = string.Format("match name: {0}", colRangeVo.name);
                    colourRgbMatchText.Content = string.Format("R={0}, G={1}, B={2}", colRangeVo.colour.R, colRangeVo.colour.G, colRangeVo.colour.B);


                    // split complementary
                    List<Colour> colList = ColourHarmoniesHelper.getSplitComplementaries(colRangeVo.colour);
                    complementaryCol1.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(colList[0].R, colList[0].G, colList[0].B));
                    complementaryCol2.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(colList[1].R, colList[1].G, colList[1].B));
                    complementaryColRGB1.Content = string.Format("R={0}, G={1}, B={2}", colList[0].R, colList[0].G, colList[0].B);
                    complementaryColRGB2.Content = string.Format("R={0}, G={1}, B={2}", colList[1].R, colList[1].G, colList[1].B);

                    //Triadic
                    colList = ColourHarmoniesHelper.getTriadicComplementaries(colRangeVo.colour);
                    TriadicCol1.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(colList[0].R, colList[0].G, colList[0].B));
                    TriadicCol2.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(colList[1].R, colList[1].G, colList[1].B));
                    TriadicColRGB1.Content = string.Format("R={0}, G={1}, B={2}", colList[0].R, colList[0].G, colList[0].B);
                    TriadicColRGB2.Content = string.Format("R={0}, G={1}, B={2}", colList[1].R, colList[1].G, colList[1].B);

                    //Analogous
                    colList = ColourHarmoniesHelper.getAnalogousComplementaries(colRangeVo.colour);
                    AnalogousCol1.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(colList[0].R, colList[0].G, colList[0].B));
                    AnalogousCol2.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(colList[1].R, colList[1].G, colList[1].B));
                    AnalogousColRGB1.Content = string.Format("R={0}, G={1}, B={2}", colList[0].R, colList[0].G, colList[0].B);
                    AnalogousColRGB2.Content = string.Format("R={0}, G={1}, B={2}", colList[1].R, colList[1].G, colList[1].B);

                    //Monochromatic
                    Colour col1 = ColourHarmoniesHelper.getMonochromatic(colRangeVo.colour, 0.2);
                    Colour col2 = ColourHarmoniesHelper.getMonochromatic(colRangeVo.colour, 0.3);
                    MonochromaticCol1.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(col1.R, col1.G, col1.B));
                    MonochromaticCol2.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(col2.R, col2.G, col2.B));
                    MonochromaticColRGB1.Content = string.Format("R={0}, G={1}, B={2}", col1.R, col1.G, col1.B);
                    MonochromaticColRGB2.Content = string.Format("R={0}, G={1}, B={2}", col2.R, col2.G, col2.B);



                }
                else
                {
                    hexColourMatch.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    colourNameMatch.Content = string.Format("match name: {0}", colRangeVo.name);
                    colourRgbText.Content = "";
                    complementaryCol1.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    complementaryCol2.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    complementaryColRGB1.Content = "";
                    complementaryColRGB2.Content = "";
                }
            }
            catch { }
        }



        private Colour hexToRgb(string hex)
        {
            byte r = 255;
            byte g = 255;
            byte b = 255;
            int start = 0;

            r = byte.Parse(hex.Substring(start, 2), System.Globalization.NumberStyles.HexNumber);
            g = byte.Parse(hex.Substring(start + 2, 2), System.Globalization.NumberStyles.HexNumber);
            b = byte.Parse(hex.Substring(start + 4, 2), System.Globalization.NumberStyles.HexNumber);

            return Color.FromArgb(r, g, b);
        }

        private void hexColourText_GotFocus_1(object sender, RoutedEventArgs e)
        {
            hexColourText.Text = "";
        }
    }
}
