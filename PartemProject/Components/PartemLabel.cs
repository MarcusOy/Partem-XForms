using System;
using Xamarin.Forms;

namespace PartemProject.Components
{
    public class PartemLabelBold : Label
    {
        public PartemLabelBold()
        {
            FontFamily = Device.RuntimePlatform == Device.iOS ? "Nexa-Bold" : null;
            LineBreakMode = LineBreakMode.TailTruncation;
            FontSize = 45;
        }
    }

    public class PartemLabelLight : Label
    {
        public PartemLabelLight()
        {
            FontFamily = Device.RuntimePlatform == Device.iOS ? "Nexa-Light" : null;
            LineBreakMode = LineBreakMode.TailTruncation;
            FontSize = 20;
        }
    }
}
