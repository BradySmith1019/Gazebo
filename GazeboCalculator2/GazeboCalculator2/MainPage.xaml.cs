using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Controls;
using GazeboCalculator2.Models;

namespace GazeboCalculator2
{
    public struct Info
    {
        //Outputs
        public double cLength, iLength, commonAngle, iPlum, sSawAngle, sPlum, polygonLength, ridgeWidth, ridgeHeight, HAP,
            ridgeThickness, rho, fRSlopePhi, iRSlopeAlpha, Q, rho2, fRSlopePhi2, iRSlopeAlpha2, bmvCommonVertical, 
            bmvCommonHorizontal, bmvHipHorizontal, bmvHipVertical, lCq, lC, RH, lH, iRW, RW, elC, elE;
    }
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        async public void onHelpClicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HelpContent
            {
                BindingContext = new HelpPage()
            });
        }

        bool roofAngleChosen = false;
        bool numComplete = false;
        bool lengthSideComplete = false;
        bool lengthError = false;
        bool lengthSideFocused = false;

        // Inputs
        int numSides;
        int lengthSide;
        int roofSlope;

        // ******************** //
        // **** CONSTANTS ***** //
        // ******************** //
        int layoutM = 12;
        double UnitRise = 6;
        double rafterWidth = 5.25;
        double rafterThickness = 1.5;
        int topPlate = 97;

        // Also JI
        double WallThickness = 3.5;

        int orientationq = 0;
        double fasciaThickness = 1.5;
        double fasciaWidth = 3.5;
        double sheathing = 0.75;

        
        // ******************** //
        // *** CELL METHODS (GOT TO G7) *** //
        // ******************** //

        /* M4 */
        double heightAbovePlate()
        {
            return ((rafterWidth * lC()) - (WallThickness * UnitRise)) / 12;
        }

        /* B6 OUTPUT */
        double polySides()
        {
            return lengthSide * saRatio();
        }

        /* E6 */
        double ridgeThickness()
        {
            return j25();
        }

        /* F6 */
        double ridgeBacking()
        {
            return (UnitRise * ridgeThickness()) / 24;
        }

        /* G6 */
        double rhoOne()
        {
            return 180 / numSides;
        }

        /* G7 */
        double rho()
        {
            double a = rhoOne();
            return a / 180 * Math.PI;
        }

        /* H6 */
        double frSlopeOne()
        {
            return ((180 / Math.PI) * Math.Atan(UnitRise / 12));
        }

        /* H7 */
        double frSlope() 
        {
            return ((180 / Math.PI) * Math.Atan(UnitRise / 12)) / 180 * Math.PI;
        }

        /* I6 */
        double irSlopeOne()
        {
            return (180 / Math.PI) * Math.Atan(UnitRise / RH());
        }

        /* I7 */
        double irSlope()
        {
            return ((180 / Math.PI) * Math.Atan(UnitRise / RH())) / 180 * Math.PI;
        }

        /* J6 */
        double Q()
        {
            return 0.5 * polySides() * UnitRise * (1 / Math.Tan(rho()));
        }

        /* L6 */
        double fRBMV()
        {
            double l = lC();
            double h = heightAbovePlate();
            return rafterWidth * lC() / 12 - heightAbovePlate();
        }

        /* M6 */
        double fRBMH()
        {
            return (fRBMV() * 12) / UnitRise;
        }

        /* L7 */
        double iRBMV()
        {
            return iRW() / Math.Cos(irSlopeOne() / a11d11()) - heightAbovePlate();
        }

        /* M7 */
        double iRBMH()
        {
            return WallThickness / Math.Cos(rho());
        }

        /* A10 */
        double lCq()
        {
            return Math.Sqrt(Math.Pow(orientationq, 2) + 144);
        }

        /* B10 */
        double lC()
        {
            return Math.Sqrt(Math.Pow(UnitRise, 2) + 144);
        }

        /* C10 */
        double RH()
        {
            return 12 * (1 / Math.Cos(rho()));
        }

        /* D10 */
        double lH()
        {
            return Math.Sqrt(Math.Pow(UnitRise, 2) + (144 * Math.Pow((1 / Math.Cos(rho())), 2)));
        }

        /* E10 */
        double iRW()
        {
            return lC() * (1 / Math.Cos(rho())) * rafterWidth / lH();
        }

        /* F10 */
        double RW()
        {
            return rafterWidth / Math.Cos(frSlope());
        }

        /* J10 */
        double elC()
        {
            return Q() / 12;
        }

        /* K10 */
        double elE()
        {
            return elC() + heightAbovePlate() - ridgeBacking();
        }

        /* L10 */
        double ridgePost()
        {
            return elE() + topPlate - RW();
        }

        /* A11 and D11 */
        double a11d11()
        {
            return 180 / Math.PI;
        }

        /* B14 */
        double cRPC()
        {
            double f = (layoutM * UnitRise) / 12;
            return f;
        }

        /* H13 */
        double cFPLength()
        {
            return elC() / Math.Sin(frSlope());
        }

        /* I13 */
        double cCutLength()
        {
            return Math.Round(cFPLength() - fRafterSAtRidge(), 2);
        }

        /* H14 */
        double iFPLength()
        {
            return elC() / Math.Sin(irSlope());
        }

        /* I14 */
        double iCutLength()
        {
            return Math.Round(iFPLength() - iRafterSAtRidge(), 2);
        }

        /* B15 */
        double iRPC()
        {
            return Math.Round(layoutM * UnitRise * Math.Cos(rho()) / 12, 2);
        }

        /* J17 */
        double fRafterSAtiRafter()
        {
            return rafterThickness * lC() * (1 / Math.Sin(rho())) / 24;
        }

        /* K17 */
        double fRafterSAtRidge()
        {
            return 0.5 * ridgeThickness() * (1 / Math.Cos(frSlope()));
        }

        /* J18 */
        double purlinSAtiRafter()
        {
            return rafterThickness * (1 / Math.Cos(rho())) / 2;
        }

        /* K19 */
        double iRafterSAtRidge()
        {
            return 0.5 * ridgeThickness() * (1 / Math.Cos(irSlope()));
        }

        /* G20 */
        double saRatio()
        {
            double val;
            double a11 = a11d11();
            switch(numSides)
            {
                case 3:
                    val = Math.Cos(45 / a11) + Math.Cos(75 / a11);
                    break;
                case 4:
                    val = Math.Sqrt(2);
                    break;
                case 5:
                    val = Math.Cos(45 / a11) + Math.Cos(27 / a11);
                    break;
                case 6:
                    val = Math.Cos(45 / a11) + Math.Cos(15 / a11) + Math.Cos(75 / a11);
                    break;
                case 7:
                    val = Math.Cos(45 / a11) + Math.Cos(45 / 7 / a11) + Math.Cos(405 / 7 / a11);
                    break;
                case 8:
                    val = 1 / Math.Tan(Math.PI / 8);
                    break;
                case 9:
                    val = Math.Cos(85 / a11) + Math.Cos(45 / a11) + Math.Cos(5 / a11) + Math.Cos(35 / a11) + Math.Cos(75 / a11);
                    break;
                case 10:
                    val = Math.Cos(81 / a11) + Math.Cos(45 / a11) + Math.Cos(9 / a11) + Math.Cos(27 / a11) + Math.Cos(63 / a11);
                    break;
                case 11:
                    val = Math.Cos(855 / 11 / a11) + Math.Cos(45 / a11) + Math.Cos(135 / 11 / a11) + Math.Cos(225 / 11 / a11)
                        + Math.Cos(585 / 11 / a11) + Math.Cos(945 / 11 / a11);
                    break;
                case 12:
                    val = 2 * (Math.Cos(75 / a11) + Math.Cos(45 / a11) + Math.Cos(15 / a11));
                    break;
                case 13:
                    val = Math.Cos(955 / 13 / a11) + Math.Cos(45 / a11) + Math.Cos(225 / 13 / a11) + Math.Cos(135 / 13 / a11)
                        + Math.Cos(495 / 13 / a11) + Math.Cos(855 / 13 / a11);
                    break;
                case 14:
                    val = Math.Cos(495 / 7 / a11) + Math.Cos(45 / a11) + Math.Cos(135 / 7 / a11) + Math.Cos(45 / 7 / a11)
                        + Math.Cos(225 / 7 / a11) + Math.Cos(405 / 7 / a11) + Math.Cos(585 / 7 / a11);
                    break;
                case 15:
                    val = Math.Cos(69 / a11) + Math.Cos(45 / a11) + Math.Cos(21 / a11) + Math.Cos(3 / a11) + Math.Cos(27 / a11)
                        + Math.Cos(51 / a11) + Math.Cos(75 / a11);
                    break;
                case 16:
                    val = 1 / Math.Tan(11.25 / a11);
                    break;
                default:
                    val = -1;
                    Console.WriteLine("check input");
                    break;
            }

            return 1 / val;
        }

        /* G23 */
        double numPieces()
        {
            return numSides * 2;
        }

        /* H23 */
        double sideLength()
        {
            return rafterThickness;
        }

        /* J23 OUTPUT */
        double j25()
        {
            return (0.5 * sideLength() / Math.Tan(Math.PI / numPieces())) * 2;
        }

        double sSawAngle()
        {
            return Math.Round(a11d11() * Math.Asin(UnitRise * Math.Sin(rho()) / lC()), 2);
        }

        List<ImageButton> buttons = new List<ImageButton>();

        public void OnSideLengthCompleted(Object sender, EventArgs e)
        {
            lengthSide = Int32.Parse(((Entry)sender).Text);
            if (!(lengthSide % 1 == 0))
            {
                ((Entry)sender).Text = "Please enter a whole number";
                ((Entry)sender).TextColor = Color.Red;
                lengthError = true;
            }

            else if (lengthSide > 144 || lengthSide < 72)
            {
                ((Entry)sender).Text = "Please enter a number between 72 and 144";
                ((Entry)sender).TextColor = Color.Red;
                lengthError = true;
            }
            else
            {
                lengthSideComplete = true;
                checkComplete();
            }
        }

        public void onSideLengthFocused(Object sender, EventArgs e)
        {
            if (lengthError)
            {
                ((Entry)sender).Text = "";
                ((Entry)sender).TextColor = Color.Black;

            }
            lengthSideFocused = true;

        }

        public void onSideLengthUnfocused(Object sender, EventArgs e)
        {
            if (lengthSideFocused)
            {
                lengthSideFocused = false;
                OnSideLengthCompleted(sender, e);
            }
        }
        public void onNumSidesPicked(Object sender, EventArgs e)
        {
            numSides = (int)((Picker)sender).ItemsSource[((Picker)sender).SelectedIndex];
            numComplete = true;
            checkComplete();
        }

        public void onSteepAngleClick(Object sender, EventArgs e)
        {
            if (!buttons.Any())
            {
                ((ImageButton)sender).BorderColor = Color.Blue;
                buttons.Add((ImageButton)sender);
            }
            else
            {
                foreach (ImageButton butt in buttons.ToList())
                {
                    butt.BorderColor = Color.LightGray;
                    buttons.Remove(butt);
                }
                ((ImageButton)sender).BorderColor = Color.Blue;
                buttons.Add((ImageButton)sender);
            }
            roofAngleChosen = true;
            checkComplete();
            roofSlope = 12;
        }

        public void onSteepAngleTwoClick(Object sender, EventArgs e)
        {
            if (!buttons.Any())
            {
                ((ImageButton)sender).BorderColor = Color.Blue;
                buttons.Add((ImageButton)sender);
            }
            else
            {
                foreach (ImageButton butt in buttons.ToList())
                {
                    butt.BorderColor = Color.LightGray;
                    buttons.Remove(butt);
                }
                ((ImageButton)sender).BorderColor = Color.Blue;
                buttons.Add((ImageButton)sender);
            }
            roofAngleChosen = true;
            checkComplete();
            roofSlope = 9;
        }

        public void onSteepAngleThreeClick(Object sender, EventArgs e)
        {
            if (!buttons.Any())
            {
                ((ImageButton)sender).BorderColor = Color.Blue;
                buttons.Add((ImageButton)sender);
            }
            else
            {
                foreach (ImageButton butt in buttons.ToList())
                {
                    butt.BorderColor = Color.LightGray;
                    buttons.Remove(butt);
                }
                ((ImageButton)sender).BorderColor = Color.Blue;
                buttons.Add((ImageButton)sender);
            }
            roofAngleChosen = true;
            checkComplete();
            roofSlope = 6;
        }

        public void checkComplete()
        {
            if (roofAngleChosen && numComplete && lengthSideComplete)
            {
                goButton.IsEnabled = true;
            }
        }

        async public void onGoClicked(Object sender, EventArgs e)
        {
            Info info = new Info
            {
                //top = topPlate,
                cLength = cCutLength(),
                iLength = iCutLength(),
                commonAngle = cRPC(),
                iPlum = iRPC(),
                sSawAngle = sSawAngle(),
                sPlum = iRPC(),
                polygonLength = Math.Round(polySides(), 2),
                ridgeWidth = Math.Round(j25(), 2),
                ridgeHeight = Math.Round(ridgePost(), 2),
                HAP = Math.Round(heightAbovePlate(), 2),
                ridgeThickness = Math.Round(ridgeBacking(), 2),
                rho = Math.Round(rhoOne(), 2),
                fRSlopePhi = Math.Round(frSlopeOne(), 2),
                iRSlopeAlpha = Math.Round(irSlopeOne(), 2),
                Q = Math.Round(Q(), 2),
                rho2 = Math.Round(rho(), 2),
                fRSlopePhi2 = Math.Round(frSlope(), 2),
                iRSlopeAlpha2 = Math.Round(irSlope(), 2),
                bmvCommonVertical = Math.Round(fRBMV(), 2),
                bmvCommonHorizontal = Math.Round(fRBMH(), 2),
                bmvHipHorizontal = Math.Round(iRBMV(), 2),
                bmvHipVertical = Math.Round(iRBMH(), 2),
                lCq = Math.Round(lCq(), 2),
                lC = Math.Round(lC(), 2),
                RH = Math.Round(RH(), 2),
                lH = Math.Round(lH(), 2),
                iRW = Math.Round(iRW(), 2),
                RW = Math.Round(RW(), 2),
                elC = Math.Round(elC(), 2),
                elE = Math.Round(elE(), 2)
        };
            //var nextPage = new GoContent();
            //var goContent = new NavigationPage(new GoContent(info));
            await Navigation.PushAsync(new GoContent(info));
            /*nextPage.BindingContext = info;
            await Navigation.PushAsync(nextPage);*/
            /*await Navigation.PushAsync(new GoContent
            {
                BindingContext = new GoPage(topPlate, numSides, lengthSide, roofSlope)
            });*/
        }

        private void lengthSidePicker_Unfocused(object sender, FocusEventArgs e)
        {

        }
    }
}
