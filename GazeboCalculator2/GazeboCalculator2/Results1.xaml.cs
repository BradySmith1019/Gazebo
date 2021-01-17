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
    public partial class Page1 : ContentPage
    {
        Info info;
        public Page1(Info info)
        {
            this.info = info;
            InitializeComponent();
            valueNineteen.Text = info.bmvCommonVertical.ToString() + "\"";
            valueTwenty.Text = info.bmvCommonHorizontal.ToString() + "\"";
            valueTwentyOne.Text = info.bmvHipHorizontal.ToString() + "\"";
            valueTwentyTwo.Text = info.bmvHipVertical.ToString() + "\"";
            valueTwentyThree.Text = info.lCq.ToString() + "\"";
            valueTwentyFour.Text = info.lC.ToString() + "\"";
            valueTwentyFive.Text = info.RH.ToString() + "\"";
            valueTwentySix.Text = info.lH.ToString() + "\"";
            valueTwentySeven.Text = info.iRW.ToString() + "\"";
            valueTwentyEight.Text = info.RW.ToString() + "\"";
            valueTwentyNine.Text = info.elC.ToString() + "\"";
            valueThirty.Text = info.elE.ToString() + "\"";
        }

        async void pageOne_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}