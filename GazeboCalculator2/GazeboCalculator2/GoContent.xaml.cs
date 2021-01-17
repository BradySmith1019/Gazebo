using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GazeboCalculator2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoContent : ContentPage
    {
        Info info;
        public GoContent(Info info)
        {
            this.info = info;
            InitializeComponent();
            valueOne.Text = info.cLength.ToString() + "\"";
            valueTwo.Text = info.commonAngle.ToString() + "\x00B0";
            valueThree.Text = info.iLength.ToString() + "\"";
            valueFour.Text = info.iPlum.ToString() + "\"";
            valueFive.Text = info.sSawAngle.ToString() + "\x00B0";
            valueSix.Text = info.sPlum.ToString() + "\"";
            valueSeven.Text = info.polygonLength.ToString() + "\"";
            valueEight.Text = info.ridgeWidth.ToString() + "\"";
            valueNine.Text = info.ridgeHeight.ToString() + "\"";
            valueTen.Text = info.HAP.ToString() + "\"";
            valueEleven.Text = info.ridgeThickness.ToString() + "\"";
            valueTwelve.Text = info.rho.ToString() + "\"";
            valueThirteen.Text = info.fRSlopePhi.ToString() + "\"";
            valueFourteen.Text = info.iRSlopeAlpha.ToString() + "\"";
            valueFifteen.Text = info.Q.ToString() + "\"";
            valueSixteen.Text = info.rho2.ToString() + "\"";
            valueSeventeen.Text = info.fRSlopePhi2.ToString() + "\"";
            valueEighteen.Text = info.iRSlopeAlpha2.ToString() + "\"";
        }

        async void pageTwo_Clicked(object sender, EventArgs e)
        {
            //var fjf = new Page(new Page1(info));
            //var resultsOneContent = new NavigationPage(new Page1(info));
            await Navigation.PushAsync(new Page1(info));
        }
    }
}